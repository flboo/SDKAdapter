using System;
using UnityEngine;
using TaurusXAdSdk.Api;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class SplashClient : AndroidJavaProxy, ISplashClient
    {
        private AndroidJavaObject mSplashAd;

        public SplashClient(string adUnitId) : base(Utils.SplashAdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            mSplashAd = new AndroidJavaObject(Utils.SplashAdClassName, new object[] { activity });
            mSplashAd.Call("setAdUnitId", adUnitId);
            mSplashAd.Call("setUnityADListener", this);
        }

        public event EventHandler<AdEventArgs> OnAdLoaded;
        public event EventHandler<AdEventArgs> OnAdShown;
        public event EventHandler<AdEventArgs> OnAdClicked;
        public event EventHandler<AdEventArgs> OnAdSkipped;
        public event EventHandler<AdEventArgs> OnAdClosed;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        #region ISplashClient

        public void SetExpressAdSize(float width, float height)
        {
            mSplashAd.Call("setExpressAdSize", Utils.GetJavaAdSize(width, height));
        }

        public void SetMuted(bool muted)
        {
            mSplashAd.Call("setMuted", muted);
        }

        public void SetBottomView(string bottomView)
        {
            mSplashAd.Call("setUnityBottomView", bottomView);
        }

        public void SetBottomText(string title, string desc)
        {
            mSplashAd.Call("setBottomText", title, desc);
        }

        public void SetNetworkConfigs(NetworkConfigs networkConfigs)
        {
            mSplashAd.Call("setNetworkConfigs", Utils.ToJavaNetworkConfigs(networkConfigs));
        }

        public void SetLineItemFilter(LineItemFilter filter)
        {
            if (filter != null)
            {
                mSplashAd.Call("setLineItemFilter", new AndroidLineItemFilter(filter));
            }
        }

        public void LoadAd() {
            mSplashAd.Call("loadAdOnly");
        }

        public bool IsReady() {
            return mSplashAd.Call<bool>("isReady");
        }

        public void Show(SplashOrientation orientation)
        {
            mSplashAd.Call("showUnity", (int) orientation);
        }

        public void Show(string sceneId, SplashOrientation orientation)
        {
            mSplashAd.Call("showUnity", sceneId, (int)orientation);
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
        
        public void onAdSkipped(AndroidJavaObject lineItem)
        {
            if (OnAdSkipped != null)
            {
                OnAdSkipped(this, Utils.GenerateAdEventArgs(lineItem));
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
