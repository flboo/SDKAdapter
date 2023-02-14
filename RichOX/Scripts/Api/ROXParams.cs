using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXBase.Api
{
    public class ROXParams
    {
        /// <summary>
        /// 当前应用AppId，必传
        /// <summary>
        public string AppId {get; set;}

        /// <summary>
        /// 当前设备唯一Id，必传
        /// <summary>
        public string DeviceId{get; set;}

        /// <summary>
        /// 当前平台Id，非必传，若使用请与营运沟通
        /// <summary>
        public string PlatformId{get; set;}

        /// <summary>
        /// 原Fission平台对应的秘钥
        /// <summary>
        public string AppKey {get; set;}

        /// <summary>
        /// 原Fission平台对应的域名
        /// <summary>
        public string Url{get; set;}

        /// <summary>
        /// 当前发行渠道，请传入非空值，非必传
        /// <summary>
        public string Channel{get; set;}

        /// <summary>
        /// 额外信息，非必传，若使用请与营运沟通
        /// <summary>
        public string ExtendInfo {get; set;}

        public string ToString() 
        {
            string result = " {" 
            + " AppId = " + AppId + " ," 
            + " DeviceId = " + DeviceId + " ," 
            + " PlatformId = " + PlatformId + " ," 
            + " AppKey = " + AppKey + " ," 
            + " Url = " + Url + " ," 
            + " Channel = " + Channel + " ," 
            + " ExtendInfo = " + ExtendInfo + " " 
            + "}"; 

            return result;
        }   
    }

}