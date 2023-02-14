using TaurusXAdSdk.Api;
using System.Collections;

namespace TaurusXAdSdk.Common
{
    public interface IAdUnitClient
    {
        string GetName();

        string GetId();

        AdType GetAdType();

        LoadMode GetLoadMode();

        BannerAdSize GetBannerAdSize();
        int GetBannerRefreshInterval(); // ms

        RewardItem GetRewardItem();

        Segment GetSegment();

        ArrayList GetLineItemList();
    }
}