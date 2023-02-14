using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using ROXBase.Common;
using ROXBase.Platforms;

namespace ROXBase.Api
{ 
    public class RichOXBase 
    {
        private static RichOXBase mInstance = new RichOXBase();
        

        public static RichOXBase Instance
        {
            get
            {
                return mInstance;
            }
        }

        private IRichOXClient mClient; 

        public RichOXBase() {
            mClient = ClientFactory.RichOXClientInstance();
            mClient.OnEvent += (sender, args) =>
            {
                if (OnEvent != null)
                {
                    OnEvent(this, args);
                }
            };
        }

        /// <summary>
        /// 当前设备唯一Id，必传
        /// <summary>
        public void SetDeviceId(string deviceId) 
        {
            mClient.SetDeviceId(deviceId);            
        }

        /// <summary>
        /// 当前平台Id，非必传，若使用请与营运沟通
        /// <summary>
        public void SetPlatformId(string platformId)
        {
            mClient.SetPlatformId(platformId);
        }

        /// <summary>
        /// 原Fission平台对应的域名，非必传
        /// 原fission平台url为https，且带"_"字符，需调用该接口，将 https 改为 http
        /// <summary>
        public void SetHostUrl(string hostUrl)
        {
            mClient.SetHostUrl(hostUrl);
        }

        /// <summary>
        /// 原Fission平台对应的秘钥，非必传
        /// 
        /// <summary>
        public void SetAppKey(string appKey)
        {
            mClient.SetAppKey(appKey);
        }
        

        /// <summary>
        /// 当前发行渠道，请传入非空值，非必传
        /// <summary>
        public void SetChannel(string channel) 
        {
            mClient.SetChannel(channel);
        }

        /// <summary>
        /// 额外信息，非必传，若使用请与营运沟通
        /// <summary>
        public void SetExtendInfo(string extendInfo)
        {
            mClient.SetExtendInfo(extendInfo);
        }

        /// <summary>
        /// 是否是海外版本， 仅供 iOS 使用
        /// <summary>
        public void SetOversea(bool oversea)
        {
            mClient.SetOversea(oversea);
        }

        /// <summary>
        /// 设置当前应用版本信息，仅供 iOS 使用
        /// 设置当前版本 code 信息
        /// <summary>
        public void SetVersionCode(int code)
        {
            mClient.SetVersionCode(code);
        }

        /// <summary>
        /// 初始化
        /// <summary> 
        public void Init(string appId, ROXInterface<bool> callback) 
        {
            mClient.Init(appId, callback);
        }

        /// <summary>
        /// 初始化
        /// <summary>  
        [Obsolete("后续请使用带有回调的初始化接口，保证初始流程正确执行")]
        public void Init(ROXParams richOxParams) 
        {
            mClient.Init(richOxParams);
        }

        public string GetAppId() 
        {
            return mClient.GetAppId();
        }

        public string GetDeviceId() 
        {
           return mClient.GetDeviceId();
        }

        public string GetUserId() 
        {
           return mClient.GetUserId();
        }

        public string GetPlatformId() {
            return mClient.GetPlatformId();
        }

        public string GetChannel() {
            return mClient.GetChannel();
        }

        public string GetFissionHostUrl() 
        {
            return mClient.GetFissionHostUrl();
        }

        public string GetFissionKey() 
        {
            return mClient.GetFissionKey();
        }

        public string GetWDExtendInfo() 
        {
            return mClient.GetWDExtendInfo();
        }

        /// <summary>
        /// 设置用户Id
        /// <summary>  
        public void SetUserId(string userId) 
        {
            mClient.SetUserId(userId);
        }

        

        /// <summary>
        /// 应用内事件上报，不带属性
        /// 事件需要后台配置，由运营确认上报需求
        /// <summary>  
        public void ReportAppEvent(string eventName) 
        {
            mClient.ReportAppEvent(eventName);
        }

        /// <summary>
        /// 应用内事件上报，带属性
        /// 事件需要后台配置，由运营确认上报需求
        /// <summary>  
        public void ReportAppEvent(string eventName, string eventValue)
        {
             mClient.ReportAppEvent(eventName, eventValue);
        }

        /// <summary>
        /// 事件查询接口
        /// 该接口对应 ReportAppEvent 上报事件
        /// <summary>  
        public void QueryEventValue(string eventName, ROXInterface<string> callback)
        {
             mClient.QueryEventValue(eventName, callback);
        }

        /// <summary>
        /// 生成当前应用唯一码
        /// <summary>  
        public string GenDefaultDeviceId() 
        {
            return mClient.GenDefaultDeviceId();
        }

        public void SetTestMode(bool testMode) {
            mClient.SetTestMode(testMode);
        }

        public event EventHandler<RichOXEventArgs> OnEvent;
    }
}
