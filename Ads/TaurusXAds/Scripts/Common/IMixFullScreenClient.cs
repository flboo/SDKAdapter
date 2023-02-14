using System;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public interface IMixFullScreenClient
    {
        event EventHandler<AdEventArgs> OnAdLoaded;
        event EventHandler<AdEventArgs> OnAdShown;
        event EventHandler<AdEventArgs> OnAdClicked;
        event EventHandler<AdEventArgs> OnAdClosed;
        event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;
        event EventHandler<AdEventArgs> OnVideoStarted;
        event EventHandler<AdEventArgs> OnVideoCompleted;
        event EventHandler<RewardedEventArgs> OnRewarded;
        event EventHandler<AdEventArgs> OnRewardFailed;

        void SetBannerAdSize(BannerAdSize adSize);

        void SetExpressAdSize(float width, float height);

        void SetMuted(bool muted);

        void SetNativeAdLayout(NativeAdLayout layout);

        void SetNetworkConfigs(NetworkConfigs networkConfigs);

        void SetLineItemFilter(LineItemFilter filter);

        void SetBackPressEnable(bool enable);

        void LoadAd();

        bool IsReady();

        LineItem GetReadyLineItem();

        void Show();

        void Show(string sceneId);

        void Destroy();
    }
}