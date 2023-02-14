using System;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Api
{
    public class TrackerAdUnitInfo
    {
        readonly ITrackerAdUnitInfoClient mClient;

        public TrackerAdUnitInfo(ITrackerAdUnitInfoClient client)
        {
            mClient = client;
        }

        public AdUnit GetAdUnit()
        {
            return mClient.GetAdUnit();
        }

        public AdContentInfo GetAdContentInfo()
        {
            return mClient.GetAdContentInfo();
        }

        [Obsolete("Please use GetAdUnit().GetId()")]
        public string GetAdUnitId()
        {
            return mClient.GetAdUnit().GetId();
        }

        [Obsolete("Please use GetAdUnit().GetName()")]
        public string GetAdUnitName()
        {
            return mClient.GetAdUnit().GetName();
        }

        [Obsolete("Please use GetAdUnit().GetAdType()")]
        public AdType GetAdType()
        {
            return mClient.GetAdUnit().GetAdType();
        }

        public override string ToString()
        {
            AdUnit adUnit = GetAdUnit();
            if(adUnit != null)
            {
                return "AdUnit is [" + adUnit + "]"
                    + ", AdContentInfo is [" + GetAdContentInfo() + "]";
            }
            return "AdUnit is null";
        }
    }
}