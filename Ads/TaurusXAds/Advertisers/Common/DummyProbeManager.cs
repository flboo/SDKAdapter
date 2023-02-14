using System;
using TaurusXAdSdk.Api;
using Advertisers.Api;

namespace Advertisers.Common
{
    public class DummyProbeManager : IProbeManager
    {
        // public event EventHandler<TrackerEventArgs> OnAdRequest;
        // public event EventHandler<TrackerEventArgs> OnAdLoaded;
        // public event EventHandler<TrackerEventArgs> OnAdFailedToLoad;
        // public event EventHandler<TrackerEventArgs> OnAdCallShow;
        // public event EventHandler<TrackerEventArgs> OnAdShown;
        // public event EventHandler<TrackerEventArgs> OnAdClicked;
        // public event EventHandler<TrackerEventArgs> OnAdClosed;
        // public event EventHandler<TrackerEventArgs> OnVideoStarted;
        // public event EventHandler<TrackerEventArgs> OnVideoCompleted;
        // public event EventHandler<TrackerEventArgs> OnRewarded;
        // public event EventHandler<TrackerEventArgs> OnRewardFailed;

        // public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitRequest;
        // public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitLoaded;
        // public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitFailedToLoad;
        // public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitCallShow;
        // public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitShown;
        // public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitClicked;
        // public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitClosed;
        // public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitVideoStarted;
        // public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitVideoCompleted;
        // public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitRewarded;
        // public event EventHandler<TrackerAdUnitEventArgs> OnAdUnitRewardFailed;

        public DummyProbeManager() {

        }


        public void init() {

        }
        public bool getReportStatus() {
            return true;
        }
        public void setReportStatus(bool status) {

        }
        public void registerTrackListener(TrackListener listener) {

        }
        public void unRegisterTrackListener(TrackListener listener) {

        }
    }
}
