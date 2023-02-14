using System;
using UnityEngine;
using TaurusXAdSdk.Api;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class MixViewClient : AndroidJavaProxy, IMixViewClient
    {
        private AndroidJavaObject mMixViewAd;

        public MixViewClient(string adUnitId) : base(Utils.AdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            mMixViewAd = new AndroidJavaObject(Utils.MixViewAdClassName, new object[] { activity });
            mMixViewAd.Call("setAdUnitId", adUnitId);
            mMixViewAd.Call("setUnityADListener", this);
        }

        #region IBannerClient

        public event EventHandler<AdEventArgs> OnAdLoaded;
        public event EventHandler<AdEventArgs> OnAdShown;
        public event EventHandler<AdEventArgs> OnAdClicked;
        public event EventHandler<AdEventArgs> OnAdClosed;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        public void SetPosition(AdPosition position)
        {
            mMixViewAd.Call("setPositionUnity", (int)position);
        }

        public void SetPosition(int x, int y)
        {
            mMixViewAd.Call("setPositionUnity", x, y);
        }

        public void SetPositionRelative(AdPosition position, int offsetX, int offsetY)
        {
            mMixViewAd.Call("setPositionUnityRelative", (int)position, offsetX, offsetY);
        }

        public void SetBannerAdSize(BannerAdSize adSize)
        {
            mMixViewAd.Call("setBannerAdSize", Utils.GetJavaBannerAdSize(adSize));
        }

        public void SetExpressAdSize(float width, float height)
        {
            mMixViewAd.Call("setExpressAdSize", Utils.GetJavaAdSize(width, height));
        }

        public void SetMuted(bool muted)
        {
            mMixViewAd.Call("setMuted", muted);
        }

        public void SetNativeAdLayout(NativeAdLayout layout)
        {
            if (layout != null)
            {
                AndroidJavaObject unityNativeAdLayout = Utils.ToJavaUnityNativeAdLayout(layout);
                if (unityNativeAdLayout != null)
                {
                    mMixViewAd.Call("setUnityNativeAdLayout", unityNativeAdLayout);
                }
            }
        }

        public void SetNetworkConfigs(NetworkConfigs networkConfigs)
        {
            mMixViewAd.Call("setNetworkConfigs", Utils.ToJavaNetworkConfigs(networkConfigs));
        }

        public void SetLineItemFilter(LineItemFilter filter)
        {
            if (filter != null)
            {
                mMixViewAd.Call("setLineItemFilter", new AndroidLineItemFilter(filter));
            }
        }

        public void SetWidth(int width)
        {
            mMixViewAd.Call("setUnityWidth", width);
        }

        public void SetHeight(int height)
        {
            mMixViewAd.Call("setUnityHeight", height);
        }

        public void SetAndroidWidth(float widthDp)
        {
            mMixViewAd.Call("setUnityWidthDp", widthDp);
        }

        public void SetAndroidHeight(float heightDp)
        {
            mMixViewAd.Call("setUnityHeightDp", heightDp);
        }

        public void LoadAd()
        {
            mMixViewAd.Call("loadAdUnity");
        }

        public bool IsReady()
        {
            return mMixViewAd.Call<bool>("isReady");
        }

        public LineItem GetReadyLineItem()
        {
            return new LineItem(new LineItemClient(mMixViewAd.Call<AndroidJavaObject>("getReadyLineItem")));
        }

        public void Show()
        {
            mMixViewAd.Call("showUnity");
        }

        public void Show(string sceneId)
        {
            mMixViewAd.Call("showUnity", sceneId);
        }

        public void Hide()
        {
            mMixViewAd.Call("hideUnity");
        }

        public void Destroy()
        {
            mMixViewAd.Call("destroy");
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
