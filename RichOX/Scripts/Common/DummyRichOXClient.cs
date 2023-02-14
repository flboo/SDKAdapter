using ROXBase.Api;
using System;
using UnityEngine;

namespace ROXBase.Common
{
    public class DummyRichOXClient : IRichOXClient
    {
        private ROXParams mParams;
        private string mAppId;
        private string mDeviceId;
        private string mPlatformId;
        private string mHostUrl;
        private string mAppkey;
        private string mChannel;
        private string mExtendInfo;
        private string mUserId;

        #region IRichOXClient

        public void SetDeviceId(string deviceId) 
        {
            mDeviceId = deviceId;
        }
       
        public void SetPlatformId(string platformId)
        {
            mPlatformId = platformId;
        }
       
        public void SetHostUrl(string hostUrl)
        {
            mHostUrl = hostUrl;
        }
       
        public void SetAppKey(string appKey)
        {
            mAppkey = appKey;
        }
       
        public void SetChannel(string channel)
        {
            mChannel = channel;
        }

        public void SetExtendInfo(string extendInfo)
        {
            mExtendInfo = extendInfo;
        }

        public void SetOversea(bool oversea) 
        {
            // do nothing
        }
       
        public void SetVersionCode(int code)
        {
            // do nothing
        }

        public void Init(string appId, ROXInterface<bool> callback)
        {
            mAppId = appId;
        }

        public void Init(ROXParams richOxParams) 
        {
            Debug.Log("go here dummy init");
            mParams = richOxParams;
        }

        public string GetAppId() 
        {
            if (mParams != null) 
            {
                return mParams.AppId;
            }
            return mAppId;
            
        }

        public string GetDeviceId() 
        {
            if (mParams != null) 
            {
                return mParams.DeviceId;
            }
            return mDeviceId;
        }

        public string GetUserId() 
        {
            if (mUserId != null) 
            {
                return mUserId;
            }
            return "";
        }

        public string GetPlatformId() 
        {
            if (mParams != null) 
            {
                return mParams.PlatformId;
            }         
            return mPlatformId;  
        }

        public string GetChannel() {
            if (mParams != null) 
            {
                return mParams.Channel;
            }
            return mChannel;
        }

        public string GetFissionHostUrl() 
        {
            if (mParams != null) 
            {
                return mParams.Url;
            }
            return mHostUrl;
        }

        public string GetFissionKey() 
        {
            if (mParams != null)
            {
                return mParams.AppKey;
            }
            return mAppkey;
        }

        public string GetWDExtendInfo() 
        {
            if (mParams != null) 
            {
               return mParams.ExtendInfo; 
            }
            return mExtendInfo;
        }

        public void SetUserId(string userId) 
        {
            mUserId = userId;
        }

        public string GenDefaultDeviceId() 
        {
            return "123456_123456";
        }

        public void SetTestMode(bool testMode)
        {
            //
        }

        public void ReportAppEvent(string eventName) 
        {
            //
        }

        public void ReportAppEvent(string eventName, string eventValue)
        {
            //
        }

        public void QueryEventValue(string eventName, ROXInterface<string> callback) 
        {
            //
        }

        public event EventHandler<RichOXEventArgs> OnEvent;

        #endregion
    }
}