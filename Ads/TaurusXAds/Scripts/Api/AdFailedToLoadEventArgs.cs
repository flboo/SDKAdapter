using System;

namespace TaurusXAdSdk.Api
{
    // Event that occurs when an ad fails to load.
    public class AdFailedToLoadEventArgs : EventArgs
    {
        public AdError AdError { get; set; }
    }
}
