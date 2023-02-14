using System.Collections.Generic;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public interface ISecondaryLineItemClient
    {
        Network GetNetwork();
        float GetEcpm();
        string GetAdUnitId();
    }
}