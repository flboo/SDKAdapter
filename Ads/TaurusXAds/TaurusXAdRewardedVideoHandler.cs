using System;
using Qarth;
using UnityEngine;
using TaurusXAdSdk.Api;

namespace Qarth
{
    public class TaurusXAdRewardedVideoHandler : AdFullScreenHandler
    {
        private RewardedVideoAd m_RewardVideoAd;

        public override bool isAdReady
        {
            get
            {
                if (m_RewardVideoAd == null)
                {
                    return false;
                }
                return m_RewardVideoAd.IsReady();
            }
        }

        protected override bool DoPreLoadAd()
        {
            if (m_RewardVideoAd != null && m_RewardVideoAd.IsReady())
            {
                return false;
            }

#if UNITY_ANDROID 
            if (m_RewardVideoAd == null)
            {
                m_RewardVideoAd = new RewardedVideoAd(TaurusXConfigUtil.GetAdUnitId(m_Config.unitID));
                m_RewardVideoAd.OnAdLoaded += HandleOnAdLoaded;
                m_RewardVideoAd.OnAdShown += HandleOnAdOpened;
                m_RewardVideoAd.OnAdClicked += HandleOnAdClick;
                m_RewardVideoAd.OnAdClosed += HandleOnAdClosed;
                m_RewardVideoAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
                //m_RewardVideoAd.OnVideoStarted += HandleOnAdStarted;
                m_RewardVideoAd.OnVideoCompleted += HandleOnAdComplate;
                m_RewardVideoAd.OnRewarded += HandleOnAdRewarded;
                //m_RewardVideoAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;

                //m_RewardVideoAd.SetCL(TaurusXAdsUtils.GenCLConfig(1));
                m_RewardVideoAd.LoadAd();
            }

            return true;
#endif
            return false;
        }

        public override string GetReadyADNetWorkName()
        {
            var lineitem = m_RewardVideoAd.GetReadyLineItem();
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

        private void HandleOnAdStarted(object sender, EventArgs e)
        {
            // HandleOnAdOpened();
        }

        protected override bool DoShowAd()
        {
            if (string.IsNullOrEmpty(m_AdInterface.adSceneId))
                m_RewardVideoAd.Show();
            else
                m_RewardVideoAd.Show(m_AdInterface.adSceneId);
            return true;
        }

        protected override void DoCleanAd()
        {
            if (m_RewardVideoAd == null)
            {
                return;
            }

            //#if UNITY_IOS
            //m_RewardVideoAd = null;
            //#endif
        }

        public override string ToString()
        {
            return "#WebeyeRewardVideo:" + m_Config.id;
        }
        protected void HandleOnAdLoaded(object sender, EventArgs args)
        {
            HandleOnAdLoaded();
        }

        protected void HandleOnAdOpened(object sender, EventArgs args)
        {
            HandleOnAdOpened();
        }

        protected void HandleOnAdClick(object sender, EventArgs args)
        {
            HandleOnAdClick();
        }

        protected void HandleOnAdClosed(object sender, EventArgs args)
        {
            m_AdEventArgs = args as AdEventArgs;
            HandleOnAdClosed();
        }

        protected void HandleOnAdComplate(object sender, EventArgs args)
        {
            HandleOnAdRewarded();
        }

        protected void HandleOnAdRewarded(object sender, EventArgs args)
        {
            HandleOnAdRewarded();
        }

        protected void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            HandleOnAdFailedToLoad(args.AdError.ToString());
        }


    }
}


