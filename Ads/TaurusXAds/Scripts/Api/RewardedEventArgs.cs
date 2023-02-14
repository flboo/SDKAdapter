using System;

namespace TaurusXAdSdk.Api
{
    // Event that occurs when an rewarded.
    public class RewardedEventArgs : AdEventArgs
    {
        public RewardItem RewardItem { get; set; }
    }
}