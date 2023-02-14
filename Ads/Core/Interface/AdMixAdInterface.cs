using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace Qarth
{
    public class AdMixAdInterface : AdFullScreenInterface
    {
        private TaurusXAdSdk.Api.AdPosition m_AdMixPos;
        public TaurusXAdSdk.Api.AdPosition adMixPos
        {
            get { return m_AdMixPos; }
            set { m_AdMixPos = value; }
        }

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
            // if (!m_IsShowing)
            // {
            //     return;
            // }
            m_IsShowing = false;

            for (int i = 0; i < m_AdHandler.Count; ++i)
            {
                m_AdHandler[i].HideAd();
                CheckAdState();
            }
        }

        public override void SyncAdPosition()
        {
            if (!m_IsShowing)
            {
                return;
            }

            for (int i = 0; i < m_AdHandler.Count; ++i)
            {
                m_AdHandler[i].SyncAdPosition();
            }
        }
    }
}
