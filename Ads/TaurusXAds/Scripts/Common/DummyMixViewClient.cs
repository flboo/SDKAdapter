using System;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public class DummyMixViewClient : IMixViewClient
    {
        public event EventHandler<AdEventArgs> OnAdLoaded;
        public event EventHandler<AdEventArgs> OnAdShown;
        public event EventHandler<AdEventArgs> OnAdClicked;
        public event EventHandler<AdEventArgs> OnAdClosed;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        #region IMixViewClient

        public void SetPosition(AdPosition position) { }

        public void SetPosition(int x, int y) { }

        public void SetPositionRelative(AdPosition position, int offsetX, int offsetY) { }

        public void SetBannerAdSize(BannerAdSize adSize) { }

        public void SetNativeAdLayout(NativeAdLayout layout) { }

        public void SetExpressAdSize(float width, float height) { }

        public void SetMuted(bool muted) { }

        public void SetNetworkConfigs(NetworkConfigs networkConfigs) { }

        public void SetLineItemFilter(LineItemFilter filter) { }

        public void SetWidth(int widthPx) { }

        public void SetHeight(int heightPx) { }

        public void SetAndroidWidth(float widthDp) { }

        public void SetAndroidHeight(float heightDp) { }

        public void LoadAd() { }

        public bool IsReady() {
            return false;
        }

        public LineItem GetReadyLineItem() {
            return null;
        }

        public void Show() { }

        public void Show(string sceneId) { }

        public void Hide() { }

        public void Destroy() { }

        #endregion
    }
}