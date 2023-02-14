using UnityEngine;
using System;
using ROXBase.Api;
using ROXBase.Common;


namespace ROXBase.Platforms.Android
{
    public class RichOXClient : AndroidJavaProxy, IRichOXClient
    {
        static RichOXClient sInstance = new RichOXClient();

        public static RichOXClient Instance {
            get {
                return sInstance;
            }
        }

        private AndroidJavaObject mUnityActivity;
        private AndroidJavaClass mRichOXClass;

        private EventListener mEventListener;

        public event EventHandler<RichOXEventArgs> OnEvent;

        private string mAppId;
        private string mDeviceId;
        private string mPlatformId;
        private string mHostUrl;
        private string mAppkey;
        private string mChannel;
        private string mExtendInfo;

        public RichOXClient() : base (ClassUtils.Object)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(ClassUtils.UnityActivityClassName);
            mUnityActivity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            mRichOXClass = new AndroidJavaClass(ClassUtils.RichOXClassName);
            mEventListener = new EventListener();
            mEventListener.OnEvent += (sender, args) =>
            {
                if (OnEvent != null)
                {
                    OnEvent(this, args);
                }
            };
            mRichOXClass.CallStatic("registerEventCallback", mEventListener);           
        }

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
            AndroidJavaObject builder = new AndroidJavaObject(ClassUtils.ROXInitBuilder);
            builder.Call<AndroidJavaObject>("setAppId", appId);
            builder.Call<AndroidJavaObject>("setDeviceId", mDeviceId);
            if (!string.IsNullOrEmpty(mHostUrl))
            {
                builder.Call<AndroidJavaObject>("setHostUrl", mHostUrl);
            }
            if (!string.IsNullOrEmpty(mAppkey))
            {
                builder.Call<AndroidJavaObject>("setAppKey", mAppkey);
            }
            if (!string.IsNullOrEmpty(mPlatformId))
            {
                builder.Call<AndroidJavaObject>("setPlatformId", mPlatformId);
            }
            if (!string.IsNullOrEmpty(mChannel)) 
            {
                builder.Call<AndroidJavaObject>("setChannel", mChannel);
            }
            if (!string.IsNullOrEmpty(mExtendInfo)) 
            {
                builder.Call<AndroidJavaObject>("setExtendInfo", mExtendInfo);
            }
            
            AndroidJavaObject commonBuilder = builder.Call<AndroidJavaObject>("build");
            AndroidInitCallback initCallback = new AndroidInitCallback();
            initCallback.OnSuccess += (sender, args) =>
            {
                Debug.Log("richox init success");
                if (callback != null)
                {
                    if (args != null) {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) => 
                        {
                            bool result = (bool) args.CommonResponse.GetResponse();
                            callback.OnSuccess(result);
                        };
                        mUnityActivity.Call("runOnUiThread", runnable);
                    }
                }
            };

            initCallback.OnFailed += (sender, args) =>
            {
                Debug.Log("richox init failed");
                if (callback != null)
                {
                    if (args != null) {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) => 
                        {
                           callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                        };
                        mUnityActivity.Call("runOnUiThread", runnable);
                    }
                }
            };
            mRichOXClass.CallStatic("init", mUnityActivity, commonBuilder, initCallback);
        }

        public void Init(ROXParams richOXParams) 
        {
            AndroidJavaObject builder = new AndroidJavaObject(ClassUtils.ROXInitBuilder);
            string appId = richOXParams.AppId;
            string deviceId = richOXParams.DeviceId;
            string appKey = richOXParams.AppKey;
            string hostUrl = richOXParams.Url;
            string platformId = richOXParams.PlatformId;
            string channel = richOXParams.Channel;
            string extendInfo = richOXParams.ExtendInfo;

            builder.Call<AndroidJavaObject>("setAppId", appId);
            builder.Call<AndroidJavaObject>("setDeviceId", deviceId);
            builder.Call<AndroidJavaObject>("setHostUrl", hostUrl);
            builder.Call<AndroidJavaObject>("setAppKey", appKey);
            if (!string.IsNullOrEmpty(platformId))
            {
                builder.Call<AndroidJavaObject>("setPlatformId", platformId);
            }
            if (!string.IsNullOrEmpty(channel)) 
            {
                builder.Call<AndroidJavaObject>("setChannel", channel);
            }
            if (!string.IsNullOrEmpty(extendInfo)) 
            {
                builder.Call<AndroidJavaObject>("setExtendInfo", extendInfo);
            }
            
            AndroidJavaObject commonBuilder = builder.Call<AndroidJavaObject>("build");

            AndroidInitCallback initCallback = new AndroidInitCallback();
            initCallback.OnSuccess += (sender, args) =>
            {
                Debug.Log("onsuccess");
            };

            initCallback.OnFailed += (sender, args) =>
            {
                Debug.Log("on failed");
            };

            mRichOXClass.CallStatic("init", mUnityActivity, commonBuilder, initCallback);
        }

        public string GetAppId() 
        {
            return mRichOXClass.CallStatic<string>("getAppId");
        }

        public string GetDeviceId() 
        {
           return mRichOXClass.CallStatic<string>("getDeviceId");
        }

        public string GetUserId() 
        {
           return mRichOXClass.CallStatic<string>("getUserId");
        }

        public string GetPlatformId() {
            return mRichOXClass.CallStatic<string>("getPlatformId");
        }

        public string GetChannel() {
            return mRichOXClass.CallStatic<string>("getChannel");
        }

        public string GetFissionHostUrl() 
        {
            return mRichOXClass.CallStatic<string>("getFissionHostUrl");
        }

        public string GetFissionKey() 
        {
            return mRichOXClass.CallStatic<string>("getFissionKey");
        }

        public string GetWDExtendInfo() 
        {
            return mRichOXClass.CallStatic<string>("getWDExtendInfo");
        }

        public void SetUserId(string userId) 
        {
            mRichOXClass.CallStatic("setUserId", userId);
        }

        public string GenDefaultDeviceId() 
        {
            return mRichOXClass.CallStatic<string>("genDefaultDeviceId", mUnityActivity);
        }

        public void SetTestMode(bool testMode) {
            mRichOXClass.CallStatic("setTestMode", testMode);
        }

        public void ReportAppEvent(string eventName) 
        {
            mRichOXClass.CallStatic("reportAppEvent", eventName);
        }

        public void ReportAppEvent(string eventName, string eventValue)
        {
            mRichOXClass.CallStatic("reportAppEvent", eventName, eventValue);
        }

        public void QueryEventValue(string eventName, ROXInterface<string> callback) 
        {
            AndroidStringCallback androidCallback = new AndroidStringCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) => 
                        {
                            string eventValue = (string) args.CommonResponse.GetResponse();
                            callback.OnSuccess(eventValue);
                        };
                        mUnityActivity.Call("runOnUiThread", runnable);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) => 
                        {
                           callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                        };
                        mUnityActivity.Call("runOnUiThread", runnable);
                    }
                }
            };
            mRichOXClass.CallStatic("queryEventValue", eventName, androidCallback);
        }

        #endregion

        #region EventListener

        class EventListener : AndroidJavaProxy {

            public event EventHandler<RichOXEventArgs> OnEvent;

            public AndroidJavaObject EventCallback;

            public EventListener() : base(ClassUtils.ROXEventCallback)
            {
            }

            public void onEvent(string name) 
            {
                if (OnEvent != null)
                {
                    RichOXEventArgs args = new RichOXEventArgs()
                    {
                        RichOXEvent = new RichOXEvent(new RichOXEventClient(name, "", ""))
                    };
                    OnEvent(this, args);
                }
            }

            public void onEvent(string name, string value) 
            {
                if (OnEvent != null)
                {
                    RichOXEventArgs args = new RichOXEventArgs()
                    {
                        RichOXEvent = new RichOXEvent(new RichOXEventClient(name, value, ""))
                    };
                    OnEvent(this, args);
                }
            }

            public void onEvent(string name, AndroidJavaObject mapValue)
            {
                // keep empty
            }

            public void onEventJson(string name, string mapValue)
            {
                if (OnEvent != null)
                {
                    RichOXEventArgs args = new RichOXEventArgs()
                    {
                        RichOXEvent = new RichOXEvent(new RichOXEventClient(name, "", mapValue))
                    };
                    OnEvent(this, args);
                }
            }
        }
        #endregion

        #region AndroidInitCallback
        class AndroidInitCallback : AndroidJavaProxy {
            public event EventHandler<RichOXEventArgs> OnSuccess;
            public event EventHandler<RichOXEventArgs> OnFailed;

            public AndroidInitCallback() : base(ClassUtils.ROXInitCallback)
            {
            }

            public void onSuccess() 
            {
                bool response = true;
                if (OnSuccess != null)
                {
                    // 什么都不做
                    RichOXEventArgs args = new RichOXEventArgs() 
                    {
                        CommonResponse = new ROXCommonResponse(response)
                    };
                    OnSuccess(this, args);
                }
            }

            public void onFailed(int code, string msg) 
            {
                if (OnFailed != null)
                {
                    RichOXEventArgs args = new RichOXEventArgs() 
                    {
                        ErrorResponse = new ROXErrorResponse(code, msg)
                    };
                    OnFailed(this, args);
                }
            }
        }
        #endregion
    }
}