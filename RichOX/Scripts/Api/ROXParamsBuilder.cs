using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXBase.Api
{
    public class ROXParamsBuilder : ROXBuilder 
    {
        private ROXParams mRoxParams;
        public ROXParams CreateROXParams 
        {
            get 
            {
                if (mRoxParams == null) {
                    mRoxParams = new ROXParams();
                }
                return mRoxParams;

            }
        }

        public override void SetAppId(string appId) {
            CreateROXParams.AppId = appId;
        }

        public override void SetDeviceId(string deviceId) {
            CreateROXParams.DeviceId = deviceId;
        }

        public override void SetPlatformId(string PlatformId) {
            CreateROXParams.PlatformId = PlatformId;
        }

        public override void SetAppKey(string appKey) {
            CreateROXParams.AppKey = appKey;
        }

        public override void SetUrl(string url) {
            CreateROXParams.Url = url;
        }

        public override void SetChannel(string channel) {
            CreateROXParams.Channel = channel;
        }

        public override void SetExtendInfo(string extendInfo) {
            CreateROXParams.ExtendInfo = extendInfo;
        }

        public override ROXParams GetROXParams() {
            return CreateROXParams;
        } 
    }
}