using System;
using UnityEngine;
using TaurusXAdSdk.Api;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class BannerClient : AndroidJavaProxy, IBannerClient
    {
        private AndroidJavaObject mBannerAd;

        public BannerClient(string adUnitId) : base(Utils.AdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            mBannerAd = new AndroidJavaObject(Utils.BannerAdClassName, new object[] { activity });
            mBannerAd.Call("setAdUnitId", adUnitId);
            mBannerAd.Call("setUnityADListener", this);
        }

        #region IBannerClient

        public event EventHandler<AdEventArgs> OnAdLoaded;
        public event EventHandler<AdEventArgs> OnAdShown;
        public event EventHandler<AdEventArgs> OnAdClicked;
        public event EventHandler<AdEventArgs> OnAdClosed;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        public void SetPosition(BannerAdPosition position) {
            mBannerAd.Call("setPositionUnity", (int)position);
        }

        public void SetPosition(int x, int y) {
            mBannerAd.Call("setPositionUnity", x, y);
        }

        public void SetAdSize(BannerAdSize adSize) {
            mBannerAd.Call("setAdSize", Utils.GetJavaBannerAdSize(adSize));
        }

        public void SetExpressAdSize(float width, float height) {
            mBannerAd.Call("setExpressAdSize", Utils.GetJavaAdSize(width, height));
        }

        public void SetMuted(bool muted) {
            mBannerAd.Call("setMuted", muted);
        }

        public void SetNetworkConfigs(NetworkConfigs networkConfigs) {
            mBannerAd.Call("setNetworkConfigs", Utils.ToJavaNetworkConfigs(networkConfigs));
        }

        public void SetLineItemFilter(LineItemFilter filter)
        {
            if (filter != null)
            {
                mBannerAd.Call("setLineItemFilter", new AndroidLineItemFilter(filter));
            }
        }

        public void LoadAd() {
            mBannerAd.Call("loadAdUnity");
        }

        public bool IsReady() {
            return mBannerAd.Call<bool>("isReady");
        }

        public LineItem GetReadyLineItem() {
            return new LineItem(new LineItemClient(mBannerAd.Call<AndroidJavaObject>("getReadyLineItem")));
        }

        public void Show() {
            mBannerAd.Call("showUnity");
        }

        public void Hide() {
            mBannerAd.Call("hideUnity");
        }

        public void Destroy() {
            mBannerAd.Call("destroy");
        }

        #endregion

        #region AdListener

        public void onAdLoaded(AndroidJavaObject lineItem)
        {
            if (OnAdLoaded != null)
            {
                OnAdLoaded(this, Utils.GenerateAdEventArgs(lineItem));
            }
        }

        public void onAdShown(AndroidJavaObject lineItem)
        {
            if (OnAdShown != null)
            {
                OnAdShown(this, Utils.GenerateAdEventArgs(lineItem));
            }
        }

        public void onAdClicked(AndroidJavaObject lineItem)
        {
            if (OnAdClicked != null)
            {
                OnAdClicked(this, Utils.GenerateAdEventArgs(lineItem));
            }
        }

        public void onAdClosed(AndroidJavaObject lineItem)
        {
            if (OnAdClosed != null)
            {
                OnAdClosed(this, Utils.GenerateAdEventArgs(lineItem));
            }
        }

        // adError is "com.taurusx.ads.core.api.listener.AdError"
        public void onAdFailedToLoad(AndroidJavaObject adError)
        {
            if (OnAdFailedToLoad != null)
            {
                OnAdFailedToLoad(this, Utils.GenerateAdFailedToLoadEventArgs(adError));
            }
        }

        #endregion
    }
}
