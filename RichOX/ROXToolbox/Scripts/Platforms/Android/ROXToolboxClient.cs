using UnityEngine;
using System;
using ROXBase.Api;
using ROXToolbox.Api;
using ROXToolbox.Common;
using ROXBase.Platforms.Android;
using System.Collections.Generic;

namespace ROXToolbox.Platforms.Android
{
    public class ROXToolboxClient : AndroidJavaProxy, IROXToolbox
    {
        static ROXToolboxClient sInstance = new ROXToolboxClient();
        private AndroidJavaObject mUnityActivity;
        private AndroidJavaClass mROXToolboxClass;


        public static ROXToolboxClient Instance
        {
            get
            {
                return sInstance;
            }
        }


        public ROXToolboxClient() : base(ClassUtils.Object)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(ClassUtils.UnityActivityClassName);
            mUnityActivity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            mROXToolboxClass = new AndroidJavaClass(ClassUtils.RichOXToolbox);
        }

        #region IROXToolbox

        public void QueryPiggyBankList(ROXInterface<List<PiggyBank>> callback)
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
                            AndroidJavaObject androidResponse = (AndroidJavaObject)args.CommonResponse.GetResponse();
                            List<PiggyBank> list = ROXToolboxUtils.generatePiggyBankList(androidResponse);
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
            mROXToolboxClass.CallStatic("queryPiggyBankList", androidCallback);
        }

        public void PiggyBankWithdraw(int piggyId, ROXInterface<bool> callback)
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
                                callback.OnSuccess(result);
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
            mROXToolboxClass.CallStatic("piggyBankWithdraw", piggyId, androidCallback);
        }

        public void Init()
        {
            mROXToolboxClass.CallStatic("init", mUnityActivity);
        }

        public void SetInterval(int interval)
        {
            mROXToolboxClass.CallStatic("setInterval", interval);
        }

        public void GetGroupInfo(ROXInterface<List<GroupInfo>> callback)
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
                            AndroidJavaObject androidResponse = (AndroidJavaObject)args.CommonResponse.GetResponse();
                            List<GroupInfo> list = ROXToolboxUtils.generateGroupinfoList(androidResponse);
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
            mROXToolboxClass.CallStatic("getGroupInfo", androidCallback);
        }

        public void GetMessageList(string groupId, int size, ROXInterface<List<ChatMessage>> callback)
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
                            AndroidJavaObject androidResponse = (AndroidJavaObject)args.CommonResponse.GetResponse();
                            List<ChatMessage> list = ROXToolboxUtils.generateChatMessageList(androidResponse);
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
            mROXToolboxClass.CallStatic("getMessageList", groupId, size, androidCallback);
        }

        public void PostChatMessage(string groupId, string nickName, string avatar, string type, string content, ROXInterface<ChatMessage> callback)
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
                            AndroidJavaObject androidResponse = (AndroidJavaObject)args.CommonResponse.GetResponse();
                            ChatMessage chatMessage = ROXToolboxUtils.generateChatMessage(androidResponse);
                            callback.OnSuccess(chatMessage);
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
            mROXToolboxClass.CallStatic("postChatMessage", groupId, nickName, avatar, type, content, androidCallback);
        }


        public void SavePrivacyData(string key, string value, ROXInterface<Boolean> callback)
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
            mROXToolboxClass.CallStatic("savePrivacyData", key, value, androidCallback);
        }
        public void QueryPrivacyData(string key, ROXInterface<PrivacyInfo> callback)
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
                            AndroidJavaObject androidResponse = (AndroidJavaObject)args.CommonResponse.GetResponse();
                            PrivacyInfo privacyInfo = ROXToolboxUtils.generatePrivacyInfo(androidResponse);
                            callback.OnSuccess(privacyInfo);
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
            mROXToolboxClass.CallStatic("queryPrivacyData", key, androidCallback);
        }

        public void QueryPrivacyDatas(List<string> keys, ROXInterface<List<PrivacyInfo>> callback)
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
                            AndroidJavaObject androidResponse = (AndroidJavaObject)args.CommonResponse.GetResponse();
                            List<PrivacyInfo> privacyInfos = ROXToolboxUtils.generatePrivacyInfoList(androidResponse);
                            callback.OnSuccess(privacyInfos);
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
            AndroidJavaObject listObject = new AndroidJavaObject("java.util.ArrayList");
            if (keys != null && keys.Count > 0)
            {
                foreach (string key in keys)
                {
                    listObject.Call<bool>("add", key);
                }
            }
            mROXToolboxClass.CallStatic("queryPrivacyDataList", listObject, androidCallback);
        }

        #endregion
    }




}