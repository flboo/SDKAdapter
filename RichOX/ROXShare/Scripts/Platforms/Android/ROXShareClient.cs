using UnityEngine;
using System;
using System.Drawing;

using System.Collections.Generic;
using System.Collections;

using ROXShare.Api;
using ROXShare.Common;

namespace ROXShare.Platforms.Android
{
    public class ROXShareClient : AndroidJavaProxy, IROXShare
    {
        static ROXShareClient sInstance = new ROXShareClient();
        private AndroidJavaObject mROXShareClient; 
        private AndroidJavaObject mUnityActivity;


        public static ROXShareClient Instance {
            get {
                return sInstance;
            }
        }


        public ROXShareClient() : base (ClassUtils.Object)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(ClassUtils.UnityActivityClassName);
            mUnityActivity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            mROXShareClient = new AndroidJavaClass(ClassUtils.ROXShare);
        }

        public void Init()
        {
            mROXShareClient.CallStatic("init", mUnityActivity);
        }

        public void SetOversea(bool oversea)
        {
            // do nothing
        }
        
        public void GetInstallParams(ROXShareInterface<Hashtable> callback)
        {
            AndroidCallback androidCallback = new AndroidCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidJavaObject response = (AndroidJavaObject) args.SuccessResponse.GetResponse();
                        Hashtable tables = new Hashtable();
                        AndroidJavaClass stringClass = new AndroidJavaClass("java.lang.String");
                        AndroidJavaObject paramsKeysObject = response.Call<AndroidJavaObject>("keySet");
                        AndroidJavaObject iterator = paramsKeysObject.Call<AndroidJavaObject>("iterator");
                        while(iterator.Call<bool>("hasNext"))
                        {
                            string paramsKey = iterator.Call<string>("next");
                            AndroidJavaObject valueObject = response.Call<AndroidJavaObject>("get", paramsKey); 
                            tables.Add(paramsKey, stringClass.CallStatic<string>("valueOf", valueObject));
                        }                        
                        callback.OnSuccess(tables);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXShareClient.CallStatic("getInstallParams", androidCallback);

        }

        public void GenShareUrl(string shareUrl, Hashtable urlParams, ROXShareInterface<string> callback)
        {
            AndroidStringCallback androidCallback = new AndroidStringCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        string finalUrl = (string) args.SuccessResponse.GetResponse();
                        callback.OnSuccess(finalUrl);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            AndroidJavaObject hashMap = new AndroidJavaObject(ClassUtils.HashMap);
            foreach (string item in urlParams.Keys) 
            {
                hashMap.Call<AndroidJavaObject>("put", item, urlParams[item]);
            }
            mROXShareClient.CallStatic("genShareUrl", shareUrl, hashMap, androidCallback);

        }

        public byte[] GetQRCodeBytes(string shareUrl, int width, int height) 
        {
            return mROXShareClient.CallStatic<byte[]>("getQRCodeBytes", shareUrl, width, height);
        }

        // public Bitmap GetQRCodeBitmap(string shareUrl, int width, int height) 
        // {
        //     return mROXNormalClass.CallStatic<Bitmap>("getQRCodeBitmap", shareUrl, width, height);
        // }

        public void ReportRegister()
        {
            mROXShareClient.CallStatic("reportRegister");
        }

        public void ReportEvent(string lable, int value)
        {
            mROXShareClient.CallStatic("reportEvent", lable, value);
        }

        public void ReportOpenShare() 
        {
             mROXShareClient.CallStatic("reportOpenShare");
        }

        public void ReportStartShare()
        {            
            mROXShareClient.CallStatic("reportStartShare");
        }

        public void ReportBindEvent(bool oversea)
        {
            // 安卓国内和海外版本使用不用的依赖，这里参数不起左右
            mROXShareClient.CallStatic("reportBindEvent");
        }

        public void ReportBindEvent(bool oversea, Hashtable bindParams)
        {
            AndroidJavaObject hashMap = new AndroidJavaObject(ClassUtils.HashMap);
            foreach (string item in bindParams.Keys) 
            {
                hashMap.Call<AndroidJavaObject>("put", item, bindParams[item]);
            }
            Debug.Log("go here ReportBindEvent with params");
            mROXShareClient.CallStatic("reportBindEvent", hashMap);
        }

        public void ReportShowShare()
        {
            mROXShareClient.CallStatic("reportShowShare");
        }

        public void ReportShareSuccess()
        {
            mROXShareClient.CallStatic("reportShareSuccess");
        }

    
    }
}