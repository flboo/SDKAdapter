using UnityEngine;
using System.Collections.Generic;
using TaurusXAdSdk.Api;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class LineItemClient : ILineItemClient
    {
        private AndroidJavaObject mLineItem;

        public LineItemClient(AndroidJavaObject lineItem)
        {
            mLineItem = lineItem;
        }

        #region ILineItemClient

        public string GetMediationVersion()
        {
            return mLineItem.Call<string>("getMediationVersion");
        }

        public string GetName()
        {
            return mLineItem.Call<string>("getName");
        }

        public AdType GetAdType()
        {
            AndroidJavaObject adType = mLineItem.Call<AndroidJavaObject>("getAdType");
            return (AdType)adType.Call<int>("getType");
        }

        public Api.Network GetNetwork()
        {
            AndroidJavaObject network = mLineItem.Call<AndroidJavaObject>("getNetwork");
            int networkId = network.Call<int>("getNetworkId");
            return (Api.Network)networkId;
        }

        public int GetPriority()
        {
            return mLineItem.Call<int>("getPriority");
        }

        public float GetEcpm()
        {
            return mLineItem.Call<float>("getEcpm");
        }

        // ms
        public int GetRequestTimeOut()
        {
            return mLineItem.Call<int>("getRequestTimeOut");
        }

        public bool IsHeaderBidding()
        {
            return mLineItem.Call<bool>("isHeaderBidding");
        }

        // ms
        public int GetHeaderBiddingTimeOut()
        {
            return mLineItem.Call<int>("getHeaderBiddingTimeOut");
        }

        public Dictionary<string, string> GetServerExtras()
        {
            return Utils.ToCSharpDictionary(mLineItem.Call<AndroidJavaObject>("getServerExtras"));
        }

        public string GetNetworkAdUnitId()
        {
            return mLineItem.Call<string>("getNetworkAdUnitId");
        }

        // Belonged AdUnit
        public AdUnit GetAdUnit()
        {
            AndroidJavaObject adUnit = mLineItem.Call<AndroidJavaObject>("getAdUnit");
            return new AdUnit(new AdUnitClient(adUnit));
        }

        public SecondaryLineItem GetSecondaryLineItem() {
            AndroidJavaObject secondaryLineItem = mLineItem.Call<AndroidJavaObject>("getSecondaryLineItem");
            return new SecondaryLineItem(new SecondaryLineItemClient(secondaryLineItem));
        }

        public string GetTId() {
            return mLineItem.Call<string>("getTId");
        }

        #endregion
    }
}
