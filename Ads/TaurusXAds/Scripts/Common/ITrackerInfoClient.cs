using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public interface ITrackerInfoClient
    {
        LineItem GetLineItem();

        AdContentInfo GetAdContentInfo();
    }
}