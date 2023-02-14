using System;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public class DummyInterstitialClient : IInterstitialClient
    {
        public event EventHandler<AdEventArgs> OnAdLoaded;
        public event EventHandler<AdEventArgs> OnAdShown;
        public event EventHandler<AdEventArgs> OnAdClicked;
        public event EventHandler<AdEventArgs> OnAdClosed;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;
        public event EventHandler<AdEventArgs> OnVideoStarted;
        public event EventHandler<AdEventArgs> OnVideoCompleted;

        #region IInterstitialClient

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

        public void Show(string sceneId) { }

        public void Destroy() { }

        #endregion
    }
}
