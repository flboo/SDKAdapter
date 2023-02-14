using System;
using TaurusXAdSdk.Common;
using TaurusXAdSdk.Platforms;

namespace TaurusXAdSdk.Api
{
    public class TaurusXTracker
    {
        static TaurusXTracker sInstance = new TaurusXTracker();

        public static TaurusXTracker Instance
        {
            get
            {
                return sInstance;
            }
        }

        private ITaurusXTrackerClient mClient;

        TaurusXTracker()
        {
            mClient = ClientFactory.TaurusXTrackerClient();
            ConfigureTaurusXTrackerEvents();
        }

        #region TrackerListener

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

        private void ConfigureTaurusXTrackerEvents()
        {
            mClient.OnAdRequest += (sender, args) =>
            {
                if (OnAdRequest != null)
                {
                    OnAdRequest(this, args);
                }
            };

            mClient.OnAdLoaded += (sender, args) =>
            {
                if (OnAdLoaded != null)
                {
                    OnAdLoaded(this, args);
                }
            };

            mClient.OnAdFailedToLoad += (sender, args) =>
            {
                if (OnAdFailedToLoad != null)
                {
                    OnAdFailedToLoad(this, args);
                }
            };

            mClient.OnAdCallShow += (sender, args) =>
            {
                if (OnAdCallShow != null)
                {
                    OnAdCallShow(this, args);
                }
            };

            mClient.OnAdShown += (sender, args) =>
            {
                if (OnAdShown != null)
                {
                    OnAdShown(this, args);
                }
            };

            mClient.OnAdClicked += (sender, args) =>
            {
                if (OnAdClicked != null)
                {
                    OnAdClicked(this, args);
                }
            };

            mClient.OnAdClosed += (sender, args) =>
            {
                if (OnAdClosed != null)
                {
                    OnAdClosed(this, args);
                }
            };

            mClient.OnVideoStarted += (sender, args) =>
            {
                if (OnVideoStarted != null)
                {
                    OnVideoStarted(this, args);
                }
            };

            mClient.OnVideoCompleted += (sender, args) =>
            {
                if (OnVideoCompleted != null)
                {
                    OnVideoCompleted(this, args);
                }
            };

            mClient.OnRewarded += (sender, args) =>
            {
                if (OnRewarded != null)
                {
                    OnRewarded(this, args);
                }
            };

            mClient.OnRewardFailed += (sender, args) =>
            {
                if (OnRewardFailed != null)
                {
                    OnRewardFailed(this, args);
                }
            };






            mClient.OnAdUnitRequest += (sender, args) =>
            {
                if (OnAdUnitRequest != null)
                {
                    OnAdUnitRequest(this, args);
                }
            };

            mClient.OnAdUnitLoaded += (sender, args) =>
            {
                if (OnAdUnitLoaded != null)
                {
                    OnAdUnitLoaded(this, args);
                }
            };

            mClient.OnAdUnitFailedToLoad += (sender, args) =>
            {
                if (OnAdUnitFailedToLoad != null)
                {
                    OnAdUnitFailedToLoad(this, args);
                }
            };

            mClient.OnAdUnitCallShow += (sender, args) =>
            {
                if (OnAdUnitCallShow != null)
                {
                    OnAdUnitCallShow(this, args);
                }
            };

            mClient.OnAdUnitShown += (sender, args) =>
            {
                if (OnAdUnitShown != null)
                {
                    OnAdUnitShown(this, args);
                }
            };

            mClient.OnAdUnitClicked += (sender, args) =>
            {
                if (OnAdUnitClicked != null)
                {
                    OnAdUnitClicked(this, args);
                }
            };

            mClient.OnAdUnitClosed += (sender, args) =>
            {
                if (OnAdUnitClosed != null)
                {
                    OnAdUnitClosed(this, args);
                }
            };

            mClient.OnAdUnitVideoStarted += (sender, args) =>
            {
                if (OnAdUnitVideoStarted != null)
                {
                    OnAdUnitVideoStarted(this, args);
                }
            };

            mClient.OnAdUnitVideoCompleted += (sender, args) =>
            {
                if (OnAdUnitVideoCompleted != null)
                {
                    OnAdUnitVideoCompleted(this, args);
                }
            };

            mClient.OnAdUnitRewarded += (sender, args) =>
            {
                if (OnAdUnitRewarded != null)
                {
                    OnAdUnitRewarded(this, args);
                }
            };

            mClient.OnAdUnitRewardFailed += (sender, args) =>
            {
                if (OnAdUnitRewardFailed != null)
                {
                    OnAdUnitRewardFailed(this, args);
                }
            };
        }
    }
}
