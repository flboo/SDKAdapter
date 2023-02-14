using System;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public interface IInterstitialClient
    {
        event EventHandler<AdEventArgs> OnAdLoaded;
        event EventHandler<AdEventArgs> OnAdShown;
        event EventHandler<AdEventArgs> OnAdClicked;
        event EventHandler<AdEventArgs> OnAdClosed;
        event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;
        event EventHandler<AdEventArgs> OnVideoStarted;
        event EventHandler<AdEventArgs> OnVideoCompleted;

        void SetExpressAdSize(float width, float height);

        void SetMuted(bool muted);

        void SetNetworkConfigs(NetworkConfigs networkConfigs);

        void SetLineItemFilter(LineItemFilter filter);

        void LoadAd();

        bool IsReady();

        LineItem GetReadyLineItem();

        void Show();

        void Show(string sceneId);

        void Destroy();
    }
}