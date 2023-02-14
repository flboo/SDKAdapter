using System;
using System.Collections.Generic;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Api
{
    public class LineItem
    {
        readonly ILineItemClient mClient;

        public LineItem(ILineItemClient client)
        {
            mClient = client;
        }

        public string GetMediationVersion()
        {
            return mClient.GetMediationVersion();
        }

        public string GetName()
        {
            return mClient.GetName();
        }

        public AdType GetAdType()
        {
            return mClient.GetAdType();
        }

        public Network GetNetwork()
        {
            return mClient.GetNetwork();
        }

        public int GetPriority()
        {
            return mClient.GetPriority();
        }

        public float GetEcpm()
        {
            return mClient.GetEcpm();
        }

        // ms
        public int GetRequestTimeOut()
        {
            return mClient.GetRequestTimeOut();
        }

        public bool IsHeaderBidding()
        {
            return mClient.IsHeaderBidding();
        }

        // ms
        public int GetHeaderBiddingTimeOut()
        {
            return mClient.GetHeaderBiddingTimeOut();
        }

        public Dictionary<string, string> GetServerExtras()
        {
            return mClient.GetServerExtras();
        }

        public string GetServerExtrasDesc()
        {
            string desc = "[";

            Dictionary<string, string> extras = GetServerExtras();
            if(extras != null) {
                foreach (KeyValuePair<string, string> item in extras) {
                    desc = desc + item.Key + ": " +item.Value + "; ";
                }
            }
            desc += "]";

            return desc;
        }

        public string GetNetworkAdUnitId()
        {
            return mClient.GetNetworkAdUnitId();
        }

        // Belonged AdUnit
        public AdUnit GetAdUnit() {
            return mClient.GetAdUnit();
        }

        // 获取二级 LineItem，仅对于 Mobrain、Max、AdMob 等聚合平台返回，表示聚合平台中的具体 LineItem
        public SecondaryLineItem GetSecondaryLineItem() {
            return mClient.GetSecondaryLineItem();
        }

        public string GetTId() {
            return mClient.GetTId();
        }

        public override string ToString() {
            AdUnit adUnit = GetAdUnit();
            return "Name is: " + GetName()
                + ", Network is: " + GetNetwork()
                + ", Priority is: " + GetPriority()
                + ", eCPM is: " + GetEcpm()
                + ", BannerAdSize is: " + adUnit.GetBannerAdSize()
                + ", BannerRefreshInterval is: " + adUnit.GetBannerRefreshInterval()
                + ", RequestTimeOut is: " + GetRequestTimeOut()
                + ", IsHeaderBidding is: " + IsHeaderBidding()
                + ", HeaderBiddingTimeOut is: " + GetHeaderBiddingTimeOut()
                + ", GetServerExtras is: " + GetServerExtrasDesc()
                + ", SecondaryLineItem is: [" + GetSecondaryLineItem() + "]";
        }

        [Obsolete("Please use GetAdUnit().GetBannerAdSize()")]
        public BannerAdSize GetBannerAdSize()
        {
            return GetAdUnit().GetBannerAdSize();
        }

        // ms
        [Obsolete("Please use GetAdUnit().GetBannerRefreshInterval())")]
        public int GetBannerRefreshInterval()
        {
            return GetAdUnit().GetBannerRefreshInterval();
        }
    }
}
