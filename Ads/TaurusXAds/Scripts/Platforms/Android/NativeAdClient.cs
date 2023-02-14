using System;
using UnityEngine;
using TaurusXAdSdk.Api;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class NativeAdClient : AndroidJavaProxy, INativeAdClient
    {
        private AndroidJavaObject mNativeAd;

        public NativeAdClient(string adUnitId) : base(Utils.AdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            mNativeAd = new AndroidJavaObject(Utils.NativeAdClassName, new object[] { activity });
            mNativeAd.Call("setAdUnitId", adUnitId);
            mNativeAd.Call("setAdListener", this);
        }

        public event EventHandler<EventArgs> OnAdLoaded;
        public event EventHandler<EventArgs> OnAdShown;
        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<EventArgs> OnAdClosed;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        #region INativeAdClient

        public void LoadAd() {
            mNativeAd.Call("loadAd");
        }

        public bool IsReady() {
            return mNativeAd.Call<bool>("isReady");
        }

        public NativeAdData GetNativeAdData() {
            return new NativeAdData(new NativeAdDataClient(mNativeAd.Call<AndroidJavaObject>("getAd")));
        }

        public void Destroy() {
            mNativeAd.Call("destroy");
        }

        #endregion

        #region AdListener

        public void onAdLoaded()
        {
            if (OnAdLoaded != null)
            {
                OnAdLoaded(this, EventArgs.Empty);
            }
        }

        public void onAdShown()
        {
            if (OnAdShown != null)
            {
                OnAdShown(this, EventArgs.Empty);
            }
        }

        public void onAdClicked()
        {
            if (OnAdClicked != null)
            {
                OnAdClicked(this, EventArgs.Empty);
            }
        }

        public void onAdClosed()
        {
            if (OnAdClosed != null)
            {
                OnAdClosed(this, EventArgs.Empty);
            }
        }

        // adError is "com.taurusx.ads.core.api.listener.AdError"
        public void onAdFailedToLoad(AndroidJavaObject adError)
        {
            if (OnAdFailedToLoad != null)
            {
                AdFailedToLoadEventArgs args = new AdFailedToLoadEventArgs()
                {
                    AdError = new AdError(new AdErrorClient(adError))
                };
                OnAdFailedToLoad(this, args);
            }
        }

        #endregion
    }
}
