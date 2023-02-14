using UnityEngine;
using System;
using ROXBase.Api;
using ROXStrategy.Api;
using ROXStrategy.Common;
using ROXBase.Platforms.Android;
using System.Collections;
using System.Collections.Generic;

namespace ROXStrategy.Platforms.Android
{
    public class ROXNormalClient : AndroidJavaProxy, IROXNormal
    {
        static ROXNormalClient sInstance = new ROXNormalClient();
        private AndroidJavaObject mROXNormalClient;
        private AndroidJavaObject mUnityActivity;
        private AndroidJavaClass mROXNormalClass;


        public static ROXNormalClient Instance
        {
            get
            {
                return sInstance;
            }
        }


        public ROXNormalClient() : base(ClassUtils.Object)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(ClassUtils.UnityActivityClassName);
            mUnityActivity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
        }

        #region IROXNormal
        public void Init(int strategyId)
        {
            mROXNormalClass = new AndroidJavaClass(ClassUtils.ROXNormalStrategy);
            mROXNormalClient = mROXNormalClass.CallStatic<AndroidJavaObject>("getInstance", strategyId);
        }

        public void GetStrategyConfig(ROXInterface<NormalStrategyConfig> callback)
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
                            AndroidJavaObject configResponse = (AndroidJavaObject)args.CommonResponse.GetResponse();
                            NormalStrategyConfig config = ROXNormalUtils.GenerateNormalStrategyConfig(configResponse);
                            callback.OnSuccess(config);
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
            mROXNormalClient.Call("getStrategyConfig", androidCallback);
        }

        public void DoMission(string taskId, double amount, ROXInterface<NormalMissionResult> callback)
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
                            NormalMissionResult missionResult = ROXNormalUtils.generateMissionResult(androidResponse);
                            callback.OnSuccess(missionResult);
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
            mROXNormalClient.Call("doMission", taskId, amount, androidCallback);
        }

        public void DoMission(string taskId, ROXInterface<NormalMissionResult> callback)
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
                            NormalMissionResult missionResult = ROXNormalUtils.generateMissionResult(androidResponse);
                            callback.OnSuccess(missionResult);
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
            mROXNormalClient.Call("doMission", taskId, androidCallback);
        }

        public void QueryAssetInfo(ROXInterface<NormalAssetsInfo> callback)
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
                            NormalAssetsInfo assetInfo = ROXNormalUtils.generateAssetInfo(androidResponse);
                            callback.OnSuccess(assetInfo);
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
            mROXNormalClient.Call("queryAssetInfo", androidCallback);

        }

        public void ExtremeWithdraw(string taskId, ROXInterface<bool> callback)
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
            mROXNormalClient.Call("extremeWithdraw", taskId, androidCallback);
        }

        public void Withdraw(string taskId, String realName, String cardId, String phoneNumber, ROXInterface<bool> callback)
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
            mROXNormalClient.Call("withdraw", taskId, realName, cardId, phoneNumber, androidCallback);
        }

        public void Transform(String exchangeId, double amounts, ROXInterface<NormalTransformResult> callback)
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
                            NormalTransformResult transformResult = ROXNormalUtils.generateTransformResult(androidResponse);
                            callback.OnSuccess(transformResult);
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
            mROXNormalClient.Call("transform", exchangeId, amounts, androidCallback);

        }

        public void Transform(String exchangeId, ROXInterface<NormalTransformResult> callback)
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
                            NormalTransformResult transformResult = ROXNormalUtils.generateTransformResult(androidResponse);
                            callback.OnSuccess(transformResult);
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
            mROXNormalClient.Call("transform", exchangeId, androidCallback);
        }

        public void QueryProgress(List<string> tasks, ROXInterface<NormalMissionsProgress> callback)
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
                            NormalMissionsProgress normalMissionsProgress = ROXNormalUtils.generateNormalMissionsProgress(androidResponse);
                            callback.OnSuccess(normalMissionsProgress);
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
            AndroidJavaObject arrayList = new AndroidJavaObject(ClassUtils.ArrayList);
            foreach (string item in tasks)
            {
                arrayList.Call<bool>("add", item);
            }
            mROXNormalClient.Call("queryProgress", arrayList, androidCallback);
        }

        public void QueryAllProgress(ROXInterface<NormalMissionsProgress> callback)
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
                            NormalMissionsProgress normalMissionsProgress = ROXNormalUtils.generateNormalMissionsProgress(androidResponse);
                            callback.OnSuccess(normalMissionsProgress);
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
            mROXNormalClient.Call("queryAllProgress", androidCallback);
        }

        public void DoCustomRulesMission(string taskId, string tid, ROXInterface<NormalMissionResult> callback)
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
                            NormalMissionResult missionResult = ROXNormalUtils.generateMissionResult(androidResponse);
                            callback.OnSuccess(missionResult);
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
            mROXNormalClient.Call("doCustomRulesMission", taskId, tid, androidCallback);
        }

        public void DoCustomRulesMission(string taskId, ROXInterface<NormalMissionResult> callback)
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
                            NormalMissionResult missionResult = ROXNormalUtils.generateMissionResult(androidResponse);
                            callback.OnSuccess(missionResult);
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
            mROXNormalClient.Call("doCustomRulesMission", taskId, androidCallback);
        }

        public void ExtremeWithdrawNew(string taskId, ROXInterface<List<NormalAssetStock>> callback)
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
                            List<NormalAssetStock> list = ROXNormalUtils.generateNormalAssetStockList(androidResponse);
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
            mROXNormalClient.Call("extremeWithdrawNew", taskId, androidCallback);
        }

        public void WithdrawNew(string taskId, String realName, String cardId, String phoneNumber, ROXInterface<List<NormalAssetStock>> callback)
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
                            List<NormalAssetStock> list = ROXNormalUtils.generateNormalAssetStockList(androidResponse);
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
            mROXNormalClient.Call("withdrawNew", taskId, realName, cardId, phoneNumber, androidCallback);
        }

        public void GeneralWithdraw(string taskId, Hashtable withdrawParam, ROXInterface<List<NormalAssetStock>> callback)
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
                            List<NormalAssetStock> list = ROXNormalUtils.generateNormalAssetStockList(androidResponse);
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
            AndroidJavaObject hashMap = new AndroidJavaObject(ClassUtils.HashMap);
            foreach (string item in withdrawParam.Keys)
            {
                if (withdrawParam[item] is double)
                {
                    AndroidJavaObject doubleObject = new AndroidJavaObject("java.lang.Double", withdrawParam[item]);
                    hashMap.Call<AndroidJavaObject>("put", item, doubleObject);
                }
                else if (withdrawParam[item] is int)
                {
                    AndroidJavaObject intObject = new AndroidJavaObject("java.lang.Integer", withdrawParam[item]);
                    hashMap.Call<AndroidJavaObject>("put", item, intObject);
                }
                else if (withdrawParam[item] is float)
                {
                    AndroidJavaObject floatObject = new AndroidJavaObject("java.lang.Float", withdrawParam[item]);
                    hashMap.Call<AndroidJavaObject>("put", item, floatObject);
                }
                else if (withdrawParam[item] is bool)
                {
                    AndroidJavaObject boolObject = new AndroidJavaObject("java.lang.Boolean", withdrawParam[item]);
                    hashMap.Call<AndroidJavaObject>("put", item, boolObject);
                }
                else
                {
                    hashMap.Call<AndroidJavaObject>("put", item, withdrawParam[item]);
                }

            }
            mROXNormalClient.Call("generalWithdraw", taskId, hashMap, androidCallback);
        }

        public void GlobalWithdraw(string taskId, GlobalWithdrawInfo info, ROXInterface<List<NormalAssetStock>> callback)
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
                            List<NormalAssetStock> list = ROXNormalUtils.generateNormalAssetStockList(androidResponse);
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
            AndroidJavaObject withdrawInfoBuilder = new AndroidJavaObject(ClassUtils.WithdrawInfoBuilder);
            if (info.PayeeAccount != null) {
                withdrawInfoBuilder.Call<AndroidJavaObject>("setPayeeAccount", info.PayeeAccount);
            }
            if (info.WalletChannel != null) {
                withdrawInfoBuilder.Call<AndroidJavaObject>("setWalletChannel", info.WalletChannel);
            }
            if (info.PayeeName != null) {
                withdrawInfoBuilder.Call<AndroidJavaObject>("setPayeeName", info.PayeeName);
            }
            if (info.PayeeFirstName != null) {
                withdrawInfoBuilder.Call<AndroidJavaObject>("setPayeeFirstName", info.PayeeFirstName);
            }
            if (info.PayeeMiddleName != null) {
                withdrawInfoBuilder.Call<AndroidJavaObject>("setPayeeMiddleName", info.PayeeMiddleName);
            }
            if (info.PayeeLastName != null) {
                withdrawInfoBuilder.Call<AndroidJavaObject>("setPayeeLastName", info.PayeeLastName);
            }
            if (info.ExtendedInfo != null) {
                withdrawInfoBuilder.Call<AndroidJavaObject>("setExtendedInfo", info.ExtendedInfo);
            }
            if (info.PayRemark != null) {
                withdrawInfoBuilder.Call<AndroidJavaObject>("setPayRemark", info.PayRemark);
            }
            AndroidJavaObject withdrawInfo = withdrawInfoBuilder.Call<AndroidJavaObject>("build");
            mROXNormalClient.Call("globalWithdraw", taskId, withdrawInfo, androidCallback);
        }

        #endregion
    }


}