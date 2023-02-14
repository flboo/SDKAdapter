using System;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public interface INativeAdClient
    {
        event EventHandler<EventArgs> OnAdLoaded;
        event EventHandler<EventArgs> OnAdShown;
        event EventHandler<EventArgs> OnAdClicked;
        event EventHandler<EventArgs> OnAdClosed;
        event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        void LoadAd();

        bool IsReady();

        NativeAdData GetNativeAdData();

        void Destroy();
    }
}
