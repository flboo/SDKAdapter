using System.Runtime.InteropServices;
using System;
using ROXBase.Api;
using ROXBase.Common;
using AOT;
using UnityEngine;


namespace ROXBase.Platforms.iOS {
    public class RichOXClient : IRichOXClient {

        static RichOXClient sInstance = new RichOXClient();

        public static RichOXClient Instance {
            get {
                return sInstance;
            }
        }

        internal delegate void RichOXBaseInitSuccessCallback(IntPtr manager);
        internal delegate void RichOXBaseAPPEventValueCallback(IntPtr manager, string eventName, string value);
        internal delegate void RichOXBaseEventCallback(IntPtr manager, string eventName, string value, string param);
        internal delegate void RichOXUnityFailureCallback(int code, string message);

  
        private ROXInterface<bool> initSuccess;
        private ROXInterface<string> queryAppEventCallback;
        private IntPtr managerPtr;


        public void SetDeviceId(string deviceId) 
        {
           Externs.RichOXBaseSetDeviceId(deviceId);
        }
       
        public void SetPlatformId(string platformId)
        {
            Externs.RichOXBaseSetFissionPlatform(platformId);
        }
       
        public void SetHostUrl(string hostUrl)
        {
           Externs.RichOXBaseSetFissionHost(hostUrl);
        }
       
        public void SetAppKey(string appKey)
        {
            Externs.RichOXBaseSetFissionKey(appKey);
        }
       
        public void SetChannel(string channel)
        {
            
        }

        public void SetExtendInfo(string extendInfo)
        {
            Externs.RichOXBaseSetExtendInfo(extendInfo);
        }

        public void SetOversea(bool oversea) 
        {
            if (oversea) {
                Externs.RichOXBaseSetOverSea();
            }
        }
       
        public void SetVersionCode(int code)
        {
            Externs.RichOXBaseSetAppVerCode(code);
        }

        public void Init(string appId, ROXInterface<bool> callback)
        {   
            managerPtr = Externs.RichOXBaseCreateManager((IntPtr)GCHandle.Alloc(this));
            initSuccess = callback; 
            Externs.RichOXBaseInit(managerPtr, appId, initSuccessCallback);
            Externs.RichOXBaseSetEventCallback(managerPtr, onEventCallback);    
        }     

        // public void Init(string appId, RichOXBaseInitSuccessCallback callback) {
        //     initSuccess = callback;
        //     IntPtr manager = Externs.RichOXBaseCreateManager((IntPtr)GCHandle.Alloc(this));

        //     Externs.RichOXBaseInit(manager, appId, initSuccessCallback);
        // }

        public void Init(ROXParams richOXParams) {
            IntPtr manager = Externs.RichOXBaseCreateManager((IntPtr)GCHandle.Alloc(this));
            string appId = richOXParams.AppId;
            string deviceId = richOXParams.DeviceId;
            string appKey = richOXParams.AppKey;
            string hostUrl = richOXParams.Url;
            string platformId = richOXParams.PlatformId;
            string extendInfo = richOXParams.ExtendInfo;

            if (!string.IsNullOrEmpty(deviceId))
            {
                Externs.RichOXBaseSetDeviceId(deviceId);
            }
            if (!string.IsNullOrEmpty(hostUrl))
            {
                Externs.RichOXBaseSetFissionHost(hostUrl);
            }
            if (!string.IsNullOrEmpty(appKey)) 
            {
                Externs.RichOXBaseSetFissionKey(appKey);
            }
            if (!string.IsNullOrEmpty(extendInfo)) 
            {
                Externs.RichOXBaseSetExtendInfo(extendInfo);
            }

            Externs.RichOXBaseInit(managerPtr, appId, null);
            Externs.RichOXBaseSetEventCallback(managerPtr, onEventCallback); 
        }

        public void SetTestMode(bool testMode) {
           Externs.RichOXBaseSetTestMode(testMode);
        }

        public string GetAppId()
        {
           return Externs.RichOXBaseGetAppId();
        }

        public string GetDeviceId() {
           return Externs.RichOXBaseGetDeviceId();
        }

        public string GetUserId() {
           return Externs.RichOXBaseGetUserId();
        }

        public void SetUserId(string userId) {
           Externs.RichOXBaseSetUserId(userId);
        }

        public string GenDefaultDeviceId() {
           return Externs.RichOXBaseGetDeviceId();
        }

        public void ReportAppEvent(string eventName) {
           Externs.RichOXReportAppEvent(eventName, null);
        }

        public void ReportAppEvent(string eventName, string eventValue) {
           Externs.RichOXReportAppEvent(eventName, eventValue);
        }

       public void QueryEventValue(string eventName, ROXInterface<string> callback) {
           queryAppEventCallback = callback;

           Externs.RichOXGetAPPEventValue(managerPtr, eventName, OnAppEventValueSuccessCallback);
       }

       public string GetPlatformId()
        {
            return Externs.RichOXBaseGetFissionPlatform();
        }

        public string GetChannel()
        {
            return "";
        }


        public string GetFissionHostUrl()
        {
            return Externs.RichOXBaseGetFissionHost();
        }

        public string GetFissionKey()
        {
            return Externs.RichOXBaseGetFissionKey();
        }

        public string GetWDExtendInfo()
        {
            return Externs.RichOXBaseGetExtendInfo();
        }


        public event EventHandler<RichOXEventArgs> OnEvent;

       #region init callback methods

        [MonoPInvokeCallback(typeof(RichOXBaseInitSuccessCallback))]
        private static void initSuccessCallback(IntPtr managerClient)
        {
            //RichOXClient client = IntPtrToRichOXClient(managerClient);
            if(Instance.initSuccess != null) {
                Debug.Log("Success");
                Instance.initSuccess.OnSuccess(true);
            }
        }

        #endregion

        [MonoPInvokeCallback(typeof(RichOXBaseEventCallback))]
        private static void onEventCallback(IntPtr manager, string eventName, string value, string param)
        {
            if( Instance.OnEvent != null) {
                RichOXEventArgs args = new RichOXEventArgs()
                {
                    RichOXEvent = new RichOXEvent(new RichOXEventClient(eventName, value, param))
                };
                Instance.OnEvent(Instance, args);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXBaseAPPEventValueCallback))]
        private static void OnAppEventValueSuccessCallback(IntPtr manager, string eventName, string value)
        {
            if( Instance.queryAppEventCallback != null) {
                Instance.queryAppEventCallback.OnSuccess(value);
            }
        }



        private static RichOXClient IntPtrToRichOXClient(IntPtr richOXClient)
        {
            GCHandle handle = (GCHandle)richOXClient;
            return handle.Target as RichOXClient;
        }
    }
}