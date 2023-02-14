using System;

namespace TaurusXAdSdk.Api
{
    // Event that occurs when an LineItem fails to load.
    public class TrackerEventArgs : EventArgs
    {
        public TrackerInfo TrackerInfo { get; set; }
    }
}
