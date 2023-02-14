﻿using System;
using TaurusXAdSdk.Common;
using TaurusXAdSdk.Platforms;

namespace TaurusXAdSdk.Api
{
    public class MixFullScreenAd
    {
        private IMixFullScreenClient mClient;
    
        public MixFullScreenAd(string adUnitId)
        {
            mClient = ClientFactory.BuildMixFullScreenClient(adUnitId);
            ConfigureMixFullScreenEvents();
        }

        #region AdListener

        public event EventHandler<AdEventArgs> OnAdLoaded;

        public event EventHandler<AdEventArgs> OnAdShown;

        public event EventHandler<AdEventArgs> OnAdClicked;

        public event EventHandler<AdEventArgs> OnAdClosed;

        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        public event EventHandler<AdEventArgs> OnVideoStarted;

        public event EventHandler<AdEventArgs> OnVideoCompleted;
        
        public event EventHandler<RewardedEventArgs> OnRewarded;

        public event EventHandler<AdEventArgs> OnRewardFailed;
        #endregion

        #region Api
        public void SetBannerAdSize(BannerAdSize adSize) {
            mClient.SetBannerAdSize(adSize);
        }

        public void SetExpressAdSize(float width, float height) {
            mClient.SetExpressAdSize(width, height);
        }

        public void SetMuted(bool muted) {
            mClient.SetMuted(muted);
        }

        public void SetNativeAdLayout(NativeAdLayout layout) {
            mClient.SetNativeAdLayout(layout);
        }

        public void SetNetworkConfigs(NetworkConfigs networkConfigs) {
            mClient.SetNetworkConfigs(networkConfigs);
        }

        public void SetLineItemFilter(LineItemFilter filter) {
            mClient.SetLineItemFilter(filter);
        }

        public void SetBackPressEnable(bool enable) {
            mClient.SetBackPressEnable(enable);
        }

        public void LoadAd() {
            mClient.LoadAd();
        }

        public bool IsReady() {
            return mClient.IsReady();
        }

        public LineItem GetReadyLineItem() {
            return mClient.GetReadyLineItem();
        }

        public void Show() {
            mClient.Show();
        }

        public void Show(string sceneId) {
            mClient.Show(sceneId);
        }

        public void Destroy() {
            mClient.Destroy();
        }

        #endregion

        private void ConfigureMixFullScreenEvents()
        {
            mClient.OnAdLoaded += (sender, args) => 
            {
                if (OnAdLoaded != null) {
                    OnAdLoaded(this, args);
                }
            };

            mClient.OnAdShown += (sender, args) => 
            {
                if (OnAdShown != null) {
                    OnAdShown(this, args);
                }
            };

            mClient.OnAdClicked += (sender, args) => 
            {
                if(OnAdClicked != null) {
                    OnAdClicked(this, args);
                }
            };

            mClient.OnAdClosed += (sender, args) => 
            {
                if(OnAdClosed != null) {
                    OnAdClosed(this, args);
                }
            };

            mClient.OnAdFailedToLoad += (sender, args) => 
            {
                if(OnAdFailedToLoad != null) {
                    OnAdFailedToLoad(this, args);
                }
            };

            mClient.OnVideoStarted += (sender, args) =>
            {
                if (OnVideoStarted != null)
                {
                    OnVideoStarted(this, args);
                }
            };

            mClient.OnVideoCompleted += (sender, args) =>
            {
                if (OnVideoCompleted != null)
                {
                    OnVideoCompleted(this, args);
                }
            };

            mClient.OnRewarded += (sender, args) =>
            {
                if (OnRewarded != null)
                {
                    OnRewarded(this, args);
                }
            };

            mClient.OnRewardFailed += (sender, args) =>
            {
                if (OnRewardFailed != null)
                {
                    OnRewardFailed(this, args);
                }
            };
        }
    }
}
