using System;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public class DummySplashClient : ISplashClient
    {
        #region ISplashClient
        public event EventHandler<AdEventArgs> OnAdLoaded;
        public event EventHandler<AdEventArgs> OnAdShown;
        public event EventHandler<AdEventArgs> OnAdClicked;
        public event EventHandler<AdEventArgs> OnAdSkipped;
        public event EventHandler<AdEventArgs> OnAdClosed;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        public void SetExpressAdSize(float width, float height) { }

        public void SetMuted(bool muted) { }

        public void SetBottomView(string bottomView) { }

        public void SetBottomText(string title, string desc) { }

        public void SetNetworkConfigs(NetworkConfigs networkConfigs) { }

        public void SetLineItemFilter(LineItemFilter filter) { }

        public void LoadAd() { }

        public bool IsReady() {
            return false;
        }
        
        public void Show(SplashOrientation orientation) { }

        public void Show(string sceneId, SplashOrientation orientation) { }

        #endregion
    }
}
