using System;
using System.Collections.Generic;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Api
{
    public class SecondaryLineItem
    {
        readonly ISecondaryLineItemClient mClient;

        public SecondaryLineItem(ISecondaryLineItemClient client)
        {
            mClient = client;
        }

        public Network GetNetwork()
        {
            return mClient.GetNetwork();
        }

        public string GetAdUnitId()
        {
            return mClient.GetAdUnitId();
        }

        public float GetEcpm()
        {
            return mClient.GetEcpm();
        }

        public override string ToString() {
            return "Network is: " + GetNetwork()
                + ", AdUnitId is: " + GetAdUnitId()
                + ", eCPM is: " + GetEcpm();
        }
    }
}