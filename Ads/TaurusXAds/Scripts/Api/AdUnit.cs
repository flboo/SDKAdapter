using System.Collections;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Api
{
    public class AdUnit
    {
        readonly IAdUnitClient mClient;

        public AdUnit(IAdUnitClient client)
        {
            mClient = client;
        }

        public string GetName()
        {
            return mClient.GetName();
        }

        public string GetId()
        {
            return mClient.GetId();
        }

        public AdType GetAdType()
        {
            return mClient.GetAdType();
        }

        public LoadMode GetLoadMode()
        {
            return mClient.GetLoadMode();
        }

        public BannerAdSize GetBannerAdSize()
        {
            return mClient.GetBannerAdSize();
        }

        // ms
        public int GetBannerRefreshInterval()
        {
            return mClient.GetBannerRefreshInterval();
        }

        public RewardItem GetRewardItem()
        {
            return mClient.GetRewardItem();
        }

        public Segment GetSegment()
        {
            return mClient.GetSegment();
        }

        public ArrayList GetLineItemList()
        {
            return mClient.GetLineItemList();
        }

        public override string ToString() {
            string lineItemString = "";
            ArrayList lineItemList = GetLineItemList();
            foreach (LineItem lineItem in lineItemList)
            {
                lineItemString = lineItemString + "\n, lineItem is " + lineItem;
            }

            return "AdUnit Name: " + GetName()
                + ", Id: " + GetId()
                + ", AdType: " + GetAdType()
                + ", LoadMode: " + GetLoadMode()
                + ", BannerAdSize: " + GetBannerAdSize()
                + ", BannerRefreshInterval: " + GetBannerRefreshInterval() + "ms"
                + ", RewardItem: " + GetRewardItem()
                + ", Segment: " + GetSegment()
                + ", LineItemList: [" + lineItemString + "]";
        }
    }
}