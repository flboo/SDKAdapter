using System;
using TaurusXAdSdk.Common;
using TaurusXAdSdk.Platforms;

namespace TaurusXAdSdk.Api
{
    public class NativeAd
    {
        private INativeAdClient mClient;

        public NativeAd(string adUnitId)
        {
            mClient = ClientFactory.BuildNativeAdClient(adUnitId);
            ConfigureBannerEvents();
        }

        #region AdListener

        public event EventHandler<EventArgs> OnAdLoaded;

        public event EventHandler<EventArgs> OnAdShown;

        public event EventHandler<EventArgs> OnAdClicked;

        public event EventHandler<EventArgs> OnAdClosed;

        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        #endregion

        #region Api

        public void LoadAd() {
            mClient.LoadAd();
        }

        public bool IsReady() {
            return mClient.IsReady();
        }

        public NativeAdData GetNativeAdData() {
            return mClient.GetNativeAdData();
        }

        public void Destroy() {
            mClient.Destroy();
        }

        #endregion

        private void ConfigureBannerEvents()
        {
            mClient.OnAdLoaded += (sender, args) =>
            {
                if (OnAdLoaded != null)
                {
                    OnAdLoaded(this, args);
                }
            };

            mClient.OnAdShown += (sender, args) =>
            {
                if (OnAdShown != null)
                {
                    OnAdShown(this, args);
                }
            };

            mClient.OnAdClicked += (sender, args) =>
            {
                if (OnAdClicked != null)
                {
                    OnAdClicked(this, args);
                }
            };

            mClient.OnAdClosed += (sender, args) =>
            {
                if (OnAdClosed != null)
                {
                    OnAdClosed(this, args);
                }
            };

            mClient.OnAdFailedToLoad += (sender, args) =>
            {
                if (OnAdFailedToLoad != null)
                {
                    OnAdFailedToLoad(this, args);
                }
            };
        }
    }
}
