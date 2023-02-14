using System;
using TaurusXAdSdk.Common;
using TaurusXAdSdk.Platforms;

namespace TaurusXAdSdk.Api
{
    public class BannerAd
    {
        private IBannerClient mClient;

        public BannerAd(string adUnitId)
        {
            mClient = ClientFactory.BuildBannerClient(adUnitId);
            ConfigureBannerEvents();
        }

        #region AdListener

        public event EventHandler<AdEventArgs> OnAdLoaded;

        public event EventHandler<AdEventArgs> OnAdShown;

        public event EventHandler<AdEventArgs> OnAdClicked;

        public event EventHandler<AdEventArgs> OnAdClosed;

        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        #endregion

        #region Api
        public void SetPosition(BannerAdPosition position) {
            mClient.SetPosition(position);
        }

        public void SetPosition(int x, int y) {
            mClient.SetPosition(x, y);
        }

        public void SetAdSize(BannerAdSize adSize) {
            mClient.SetAdSize(adSize);
        }

        public void SetExpressAdSize(float width, float height) {
            mClient.SetExpressAdSize(width, height);
        }

        public void SetMuted(bool muted) {
            mClient.SetMuted(muted);
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

        public LineItem GetReadyLineItem() {
            return mClient.GetReadyLineItem();
        }

        public void Show() {
            mClient.Show();
        }

        public void Hide() {
            mClient.Hide();
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
