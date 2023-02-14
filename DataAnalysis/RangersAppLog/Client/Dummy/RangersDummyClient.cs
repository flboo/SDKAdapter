using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qarth
{
    public class RangersDummyClient : IRangersClient
    {
        public void InitConf(string appId, string channel)
        {
        }

        public void SetUserId(string uuid)
        {
        }

        public void SendEvent(string label, Dictionary<string, object> dictParams = null)
        {
        }

        public int GetABIntValue(string key, int defaultVal = 0)
        {
            return defaultVal;
        }
        public string GetABStringValue(string key, string defaultVal = "")
        {
            return defaultVal;
        }
        public bool GetABBoolValue(string key, bool defaultVal = false)
        {
            return defaultVal;
        }

        public string GetABVersion()
        {
            return null;
        }
    }
}
