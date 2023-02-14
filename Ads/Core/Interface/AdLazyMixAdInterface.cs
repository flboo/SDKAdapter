using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace Qarth
{
    public class AdLazyMixAdInterface : AdInterface
    {
        private AdHandler m_CurrentHandler;
        private int m_WaitLoadCount = 0;
        private TaurusXAdSdk.Api.AdPosition m_AdMixPos;
        public TaurusXAdSdk.Api.AdPosition adMixPos
        {
            get { return m_AdMixPos; }
            set { m_AdMixPos = value; }
        }

        private int m_CheckTickTimer = -1;
        private int m_CheckDuration = 1;

        private string m_AdStyle;

        public string adStyle
        {
            get { return m_AdStyle; }
        }

        private List<string> m_AdInteractAreas = new List<string>();

        public List<string> adInteractAreas
        {
            get { return m_AdInteractAreas; }
        }

        private int m_Width;

        public int width
        {
            get { return m_Width; }
        }


        private int m_Height;

        public int height
        {
            get { return m_Height; }
        }


        protected override void InitInterfaceView()
        {
            var conf = TDAdNativeViewTable.GetData(m_AdInterfaceName);
            if (conf != null)
            {
                switch (conf.widthType)
                {
                    case "Pixel":
                        m_Width = (int)conf.width;
                        break;
                    case "Ratio":
                        m_Width = (int)(Screen.width * conf.width);
                        break;
                    default:
                        m_Width = 0;
                        break;
                }

                switch (conf.heightType)
                {
                    case "Pixel":
                        m_Height = (int)conf.height;
                        break;
                    case "Ratio":
                        m_Height = (int)(Screen.height * conf.height);
                        break;
                    default:
                        m_Height = 0;
                        break;
                }

                m_AdStyle = conf.layoutName;
                m_AdInteractAreas = Helper.String2ListString(conf.interactiveArea, "|");
            }
        }

        public override void HideAd()
        {
            if (!m_IsShowing)
            {
                return;
            }

            m_IsShowing = false;

            if (m_CurrentHandler != null)
            {
                m_CurrentHandler.HideAd();
                m_CurrentHandler = null;
                CleanChecker();
            }
        }

        public override void SyncAdPosition()
        {
            if (!m_IsShowing)
            {
                return;
            }

            if (m_CurrentHandler != null)
            {
                m_CurrentHandler.SyncAdPosition();
            }
        }

        public override bool ShowAd(string rewardID)
        {
            if (m_IsShowing)
            {
                return false;
            }
            SelectAd2Show();
            m_IsShowing = true;

            return true;
        }

        protected void SelectAd2Show()
        {
            AdHandler next = null;

            for (int i = 0; i < m_AdHandler.Count; ++i)
            {
                if (m_AdHandler[i].adState != AdState.Failed)
                {
                    next = m_AdHandler[i];
                    break;
                }
            }

            if (next != m_CurrentHandler)
            {
                if (m_CurrentHandler != null)
                {
                    //Log.e(">>>>>>>>>>>>>>>>>>>>>>HideAd1");
                    m_CurrentHandler.HideAd();
                }

                m_CurrentHandler = next;

                if (m_CurrentHandler != null)
                {
                    m_CurrentHandler.PreLoadAd();
                    //Log.e(">>>>>>>>>>>>>>>>>>>>>>adstate1: " + m_CurrentHandler.adState.ToString());
                    StartCheckTimeTick();
                }
            }
            else
            {
                if (m_CurrentHandler == null)
                {
                    return;
                }

                if (!m_IsShowing)
                {
                    m_CurrentHandler.ShowAd();
                }
            }
        }

        protected void StartCheckTimeTick()
        {
            CleanChecker();
            m_WaitLoadCount = 0;
            m_CheckTickTimer = Timer.S.Post2Really(OnCheckCurrentAdStateTick, m_CheckDuration);
        }

        void OnCheckCurrentAdStateTick(int count)
        {
            CheckAdState();
            CleanChecker();

            if (m_CurrentHandler == null)
            {
                return;
            }

            //Log.e(">>>>>>>>>>>>>>>>>>>>>>adstate2: " + m_CurrentHandler.adState.ToString());
            if (m_CurrentHandler.adState == AdState.Loading)
            {
                if (m_WaitLoadCount < 7)
                {
                    m_CheckTickTimer = Timer.S.Post2Really(OnCheckCurrentAdStateTick, m_CheckDuration);
                }

                m_WaitLoadCount += 1;
            }
            else if (m_CurrentHandler.adState == AdState.Loaded)
            {
                m_CurrentHandler.ShowAd();
                //Log.e(">>>>>>>>>>>>>>>>>>>>>>show2");
            }
        }

        void CleanChecker()
        {
            if (m_CheckTickTimer > 0)
            {
                Timer.S.Cancel(m_CheckTickTimer);
                m_CheckTickTimer = -1;
            }
        }

        public override void OnAdLoad(TDAdConfig config)
        {
            base.OnAdLoad(config);
            CheckAdState();
        }

        public override void OnAdOpen()
        {
            base.OnAdOpen();
            CheckAdState();
            m_WaitLoadCount = 0;
        }
    }
}
