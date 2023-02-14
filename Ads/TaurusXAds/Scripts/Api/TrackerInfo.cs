using System;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Api
{
    public class TrackerInfo
    {
        readonly ITrackerInfoClient mClient;

        public TrackerInfo(ITrackerInfoClient client)
        {
            mClient = client;
        }

        public LineItem GetLineItem()
        {
            return mClient.GetLineItem();
        }

        public AdContentInfo GetAdContentInfo()
        {
            return mClient.GetAdContentInfo();
        }

        [Obsolete("Please use GetLineItem().GetAdUnit().GetId()")]
        public string GetAdUnitId()
        {
            return GetLineItem().GetAdUnit().GetId();
        }

        [Obsolete("Please use GetLineItem().GetAdUnit().GetName()")]
        public string GetAdUnitName()
        {
            return GetLineItem().GetAdUnit().GetName();
        }

        [Obsolete("Please use GetLineItem().GetAdType()")]
        public AdType GetAdType()
        {
            return GetLineItem().GetAdType();
        }

        [Obsolete("Please use GetLineItem().GetNetwork()")]
        public Network GetNetworkId()
        {
            return GetLineItem().GetNetwork();
        }

        [Obsolete("Please use GetLineItem().GetEcpm()")]
        public float GeteCPM()
        {
            return GetLineItem().GetEcpm();
        }

        [Obsolete("Please use GetLineItem().GetNetworkAdUnitId()")]
        public string GetNetworkAdUnitId()
        {
            return GetLineItem().GetNetworkAdUnitId();
        }

        public override string ToString() {
            LineItem lineItem = GetLineItem();
            AdUnit adUnit = lineItem.GetAdUnit();

            return "LineItem is [" + lineItem + "]"
                + ", AdContentInfo is [" + GetAdContentInfo() + "]";
        }
    }
}
