using System;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public interface IBannerClient
    {
        event EventHandler<AdEventArgs> OnAdLoaded;
        event EventHandler<AdEventArgs> OnAdShown;
        event EventHandler<AdEventArgs> OnAdClicked;
        event EventHandler<AdEventArgs> OnAdClosed;
        event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        void SetPosition(BannerAdPosition position);

        void SetPosition(int x, int y);

        void SetAdSize(BannerAdSize adSize);

        void SetExpressAdSize(float width, float height);

        void SetMuted(bool muted);

        void SetNetworkConfigs(NetworkConfigs networkConfigs);

        void SetLineItemFilter(LineItemFilter filter);

        void LoadAd();

        bool IsReady();

        LineItem GetReadyLineItem();

        void Show();

        void Hide();

        void Destroy();
    }
}
