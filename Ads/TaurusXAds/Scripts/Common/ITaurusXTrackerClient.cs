using System;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public interface ITaurusXTrackerClient
    {
        event EventHandler<TrackerEventArgs> OnAdRequest;
        event EventHandler<TrackerEventArgs> OnAdLoaded;
        event EventHandler<TrackerEventArgs> OnAdFailedToLoad;
        event EventHandler<TrackerEventArgs> OnAdShown;
        event EventHandler<TrackerEventArgs> OnAdCallShow;
        event EventHandler<TrackerEventArgs> OnAdClicked;
        event EventHandler<TrackerEventArgs> OnAdSkipped;
        event EventHandler<TrackerEventArgs> OnAdClosed;
        event EventHandler<TrackerEventArgs> OnVideoStarted;
        event EventHandler<TrackerEventArgs> OnVideoCompleted;
        event EventHandler<TrackerEventArgs> OnRewarded;
        event EventHandler<TrackerEventArgs> OnRewardFailed;

        event EventHandler<TrackerAdUnitEventArgs> OnAdUnitRequest;
        event EventHandler<TrackerAdUnitEventArgs> OnAdUnitLoaded;
        event EventHandler<TrackerAdUnitEventArgs> OnAdUnitFailedToLoad;
        event EventHandler<TrackerAdUnitEventArgs> OnAdUnitShown;
        event EventHandler<TrackerAdUnitEventArgs> OnAdUnitCallShow;
        event EventHandler<TrackerAdUnitEventArgs> OnAdUnitClicked;
        event EventHandler<TrackerAdUnitEventArgs> OnAdUnitSkipped;
        event EventHandler<TrackerAdUnitEventArgs> OnAdUnitClosed;
        event EventHandler<TrackerAdUnitEventArgs> OnAdUnitVideoStarted;
        event EventHandler<TrackerAdUnitEventArgs> OnAdUnitVideoCompleted;
        event EventHandler<TrackerAdUnitEventArgs> OnAdUnitRewarded;
        event EventHandler<TrackerAdUnitEventArgs> OnAdUnitRewardFailed;
    }
}
