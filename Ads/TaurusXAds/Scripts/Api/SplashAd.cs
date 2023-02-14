using System;
using TaurusXAdSdk.Common;
using TaurusXAdSdk.Platforms;

namespace TaurusXAdSdk.Api
{
    public class SplashAd
    {
        private ISplashClient mClient;
    
        public SplashAd(string adUnitId)
        {
            mClient = ClientFactory.BuildSplashClient(adUnitId);
            ConfigureSplashEvents();
        }
        
        public event EventHandler<AdEventArgs> OnAdLoaded;
        public event EventHandler<AdEventArgs> OnAdShown;
        public event EventHandler<AdEventArgs> OnAdClicked;
        public event EventHandler<AdEventArgs> OnAdSkipped;
        public event EventHandler<AdEventArgs> OnAdClosed;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        #region Api

        /**
         * Set express ad size, for TikTok.
         */
        public void SetExpressAdSize(float width, float height) {
            mClient.SetExpressAdSize(width, height);
        }

        /**
         * Set wether
         */
        public void SetMuted(bool muted) {
            mClient.SetMuted(muted);
        }

        public void SetBottomView(string bottomView) {
            mClient.SetBottomView(bottomView);
        }

        public void SetBottomText(string title, string desc) {
            mClient.SetBottomText(title, desc);
        }

        public void SetNetworkConfigs(NetworkConfigs networkConfigs) {
            mClient.SetNetworkConfigs(networkConfigs);
        }

        public void SetLineItemFilter(LineItemFilter filter) {
            mClient.SetLineItemFilter(filter);
        }

        public void LoadAd() {
            mClient.LoadAd();
        }

        public bool IsReady() {
            return mClient.IsReady();
        }

        public void Show(SplashOrientation orientation) {
            mClient.Show(orientation);
        }

        public void Show(string sceneId, SplashOrientation orientation) {
            mClient.Show(sceneId, orientation);
        }

        private void ConfigureSplashEvents()
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

            mClient.OnAdSkipped += (sender, args) => 
            {
                if(OnAdSkipped != null) {
                    OnAdSkipped(this, args);
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
        }

        #endregion
    }
}