using System;
using TaurusXAdSdk.Api;
using Advertisers.Api;

namespace Advertisers.Common
{
    public interface IProbeManager
    {
        // event EventHandler<TrackerEventArgs> OnAdRequest;
        // event EventHandler<TrackerEventArgs> OnAdLoaded;
        // event EventHandler<TrackerEventArgs> OnAdFailedToLoad;
        // event EventHandler<TrackerEventArgs> OnAdCallShow;
        // event EventHandler<TrackerEventArgs> OnAdShown;
        // event EventHandler<TrackerEventArgs> OnAdClicked;
        // event EventHandler<TrackerEventArgs> OnAdClosed;
        // event EventHandler<TrackerEventArgs> OnVideoStarted;
        // event EventHandler<TrackerEventArgs> OnVideoCompleted;
        // event EventHandler<TrackerEventArgs> OnRewarded;
        // event EventHandler<TrackerEventArgs> OnRewardFailed;

        // event EventHandler<TrackerAdUnitEventArgs> OnAdUnitRequest;
        // event EventHandler<TrackerAdUnitEventArgs> OnAdUnitLoaded;
        // event EventHandler<TrackerAdUnitEventArgs> OnAdUnitFailedToLoad;
        // event EventHandler<TrackerAdUnitEventArgs> OnAdUnitCallShow;
        // event EventHandler<TrackerAdUnitEventArgs> OnAdUnitShown;
        // event EventHandler<TrackerAdUnitEventArgs> OnAdUnitClicked;
        // event EventHandler<TrackerAdUnitEventArgs> OnAdUnitClosed;
        // event EventHandler<TrackerAdUnitEventArgs> OnAdUnitVideoStarted;
        // event EventHandler<TrackerAdUnitEventArgs> OnAdUnitVideoCompleted;
        // event EventHandler<TrackerAdUnitEventArgs> OnAdUnitRewarded;
        // event EventHandler<TrackerAdUnitEventArgs> OnAdUnitRewardFailed;
        void init(); 
        bool getReportStatus();
        void setReportStatus(bool status);
        void registerTrackListener(TrackListener listener);
        void unRegisterTrackListener(TrackListener listener);
    }
}
