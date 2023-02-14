using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qarth
{
    public interface IRangersClient
    {

        void InitConf(string appId, string channel);

        void SetUserId(string uuid);

        void SendEvent(string label, Dictionary<string, object> dictParams = null);

        int GetABIntValue(string key, int defaultVal = 0);

        string GetABStringValue(string key, string defaultVal = "");

        bool GetABBoolValue(string key, bool defaultVal = false);

        string GetABVersion();
    }
}
