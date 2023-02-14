using System;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public class DummyBannerClient : IBannerClient
    {
        public event EventHandler<AdEventArgs> OnAdLoaded;
        public event EventHandler<AdEventArgs> OnAdShown;
        public event EventHandler<AdEventArgs> OnAdClicked;
        public event EventHandler<AdEventArgs> OnAdClosed;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        #region IBannerClient

        public void SetPosition(BannerAdPosition position) { }

        public void SetPosition(int x, int y) { }

        public void SetAdSize(BannerAdSize adSize) { }

        public void SetExpressAdSize(float width, float height) { }

        public void SetMuted(bool muted) { }

        public void SetNetworkConfigs(NetworkConfigs networkConfigs) { }

        public void SetLineItemFilter(LineItemFilter filter) { }

        public void LoadAd() { }

        public bool IsReady() {
            return false;
        }

        public LineItem GetReadyLineItem() {
            return null;
        }

        public void Show() { }

        public void Hide() { }

        public void Destroy() { }

        #endregion
    }
}
