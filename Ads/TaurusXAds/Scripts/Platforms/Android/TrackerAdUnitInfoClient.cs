using UnityEngine;
using TaurusXAdSdk.Common;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Platforms.Android
{
    public class TrackerAdUnitInfoClient : ITrackerAdUnitInfoClient
    {
        private AndroidJavaObject mAdUnitInfo;

        public TrackerAdUnitInfoClient(AndroidJavaObject adUnitInfo)
        {
            mAdUnitInfo = adUnitInfo;
        }

        #region ITrackerAdUnitInfoClient

        public AdUnit GetAdUnit()
        {
            AndroidJavaObject adUnit = mAdUnitInfo.Call<AndroidJavaObject>("getAdUnit");
            return new AdUnit(new AdUnitClient(adUnit));
        }

        public AdContentInfo GetAdContentInfo()
        {
            AndroidJavaObject contentInfo = mAdUnitInfo.Call<AndroidJavaObject>("getAdContentInfo");
            return new AdContentInfo(new AdContentInfoClient(contentInfo));
        }

        #endregion
    }
}
