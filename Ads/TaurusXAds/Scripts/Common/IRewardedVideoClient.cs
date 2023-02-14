using System;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public interface IRewardedVideoClient
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

        void SetMuted(bool muted);

        void SetNetworkConfigs(NetworkConfigs networkConfigs);

        void SetLineItemFilter(LineItemFilter filter);

        void LoadAd();

        bool IsReady();

        LineItem GetReadyLineItem();

        RewardItem GetRewardItem();

        void Show();

        void Show(string sceneId);

        void Destroy();
    }
}
