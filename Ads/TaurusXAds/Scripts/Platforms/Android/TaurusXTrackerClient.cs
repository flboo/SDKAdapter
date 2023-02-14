using System;
using UnityEngine;
using TaurusXAdSdk.Api;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class TaurusXTrackerClient : AndroidJavaProxy, ITaurusXTrackerClient
    {
        private AndroidJavaObject mTaurusXClient;

        public TaurusXTrackerClient() : base(Utils.TrackerListenerClassName)
        {
            AndroidJavaClass sdkTrackerClass = new AndroidJavaClass(Utils.TrackerClassName);
            mTaurusXClient = sdkTrackerClass.CallStatic<AndroidJavaObject>("getInstance");
            mTaurusXClient.Call("registerListener", this);
        }

        #region ITaurusXTrackerClient

        public event EventHandler<TrackerEventArgs> OnAdRequest;
        public event EventHandler<TrackerEventArgs> OnAdLoaded;
        public event EventHandler<TrackerEventArgs> OnAdFailedToLoad;
        public event EventHandler<TrackerEventArgs> OnAdShown;
        public event EventHandler<TrackerEventArgs> OnAdCallShow;
        public event EventHandler<TrackerEventArgs> OnAdClicked;
        public event EventHandler<TrackerEventArgs> OnAdSkipped;
        public event EventHandler<TrackerEventArgs> OnAdClosed;
        public event EventHandler<TrackerEventArgs> OnVideoStarted;
        public event EventHandler<TrackerEventArgs> OnVideoCompleted;
        public event EventHandler<TrackerEventArgs> OnRewarded;
        public event EventHandler<TrackerEventArgs> OnRewardFailed;

        public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitRequest;
        public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitLoaded;
        public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitFailedToLoad;
        public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitShown;
        public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitCallShow;
        public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitClicked;
        public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitSkipped;
        public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitClosed;
        public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitVideoStarted;
        public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitVideoCompleted;
        public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitRewarded;
        public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitRewardFailed;

        #endregion


        #region TrackerListener

        private TrackerEventArgs FromTrackerInfo(AndroidJavaObject trackerInfo) {
            TrackerEventArgs args = new TrackerEventArgs()
            {
                TrackerInfo = new TrackerInfo(new TrackerInfoClient(trackerInfo))
            };
            return args;
        }

        public void onAdRequest(AndroidJavaObject trackerInfo)
        {
            if (OnAdRequest != null)
            {
                TrackerEventArgs args = FromTrackerInfo(trackerInfo);
                OnAdRequest(this, args);
            }
        }

        public void onAdLoaded(AndroidJavaObject trackerInfo)
        {
            if (OnAdLoaded != null)
            {
                TrackerEventArgs args = FromTrackerInfo(trackerInfo);
                OnAdLoaded(this, args);
            }
        }

        public void onAdFailedToLoad(AndroidJavaObject trackerInfo)
        {
            if (OnAdFailedToLoad != null)
            {
                TrackerEventArgs args = FromTrackerInfo(trackerInfo);
                OnAdFailedToLoad(this, args);
            }
        }

        public void onAdCallShow(AndroidJavaObject trackerInfo)
        {
            if (OnAdCallShow != null)
            {
                TrackerEventArgs args = FromTrackerInfo(trackerInfo);
                OnAdCallShow(this, args);
            }
        }

        public void onAdShown(AndroidJavaObject trackerInfo)
        {
            if (OnAdShown != null)
            {
                TrackerEventArgs args = FromTrackerInfo(trackerInfo);
                OnAdShown(this, args);
            }
        }

        public void onAdClicked(AndroidJavaObject trackerInfo)
        {
            if (OnAdClicked != null)
            {
                TrackerEventArgs args = FromTrackerInfo(trackerInfo);
                OnAdClicked(this, args);
            }
        }

        public void onAdSkipped(AndroidJavaObject trackerInfo)
        {
            if (OnAdSkipped != null)
            {
                TrackerEventArgs args = FromTrackerInfo(trackerInfo);
                OnAdSkipped(this, args);
            }
        }

        public void onAdClosed(AndroidJavaObject trackerInfo)
        {
            if (OnAdClosed != null)
            {
                TrackerEventArgs args = FromTrackerInfo(trackerInfo);
                OnAdClosed(this, args);
            }
        }

        public void onVideoStarted(AndroidJavaObject trackerInfo)
        {
            if (OnVideoStarted != null)
            {
                TrackerEventArgs args = FromTrackerInfo(trackerInfo);
                OnVideoStarted(this, args);
            }
        }

        public void onVideoCompleted(AndroidJavaObject trackerInfo)
        {
            if (OnVideoCompleted != null)
            {
                TrackerEventArgs args = FromTrackerInfo(trackerInfo);
                OnVideoCompleted(this, args);
            }
        }

        public void onRewarded(AndroidJavaObject trackerInfo)
        {
            if (OnRewarded != null)
            {
                TrackerEventArgs args = FromTrackerInfo(trackerInfo);
                OnRewarded(this, args);
            }
        }

        public void onRewardFailed(AndroidJavaObject trackerInfo)
        {
            if (OnRewardFailed != null)
            {
                TrackerEventArgs args = FromTrackerInfo(trackerInfo);
                OnRewardFailed(this, args);
            }
        }






        private TrackerAdUnitEventArgs FromAdUnitInfo(AndroidJavaObject adUnitInfo)
        {
            TrackerAdUnitEventArgs args = new TrackerAdUnitEventArgs()
            {
                TrackerAdUnitInfo = new TrackerAdUnitInfo(new TrackerAdUnitInfoClient(adUnitInfo))
            };
            return args;
        }

        public void onAdUnitRequest(AndroidJavaObject adUnitInfo)
        {
            if (OnAdUnitRequest != null)
            {
                TrackerAdUnitEventArgs args = FromAdUnitInfo(adUnitInfo);
                OnAdUnitRequest(this, args);
            }
        }

        public void onAdUnitLoaded(AndroidJavaObject adUnitInfo)
        {
            if (OnAdUnitLoaded != null)
            {
                TrackerAdUnitEventArgs args = FromAdUnitInfo(adUnitInfo);
                OnAdUnitLoaded(this, args);
            }
        }

        public void onAdUnitFailedToLoad(AndroidJavaObject adUnitInfo)
        {
            if (OnAdUnitFailedToLoad != null)
            {
                TrackerAdUnitEventArgs args = FromAdUnitInfo(adUnitInfo);
                OnAdUnitFailedToLoad(this, args);
            }
        }

        public void onAdUnitCallShow(AndroidJavaObject adUnitInfo)
        {
            if (OnAdUnitCallShow != null)
            {
                TrackerAdUnitEventArgs args = FromAdUnitInfo(adUnitInfo);
                OnAdUnitCallShow(this, args);
            }
        }

        public void onAdUnitShown(AndroidJavaObject adUnitInfo)
        {
            if (OnAdUnitShown != null)
            {
                TrackerAdUnitEventArgs args = FromAdUnitInfo(adUnitInfo);
                OnAdUnitShown(this, args);
            }
        }

        public void onAdUnitClicked(AndroidJavaObject adUnitInfo)
        {
            if (OnAdUnitClicked != null)
            {
                TrackerAdUnitEventArgs args = FromAdUnitInfo(adUnitInfo);
                OnAdUnitClicked(this, args);
            }
        }

        public void onAdUnitSkipped(AndroidJavaObject adUnitInfo)
        {
            if (OnAdUnitSkipped != null)
            {
                TrackerAdUnitEventArgs args = FromAdUnitInfo(adUnitInfo);
                OnAdUnitSkipped(this, args);
            }
        }

        public void onAdUnitClosed(AndroidJavaObject adUnitInfo)
        {
            if (OnAdUnitClosed != null)
            {
                TrackerAdUnitEventArgs args = FromAdUnitInfo(adUnitInfo);
                OnAdUnitClosed(this, args);
            }
        }

        public void onAdUnitVideoStarted(AndroidJavaObject adUnitInfo)
        {
            if (OnAdUnitVideoStarted != null)
            {
                TrackerAdUnitEventArgs args = FromAdUnitInfo(adUnitInfo);
                OnAdUnitVideoStarted(this, args);
            }
        }

        public void onAdUnitVideoCompleted(AndroidJavaObject adUnitInfo)
        {
            if (OnAdUnitVideoCompleted != null)
            {
                TrackerAdUnitEventArgs args = FromAdUnitInfo(adUnitInfo);
                OnAdUnitVideoCompleted(this, args);
            }
        }

        public void onAdUnitRewarded(AndroidJavaObject adUnitInfo)
        {
            if (OnAdUnitRewarded != null)
            {
                TrackerAdUnitEventArgs args = FromAdUnitInfo(adUnitInfo);
                OnAdUnitRewarded(this, args);
            }
        }

        public void onAdUnitRewardFailed(AndroidJavaObject adUnitInfo)
        {
            if (OnAdUnitRewardFailed != null)
            {
                TrackerAdUnitEventArgs args = FromAdUnitInfo(adUnitInfo);
                OnAdUnitRewardFailed(this, args);
            }
        }

        #endregion
    }
}
