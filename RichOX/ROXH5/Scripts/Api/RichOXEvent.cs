using System;
using System.Collections.Generic;
using RichOX.Common;

namespace RichOX.Api
{
    public class RichOXEvent
    {
        readonly IRichOXEventClient mClient;

        public RichOXEvent(IRichOXEventClient client)
        {
            mClient = client;
        }

        public string GetName()
        {
            return mClient.GetName();
        }

        public string GetValue() {
            return mClient.GetValue();
        }

        public Dictionary<string, string> GetMapValue() {
            return mClient.GetMapValue();
        }
    }
}
