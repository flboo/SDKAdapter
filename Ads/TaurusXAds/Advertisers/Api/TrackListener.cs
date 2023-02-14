using System;
using TaurusXAdSdk.Api;
using TaurusXAdSdk.Platforms.Android;

namespace Advertisers.Api
{
    public class TrackListener
    {
        public TrackListener()
        {
            
        }

        #region TrackListener

        public event EventHandler<TrackerEventArgs> OnAdRequest;
        public event EventHandler<TrackerEventArgs> OnAdLoaded;
        public event EventHandler<TrackerEventArgs> OnAdFailedToLoad;
        public event EventHandler<TrackerEventArgs> OnAdShown;
        public event EventHandler<TrackerEventArgs> OnAdCallShow;
        public event EventHandler<TrackerEventArgs> OnAdClicked;
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
        public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitClosed;
        public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitVideoStarted;
        public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitVideoCompleted;
        public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitRewarded;
        public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitRewardFailed;

        #endregion

        public void reportOnAdRequest(TrackerEventArgs args) {
            if (OnAdRequest != null) {
                OnAdRequest(this, args);
            }
        }

        public void reportOnAdLoaded(TrackerEventArgs args) {
            if (OnAdLoaded != null) {
                OnAdLoaded(this, args);
            }
        }

        public void reportOnAdFailedToLoad(TrackerEventArgs args) {
            if (OnAdFailedToLoad != null) {
                OnAdFailedToLoad(this, args);
            }
        }

        public void reportOnAdShown(TrackerEventArgs args) {
            if (OnAdShown != null) {
                OnAdShown(this, args);
            }
        }

        public void reportOnAdCallShow(TrackerEventArgs args) {
            if (OnAdCallShow != null) {
                OnAdCallShow(this, args);
            }
        }

        public void reportOnAdClicked(TrackerEventArgs args) {
            if (OnAdClicked != null) {
                OnAdClicked(this, args);
            }
        }

        public void reportOnAdClosed(TrackerEventArgs args) {
            if (OnAdClosed != null) {
                OnAdClosed(this, args);
            }
        }

        public void reportOnVideoStarted(TrackerEventArgs args) {
            if (OnVideoStarted != null) {
                OnVideoStarted(this, args);
            }
        }

        public void reportOnVideoCompleted(TrackerEventArgs args) {
            if (OnVideoCompleted != null) {
                OnVideoCompleted(this, args);
            }
        }

        public void reportOnRewarded(TrackerEventArgs args) {
            if (OnRewarded != null) {
                OnRewarded(this, args);
            }
        }

         public void reportOnRewardFailed(TrackerEventArgs args) {
            if (OnRewardFailed != null) {
                OnRewardFailed(this, args);
            }
        }

        public void reportOnAdUnitRequest(TrackerAdUnitEventArgs args) {
            if (OnAdUnitRequest != null) {
                OnAdUnitRequest(this, args);
            }
        }

        public void reportOnAdUnitLoaded(TrackerAdUnitEventArgs args) {
            if (OnAdUnitLoaded != null) {
                OnAdUnitLoaded(this, args);
            }
        }

        public void reportOnAdUnitFailedToLoad(TrackerAdUnitEventArgs args) {
            if (OnAdUnitFailedToLoad != null) {
                OnAdUnitFailedToLoad(this, args);
            }
        }

        public void reportOnAdUnitShown(TrackerAdUnitEventArgs args) {
            if (OnAdUnitShown != null) {
                OnAdUnitShown(this, args);
            }
        }

        public void reportOnAdUnitCallShow(TrackerAdUnitEventArgs args) {
            if (OnAdUnitCallShow != null) {
                OnAdUnitCallShow(this, args);
            }
        }

         public void reportOnAdUnitClicked(TrackerAdUnitEventArgs args) {
            if (OnAdUnitClicked != null) {
                OnAdUnitClicked(this, args);
            }
        }

        public void reportOnAdUnitClosed(TrackerAdUnitEventArgs args) {
            if (OnAdUnitClosed != null) {
                OnAdUnitClosed(this, args);
            }
        }

        public void reportOnAdUnitVideoStarted(TrackerAdUnitEventArgs args) {
            if (OnAdUnitVideoStarted != null) {
                OnAdUnitVideoStarted(this, args);
            }
        }

        public void reportOnAdUnitVideoCompleted(TrackerAdUnitEventArgs args) {
            if (OnAdUnitVideoCompleted != null) {
                OnAdUnitVideoCompleted(this, args);
            }
        }

        public void reportOnAdUnitRewarded(TrackerAdUnitEventArgs args) {
            if (OnAdUnitRewarded != null) {
                OnAdUnitRewarded(this, args);
            }
        }

        public void reportOnAdUnitRewardFailed(TrackerAdUnitEventArgs args) {
            if (OnAdUnitRewardFailed != null) {
                OnAdUnitRewardFailed(this, args);
            }
        }
    }        
}
