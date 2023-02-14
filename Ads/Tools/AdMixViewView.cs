using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Qarth
{
    [RequireComponent(typeof(RectTransform))]
    public class AdMixViewView : MonoBehaviour, IAdInterfaceEventListener
    {
        [SerializeField]
        private bool m_AutoShowOnEnable = true;
        [SerializeField]
        private GameObject m_ObjViewbg;

        private string m_AdPlacementName;
        private bool m_IsShowingAd = false;
        private AdMixAdInterface m_AdInterface;
        private AdPlacement m_AdPlacement;

        private CanvasScaler m_CanvasScaler;

        private int m_RetryTimer = -1;
        private int m_RetryDuration = 2;
        private int m_RetryCount = 2;

        public string adPlacementID
        {
            get { return m_AdPlacementName; }
        }

        public void BindAdInterface(string placementID)
        {
            m_AdPlacementName = placementID;
            m_AdPlacement = AdsMgr.S.GetAdPlacement(m_AdPlacementName);
            if (!string.IsNullOrEmpty(m_AdPlacementName))
            {
                m_AdInterface = AdsMgr.S.GetAdInterfaceByPlacementID(m_AdPlacementName, 0) as AdMixAdInterface;
                m_AdInterface.adSceneId = m_AdPlacementName;
            }
            if (m_ObjViewbg != null)
                m_ObjViewbg.SetActive(false);
        }

        public void SetRetryParams(int dur = 2, int count = 2)
        {
            m_RetryDuration = dur;
            m_RetryCount = count;
        }

        // public void ShowAd()
        // {
        //     if (m_AdInterface == null)
        //     {
        //         return;
        //     }

        //     DoShowAd();
        // }

        public void ShowAd(int x = 0, int y = 0,
            TaurusXAdSdk.Api.AdPosition pos = TaurusXAdSdk.Api.AdPosition.Custom)
        {
            if (m_AdInterface == null)
                return;

            m_AdInterface.adMixPos = pos;
#if UNITY_ANDROID
            m_AdInterface.adCustomGrid = new Vector2Int((int)DisplayMetricsUtil.PixelToDp(x),
                (int)DisplayMetricsUtil.PixelToDp(AppropDifferScreenY(y)));
#elif UNITY_IOS
            var yPos = DisplayMetricsUtil.CalcSafeArea(DisplayMetricsUtil.PxToPt(AppropDifferScreenY(y)), pos);
            m_AdInterface.adCustomGrid = new Vector2Int(DisplayMetricsUtil.PxToPt(x), yPos);
#endif
            DoShowAd();
        }

        public bool IsAdTimeShowable()
        {
            return m_AdPlacement != null && m_AdPlacement.IsTimeShowAble();
        }

        void DoShowAd()
        {
            CleanRetryTimer();


            if (!m_AdInterface.ShowAd(""))
            {
                if (m_RetryDuration > 0)
                {
                    m_RetryTimer = Timer.S.Post2Scale(OnRetryCount, m_RetryDuration, m_RetryCount);
                }
                if (m_ObjViewbg != null)
                    m_ObjViewbg.gameObject.SetActive(false);
            }
            else
            {
                if (m_ObjViewbg != null)
                    m_ObjViewbg.gameObject.SetActive(true);
                EventSystem.S.Send(SDKEventID.OnMixViewViewShowed);
                if (m_AdPlacement != null)
                    m_AdPlacement.RecordShowTime();
            }
        }

        void OnRetryCount(int count)
        {
            if (m_AdInterface.ShowAd(""))
            {
                EventSystem.S.Send(SDKEventID.OnMixViewViewShowed);
                if (m_AdPlacement != null)
                    m_AdPlacement.RecordShowTime();
                CleanRetryTimer();
            }
        }

        void CleanRetryTimer()
        {
            if (m_RetryTimer > 0)
            {
                Timer.S.Cancel(m_RetryTimer);
                m_RetryTimer = -1;
            }
        }

        public void SetMixViewAdPosY(int x = 0, int y = 0,
            TaurusXAdSdk.Api.AdPosition pos = TaurusXAdSdk.Api.AdPosition.Custom)
        {

            if (m_AdInterface == null)
            {
                return;
            }
            m_AdInterface.adMixPos = pos;
#if UNITY_ANDROID
            m_AdInterface.adCustomGrid = new Vector2Int((int)DisplayMetricsUtil.PixelToDp(x),
                (int)DisplayMetricsUtil.PixelToDp(AppropDifferScreenY(y)));
#elif UNITY_IOS
            var yPos = DisplayMetricsUtil.CalcSafeArea(DisplayMetricsUtil.PxToPt(AppropDifferScreenY(y)), pos);
            m_AdInterface.adCustomGrid = new Vector2Int(DisplayMetricsUtil.PxToPt(x), yPos);
#endif
            m_AdInterface.SyncAdPosition();
        }

        public void HideAd()
        {
            if (m_AdInterface == null)
            {
                return;
            }

            CleanRetryTimer();
            m_AdInterface.HideAd();
            if (m_ObjViewbg != null)
                m_ObjViewbg.gameObject.SetActive(false);
        }

        int AppropDifferScreenY(int y)
        {
            //Log.e(">>>>>>>>" + Screen.height + " , " + Screen.dpi + " , ");
            if (Screen.height * 1.0f / Screen.width > 2)
            {
                var rectRoot = UIMgr.S.uiRoot.panelRoot.GetComponent<RectTransform>();
                y -= (int)rectRoot.offsetMax.y;
            }

            if (m_CanvasScaler == null)
                m_CanvasScaler = UIMgr.S.uiRoot.rootCanvas.GetComponent<CanvasScaler>();
            var rate = Screen.width * 1.0f / m_CanvasScaler.referenceResolution.x;
            //Log.e(">>>>>>>>" + (int)(y * rate));
            return (int)(y * rate);
        }

        // private void OnEnable()
        // {
        //     if (m_AutoShowOnEnable)
        //     {
        //         ShowAd();
        //     }
        // }

        // private void OnDisable()
        // {
        //     HideAd();
        // }

        // private void OnDestroy()
        // {
        //     HideAd();
        // }

        #region callbsck

        public void OnAdLoadEvent()
        {

        }

        public void OnAdLoadFailedEvent()
        {

        }

        public void OnAdRewardEvent()
        {

        }

        public void OnAdCloseEvent()
        {

        }

        public void OnAdClickEvent()
        {

        }

        #endregion
    }
}
