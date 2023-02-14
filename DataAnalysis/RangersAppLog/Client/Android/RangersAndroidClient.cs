using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qarth
{
    public class RangersAndroidClient : IRangersClient
    {
        AndroidJavaClass mRangersLogClass;
        AndroidJavaObject mRangersInitConf;

        AndroidJavaObject mContext;
        AndroidJavaObject mApplication;

        bool mInited;

        public RangersAndroidClient()
        {
            mRangersLogClass = new AndroidJavaClass("com.bytedance.applog.AppLog");

            AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            mContext = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            mApplication = mContext.Call<AndroidJavaObject>("getApplication");
        }

        public void InitConf(string appId, string channel)
        {
            mRangersInitConf = new AndroidJavaObject("com.bytedance.applog.InitConfig", appId, channel);
            if (mRangersInitConf != null)
            {
                // support domestic only
                mRangersInitConf = mRangersInitConf.Call<AndroidJavaObject>("setUriConfig", 1);

                //mRangersLogClass.CallStatic("setEnableLog", true);
                //mRangersInitConf = mRangersInitConf.Call<AndroidJavaObject>("setPicker", new AndroidJavaObject("com.bytedance.applog.IPicker", mApplication, mRangersInitConf));

                mRangersInitConf.Call("setAbEnable", true);
                mRangersInitConf = mRangersInitConf.Call<AndroidJavaObject>("setEnablePlay", true);
                //Log.e(">>>>>>>>>>>>>>>>>>>>>>>>>" + 456);
                mRangersInitConf = mRangersInitConf.Call<AndroidJavaObject>("setAutoStart", true);

                mRangersLogClass.CallStatic("init", mContext, mRangersInitConf);
                mInited = true;
            }
        }

        public void SetUserId(string uuid)
        {
            if (!mInited)
                return;
            mRangersLogClass.CallStatic("setUserUniqueID", uuid);
        }

        public void SendEvent(string label, Dictionary<string, object> dictParams = null)
        {
            if (!mInited)
                return;

            if (dictParams == null)
                mRangersLogClass.CallStatic("onEventV3", label);
            else
            {
                AndroidJavaObject map = new AndroidJavaObject("org.json.JSONObject");
                foreach (KeyValuePair<string, object> pair in dictParams)
                {
                    //map = map.Call<AndroidJavaObject>("put", pair.Key, pair.Value);
                    if (pair.Value is int)
                    {
                        map = map.Call<AndroidJavaObject>("put", pair.Key, (int)pair.Value);
                    }
                    else if (pair.Value is float)
                    {
                        float value = (float)pair.Value;
                        map = map.Call<AndroidJavaObject>("put", pair.Key, (double)value);
                    }
                    else
                    {
                        map = map.Call<AndroidJavaObject>("put", pair.Key, pair.Value.ToString());
                    }
                }
                mRangersLogClass.CallStatic("onEventV3", label, map);
            }
        }

        public int GetABIntValue(string key, int defaultVal = 0)
        {
            if (!mInited)
                return defaultVal;
            return mRangersLogClass.CallStatic<int>("getAbConfig", key, defaultVal);
        }
        public string GetABStringValue(string key, string defaultVal = "")
        {
            if (!mInited)
                return defaultVal;
            return mRangersLogClass.CallStatic<string>("getAbConfig", key, defaultVal);
        }
        public bool GetABBoolValue(string key, bool defaultVal = false)
        {
            if (!mInited)
                return defaultVal;
            return mRangersLogClass.CallStatic<bool>("getAbConfig", key, defaultVal);
        }

        public string GetABVersion()
        {
            if (!mInited)
                return null;
            return mRangersLogClass.CallStatic<string>("getAbSdkVersion");
        }
    }

}
