using System;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public interface ISplashClient
    {
        event EventHandler<AdEventArgs> OnAdLoaded;
        event EventHandler<AdEventArgs> OnAdShown;
        event EventHandler<AdEventArgs> OnAdClicked;
        event EventHandler<AdEventArgs> OnAdSkipped;
        event EventHandler<AdEventArgs> OnAdClosed;
        event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        void SetExpressAdSize(float width, float height);

        void SetMuted(bool muted);

        void SetBottomView(string bottomView);

        void SetBottomText(string title, string desc);

        void SetNetworkConfigs(NetworkConfigs networkConfigs);

        void SetLineItemFilter(LineItemFilter filter);

        void LoadAd();

        bool IsReady();

        void Show(SplashOrientation orientation);

        void Show(string sceneId, SplashOrientation orientation);
    }
}