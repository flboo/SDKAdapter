using System;
using TaurusXAdSdk.Common;
using TaurusXAdSdk.Platforms;

namespace TaurusXAdSdk.Api
{
    public class MixViewAd
    {
        private IMixViewClient mClient;

        public MixViewAd(string adUnitId)
        {
            mClient = ClientFactory.BuildMixViewClient(adUnitId);
            ConfigureMixViewEvents();
        }

        #region AdListener

        public event EventHandler<AdEventArgs> OnAdLoaded;

        public event EventHandler<AdEventArgs> OnAdShown;

        public event EventHandler<AdEventArgs> OnAdClicked;

        public event EventHandler<AdEventArgs> OnAdClosed;

        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        #endregion

        #region Api

        public void SetPosition(AdPosition position) {
            mClient.SetPosition(position);
        }

        public void SetPosition(int x, int y) {
            mClient.SetPosition(x, y);
        }

        public void SetPositionRelative(AdPosition position, int offsetX, int offsetY) {
            mClient.SetPositionRelative(position, offsetX, offsetY);
        }

        public void SetBannerAdSize(BannerAdSize adSize) {
            mClient.SetBannerAdSize(adSize);
        }

        public void SetNativeAdLayout(NativeAdLayout layout) {
            mClient.SetNativeAdLayout(layout);
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

        [Obsolete("Please use SetAndroidWidth(int widthDp)", true)]
        public void SetWidth(int widthPx) {
            mClient.SetWidth(widthPx);
        }

        [Obsolete("Please use SetAndroidHeight(int heightDp)", true)]
        public void SetHeight(int heightPx) {
            mClient.SetHeight(heightPx);
        }

        public void SetAndroidWidth(float widthDp) {
            mClient.SetAndroidWidth(widthDp);
        }

        public void SetAndroidHeight(float heightDp) {
            mClient.SetAndroidHeight(heightDp);
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

        public void Hide() {
            mClient.Hide();
        }

        public void Destroy() {
            mClient.Destroy();
        }

        #endregion

        private void ConfigureMixViewEvents()
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
