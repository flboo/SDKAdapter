using System;
using UnityEngine;
using TaurusXAdSdk.Api;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class InterstitialClient : AndroidJavaProxy, IInterstitialClient
    {
        private AndroidJavaObject mInterstitialAd;
        private AndroidJavaObject mActivity;

        public InterstitialClient(string adUnitId) : base(Utils.InterstitialAdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            mActivity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            mInterstitialAd = new AndroidJavaObject(Utils.InterstitialAdClassName, new object[] { mActivity });
            mInterstitialAd.Call("setAdUnitId", adUnitId);
            mInterstitialAd.Call("setUnityADListener", this);
        }
        
        public event EventHandler<AdEventArgs> OnAdLoaded;
        public event EventHandler<AdEventArgs> OnAdShown;
        public event EventHandler<AdEventArgs> OnAdClicked;
        public event EventHandler<AdEventArgs> OnAdClosed;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;
        public event EventHandler<AdEventArgs> OnVideoStarted;
        public event EventHandler<AdEventArgs> OnVideoCompleted;

        #region IInterstitialClient

        public void SetExpressAdSize(float width, float height)
        {
            mInterstitialAd.Call("setExpressAdSize", Utils.GetJavaAdSize(width, height));
        }

        public void SetMuted(bool muted)
        {
            mInterstitialAd.Call("setMuted", muted);
        }

        public void SetNetworkConfigs(NetworkConfigs networkConfigs)
        {
            mInterstitialAd.Call("setNetworkConfigs", Utils.ToJavaNetworkConfigs(networkConfigs));
        }

        public void SetLineItemFilter(LineItemFilter filter)
        {
            if (filter != null)
            {
                mInterstitialAd.Call("setLineItemFilter", new AndroidLineItemFilter(filter));
            }
        }

        public void LoadAd()
        {
            mInterstitialAd.Call("loadAd");
        }

        public bool IsReady()
        {
            return mInterstitialAd.Call<bool>("isReady");
        }

        public LineItem GetReadyLineItem()
        {
            return new LineItem(new LineItemClient(mInterstitialAd.Call<AndroidJavaObject>("getReadyLineItem")));
        }

        public void Show()
        {
            mInterstitialAd.Call("show", mActivity);
        }

        public void Show(string sceneId) {
            mInterstitialAd.Call("show", mActivity, sceneId);
        }

        public void Destroy() {
            mInterstitialAd.Call("destroy");
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

        public void onVideoStarted(AndroidJavaObject lineItem)
        {
            if (OnVideoStarted != null)
            {
                OnVideoStarted(this, Utils.GenerateAdEventArgs(lineItem));
            }
        }

        public void onVideoCompleted(AndroidJavaObject lineItem)
        {
            if (OnVideoCompleted != null)
            {
                OnVideoCompleted(this, Utils.GenerateAdEventArgs(lineItem));
            }
        }

        #endregion
    }
}
