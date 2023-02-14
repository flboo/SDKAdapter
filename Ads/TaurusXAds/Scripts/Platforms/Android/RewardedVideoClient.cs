using System;
using UnityEngine;
using TaurusXAdSdk.Api;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class RewardedVideoClient : AndroidJavaProxy, IRewardedVideoClient
    {
        private AndroidJavaObject mRewardedVideoAd;
        private AndroidJavaObject mActivity;

        public RewardedVideoClient(string adUnitId) : base(Utils.RewardedVideoAdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            mActivity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            mRewardedVideoAd = new AndroidJavaObject(Utils.RewardedVideoAdClassName, new object[] { mActivity });
            mRewardedVideoAd.Call("setAdUnitId", adUnitId);
            mRewardedVideoAd.Call("setUnityADListener", this);
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

        #region IRewardedVideoClient

        public void SetMuted(bool muted)
        {
            mRewardedVideoAd.Call("setMuted", muted);
        }

        public void SetNetworkConfigs(NetworkConfigs networkConfigs)
        {
            mRewardedVideoAd.Call("setNetworkConfigs", Utils.ToJavaNetworkConfigs(networkConfigs));
        }

        public void SetLineItemFilter(LineItemFilter filter)
        {
            if (filter != null)
            {
                mRewardedVideoAd.Call("setLineItemFilter", new AndroidLineItemFilter(filter));
            }
        }

        public void LoadAd()
        {
            mRewardedVideoAd.Call("loadAd");
        }

        public bool IsReady()
        {
            return mRewardedVideoAd.Call<bool>("isReady");
        }

        public LineItem GetReadyLineItem()
        {
            return new LineItem(new LineItemClient(mRewardedVideoAd.Call<AndroidJavaObject>("getReadyLineItem")));
        }

        public RewardItem GetRewardItem()
        {
            return new RewardItem(new RewardItemClient(mRewardedVideoAd.Call<AndroidJavaObject>("getRewardItem")));
        }

        public void Show()
        {
            mRewardedVideoAd.Call("show", mActivity);
        }

        public void Show(string sceneId)
        {
            mRewardedVideoAd.Call("show", mActivity, sceneId);
        }

        public void Destroy()
        {
            mRewardedVideoAd.Call("destroy");
        }

        #endregion

        #region RewardedVideoAdListener

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
            if(OnVideoStarted != null)
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

        // rewardItem is "com.taurusx.ads.core.api.ad.RewardItem"
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
