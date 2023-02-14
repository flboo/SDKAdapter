using System;
using UnityEngine;

namespace Qarth
{
    [Serializable]
    public class TGCenterConfig : SDKAdapterConfig
    {
        public TGCenterConfig()
        {
            isEnable = false;
        }

        public string appIDAndroid;
        public string appIDIos;
        public string AppleAppID;
        public WechatConfig wechatConfig;
        public UdeskConfig udeskConfig;
    }


    [Serializable]
    public class UdeskConfig
    {
        public bool isEnable = false;
        public string domainIOS;
        public string appidIOS;
        public string appkeyIOS;
    }


    [Serializable]
    public class WechatConfig
    {
        public bool isEnable = false;
        public string wechatAppId;
        public string wechatSecret;
    }

}
