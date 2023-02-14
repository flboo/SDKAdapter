using UnityEngine;
using System.Collections.Generic;
using TaurusXAdSdk.Api;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class SecondaryLineItemClient : ISecondaryLineItemClient
    {
        private AndroidJavaObject mSecondaryLineItem;

        public SecondaryLineItemClient(AndroidJavaObject secondaryLineItem)
        {
            mSecondaryLineItem = secondaryLineItem;
        }

        #region ISecondaryLineItemClient

        public Api.Network GetNetwork()
        {
            if(mSecondaryLineItem != null) {
                AndroidJavaObject network = mSecondaryLineItem.Call<AndroidJavaObject>("getNetwork");
                int networkId = network.Call<int>("getNetworkId");
                return (Api.Network)networkId;
            } else {
                return Api.Network.Unknown;
            }
        }

        public float GetEcpm()
        {
            return mSecondaryLineItem !=null 
                ? (float)mSecondaryLineItem.Call<double>("geteCPM")
                : 0;
        }

        public string GetAdUnitId()
        {
            return mSecondaryLineItem !=null 
                ? mSecondaryLineItem.Call<string>("getAdUnitId") 
                : "";
        }
        #endregion
    }
}
