using UnityEngine;
using System;
using ROXBase.Api;
using ROXBase.Common;
using System.Collections.Generic;

namespace ROXBase.Platforms.Android
{
    public class RichOXUserManagerClient : AndroidJavaProxy, IRichOXUserManager
    {
        static RichOXUserManagerClient sInstance = new RichOXUserManagerClient();

        public static RichOXUserManagerClient Instance
        {
            get
            {
                return sInstance;
            }
        }

        private AndroidJavaObject mUnityActivity;
        private AndroidJavaClass mROXUserClass;

        public RichOXUserManagerClient() : base(ClassUtils.Object)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(ClassUtils.UnityActivityClassName);
            mUnityActivity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            mROXUserClass = new AndroidJavaClass(ClassUtils.ROXUser);
        }

        #region IROXUserClient

        public void RegisterVisitor(ROXInterface<ROXUserBean> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            AndroidJavaObject userInfo = (AndroidJavaObject)args.CommonResponse.GetResponse();
                            ROXUserBean roxUserInfo = RichOXUtils.GenerateROXUserBean(userInfo);
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
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                        };
                        mUnityActivity.Call("runOnUiThread", runnable);
                    }
                }
            };
            mROXUserClass.CallStatic("registerVisitor", "", androidCallback);
        }

        public void RegisterWithFacebook(string openId, string token, ROXInterface<ROXUserBean> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            AndroidJavaObject userInfo = (AndroidJavaObject)args.CommonResponse.GetResponse();
                            ROXUserBean roxUserInfo = RichOXUtils.GenerateROXUserBean(userInfo);
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
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                        };
                        mUnityActivity.Call("runOnUiThread", runnable);
                    }
                }
            };
            mROXUserClass.CallStatic("registerWithFacebook", openId, token, androidCallback);
        }

        public void RegisterWithGoogle(String token, ROXInterface<ROXUserBean> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            AndroidJavaObject userInfo = (AndroidJavaObject)args.CommonResponse.GetResponse();
                            ROXUserBean roxUserInfo = RichOXUtils.GenerateROXUserBean(userInfo);
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
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                        };
                        mUnityActivity.Call("runOnUiThread", runnable);
                    }
                }
            };
            mROXUserClass.CallStatic("registerWithGoogle", token, androidCallback);
        }

        public void RegisterByApple(string appleName, string token, ROXInterface<ROXUserBean> callback)
        {
            // nothing to do 
        }

        public void RegisterWithWechat(string wxAppId, string wxCode, ROXInterface<ROXUserBean> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            AndroidJavaObject userInfo = (AndroidJavaObject)args.CommonResponse.GetResponse();
                            ROXUserBean roxUserInfo = RichOXUtils.GenerateROXUserBean(userInfo);
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
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                        };
                        mUnityActivity.Call("runOnUiThread", runnable);
                    }
                }
            };
            mROXUserClass.CallStatic("registerWithWechat", wxAppId, wxCode, androidCallback);
        }

        public void StartBindAccount(string type, string appid, string token, ROXInterface<ROXUserBean> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            AndroidJavaObject userInfo = (AndroidJavaObject)args.CommonResponse.GetResponse();
                            ROXUserBean roxUserInfo = RichOXUtils.GenerateROXUserBean(userInfo);
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
                    if (args != null)
                    {
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

        public void GetUserInfo(ROXInterface<ROXUserBean> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            AndroidJavaObject userInfo = (AndroidJavaObject)args.CommonResponse.GetResponse();
                            ROXUserBean roxUserInfo = RichOXUtils.GenerateROXUserBean(userInfo);
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
                    if (args != null)
                    {
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

        public void GetUserInfoByUserId(string userId, ROXInterface<ROXUserBean> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            AndroidJavaObject userInfo = (AndroidJavaObject)args.CommonResponse.GetResponse();
                            ROXUserBean roxUserInfo = RichOXUtils.GenerateROXUserBean(userInfo);
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
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                        };
                        mUnityActivity.Call("runOnUiThread", runnable);
                    }
                }
            };
            mROXUserClass.CallStatic("getSpecialUserInfo", userId, androidCallback);
        }

        public void StartRetrieveInviter(ROXInterface<ROXUserBeanBase> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            AndroidJavaObject userInfo = (AndroidJavaObject)args.CommonResponse.GetResponse();
                            ROXUserBeanBase roxUserInfo = RichOXUtils.GenerateROXUserBeanBase(userInfo);
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
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                        };
                        mUnityActivity.Call("runOnUiThread", runnable);
                    }
                }
            };
            mROXUserClass.CallStatic("startRetrieveInviter", androidCallback);
        }

        public void Logout(ROXInterface<bool> callback)
        {
            AndroidBooleanCallback androidCallback = new AndroidBooleanCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            bool result = (bool)args.CommonResponse.GetResponse();
                            if (result)
                            {
                                callback.OnSuccess(true);
                            }
                            else
                            {
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
                    if (args != null)
                    {
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
                    if (args != null)
                    {
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
                    if (args != null)
                    {
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

        public void GetUserExternalInfo(ROXInterface<ROXUserExternalInfo> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            AndroidJavaObject externalInfo = (AndroidJavaObject)args.CommonResponse.GetResponse();
                            ROXUserExternalInfo externalUser = RichOXUtils.GenerateROXUserExternalInfo(externalInfo);
                            callback.OnSuccess(externalUser);
                        };
                        mUnityActivity.Call("runOnUiThread", runnable);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                        };
                        mUnityActivity.Call("runOnUiThread", runnable);
                    }
                }
            };
            mROXUserClass.CallStatic("qttUserInfoQuery", androidCallback);
        }

        public void GetAPAuthInfo(ROXInterface<string> callback) 
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
            mROXUserClass.CallStatic("getAPAuthInfo", androidCallback);

        }

        public void BindWallet(String type, string walletInfo, ROXInterface<bool> callback) 
        {
            AndroidBooleanCallback androidCallback = new AndroidBooleanCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            bool result = (bool)args.CommonResponse.GetResponse();
                            if (result)
                            {
                                callback.OnSuccess(true);
                            }
                            else
                            {
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
                    if (args != null)
                    {
                        AndroidRunnable runnable = new AndroidRunnable();
                        runnable.Run += (runnableSender, runnableArgs) =>
                        {
                            callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                        };
                        mUnityActivity.Call("runOnUiThread", runnable);
                    }
                }
            };
            mROXUserClass.CallStatic("bindWallet", type, walletInfo, androidCallback);
        }

        #endregion
    }
}