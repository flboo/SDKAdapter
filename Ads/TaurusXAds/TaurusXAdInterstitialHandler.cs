using System;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using UnityEngine;
using TaurusXAdSdk.Api;

namespace Qarth
{
    public class TaurusXAdInterstitialHandler : AdFullScreenHandler
    {
        private InterstitialAd m_InterstitialAd;

        public override bool isAdReady
        {
            get
            {
                if (m_InterstitialAd == null)
                {
                    return false;
                }
                return m_InterstitialAd.IsReady();
            }
        }

        protected override bool DoPreLoadAd()
        {
            if (string.IsNullOrEmpty(m_Config.unitID))
            {
                return false;
            }



#if UNITY_IOS
            if (m_InterstitialAd != null)
            {
                return false;
            }
#elif UNITY_ANDROID //&& !UNITY_EDITOR

            if (m_InterstitialAd == null)
            {
                m_InterstitialAd = new InterstitialAd(TaurusXConfigUtil.GetAdUnitId(m_Config.unitID));

                m_InterstitialAd.OnAdLoaded += HandleOnAdLoaded;
                m_InterstitialAd.OnAdShown += HandleOnAdOpened;
                //m_InterstitialAd.OnAdClicked += HandleOnAdOpened;
                m_InterstitialAd.OnAdClosed += HandleOnAdClosed;
                m_InterstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;

                //m_InterstitialAd.SetCL(TaurusXAdsUtils.GenCLConfig(1));
                m_InterstitialAd.LoadAd();
            }
            return true;
#endif
            return false;
        }
        protected override bool DoShowAd()
        {
            if (string.IsNullOrEmpty(m_AdInterface.adSceneId))
                m_InterstitialAd.Show();
            else
                m_InterstitialAd.Show(m_AdInterface.adSceneId);
            return true;
        }

        protected override void DoCleanAd()
        {
            if (m_InterstitialAd == null)
            {
                return;
            }

#if UNITY_IOS
            m_InterstitialAd.Destroy();
            m_InterstitialAd = null;            
#endif
        }

        public override string ToString()
        {
            return "#WebeyeInterstitial:" + m_Config.id;
        }

        protected void HandleOnAdLoaded(object sender, EventArgs args)
        {
            HandleOnAdLoaded();
        }

        protected void HandleOnAdOpened(object sender, EventArgs args)
        {
            HandleOnAdOpened();
        }

        protected void HandleOnAdClosed(object sender, EventArgs args)
        {
            HandleOnAdClosed();
        }

        protected void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            HandleOnAdFailedToLoad(args.AdError.GetMessage());
        }
    }
}

