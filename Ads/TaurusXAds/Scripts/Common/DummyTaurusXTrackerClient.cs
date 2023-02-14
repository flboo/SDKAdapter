using System;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public class DummyTaurusXTrackerClient : ITaurusXTrackerClient
    {
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
    }
}
