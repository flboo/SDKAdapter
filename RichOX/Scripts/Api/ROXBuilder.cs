using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXBase.Api
{
    public abstract class ROXBuilder
    {
        public abstract void SetAppId(string appId);
        public abstract void SetDeviceId(string deviceId);
        public abstract void SetPlatformId(string PlatformId);
        public abstract void SetAppKey(string appKey);
        public abstract void SetUrl(string url);
        public abstract void SetChannel(string channel);
        public abstract void SetExtendInfo(string extendInfo);

        public abstract ROXParams GetROXParams();
    }

}


