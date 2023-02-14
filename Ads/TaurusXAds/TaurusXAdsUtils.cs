using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TaurusXAdSdk.Api;

namespace Qarth
{
    public class TaurusXAdsUtils
    {
        public static CLConfig GenCLConfig(int cacheCount = 1)
        {
            var conf = new CLConfig();
            conf.SetCacheCount(cacheCount);
            return conf;
        }
    }
}