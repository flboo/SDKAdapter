using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public interface ITrackerAdUnitInfoClient
    {
        AdUnit GetAdUnit();

        AdContentInfo GetAdContentInfo();
    }
}
