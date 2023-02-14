using System;

namespace TaurusXAdSdk.Api
{
    // Event that occurs when an AdUnit fails to load.
    public class TrackerAdUnitEventArgs : EventArgs
    {
        public TrackerAdUnitInfo TrackerAdUnitInfo { get; set; }
    }
}
