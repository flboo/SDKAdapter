using UnityEngine;
using System.Collections;
using TaurusXAdSdk.Common;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Platforms.Android
{
    public class AdUnitClient : IAdUnitClient
    {
        private AndroidJavaObject mAdUnit;

        public AdUnitClient(AndroidJavaObject adUnit)
        {
            mAdUnit = adUnit;
        }

        #region IAdUnitClient

        public string GetName()
        {
            return mAdUnit.Call<string>("getName");
        }

        public string GetId()
        {
            return mAdUnit.Call<string>("getId");
        }

        public AdType GetAdType()
        {
            AndroidJavaObject adType = mAdUnit.Call<AndroidJavaObject>("getAdType");
            return (AdType)adType.Call<int>("getType");
        }

        public LoadMode GetLoadMode()
        {
            AndroidJavaObject loadMode = mAdUnit.Call<AndroidJavaObject>("getLoadMode");
            return new LoadMode(new LoadModeClient(loadMode));
        }

        public BannerAdSize GetBannerAdSize()
        {
            AndroidJavaObject adSize = mAdUnit.Call<AndroidJavaObject>("getBannerAdSize");
            return Utils.FromJavaBannerAdSize(adSize);
        }

        // ms
        public int GetBannerRefreshInterval()
        {
            return mAdUnit.Call<int>("getBannerRefreshInterval");
        }

        public RewardItem GetRewardItem()
        {
            return new RewardItem(new RewardItemClient(mAdUnit.Call<AndroidJavaObject>("getRewardItem")));
        }

        public Segment GetSegment()
        {
            return new Segment(new SegmentClient(mAdUnit.Call<AndroidJavaObject>("getSegment")));
        }

        public ArrayList GetLineItemList()
        {
            ArrayList lineItemList = new ArrayList();

            AndroidJavaObject list = mAdUnit.Call<AndroidJavaObject>("getLineItemList");
            int size = list.Call<int>("size");
            for(int i = 0; i < size; i ++)
            {
                AndroidJavaObject lineItem = list.Call<AndroidJavaObject>("get", i);
                lineItemList.Add(new LineItem(new LineItemClient(lineItem)));
            }

            return lineItemList;
        }

        #endregion
    }
}
