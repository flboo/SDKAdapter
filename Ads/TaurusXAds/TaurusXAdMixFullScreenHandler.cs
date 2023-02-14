using System;
using Qarth;
using UnityEngine;
using TaurusXAdSdk.Api;


namespace Qarth
{
    public class TaurusXAdMixFullScreenHandler : AdFullScreenHandler
    {
        private MixFullScreenAd m_MixFullscreenAd;

        private AdMixAdInterface m_MixViewInterface
        {
            get { return m_AdInterface as AdMixAdInterface; }
        }

        public override bool isAdReady
        {
            get
            {
                if (m_MixFullscreenAd == null)
                {
                    return false;
                }
                return m_MixFullscreenAd.IsReady();
            }
        }

        protected override bool DoPreLoadAd()
        {
            if (m_MixFullscreenAd != null)
            {
                m_MixFullscreenAd.LoadAd();
                return true;
            }
#if UNITY_ANDROID
            if (m_MixFullscreenAd == null)
            {
                m_MixFullscreenAd = new MixFullScreenAd(TaurusXConfigUtil.GetAdUnitId(m_Config.unitID));
                m_MixFullscreenAd.OnAdLoaded += HandleOnAdLoaded;
                // 广告请求失败时调用
                m_MixFullscreenAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
                // 广告展示时调用
                m_MixFullscreenAd.OnAdShown += HandleOnAdOpened;
                // 广告点击时调用
                m_MixFullscreenAd.OnAdClicked += HandleOnAdClick;
                // 广告关闭时调用
                m_MixFullscreenAd.OnAdClosed += HandleOnAdClosed;
                //广告奖励时调用
                m_MixFullscreenAd.OnRewarded += HandleOnAdReward;

                //自渲染
                RequestMixFullScreenAd();

                //模板
                m_MixFullscreenAd.SetExpressAdSize(DisplayMetricsUtil.PixelToDp(Screen.width), DisplayMetricsUtil.PixelToDp(Screen.height));
                m_MixFullscreenAd.LoadAd();
            }
            return true;
#endif

            return false;
        }

        protected override bool DoShowAd()
        {
            if (m_MixFullscreenAd == null)
            {
                return false;
            }

            m_MixFullscreenAd.Show();
            return true;
        }

        public override string GetReadyADNetWorkName()
        {
            var lineitem = m_MixFullscreenAd.GetReadyLineItem();
            if (lineitem != null)
            {
                return lineitem.GetNetwork().ToString();
            }

            return "";
        }

        public override bool ShowAd()
        {
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

        protected override void DoCleanAd()
        {
            if (m_MixFullscreenAd == null)
            {
                return;
            }
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
            m_AdEventArgs = args as AdEventArgs;
            HandleOnAdClosed();
        }

        protected void HandleOnAdReward(object sender, EventArgs args)
        {
            HandleOnAdRewarded();
        }

        protected void HandleOnAdOpened(object sender, EventArgs args)
        {
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
        private void RequestMixFullScreenAd()
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
            m_MixFullscreenAd.SetNativeAdLayout(layout);
        }


    }
}

