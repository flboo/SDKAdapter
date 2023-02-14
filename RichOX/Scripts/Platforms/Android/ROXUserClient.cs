using UnityEngine;
using System;
using ROXBase.Api;
using ROXBase.Common;
using System.Collections.Generic;

namespace ROXBase.Platforms.Android
{
    public class ROXUserClient : AndroidJavaProxy, IROXUser
    {
        static ROXUserClient sInstance = new ROXUserClient();

        public static ROXUserClient Instance {
            get {
                return sInstance;
            }
        }

        private AndroidJavaObject mUnityActivity;
        private AndroidJavaClass mROXUserClass;

        public ROXUserClient() : base (ClassUtils.Object)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(ClassUtils.UnityActivityClassName);
            mUnityActivity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            mROXUserClass = new AndroidJavaClass(ClassUtils.RichOXUser);
        }

        #region IROXUserClient
        public void RegisterVisitor(string source, ROXInterface<ROXUserInfo> callback) 
        {   
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {                       
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) => 
                        {
                            AndroidJavaObject userInfo = (AndroidJavaObject) args.CommonResponse.GetResponse();
                            ROXUserInfo roxUserInfo = RichOXUtils.GenerateUser(userInfo);                        
                            callback.OnSuccess(roxUserInfo);
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
            mROXUserClass.CallStatic("registerVisitor", 0, source, androidCallback);
        }

        public void StartBindAccount(string type, string appid, string token, ROXInterface<ROXUserInfo> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) => 
                        {
                            AndroidJavaObject userInfo = (AndroidJavaObject) args.CommonResponse.GetResponse();
                            ROXUserInfo roxUserInfo = RichOXUtils.GenerateUser(userInfo);
                            callback.OnSuccess(roxUserInfo);
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
            mROXUserClass.CallStatic("startBindAccount", type, appid, token, androidCallback);

        }

        public void RegisterWithWechat(string wxAppId, string wxCode, string source, ROXInterface<ROXUserInfo> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) => 
                        {
                            AndroidJavaObject userInfo = (AndroidJavaObject) args.CommonResponse.GetResponse();
                            ROXUserInfo roxUserInfo = RichOXUtils.GenerateUser(userInfo);
                            callback.OnSuccess(roxUserInfo);
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
            mROXUserClass.CallStatic("registerWithWechat", wxAppId, wxCode, source, androidCallback);
        }

        public void GetUserInfo(ROXInterface<ROXUserInfo> callback) 
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) => 
                        {
                            AndroidJavaObject userInfo = (AndroidJavaObject) args.CommonResponse.GetResponse();
                            ROXUserInfo roxUserInfo = RichOXUtils.GenerateUser(userInfo);
                            callback.OnSuccess(roxUserInfo);
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
            mROXUserClass.CallStatic("getUserInfo", androidCallback);
        }

        public void GetUserRanking(int count, int accountType, string rankingType, ROXInterface<List<ROXUserInfo>> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) => 
                        {
                            AndroidJavaObject userList = (AndroidJavaObject) args.CommonResponse.GetResponse();
                            List<ROXUserInfo> list = new List<ROXUserInfo>();
                            int size = userList.Call<int>("size");
                            for (int i=0; i<size; i++) 
                            {
                                AndroidJavaObject userInfo =  userList.Call<AndroidJavaObject>("get", i);
                                ROXUserInfo roxUserInfo = RichOXUtils.GenerateUser(userInfo);
                                list.Add(roxUserInfo);
                            }
                            callback.OnSuccess(list);
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
            mROXUserClass.CallStatic("getUserRanking", count, accountType, rankingType, androidCallback);
        }

        public void GetSpecificUsersInfo(List<string> userList, ROXInterface<List<ROXUserInfoSimple>> callback) 
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) => 
                        {
                            AndroidJavaObject responseList = (AndroidJavaObject) args.CommonResponse.GetResponse();
                            List<ROXUserInfoSimple> list = new List<ROXUserInfoSimple>();
                            int size = responseList.Call<int>("size");
                            for (int i=0; i<size; i++) 
                            {
                                AndroidJavaObject userInfo =  responseList.Call<AndroidJavaObject>("get", i);
                                ROXUserInfoSimple roxSimpleUserInfo = RichOXUtils.GenerateSimpleUser(userInfo);
                                list.Add(roxSimpleUserInfo);
                            }
                            callback.OnSuccess(list);
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
            AndroidJavaObject arrayList = new AndroidJavaObject(ClassUtils.ArrayList);
            foreach (string item in userList)
            {
                arrayList.Call<bool>("add", item);
            }
            mROXUserClass.CallStatic("getSpecificUsersInfo", arrayList, androidCallback);
        }

        public void GetUserToken(ROXInterface<ROXUserToken> callback) 
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) => 
                        {
                            AndroidJavaObject userInfo = (AndroidJavaObject) args.CommonResponse.GetResponse();
                            ROXUserToken token = RichOXUtils.GenerateUserToken(userInfo);
                            callback.OnSuccess(token);
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
            mROXUserClass.CallStatic("getUserToken", androidCallback);
        }

        public void Logout(ROXInterface<bool> callback) 
        {
            AndroidBooleanCallback androidCallback = new AndroidBooleanCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                Debug.Log("go here logout ...................");
                if (callback != null)
                {
                    if (args != null) {
                        
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) => 
                        {
                            bool result = (bool) args.CommonResponse.GetResponse();
                            if (result) {
                                callback.OnSuccess(true);
                            } else {
                                callback.OnFailed(-1, "get result, but unknown ...");
                            }
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
            mROXUserClass.CallStatic("logout", androidCallback);
        }

        public void BindInviter(string inviterUid, ROXInterface<bool> callback)
        {
            AndroidBooleanCallback androidCallback = new AndroidBooleanCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) => 
                        {
                            callback.OnSuccess(true);
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
            mROXUserClass.CallStatic("bindInviter", inviterUid, androidCallback);
        }
        #endregion
    }
}