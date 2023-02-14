using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace Qarth
{
    [Serializable]
    public class DataAnalysisConfig
    {
        public bool isEnable = true;

        public AppsflyerConfig appsflyerConfig;

        public GASdkConfig gasdkConfig;

        public RangersConfig rangersConfig;
        public TalkingDataConfig tdConfig;

        public EmbedSDKConfig embedsdkConfig;

        public SecMetaConfig secMetaConfig;
        public GDTActionConfig GDTActionConfig;

    }

    // [Serializable]
    // public class DataeyeConfig : SDKAdapterConfig
    // {
    //     public string appID;

    //     public override string adapterClassName
    //     {
    //         get
    //         {
    //             return "Qarth.DataeyeAdapter";
    //         }
    //     }
    // }

    // [Serializable]
    // public class GameAnalysisConfig : SDKAdapterConfig
    // {
    //     public override string adapterClassName
    //     {
    //         get
    //         {
    //             return "Qarth.GameAnalysisAdapter";
    //         }
    //     }
    // }

    [Serializable]
    public class SecMetaConfig : SDKAdapterConfig
    {
        public string secMetaLiscense;
    }


    [Serializable]
    public class AppsflyerConfig : SDKAdapterConfig
    {
        public string appKey;

        public override string adapterClassName
        {
            get
            {
                return "Qarth.AppsflyerDataAdapter";
            }
        }
    }

    [Serializable]
    public class UmengConfig : SDKAdapterConfig
    {
        public string iosAppKey;
        public string androidAppKey;

        public string appChannelId;

        public override string adapterClassName
        {
            get
            {
                return "Qarth.UmengAdapter";
            }
        }
    }

    [Serializable]
    public class GASdkConfig : SDKAdapterConfig
    {
        public bool closeCtrlList;

        public override string adapterClassName
        {
            get
            {
                return "Qarth.GASdkAdapter";
            }
        }
    }


    [Serializable]
    public class TalkingDataConfig : SDKAdapterConfig
    {
        public string appId;

        public override string adapterClassName
        {
            get
            {
                return "Qarth.TalkingDataAdapter";
            }
        }
    }

    // [Serializable]
    // public class FacebookDataConfig : SDKAdapterConfig
    // {
    //     public override string adapterClassName
    //     {
    //         get
    //         {
    //             return "Qarth.FacebookDataAdapter";
    //         }
    //     }
    // }

    [Serializable]
    public class EmbedSDKConfig : SDKAdapterConfig
    {
        public bool enableAF = true;
        public string afAppKey = "DdWbxT9VRELdEsZiAcnGea";

        public override string adapterClassName
        {
            get
            {
                return "Qarth.EmbedSDKAdapter";
            }
        }
    }

    [Serializable]
    public class GDTActionConfig : SDKAdapterConfig
    {
        public string userActionSetId;
        public string appSecretKey;
        public string appId;
        public GDTActionConfig()
        {
            isEnable = false;
        }
    }

    [Serializable]
    public class JPushConfig : SDKAdapterConfig
    {
        public string JPushAppKey = "";
    }
    [Serializable]
    public class RangersConfig : SDKAdapterConfig
    {
        public string appId;
        public string appName;
        public string channel;

        public override string adapterClassName
        {
            get
            {
                return "Qarth.RangersAdapter";
            }
        }
    }

}
