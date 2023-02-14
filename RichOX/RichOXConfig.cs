using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Qarth
{
    [Serializable]
    public class RichOXConfig
    {
        public bool isEnable;
        public bool debug;
        public int strategyId;
        public string platformId;
        public bool usePiggyBank;

        [Tooltip("如果是Fission升级到Richox, 且需要兼顾老用户，需勾选")]
        public bool customDeviceID;
        public ROXH5Config roxH5Config;
        public ROXShareConfig roxShareConfig;
        public ROXSectConfig roxSectConfig;
        public ROXChatConfig roxChatConfig;
    }
    [Serializable]
    public class ROXH5Config
    {
        public bool enable;
    }
    [Serializable]
    public class ROXShareConfig
    {
        public bool enable;
        public string shareUrl;
    }
    [Serializable]
    public class ROXSectConfig
    {
        public bool enable;
    }
    [Serializable]
    public class ROXChatConfig
    {
        public bool enable;
        /// <summary>
        /// sdk端向服务器自动拉取消息的时间间隔秒数
        /// </summary>
        public int interval = 10;
    }
}