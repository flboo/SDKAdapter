using UnityEngine;
using TaurusXAdSdk.Common;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Platforms.Android
{
    public class TrackerInfoClient : ITrackerInfoClient
    {
        private AndroidJavaObject mTrackerInfo;

        public TrackerInfoClient(AndroidJavaObject trackerInfo)
        {
            mTrackerInfo = trackerInfo;
        }

        #region ITrackerInfoClient

        public LineItem GetLineItem()
        {
            AndroidJavaObject lineItem = mTrackerInfo.Call<AndroidJavaObject>("getLineItem");
            return new LineItem(new LineItemClient(lineItem));
        }

        public AdContentInfo GetAdContentInfo()
        {
            AndroidJavaObject contentInfo = mTrackerInfo.Call<AndroidJavaObject>("getAdContentInfo");
            return new AdContentInfo(new AdContentInfoClient(contentInfo));
        }

        #endregion
    }
}
