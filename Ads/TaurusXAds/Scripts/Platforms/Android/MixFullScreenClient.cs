using System;
using UnityEngine;
using TaurusXAdSdk.Api;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class MixFullScreenClient : AndroidJavaProxy, IMixFullScreenClient
    {
        private AndroidJavaObject mMixFullScreenAd;
        private AndroidJavaObject mActivity;

        public MixFullScreenClient(string adUnitId) : base(Utils.RewardedVideoAdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            mActivity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            mMixFullScreenAd = new AndroidJavaObject(Utils.MixFullScreenAdClassName, new object[] { mActivity });
            mMixFullScreenAd.Call("setAdUnitId", adUnitId);
            mMixFullScreenAd.Call("setUnityADListener", this);
        }

        public event EventHandler<AdEventArgs> OnAdLoaded;
        public event EventHandler<AdEventArgs> OnAdShown;
        public event EventHandler<AdEventArgs> OnAdClicked;
        public event EventHandler<AdEventArgs> OnAdClosed;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;
        public event EventHandler<AdEventArgs> OnVideoStarted;
        public event EventHandler<AdEventArgs> OnVideoCompleted;
        public event EventHandler<RewardedEventArgs> OnRewarded;
        public event EventHandler<AdEventArgs> OnRewardFailed;

        #region IMixFullScreenClient

        public void SetBannerAdSize(BannerAdSize adSize)
        {
            mMixFullScreenAd.Call("setBannerAdSize", Utils.GetJavaBannerAdSize(adSize));
        }

        public void SetNativeAdLayout(NativeAdLayout layout)
        {
            if (layout != null) {
                AndroidJavaObject unityNativeAdLayout = Utils.ToJavaUnityNativeAdLayout(layout);
                if (unityNativeAdLayout != null)
                {
                    mMixFullScreenAd.Call("setUnityNativeAdLayout", unityNativeAdLayout);
                }
            }
        }

        public void SetExpressAdSize(float width, float height)
        {
            mMixFullScreenAd.Call("setExpressAdSize", Utils.GetJavaAdSize(width, height));
        }

        public void SetMuted(bool muted)
        {
            mMixFullScreenAd.Call("setMuted", muted);
        }

        public void SetNetworkConfigs(NetworkConfigs networkConfigs)
        {
            mMixFullScreenAd.Call("setNetworkConfigs", Utils.ToJavaNetworkConfigs(networkConfigs));
        }

        public void SetLineItemFilter(LineItemFilter filter)
        {
            if (filter != null)
            {
                mMixFullScreenAd.Call("setLineItemFilter", new AndroidLineItemFilter(filter));
            }
        }

        public void SetBackPressEnable(bool enable)
        {
            mMixFullScreenAd.Call("setBackPressEnable", enable);
        }

        public void LoadAd()
        {
            mMixFullScreenAd.Call("loadAdUnity");
        }

        public bool IsReady()
        {
            return mMixFullScreenAd.Call<bool>("isReady");
        }

        public LineItem GetReadyLineItem()
        {
            return new LineItem(new LineItemClient(mMixFullScreenAd.Call<AndroidJavaObject>("getReadyLineItem")));
        }

        public void Show()
        {
            mMixFullScreenAd.Call("show", mActivity);
        }

        public void Show(string sceneId)
        {
            mMixFullScreenAd.Call("show", mActivity, sceneId);
        }

        public void Destroy()
        {
            mMixFullScreenAd.Call("destroy");
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

        public void onVideoStarted(AndroidJavaObject lineItem) {
            if (OnVideoStarted != null)
            {
                OnVideoStarted(this, Utils.GenerateAdEventArgs(lineItem));
            }
        }

        public void onVideoCompleted(AndroidJavaObject lineItem) {
            if (OnVideoCompleted != null)
            {
                OnVideoCompleted(this, Utils.GenerateAdEventArgs(lineItem));
            }
        }

        public void onRewarded(AndroidJavaObject lineItem, AndroidJavaObject rewardItem)
        {
            if (OnRewarded != null)
            {
                RewardedEventArgs args = new RewardedEventArgs()
                {
                    LineItem = new LineItem(new LineItemClient(lineItem)),
                    RewardItem = new RewardItem(new RewardItemClient(rewardItem))
                };
                OnRewarded(this, args);
            }
        }

        public void onRewardFailed(AndroidJavaObject lineItem)
        {
            if (OnRewardFailed != null)
            {
                OnRewardFailed(this, Utils.GenerateAdEventArgs(lineItem));
            }
        }

        #endregion
    }
}
