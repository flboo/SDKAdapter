using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Qarth
{
    [RequireComponent(typeof(RectTransform))]
    public class AdMixViewLazyView : MonoBehaviour, IAdInterfaceEventListener
    {
        [SerializeField]
        private bool m_AutoShowOnEnable = true;
        [SerializeField]
        private GameObject m_ObjViewbg;

        private string m_AdPlacementName;
        private bool m_IsShowingAd = false;
        private AdLazyMixAdInterface m_AdInterface;
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
                m_AdInterface = AdsMgr.S.GetAdInterfaceByPlacementID(m_AdPlacementName, 0) as AdLazyMixAdInterface;
            if (m_ObjViewbg != null)
                m_ObjViewbg.SetActive(false);
        }

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
            if (m_ObjViewbg != null)
                m_ObjViewbg.gameObject.SetActive(true);

            m_AdInterface.ShowAd("");
            if (m_AdPlacement != null)
                m_AdPlacement.RecordShowTime();
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
