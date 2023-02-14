using System;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public interface IMixViewClient
    {
        event EventHandler<AdEventArgs> OnAdLoaded;
        event EventHandler<AdEventArgs> OnAdShown;
        event EventHandler<AdEventArgs> OnAdClicked;
        event EventHandler<AdEventArgs> OnAdClosed;
        event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        void SetPosition(AdPosition position);

        void SetPosition(int x, int y);

        void SetPositionRelative(AdPosition position, int offsetX, int offsetY);

        void SetBannerAdSize(BannerAdSize adSize);

        void SetExpressAdSize(float width, float height);

        void SetMuted(bool muted);

        void SetNativeAdLayout(NativeAdLayout layout);

        void SetNetworkConfigs(NetworkConfigs networkConfigs);

        void SetLineItemFilter(LineItemFilter filter);

        void SetWidth(int widthPx);

        void SetHeight(int heightPx);

        void SetAndroidWidth(float widthDp);

        void SetAndroidHeight(float heightDp);

        void LoadAd();

        bool IsReady();

        LineItem GetReadyLineItem();

        void Show();

        void Show(string sceneId);

        void Hide();

        void Destroy();
    }
}
