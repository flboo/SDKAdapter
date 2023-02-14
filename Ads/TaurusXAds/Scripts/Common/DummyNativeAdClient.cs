using System;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public class DummyNativeAdClient : INativeAdClient
    {
        public event EventHandler<EventArgs> OnAdLoaded;
        public event EventHandler<EventArgs> OnAdShown;
        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<EventArgs> OnAdClosed;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        #region INativeAdClient

        public void LoadAd() { }

        public bool IsReady()
        {
            return false;
        }

        public NativeAdData GetNativeAdData() {
            return null;
        }

        public void Destroy() { }

        #endregion
    }
}
