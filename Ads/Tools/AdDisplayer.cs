using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Qarth;
using DG.Tweening;
using TaurusXAdSdk.Api;
using UnityEngine.UI;

namespace Qarth
{
    public class AdDisplayer : AbstractPanel, IAdInterfaceEventListener
    {
        public class AdDisplayerBuilder
        {
            public delegate void AdShowResultDelegate(bool isShowSuccess, bool isRewardSuccess, bool isClickAd, string rewardID, string tid, float ecpm);

            protected string m_PlacementID;
            protected string m_RewardID;
            protected AdShowResultDelegate m_OnAdShowResultCallback;
            protected bool m_ForceShow = false;
            protected string m_AdTag;
            public string adTag
            {
                get { return m_AdTag; }
            }

            public TDAdPlacement placementData
            {
                get
                {
                    if (string.IsNullOrEmpty(m_PlacementID))
                    {
                        return null;
                    }

                    TDAdPlacement data = TDAdPlacementTable.GetData(m_PlacementID);
                    return data;
                }
            }

            public bool forceShow
            {
                get { return m_ForceShow; }
            }

            public string rewardID
            {
                get { return m_RewardID; }
            }

            public string GetAdInterfaceName(int index)
            {
                switch (index)
                {
                    case 0:
                        return placementData.adInterface0;
                    case 1:
                        return placementData.adInterface1;
                    default:
                        return null;
                }
            }

            public int GetAdInterfaceCount()
            {
                return 2;
            }

            public AdShowResultDelegate onAdShowResultCallback
            {
                get { return m_OnAdShowResultCallback; }
            }

            public void Show(string tag)
            {
                TaurusXAdsAdapter.AdTag = tag;
                if (placementData == null || string.IsNullOrEmpty(placementData.adInterface0))
                {
                    Log.e("Invalid First AD PlacementID.");
                    return;
                }

                m_AdTag = tag;
                var inter = AdsMgr.S.GetAdInterfaceByPlacementID(m_PlacementID, 0);
                if (inter != null)
                    inter.adSceneId = tag;

                UIMgr.S.OpenTopPanel(SDKUI.AdDisplayer, null, this);
            }

            public void CustomAdTag(string state)
            {
                if (string.IsNullOrEmpty(m_AdTag))
                {
                    return;
                }

                // DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.AD_DISPLAY, string.Format("{0}-{1}", m_AdTag, state));
            }

            public void CustomAdTag_Complex(string state)
            {
                if (string.IsNullOrEmpty(m_AdTag))
                {
                    return;
                }
                var dict = new Dictionary<string, string>();
                dict.Add(DataAnalysisDefine.TT_AD_POSITION, m_AdTag);
                dict.Add(DataAnalysisDefine.TT_IS_AD_SUCCESS, state);
                DataAnalysisMgr.S.CustomEventDic(DataAnalysisDefine.TT_AD_SHOW, dict);
            }

            public void CustomAdTag_RequestClick()
            {
                if (string.IsNullOrEmpty(m_AdTag))
                {
                    return;
                }
                var dict = new Dictionary<string, string>();
                dict.Add(DataAnalysisDefine.TT_AD_POSITION, m_AdTag);
                DataAnalysisMgr.S.CustomEventDic(DataAnalysisDefine.TT_AD_CLICK, dict);
            }

            public AdDisplayerBuilder SetPlacementID(string placementID)
            {
                m_PlacementID = placementID;
                return this;
            }

            public AdDisplayerBuilder SetRewardID(string name)
            {
                m_RewardID = name;
                return this;
            }

            public AdDisplayerBuilder SetForceShow(bool forceShow = false)
            {
                m_ForceShow = forceShow;
                return this;
            }

            public AdDisplayerBuilder SetOnAdShowResultCallback(AdShowResultDelegate callBack)
            {
                m_OnAdShowResultCallback = callBack;
                return this;
            }
        }

        public static AdDisplayerBuilder Builder()
        {
            return new AdDisplayerBuilder();
        }

        [SerializeField]
        private GameObject m_LoadingRoot;
        [SerializeField]
        private Image m_MaskImg;
        private AdDisplayerBuilder m_Builder;
        private int m_WaitTimer = -1;
        private int m_AdIndex = -1;
        private bool m_IsShowSuccess = false;
        private bool m_IsRewardSuccess = false;
        private bool m_IsClickAd = false;
        private bool m_IsFinish = false;
        protected AdInterface m_AdInterface;
        private AdPlacement m_Placement;
        private AdEventArgs m_AdArgs;

        private string currentAdName
        {
            get
            {
                if (m_Builder == null)
                {
                    return null;
                }

                return m_Builder.GetAdInterfaceName(m_AdIndex);
            }
        }

        public string adPlacementID
        {
            get
            {
                if (m_Placement == null)
                {
                    return "";
                }

                return m_Placement.data.id;
            }
        }

        protected override void OnOpen()
        {
            m_LoadingRoot.SetActive(false);
            m_MaskImg.enabled = false;
        }

        protected override void OnClose()
        {

#if UNITY_EDITOR
            m_IsShowSuccess = true;
            m_IsRewardSuccess = true;
            m_IsClickAd = true;
#endif

            CancelWaitAdLoading();

            if (m_Builder != null && m_Builder.onAdShowResultCallback != null)
            {
                Log.i(string.Format("AD: onAdShowResultCallback {0} + {1} +{2}", m_IsShowSuccess, m_IsRewardSuccess, m_IsClickAd));

                string tid = m_AdArgs != null ? m_AdArgs.LineItem.GetTId() : "";

                float showEcpm222 = m_AdArgs != null ? (m_AdArgs.LineItem.GetSecondaryLineItem() != null ? m_AdArgs.LineItem.GetSecondaryLineItem().GetEcpm() : 0) : 0;

                // Debug.LogError("------------currentTid---------:" + tid);

                if (!string.IsNullOrEmpty(tid))
                {
                    var lstTids = Helper.String2ListString(PlayerPrefs.GetString("tauruxTid", ""));
                    if (lstTids.Contains(tid))
                    {
                        if (m_IsRewardSuccess)
                        {
                            DataAnalysisMgr.S.CustomEventLifeCircleSingle("RepeatTid", tid);
                        }
                        tid = "";
                        showEcpm222 = 0;
                    }
                    else
                    {
                        lstTids.Add(tid);
                        PlayerPrefs.SetString("tauruxTid",
                            string.Format("{0}{1};", PlayerPrefs.GetString("tauruxTid", ""), tid));
                    }
                }

                m_Builder.onAdShowResultCallback(m_IsShowSuccess, m_IsRewardSuccess, m_IsClickAd, m_Builder.rewardID, tid, showEcpm222);
                m_AdArgs = null;
            }
            m_Builder = null;
        }

        protected override void OnPanelOpen(params object[] args)
        {
            m_Builder = args[0] as AdDisplayerBuilder;

            if (m_Builder == null)
            {
                OnAdShowFailed();
                return;
            }

            //??????????????????
            if (!m_Builder.forceShow)
            {
                if (AdsMgr.S.isNoAdsMode)
                {
                    m_IsRewardSuccess = m_IsShowSuccess = true;
                    m_IsFinish = true;
                    return;
                }
            }

            m_Placement = AdsMgr.S.GetAdPlacement(m_Builder.placementData.id);

            //???????????????????????????????????????
            if (!m_Placement.data.isEnable)
            {
                if (m_Placement.data.rewardWhenDisable)
                {
                    m_IsRewardSuccess = m_IsShowSuccess = true;
                }
                else
                {
                    m_IsRewardSuccess = m_IsShowSuccess = false;
                }
                m_IsFinish = true;
                return;
            }

            //????????????????????????
            if (!m_Placement.IsTimeShowAble())
            {
                m_IsRewardSuccess = m_IsShowSuccess = m_IsClickAd = false;
                m_IsFinish = true;
                return;
            }

            //OpenDependPanel(EngineUI.MaskPanel, -1);

            DataAnalysisMgr.S.SatoriEvt_ADRewardRequest(m_Builder.adTag);
            //m_Builder.CustomAdTag_RequestClick();

            m_AdIndex = 0;
            m_IsRewardSuccess = m_IsShowSuccess = m_IsClickAd = false;
            m_IsFinish = false;

            ShowAD();
        }

        public void OnAdLoadFailedEvent()
        {
            CancelWaitAdLoading();
            ShowAD();
        }

        public void OnAdLoadEvent()
        {
            CancelWaitAdLoading();
            ShowAD();
        }

        public void OnAdCloseEvent()
        {
            //DataAnalysisMgr.S.CustomEventWithDate(DataAnalysisDefine.AD_SHOW_COUNT, "1");

            m_MaskImg.enabled = true;
            m_AdIndex++;

            AdFullScreenHandler handler = m_AdInterface.GetAdHandlerByPlatform("wesdk") as AdFullScreenHandler;
            if (handler != null)
            {
                m_AdArgs = handler.adEventArgs;
            }

            bool showSecInter = CheckSecInterShow();
            if (showSecInter)
            {
                Timer.S.Cancel(m_WaitTimer);
                m_WaitTimer = -1;
                m_IsFinish = false;
                ShowAD();
            }
            else
            {
                CancelWaitAdLoading();
                TryCloseSelfPanel();
            }
        }

        private bool CheckSecInterShow()
        {
            if (m_AdIndex > 1)
            {
                return false;
            }

            string interfaceName = m_Builder.GetAdInterfaceName(m_AdIndex);
            if (string.IsNullOrEmpty(interfaceName))
            {
                return false;
            }

            AdInterface adInterface = AdsMgr.S.GetAdInterface(interfaceName);

            if (adInterface == null)
            {
                return false;
            }

            string attr = PlayerPrefs.GetString("AppsFlyerAttr", "");
            if (!attr.Trim().ToLower().Equals("non-organic"))
            {
                return false;
            }

            return AdsAnalysisMgr.S.IsSecInterAvaliable();
        }

        public void OnAdRewardEvent()
        {
            m_IsRewardSuccess = true;
            CancelWaitAdLoading();
            AdsAnalysisMgr.S.AddRewardShowCount();
        }

        protected void TryCloseSelfPanel()
        {
            if (!m_IsFinish)
            {
                m_IsFinish = true;
                BindAdInterface(null);
            }
        }

        protected void ShowAD()
        {
            if (m_IsFinish || m_IsShowSuccess)
            {
                if (m_AdIndex < 1)
                {
                    return;
                }
            }

            BindAdInterface(null);

            if (m_AdIndex >= m_Builder.GetAdInterfaceCount())
            {
                OnAdShowFailed();
                // m_Builder.CustomAdTag("failed");
                // m_Builder.CustomAdTag_Complex("failed");
                return;
            }

            string adInterfaceName = m_Builder.GetAdInterfaceName(m_AdIndex);

            if (string.IsNullOrEmpty(adInterfaceName))
            {
                OnAdShowFailed();
                return;
            }

            if (!ShowFullAd(adInterfaceName, true))
            {
                OnAdShowFailed();
            }
        }

        public void BindAdInterface(AdInterface ad)
        {
            if (m_AdInterface != null)
            {
                m_AdInterface.adEventListener = null;
            }

            m_AdInterface = ad;

            if (m_AdInterface != null)
            {
                m_AdInterface.adEventListener = this;
                if (m_Builder != null)
                    m_AdInterface.adSceneId = m_Builder.adTag;
            }
        }

        protected bool ShowFullAd(string adInterfaceName, bool wait)
        {
            AdInterface adInterface = AdsMgr.S.GetAdInterface(adInterfaceName);

            if (adInterface == null)
            {
                Log.e("Not Find ADInterface:" + adInterfaceName);
                return false;
            }

            if (adInterface.isAdReady)
            {
                if (adInterface.adType == AdType.Interstitial)
                {
                    if (m_AdIndex < 1)
                    {
                        m_IsRewardSuccess = true;
                    }
                    AdsAnalysisMgr.S.AddInterShowCount();
                }

                if (adInterface.ShowAd(m_Builder.rewardID))
                {
                    m_IsShowSuccess = true;
                    // m_Builder.CustomAdTag(m_AdIndex.ToString());
                    // m_Builder.CustomAdTag_Complex("success");
                    BindAdInterface(adInterface);
                    m_Placement.RecordShowTime();
                    DataAnalysisMgr.S.SatoriEvt_ADRewardSceneShow(m_Builder.adTag);
                    return true;
                }

                return false;
            }
            else
            {
                if (wait && m_Placement.data.maxWaitTime > 0)
                {
                    StartWaitAdLoading();
                    BindAdInterface(adInterface);
                    adInterface.PreLoadAd();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        protected void StartWaitAdLoading()
        {
            if (m_WaitTimer > 0)
            {
                return;
            }

            m_WaitTimer = Timer.S.Post2Really(OnWaitTimeTick, m_Placement.data.maxWaitTime);
            m_LoadingRoot.SetActive(true);
            m_LoadingRoot.transform.DORotate(new Vector3(0, 0, -360), 5, RotateMode.LocalAxisAdd)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
        }

        protected void CancelWaitAdLoading()
        {
            if (m_WaitTimer <= 0)
            {
                return;
            }

            Timer.S.Cancel(m_WaitTimer);
            m_WaitTimer = -1;
            m_LoadingRoot.SetActive(false);
        }

        protected void OnWaitTimeTick(int count)
        {
            m_WaitTimer = -1;
            ShowAD();
        }

        protected void OnAdShowFailed()
        {
            TryCloseSelfPanel();
        }

        protected void Update()
        {
            if (m_IsFinish)
            {
                m_IsFinish = false;
                CloseSelfPanel();
            }
        }

        public void OnAdClickEvent()
        {
            m_IsClickAd = true;
        }
    }
}
