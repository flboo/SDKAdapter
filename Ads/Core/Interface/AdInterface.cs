using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using TaurusXAdSdk.Api;

namespace Qarth
{
    public interface IAdInterfaceEventListener
    {
        void OnAdLoadEvent();
        void OnAdLoadFailedEvent();
        void OnAdRewardEvent();
        void OnAdCloseEvent();
        void OnAdClickEvent();
        string adPlacementID
        {
            get;
        }
    }

    public class AdInterface
    {
        public Action<bool> On_AdStateUpdateEvent;
        public static int MAX_LOADING_AD_COUNT = 2;
        public static int MAX_LOADED_AD_COUNT = 12;
        public static float FULLSCREEN_AD_LOAD_OFFSET = 3;

        public static int AD_FAILED_WAIT_DURATION = 50;
        public static int AD_FAILED_ADD_OFFSET = 5;

        protected string m_AdSceneId;
        protected int m_AdType;
        protected List<AdHandler> m_AdHandler;
        protected bool m_IsShowing = false;
        protected string m_RewardID;
        protected AdSize m_AdSize;
        protected AdPosition m_AdPosition;
        protected Vector2Int m_AdCustomGrid;

        protected string m_AdInterfaceName;
        protected bool m_HasReward;
        protected IAdInterfaceEventListener m_EventListener;
        protected bool m_PreAdState = false;


        public AdHandler GetAdHandlerByPlatform(string adPlatform)
        {
            if (m_AdHandler == null || m_AdHandler.Count == 0)
            {
                return null;
            }

            for (int i = m_AdHandler.Count-1; i >= 0; --i)
            {
                if (m_AdHandler[i].adAdapter.adPlatform == adPlatform)
                {
                    return m_AdHandler[i];
                }
            }

            return null;
        }

        public List<AdHandler> GetHandlerList()
        {
            return m_AdHandler;
        }


        public IAdInterfaceEventListener adEventListener
        {
            get { return m_EventListener; }
            set
            {
                m_EventListener = value;
            }
        }

        public bool isAdReady
        {
            get
            {
                if (adType == AdType.Interstitial && !AdsAnalysisMgr.S.IsInterAvailable())
                {
                    return false;
                }

                for (int i = 0; i < m_AdHandler.Count; ++i)
                {
                    if (m_AdHandler[i].isAdReady)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public bool CheckIsAdReady(string platform)
        {
            platform = platform.ToLower();

            for (int i = 0; i < m_AdHandler.Count; ++i)
            {
                if (m_AdHandler[i].isAdReady && m_AdHandler[i].adConfig.adPlatform == platform)
                {
                    return true;
                }
            }

            return false;
        }

        public void CheckAdState()
        {
            bool state = isAdReady;
            if (state != m_PreAdState)
            {
                m_PreAdState = state;
                if (On_AdStateUpdateEvent != null)
                {
                    On_AdStateUpdateEvent(state);
                }
            }
        }

        public string adInterfaceName
        {
            get { return m_AdInterfaceName; }
        }

        public string rewardID
        {
            get { return m_RewardID; }
        }

        public string adSceneId
        {
            get { return m_AdSceneId; }
            set
            {
                if (TDAdSceneConfigTable.GetData(value) != null)
                    m_AdSceneId = TDAdSceneConfigTable.GetData(value).sceneId;
                else
                    m_AdSceneId = value;
            }
        }

        public int adType
        {
            get { return m_AdType; }
        }

        public AdSize adSize
        {
            get { return m_AdSize; }
            set
            {
                m_AdSize = value;
            }
        }

        public AdPosition adPosition
        {
            get { return m_AdPosition; }
            set { m_AdPosition = value; }
        }

        public Vector2Int adCustomGrid
        {
            get
            {
                return m_AdCustomGrid;
            }

            set
            {
                m_AdCustomGrid = value;
            }
        }

        protected virtual bool isLogEnable
        {
            get { return true; }
        }

        public void InitAdInterface(string interfaceName, int adType)
        {
            m_AdType = adType;
            m_AdInterfaceName = interfaceName;
            InitInterfaceView();

            m_AdHandler = new List<AdHandler>();
            InitAdHandlerList();
        }

        public void RebuildAdHandlerList()
        {
            InitAdHandlerList();
        }


        protected virtual void InitInterfaceView()
        {
        }

        public virtual bool ShowAd(string rewardID)
        {
            return false;
        }

        public virtual void PreLoadAd()
        {
        }

        public virtual void SyncAdPosition()
        {

        }

        public virtual void HideAd()
        {

        }

        public virtual void ShowSplashAD()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            var appId = TaurusXConfigUtil.GetAppId();
            TaurusXAds.Init(appId); 
#endif

            for (int i = 0; i < m_AdHandler.Count; ++i)
            {
                if (m_AdHandler[i].isAdReady)
                {
                    if (m_AdHandler[i].ShowAd())
                    {
                        m_IsShowing = true;
                    }
                }
            }
        }

        protected void InitAdHandlerList()
        {
            BeforeAdHandlerInit();

            var adDataList = TDAdConfigTable.GetAdDataByInterface(m_AdInterfaceName);

            if (adDataList.Count <= 0)
            {
                Log.w("Not Find AdConfig For Interface:" + m_AdInterfaceName);
                return;
            }

            for (int i = 0; i < adDataList.Count; ++i)
            {
                var handler = AdsMgr.S.CreateAdHandler(adDataList[i]);
                if (handler != null)
                {
                    m_AdHandler.Add(handler);
                    ProcessNewAdHandler(handler);
                }
            }

            AfterAdHandlerInit();

            m_AdHandler.Sort(AdHandlerSorter);

            for (int i = 0; i < m_AdHandler.Count; ++i)
            {
                m_AdHandler[i].SetAdInterface(this);
            }
        }

        protected virtual void BeforeAdHandlerInit()
        {

        }

        protected virtual void AfterAdHandlerInit()
        {

        }

        protected virtual void ProcessNewAdHandler(AdHandler handler)
        {

        }

        protected int AdHandlerSorter(AdHandler a, AdHandler b)
        {
            return b.ecpm - a.ecpm;
        }

        ///////////////////////////////////
        #region Handler 事件
        public virtual void OnAdLoad(TDAdConfig config)
        {
            //++AdAnalysisMgr.S.interstitialLoadCount;
            if (m_EventListener != null)
            {
                m_EventListener.OnAdLoadEvent();
            }
        }

        public virtual void OnAdLoadFailed()
        {
            if (m_EventListener != null)
            {
                m_EventListener.OnAdLoadFailedEvent();
            }

            PreLoadAd();
        }

        public virtual void OnAdOpen()
        {

        }

        public void OnAdClick()
        {
            if (m_EventListener != null)
            {
                m_EventListener.OnAdClickEvent();
            }
        }

        public void OnAdClose(TDAdConfig config)
        {
            m_IsShowing = false;

            if (m_EventListener != null)
            {
                m_EventListener.OnAdCloseEvent();
            }

            PreLoadAd();
        }

        public void OnAdReward()
        {
            m_HasReward = true;
            if (m_EventListener != null)
            {
                m_EventListener.OnAdRewardEvent();
            }
        }

        public void OnAdRequest(TDAdConfig config)
        {

        }

        public void ForceEndShowing()
        {
            m_IsShowing = false;
        }

        #endregion
    }
}
