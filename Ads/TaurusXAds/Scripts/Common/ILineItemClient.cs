using System.Collections.Generic;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public interface ILineItemClient
    {
        string GetMediationVersion();

        string GetName();

        AdType GetAdType();

        Network GetNetwork();

        int GetPriority();

        float GetEcpm();

        // ms
        int GetRequestTimeOut();

        bool IsHeaderBidding();

        // ms
        int GetHeaderBiddingTimeOut();

        Dictionary<string, string> GetServerExtras();
        string GetNetworkAdUnitId();

        // Belonged AdUnit
        AdUnit GetAdUnit();
        
        SecondaryLineItem GetSecondaryLineItem();
        string GetTId();
    }
}