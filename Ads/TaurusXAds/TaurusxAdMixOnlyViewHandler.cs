using System;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using TaurusXAdSdk.Api;
using UnityEngine;


namespace GameWish.Game
{
    public class TaurusxAdMixOnlyViewHandler : AdMixHandler
    {
        private MixViewAd m_MixViewAd;
        private bool m_IsReady;

        private AdMixAdInterface m_MixViewInterface
        {
            get { return m_AdInterface as AdMixAdInterface; }
        }

        public override bool isAdReady
        {
            get
            {
                if (m_MixViewAd == null)
                {
                    return false;
                }

                if (m_IsReady == false)
                {
                    m_IsReady = m_MixViewAd.IsReady();
                }
                return m_IsReady;
            }
        }

        protected override bool DoPreLoadAd()
        {
            if (m_MixViewAd != null && m_MixViewAd.IsReady())
            {
                return false;
            }
#if UNITY_ANDROID
            if (m_MixViewAd == null)
            {
                m_MixViewAd = new MixViewAd(TaurusXConfigUtil.GetAdUnitId(m_Config.unitID));
                m_MixViewAd.OnAdLoaded += HandleOnAdLoaded;
                // 广告请求失败时调用
                m_MixViewAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
                // 广告展示时调用
                m_MixViewAd.OnAdShown += HandleOnAdOpened;
                // 广告点击时调用
                m_MixViewAd.OnAdClicked += HandleOnAdClick;
                // 广告关闭时调用
                //m_MixViewAd.OnAdClosed += HandleOnAdClosed;
                RequestMixViewAd();
                m_MixViewAd.LoadAd();
            }
            return true;
#endif

            return false;
        }

        public override string GetReadyADNetWorkName()
        {
            var lineitem = m_MixViewAd.GetReadyLineItem();
            if (lineitem != null)
            {
                return lineitem.GetNetwork().ToString();
            }

            return "";
        }

        protected override bool DoShowAd()
        {
            if (m_MixViewAd == null)
            {
                return false;
            }

            m_IsShowing = true;
            SyncAdPosition();
            m_MixViewAd.Show();

            //mix show
            DataAnalysisMgr.S.CustomEvent("w_view_pic");
            return true;
        }

        public override void SyncAdPosition()
        {
            if (m_MixViewAd == null)
            {
                return;
            }

            if (m_MixViewInterface.adMixPos == TaurusXAdSdk.Api.AdPosition.Custom)
            {
                m_MixViewAd.SetPosition(m_AdInterface.adCustomGrid.x, m_AdInterface.adCustomGrid.y);
            }
            else
            {
                m_MixViewAd.SetPositionRelative(m_MixViewInterface.adMixPos, m_AdInterface.adCustomGrid.x, m_AdInterface.adCustomGrid.y);
            }
        }

        public override bool ShowAd()
        {
            Log.i("tuia_show");

            //mix request
            DataAnalysisMgr.S.CustomEvent("w_read");

            if (!isAdReady)
            {
                return false;
            }

            if (DoShowAd())
            {
                m_AdState = AdState.Showing;
            }

            return m_AdState == AdState.Showing;
        }

        protected override void DoHideAd()
        {
            if (m_MixViewAd == null)
            {
                return;
            }

            m_IsShowing = false;
            m_MixViewAd.Hide();

            if (m_IsReady)
            {
                m_AdState = AdState.Loaded;
            }
            else
            {
                m_MixViewAd.LoadAd();
                m_AdState = AdState.Loading;
            }

        }

        protected override void DoCleanAd()
        {
            if (m_MixViewAd == null)
            {
                return;
            }
            m_MixViewAd = null;
        }

        protected void HandleOnAdLoaded(object sender, EventArgs args)
        {
            HandleOnAdLoaded();
        }
        protected void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            HandleOnAdFailedToLoad(args.AdError.GetMessage());
        }
        protected void HandleOnAdClosed(object sender, EventArgs args)
        {
            HandleOnAdClosed();
        }
        protected void HandleOnAdOpened(object sender, EventArgs args)
        {
            m_IsShowing = true;
            HandleOnAdOpened();
        }
        protected void HandleOnAdClick(object sender, EventArgs args)
        {
            HandleOnAdClick();
            ThreadMgr.S.mainThread.PostAction(() =>
            {
                EventSystem.S.Send(SDKEventID.OnMixViewViewClicked);
            });

        }

        //设置布局
        private void RequestMixViewAd()
        {
            // 设置布局文件，确保你的 Android 工程里面有 res/layout/taurusx_ads_nativead_medium.xml 文件
            NativeAdLayout layout = new NativeAdLayout(m_MixViewInterface.adStyle);
            // 设置点击区域，默认全部区域可点击,root|action|choice|store
            var builder = InteractiveArea.Builder();
            for (int i = 0; i < m_MixViewInterface.adInteractAreas.Count; i++)
            {
                switch (m_MixViewInterface.adInteractAreas[i])
                {
                    case "title":
                        builder.AddTitle();
                        break;
                    case "subtitle":
                        builder.AddSubTitle();
                        break;
                    case "price":
                        builder.AddPrice();
                        break;
                    case "icon":
                        builder.AddIconLayout();
                        break;
                    case "body":
                        builder.AddBody();
                        break;
                    case "adv":
                        builder.AddAdvertiser();
                        break;
                    case "media":
                        builder.AddMediaViewLayout();
                        break;
                    case "ratebar":
                        builder.AddRatingBar();
                        break;
                    case "ratetext":
                        builder.AddRatingTextView();
                        break;
                    case "root":
                        builder.AddRootLayout();
                        break;
                    case "action":
                        builder.AddCallToAction();
                        break;
                    case "choice":
                        builder.AddAdChoicesLayout();
                        break;
                    case "store":
                        builder.AddStore();
                        break;
                }
            }
            layout.SetInteractiveArea(builder);
            m_MixViewAd.SetNativeAdLayout(layout);
            // 设置广告宽度，单位 px
            if (m_MixViewInterface.width > 0)
                m_MixViewAd.SetAndroidWidth(DisplayMetricsUtil.PixelToDp(m_MixViewInterface.width));
            if (m_MixViewInterface.height > 0)
                m_MixViewAd.SetAndroidHeight(DisplayMetricsUtil.PixelToDp(m_MixViewInterface.height));
        }
    }

}