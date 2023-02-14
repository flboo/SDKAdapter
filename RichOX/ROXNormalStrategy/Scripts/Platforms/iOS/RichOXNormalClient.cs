using System.Runtime.InteropServices;
using System;
using ROXBase.Api;
using ROXBase.Common;
using ROXStrategy.Api;
using ROXStrategy.Common;
using System.Collections;
using System.Collections.Generic;
using ROXBase.Platforms.iOS;
using AOT;

namespace ROXStrategy.Platforms.iOS
{
    public class ROXNormalClient : IROXNormal
    {
        private IntPtr mNormalStrategyPtr;
        private IntPtr normalStrategyClientPtr;


        #region callback types

        internal delegate void RichOXGetNormalStrategySettingCallback(IntPtr strategyClient, IntPtr setting);
        internal delegate void RichOXUnityFailureCallback(IntPtr strategyClient, int errorCode,string errorMessage);
        internal delegate void RichOXGetNormalStrategySyncPrizeCallback(IntPtr strategyClient, IntPtr status);
        internal delegate void RichOXGetNormalStrategyAssetExchangeCallback(IntPtr strategyClient, IntPtr result);
        internal delegate void RichOXGetNormalStrategyGetTaskProcessInfoCallback(IntPtr strategyClient, IntPtr taskProcessInfo);
        internal delegate void RichOXGetNormalStrategyDoMissionCallback(IntPtr strategyClient, IntPtr result);
        internal delegate void RichOXGetNormalStrategyWithdrawCallback(IntPtr strategyClient, IntPtr result);

        #endregion

        // This property should be used when setting the normalStrategy.
        private IntPtr normalStrategyPtr
        {
            get { return mNormalStrategyPtr; }

            set
            {
                Externs.RichOXNormalStrategyRelease(mNormalStrategyPtr);
                mNormalStrategyPtr = value;
            }
        }

        private ROXInterface<NormalStrategyConfig> getSettingCallback;
        private ROXInterface<NormalMissionResult> doMissionCallback;
        private ROXInterface<NormalAssetsInfo> queryAssetInfoCallback;
        private ROXInterface<NormalTransformResult> assetExchangeCallback;
        private ROXInterface<NormalMissionsProgress> queryProcessCallback;
        private ROXInterface<List<NormalAssetStock>> withdrawCallback;
        private ROXInterface<List<NormalAssetStock>> globalWithdrawCallback;

        public void Init(int strategyId){
            normalStrategyClientPtr = (IntPtr)GCHandle.Alloc(this);
            normalStrategyPtr = Externs.RichOXNormalStrategyGetStrategyInstance(normalStrategyClientPtr, strategyId);
        }

        public void GetStrategyConfig(ROXInterface<NormalStrategyConfig> callback) {
            getSettingCallback = callback;

            Externs.RichOXNormalStrategySyncList(normalStrategyPtr, NormalStrategySettingSuccessCallback, getNormalStrategySettingFailedCallback);
        }

        public void DoMission(string taskId, double amount, ROXInterface<NormalMissionResult> callback) {
            doMissionCallback = callback;

            Externs.RichOXNormalStrategyDoMission(normalStrategyPtr, taskId, (float)amount, null, doMissionSuccessCallback, doMissionFailedCallback);
        }

        public void DoMission(string taskId, ROXInterface<NormalMissionResult> callback) {
            doMissionCallback = callback;

            Externs.RichOXNormalStrategyDoMission(normalStrategyPtr, taskId, 0, null, doMissionSuccessCallback, doMissionFailedCallback);
        }

        public void QueryAssetInfo(ROXInterface<NormalAssetsInfo> callback) {
            queryAssetInfoCallback = callback;

            Externs.RichOXNormalStrategySyncCurrentPrize(normalStrategyPtr, queryAssetInfoSuccessCallback, queryAssetInfoFailedCallback);
        }

        public void ExtremeWithdraw(string taskId , ROXInterface<bool> callback) {

        }

        public void Withdraw(string taskId, String realName, String cardId, String phoneNumber, ROXInterface<bool> callback) {

        }

        public void Transform(String exchangeId, double amounts, ROXInterface<NormalTransformResult> callback) {
            assetExchangeCallback = callback;

            Externs.RichOXNormalStrategyExchangeAsset(normalStrategyPtr, exchangeId, (float)amounts, assetExchangeSuccessCallback, assetExchangeFailedCallback);
        }

        public void Transform(String exchangeId, ROXInterface<NormalTransformResult> callback) {
            assetExchangeCallback = callback;

            Externs.RichOXNormalStrategyExchangeAsset(normalStrategyPtr, exchangeId, 0, assetExchangeSuccessCallback, assetExchangeFailedCallback);
        }

        public void QueryProgress(List<string> tasks, ROXInterface<NormalMissionsProgress> callback){
            queryProcessCallback = callback;

            Externs.RichOXNormalStrategyGetTaskProcessInfo(normalStrategyPtr, tasks.ToArray(), tasks.Count, queryProcessSuccessCallback, queryProcessFailedCallback);
        }

        public void QueryAllProgress(ROXInterface<NormalMissionsProgress> callback) {
            queryProcessCallback = callback;

            Externs.RichOXNormalStrategyGetTaskProcessInfo(normalStrategyPtr, null, 0, queryProcessSuccessCallback, queryProcessFailedCallback);
        }

        public void DoCustomRulesMission(string taskId, string tid, ROXInterface<NormalMissionResult> callback) {
            doMissionCallback = callback;

            Externs.RichOXNormalStrategyDoMission(normalStrategyPtr, taskId, 0, tid, doMissionSuccessCallback, doMissionFailedCallback);
        }

        public void DoCustomRulesMission(string taskId, ROXInterface<NormalMissionResult> callback) {
            doMissionCallback = callback;

            Externs.RichOXNormalStrategyDoMission(normalStrategyPtr, taskId, 0, null, doMissionSuccessCallback, doMissionFailedCallback);
        }

        public void ExtremeWithdrawNew(string taskId , ROXInterface<List<NormalAssetStock>> callback) {
            withdrawCallback = callback;
            Externs.RichOXNormalStrategyWithdraw(normalStrategyPtr, taskId, IntPtr.Zero, withdrawSuccessCallback, withdrawFailedCallback);
        }

        public void WithdrawNew(string taskId, String realName, String cardId, String phoneNumber, ROXInterface<List<NormalAssetStock>> callback) {
            withdrawCallback = callback;

            IntPtr withdrawInfo = ROXBase.Platforms.iOS.Externs.RichOXCreateWithdrawInfo(null);
            ROXBase.Platforms.iOS.Externs.RichOXWithdrawInfoSetRealName(withdrawInfo, realName);
            ROXBase.Platforms.iOS.Externs.RichOXWithdrawInfoSetIdCard(withdrawInfo, cardId);
            ROXBase.Platforms.iOS.Externs.RichOXWithdrawInfoSetPhoneNo(withdrawInfo, phoneNumber);

            Externs.RichOXNormalStrategyWithdraw(normalStrategyPtr, taskId, withdrawInfo, withdrawSuccessCallback, withdrawFailedCallback);
        }

        public void GeneralWithdraw(string taskId, Hashtable withdrawParam, ROXInterface<List<NormalAssetStock>> callback) {
            withdrawCallback = callback;

            IntPtr withdrawInfo = ROXBase.Platforms.iOS.Externs.RichOXCreateWithdrawInfo(null);

            IntPtr extParam = ROXBase.Platforms.iOS.Externs.RichOXCreateMutableDictionaryInfo();

            foreach (string item in withdrawParam.Keys) 
            {
                if (item.Equals("real_name")) {
                    ROXBase.Platforms.iOS.Externs.RichOXWithdrawInfoSetRealName(withdrawInfo, (string)withdrawParam[item]);
                } else if (item.Equals("id_card")) {
                    ROXBase.Platforms.iOS.Externs.RichOXWithdrawInfoSetIdCard(withdrawInfo, (string)withdrawParam[item]);
                } else if (item.Equals("phone")) {
                    ROXBase.Platforms.iOS.Externs.RichOXWithdrawInfoSetPhoneNo(withdrawInfo, (string)withdrawParam[item]); 
                } else if (item.Equals("withdraw_way")) {
                    ROXBase.Platforms.iOS.Externs.RichOXWithdrawInfoSetWithdrawWay(withdrawInfo, (string)withdrawParam[item]); 
                } else if (item.Equals("withdraw_amount")) {
                    ROXBase.Platforms.iOS.Externs.RichOXWithdrawInfoSetWithdrawAmount(withdrawInfo, (double)withdrawParam[item]); 
                } else {
                    if (withdrawParam[item] is int) {
                        ROXBase.Platforms.iOS.Externs.RichOXTypeMutableDictionarySetIntValue(extParam, item, (int)withdrawParam[item]); 
                    } else if (withdrawParam[item] is long) {
                        ROXBase.Platforms.iOS.Externs.RichOXTypeMutableDictionarySetLongValue(extParam, item, (long)withdrawParam[item]); 
                    } else if (withdrawParam[item] is bool) {
                        ROXBase.Platforms.iOS.Externs.RichOXTypeMutableDictionarySetBoolValue(extParam, item, (bool)withdrawParam[item]); 
                    } else if (withdrawParam[item] is string) {
                        ROXBase.Platforms.iOS.Externs.RichOXTypeMutableDictionarySetStringValue(extParam, item, (string)withdrawParam[item]); 
                    }
                }
            }

            ROXBase.Platforms.iOS.Externs.RichOXWithdrawInfoSetExtParam(withdrawInfo, extParam);

            Externs.RichOXNormalStrategyWithdraw(normalStrategyPtr, taskId, withdrawInfo, withdrawSuccessCallback, withdrawFailedCallback);
        }

        public void GlobalWithdraw(string taskId, GlobalWithdrawInfo info, ROXInterface<List<NormalAssetStock>> callback)
        {
            globalWithdrawCallback = callback;

            IntPtr withdrawInfo = Externs.createGlobalWithdrawInfo(info.WalletChannel, info.PayeeAccount);
            Externs.RichOXGlobalWithdrawInfoSetExtendedInfo(withdrawInfo, info.ExtendedInfo);
            Externs.RichOXGlobalWithdrawInfoSetPayRemark(withdrawInfo, info.PayRemark);
            Externs.RichOXGlobalWithdrawInfoSetName(withdrawInfo, info.PayeeName);
            Externs.RichOXGlobalWithdrawInfoSetFirstName(withdrawInfo, info.PayeeFirstName);
            Externs.RichOXGlobalWithdrawInfoSetLastName(withdrawInfo, info.PayeeLastName);
            Externs.RichOXGlobalWithdrawInfoSetMiddleName(withdrawInfo, info.PayeeMiddleName);

            Externs.RichOXNormalStrategyGlobalWithdraw(normalStrategyPtr, taskId, withdrawInfo, globalWithdrawSuccessCallback, globalWithdrawFailedCallback);
        }

        #region strategy callback methods

        [MonoPInvokeCallback(typeof(RichOXGetNormalStrategySettingCallback))]
        private static void NormalStrategySettingSuccessCallback(IntPtr strategyClient, IntPtr setting)
        {
            ROXNormalClient client = IntPtrToRichOXNormalClient(strategyClient);
            if (client != null && client.getSettingCallback != null)
            {
                NormalStrategyConfig config = new NormalStrategyConfig();

                config.StrategyId = Externs.RichOXNormalStrategyTypeSettingGetStrategyId(setting);
                config.StrategyType = Externs.RichOXNormalStrategyTypeSettingGetStrategyType(setting);
                config.StrategyName = Externs.RichOXNormalStrategyTypeSettingGetStrategyName(setting);
                config.StartTime = Externs.RichOXNormalStrategyTypeSettingGetStrategyStartTime(setting);
                config.EndTime = Externs.RichOXNormalStrategyTypeSettingGetStrategyEndTime(setting);
                config.AppId = Externs.RichOXNormalStrategyTypeSettingGetStrategyAppId(setting);
                config.AbId = Externs.RichOXNormalStrategyTypeSettingGetStrategyABId(setting);
                config.AbGroup = Externs.RichOXNormalStrategyTypeSettingGetStrategyABGroup(setting);

                NormalMissionInfo missionInfo = new NormalMissionInfo();

                IntPtr taskInfo = Externs.RichOXNormalStrategyTypeSettingGetStrategyTaskInfo(setting);
                IntPtr assetList = Externs.RichOXNormalStrategyTypeTaskInfoGetAssetInfoArray(taskInfo);

                if (assetList != null) {
                    List<StrategyAsset> unityAssetList = new List<StrategyAsset>();
                    int size = (int)Externs.RichOXNormalStrategyTypeAssetInfoArrayGetCount(assetList);
                    for (int i=0; i<size; i++) {
                        IntPtr assetInfo = Externs.RichOXNormalStrategyTypeAssetInfoArrayGetItem(assetList, i);
                        StrategyAsset asset = new StrategyAsset();
                        asset.AssetName = Externs.RichOXNormalStrategyAssetInfoGetName(assetInfo);
                        asset.ExchangeRate = Externs.RichOXNormalStrategyAssetInfoGetExchangeRate(assetInfo);
                        unityAssetList.Add(asset);
                    }
                    missionInfo.StrategyAssetList = unityAssetList;
                }       
                
                IntPtr exchangeList = Externs.RichOXNormalStrategyTypeTaskInfoGetAssetExchangeArrayInfoArray(taskInfo);
            
                if (exchangeList != null) {
                    List<StrategyExchangeTask> unityExchangeTaskList = new List<StrategyExchangeTask>();
                    int size = (int)Externs.RichOXNormalStrategyTypeAssetExchangeArrayGetCount(exchangeList);
                    for (int i=0; i<size; i++) {
                        IntPtr exchangeObject = Externs.RichOXNormalStrategyTypeAssetExchangeInfoArrayGetItem(exchangeList, i);
                        StrategyExchangeTask exchangeTask = new StrategyExchangeTask();
                        exchangeTask.ExchangeId = Externs.RichOXNormalStrategyAssetExchangeInfoGetExchangeId(exchangeObject);
                        exchangeTask.OriginAssetName = Externs.RichOXNormalStrategyAssetExchangeInfoGetFromAssetName(exchangeObject);
                        exchangeTask.OriginAssetAmount = Externs.RichOXNormalStrategyAssetExchangeInfoGetFromPrizeAmount(exchangeObject);
                        exchangeTask.ExchangeAssetName = Externs.RichOXNormalStrategyAssetExchangeInfoGetToAssetName(exchangeObject);
                        exchangeTask.ExchangeAssetAmount = Externs.RichOXNormalStrategyAssetExchangeInfoGetToPrizeAmount(exchangeObject);
                        unityExchangeTaskList.Add(exchangeTask);
                    }
                    missionInfo.StrategyExchangeTaskList = unityExchangeTaskList;
                } 

                IntPtr taskList = Externs.RichOXNormalStrategyTypeTaskInfoGetAssetTaskArrayInfoArray(taskInfo);
                if (taskList != null) {
                    List<StrategyMissionTask> unityMissionTaskList = new List<StrategyMissionTask>();
                    int size = (int)Externs.RichOXNormalStrategyTypeTaskArrayGetCount(taskList);
                    for (int i=0; i<size; i++) {
                        IntPtr taskObject = Externs.RichOXNormalStrategyTypeTaskArrayGetItem(taskList, i);
                        StrategyMissionTask missionTask = new StrategyMissionTask();
                        missionTask.Id = Externs.RichOXNormalStrategyTaskGetTaskId(taskObject);
                        missionTask.Name = Externs.RichOXNormalStrategyTaskGetName(taskObject);
                        missionTask.AssetName = Externs.RichOXNormalStrategyTaskGetAssetName(taskObject);
                        missionTask.Frequency = Externs.RichOXNormalStrategyTaskGetFrequency(taskObject);
                        missionTask.FrequencyType = Externs.RichOXNormalStrategyTaskGetFrequencyType(taskObject);
                        missionTask.PrizeAmount = Externs.RichOXNormalStrategyTaskGetPrizeAmount(taskObject);
                        missionTask.PrizeType = Externs.RichOXNormalStrategyTaskGetPrizeType(taskObject);            
                        unityMissionTaskList.Add(missionTask);
                    }
                    missionInfo.StrategyMissionTaskList = unityMissionTaskList;
                } 

                config.MissionTaskInfo = missionInfo;

                IntPtr withdrawArray = Externs.RichOXNormalStrategyTypeSettingGetStrategyWithdrawSetting(setting);
                if (withdrawArray != null) {
                    List<NormalStrategyWithdrawTask> unityWithdrawTaskList = new List<NormalStrategyWithdrawTask>();
                    int size = (int)Externs.RichOXNormalStrategyTypeItemArrayGetCount(withdrawArray);
                    for (int i=0; i<size; i++) {
                        IntPtr item = Externs.RichOXNormalStrategyTypeItemArrayGetItem(withdrawArray, i);
                        NormalStrategyWithdrawTask withdrawTask = new NormalStrategyWithdrawTask();
                        withdrawTask.Id = Externs.RichOXNormalStrategyTypeItemGetPackageId(item);
                        withdrawTask.Name = Externs.RichOXNormalStrategyTypeItemGetName(item);;
                        withdrawTask.RewardAmount = Externs.RichOXNormalStrategyTypeItemGetAmount(item);
                        withdrawTask.FrequencyType = Externs.RichOXNormalStrategyTypeItemGetFrequencyType(item);
                        withdrawTask.IsExtreme = Externs.RichOXNormalStrategyTypeItemGetWithdrawType(item) == 1;

                        withdrawTask.CostAssetsAmount = Externs.RichOXNormalStrategyTypeItemGetCostAsset(item);
                        withdrawTask.Frequency = Externs.RichOXNormalStrategyTypeItemGetFrequency(item);
                        withdrawTask.AssetName = Externs.RichOXNormalStrategyTypeItemGetAssetName(item); 

                        withdrawTask.WithdrawType = Externs.RichOXNormalStrategyTypeItemGetWithdrawAmountType(item);
                        withdrawTask.MinCash = Externs.RichOXNormalStrategyTypeItemGetMinCash(item);
                        withdrawTask.MaxCash = Externs.RichOXNormalStrategyTypeItemGetMaxCash(item);

                        unityWithdrawTaskList.Add(withdrawTask);
                    }
                    config.NormalWithdrawTaskList = unityWithdrawTaskList;
                } 

                client.getSettingCallback.OnSuccess(config);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        public static void getNormalStrategySettingFailedCallback(IntPtr strategyClient, int code, string message) {
            ROXNormalClient client = IntPtrToRichOXNormalClient(strategyClient);
            if (client != null && client.getSettingCallback != null)
            {
                client.getSettingCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXGetNormalStrategyDoMissionCallback))]
        private static void doMissionSuccessCallback(IntPtr strategyClient, IntPtr result)
        {
            ROXNormalClient client = IntPtrToRichOXNormalClient(strategyClient);
            if (client != null && client.doMissionCallback != null)
            {
                NormalMissionResult missionResult = new NormalMissionResult();
                missionResult.AssetName = Externs.RichOXNormalStrategyTaskResultGetAssetName(result);
                missionResult.PrizeDelta = Externs.RichOXNormalStrategyTaskResultGetDeltaPrizeValue(result);
                missionResult.PrizeTotal = Externs.RichOXNormalStrategyTaskResultGetTotalPrizeValue(result);

                IntPtr assetStatusArray = Externs.RichOXNormalStrategyTaskResultGetGetAssetStatusArray(result);

                if (assetStatusArray != null) {
                    List<NormalAssetStock> unityAssetList = new List<NormalAssetStock>();
                    int size = (int)Externs.RichOXNormalStrategyTypeAssetStatusArrayGetCount(assetStatusArray);
                    for (int i=0; i<size; i++) {
                        IntPtr assetStatus = Externs.RichOXNormalStrategyTypeAssetStatusArrayGetItem(assetStatusArray, i);
                        NormalAssetStock stock = new NormalAssetStock();
                        stock.AssetName = Externs.RichOXNormalStrategyAssetStatusGetName(assetStatus);
                        stock.AssetAmount = Externs.RichOXNormalStrategyAssetStatusGetAmount(assetStatus);
                        unityAssetList.Add(stock);
                    }
                    missionResult.AssetList = unityAssetList;
                }
            
                client.doMissionCallback.OnSuccess(missionResult);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        public static void doMissionFailedCallback(IntPtr strategyClient, int code, string message) {
            ROXNormalClient client = IntPtrToRichOXNormalClient(strategyClient);
            if (client != null && client.doMissionCallback != null)
            {
                client.doMissionCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXGetNormalStrategySyncPrizeCallback))]
        private static void queryAssetInfoSuccessCallback(IntPtr strategyClient, IntPtr result)
        {
            ROXNormalClient client = IntPtrToRichOXNormalClient(strategyClient);
            if (client != null && client.queryAssetInfoCallback != null)
            {
                NormalAssetsInfo assetInfo = new NormalAssetsInfo();
                assetInfo.Uid = Externs.RichOXNormalStrategyTypeStatusGetUId(result);
                assetInfo.StrategyId = Externs.RichOXNormalStrategyTypeStatusGetStrategyId(result);

                IntPtr assetInfos = Externs.RichOXNormalStrategyTypeStatusGetAssetStatusArray(result);
                if (assetInfos != null) {
                    List<NormalAssetStock> unityAssetList = new List<NormalAssetStock>();
                    int size = (int)Externs.RichOXNormalStrategyTypeAssetStatusArrayGetCount(assetInfos);
                    for (int i=0; i<size; i++) {
                        IntPtr assetObject = Externs.RichOXNormalStrategyTypeAssetStatusArrayGetItem(assetInfos, i);
                        NormalAssetStock stock = new NormalAssetStock();
                        stock.AssetName = Externs.RichOXNormalStrategyAssetStatusGetName(assetObject);
                        stock.AssetAmount = Externs.RichOXNormalStrategyAssetStatusGetAmount(assetObject);
                        unityAssetList.Add(stock);
                    }
                    assetInfo.AssetStockList = unityAssetList;
                }

                IntPtr withdrawRecords = Externs.RichOXNormalStrategyTypeStatusGetWithdrawRecordDataArray(result);
                if (withdrawRecords != null) {
                    List<StrategyWithdrawRecord> unityRecordList = new List<StrategyWithdrawRecord>();
                    int size = (int)Externs.RichOXNormalStrategyTypeWithdrawRecordDataArrayGetCount(withdrawRecords);
                    for (int i=0; i<size; i++) {
                        IntPtr withdrawObject = Externs.RichOXNormalStrategyTypeWithdrawRecordDataArrayGetItem(withdrawRecords, i);
                        StrategyWithdrawRecord record = new StrategyWithdrawRecord();
                        record.Id = Externs.RichOXNormalStrategyTypeWithdrawRecordDataGetWithdrawId(withdrawObject);
                        record.WithdrawTaskId = Externs.RichOXNormalStrategyTypeWithdrawRecordDataGetTaskId(withdrawObject);        
                        record.UserId = Externs.RichOXNormalStrategyTypeWithdrawRecordDataGetUserId(withdrawObject);
                        record.PayRemark = Externs.RichOXNormalStrategyTypeWithdrawRecordDataGetPayRemark(withdrawObject);
                        record.CashAmount = Externs.RichOXNormalStrategyTypeWithdrawRecordDataGetAmount(withdrawObject);
                        record.RequestDay = Externs.RichOXNormalStrategyTypeWithdrawRecordDataGetRequestDay(withdrawObject);
                        record.WithdrawChannel = Externs.RichOXNormalStrategyTypeWithdrawRecordDataGetWithdrawChannel(withdrawObject);
                        record.Status = Externs.RichOXNormalStrategyTypeWithdrawRecordDataGetStatus(withdrawObject);
                        unityRecordList.Add(record);
                    }
                    assetInfo.Records = unityRecordList;
                }
            
                client.queryAssetInfoCallback.OnSuccess(assetInfo);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        public static void queryAssetInfoFailedCallback(IntPtr strategyClient, int code, string message) {
            ROXNormalClient client = IntPtrToRichOXNormalClient(strategyClient);
            if (client != null && client.queryAssetInfoCallback != null)
            {
                client.queryAssetInfoCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXGetNormalStrategyAssetExchangeCallback))]
        private static void assetExchangeSuccessCallback(IntPtr strategyClient, IntPtr result)
        {
            ROXNormalClient client = IntPtrToRichOXNormalClient(strategyClient);
            if (client != null && client.assetExchangeCallback != null)
            {
                NormalTransformResult transformResult = new NormalTransformResult();

                TransformAssetInfo originInfo = new TransformAssetInfo();
                originInfo.AssetName = Externs.RichOXNormalStrategyExchangeResultGetFromAssetName(result);
                originInfo.PrizeDelta = Externs.RichOXNormalStrategyExchangeResultGetFromAssetDeltaPrizeValue(result);
                originInfo.PrizeTotal = Externs.RichOXNormalStrategyExchangeResultGetFromAssetTotalPrizeValue(result);
                transformResult.OriginAsset = originInfo;
                TransformAssetInfo destInfo = new TransformAssetInfo();
                destInfo.AssetName = Externs.RichOXNormalStrategyExchangeResultGetToAssetName(result);
                destInfo.PrizeDelta = Externs.RichOXNormalStrategyExchangeResultGetToAssetDeltaPrizeValue(result);
                destInfo.PrizeTotal = Externs.RichOXNormalStrategyExchangeResultGetToAssetTotalPrizeValue(result);
                transformResult.DestinationAsset = destInfo;


                IntPtr assetListObject = Externs.RichOXNormalStrategyExchangeResultGetAssetStatusArray(result);
                if (assetListObject != null) {
                    List<NormalAssetStock> unityAssetList = new List<NormalAssetStock>();
                    int size = (int)Externs.RichOXNormalStrategyTypeAssetStatusArrayGetCount(assetListObject);
                    for (int i=0; i<size; i++) {
                        IntPtr assetObject = Externs.RichOXNormalStrategyTypeAssetStatusArrayGetItem(assetListObject, i);
                        NormalAssetStock stock = new NormalAssetStock();
                        stock.AssetName = Externs.RichOXNormalStrategyAssetStatusGetName(assetObject);
                        stock.AssetAmount = Externs.RichOXNormalStrategyAssetStatusGetAmount(assetObject);
                        unityAssetList.Add(stock);
                    }
                    transformResult.AssetList = unityAssetList;
                }
            
                client.assetExchangeCallback.OnSuccess(transformResult);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        public static void assetExchangeFailedCallback(IntPtr strategyClient, int code, string message) {
            ROXNormalClient client = IntPtrToRichOXNormalClient(strategyClient);
            if (client != null && client.assetExchangeCallback != null)
            {
                client.assetExchangeCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXGetNormalStrategyGetTaskProcessInfoCallback))]
        private static void queryProcessSuccessCallback(IntPtr strategyClient, IntPtr result)
        {
            ROXNormalClient client = IntPtrToRichOXNormalClient(strategyClient);
            if (client != null && client.queryProcessCallback != null)
            {
                NormalMissionsProgress normalMissionsProgress = new NormalMissionsProgress();

                normalMissionsProgress.Uid = Externs.RichOXNormalStrategyTaskProcessResultGetUId(result);
                
                IntPtr missionListObject = Externs.RichOXNormalStrategyTaskProcessResultGetTaskProcessArray(result);
                if (missionListObject != null) {
                    int size = (int)Externs.RichOXNormalStrategyTaskProcessArrayGetCount(missionListObject);
                    List<MissionProgress> missionList = new List<MissionProgress>();                
                    for (int index = 0 ; index < size; index ++ ) 
                    {
                        IntPtr missionObject = Externs.RichOXNormalStrategyTaskProcessArrayGetItem(missionListObject, index);
                        MissionProgress missionProgress = new MissionProgress();
                        missionProgress.Id = Externs.RichOXNormalStrategyTaskGetTaskId(missionObject);
                        missionProgress.Name = Externs.RichOXNormalStrategyTaskGetName(missionObject);
                        missionProgress.Frequency = Externs.RichOXNormalStrategyTaskGetFrequency(missionObject);
                        missionProgress.FrequencyType = Externs.RichOXNormalStrategyTaskGetFrequencyType(missionObject);
                        missionProgress.DoneTimes = Externs.RichOXNormalStrategyTaskProcessGetUpdateTimes(missionObject);
                        missionProgress.TimeStap = Externs.RichOXNormalStrategyTaskProcessGetLastUpdateTime(missionObject);
                        missionList.Add(missionProgress);
                    }
                    normalMissionsProgress.ProgressList = missionList;
                }
                client.queryProcessCallback.OnSuccess(normalMissionsProgress);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        public static void queryProcessFailedCallback(IntPtr strategyClient, int code, string message) {
            ROXNormalClient client = IntPtrToRichOXNormalClient(strategyClient);
            if (client != null && client.queryProcessCallback != null)
            {
                client.queryProcessCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXGetNormalStrategyWithdrawCallback))]
        public static void withdrawSuccessCallback(IntPtr strategyClient, IntPtr result)
        {
            ROXNormalClient client = IntPtrToRichOXNormalClient(strategyClient);
            if (client != null && client.withdrawCallback != null)
            {
                List<NormalAssetStock> unityAssetList = new List<NormalAssetStock>();
                IntPtr assetListObject = Externs.RichOXNormalStrategyWithdrawResultGetAssetStatusArray(result);
                if(assetListObject != null) {
                    int size = (int)Externs.RichOXNormalStrategyTypeAssetStatusArrayGetCount(assetListObject);
                    for (int i=0; i<size; i++) {
                        IntPtr assetObject = Externs.RichOXNormalStrategyTypeAssetStatusArrayGetItem(assetListObject, i);
                        NormalAssetStock stock = new NormalAssetStock();
                        stock.AssetName = Externs.RichOXNormalStrategyAssetStatusGetName(assetObject);
                        stock.AssetAmount = Externs.RichOXNormalStrategyAssetStatusGetAmount(assetObject);
                        unityAssetList.Add(stock);
                    }
                }

                client.withdrawCallback.OnSuccess(unityAssetList);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        public static void withdrawFailedCallback(IntPtr strategyClient, int code, string message) {
            ROXNormalClient client = IntPtrToRichOXNormalClient(strategyClient);
            if (client != null && client.withdrawCallback != null)
            {
                client.withdrawCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXGetNormalStrategyWithdrawCallback))]
        public static void globalWithdrawSuccessCallback(IntPtr strategyClient, IntPtr result)
        {
            ROXNormalClient client = IntPtrToRichOXNormalClient(strategyClient);
            if (client != null && client.globalWithdrawCallback != null)
            {
                List<NormalAssetStock> unityAssetList = new List<NormalAssetStock>();
                IntPtr assetListObject = Externs.RichOXNormalStrategyWithdrawResultGetAssetStatusArray(result);
                if(assetListObject != null) {
                    int size = (int)Externs.RichOXNormalStrategyTypeAssetStatusArrayGetCount(assetListObject);
                    for (int i=0; i<size; i++) {
                        IntPtr assetObject = Externs.RichOXNormalStrategyTypeAssetStatusArrayGetItem(assetListObject, i);
                        NormalAssetStock stock = new NormalAssetStock();
                        stock.AssetName = Externs.RichOXNormalStrategyAssetStatusGetName(assetObject);
                        stock.AssetAmount = Externs.RichOXNormalStrategyAssetStatusGetAmount(assetObject);
                        unityAssetList.Add(stock);
                    }
                }

                client.globalWithdrawCallback.OnSuccess(unityAssetList);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        public static void globalWithdrawFailedCallback(IntPtr strategyClient, int code, string message) {
            ROXNormalClient client = IntPtrToRichOXNormalClient(strategyClient);
            if (client != null && client.globalWithdrawCallback != null)
            {
                client.globalWithdrawCallback.OnFailed(code, message);
            }
        }

        #endregion

        private static ROXNormalClient IntPtrToRichOXNormalClient(IntPtr strategyClient)
        {
            GCHandle handle = (GCHandle)strategyClient;
            return handle.Target as ROXNormalClient;
        }
    }

    
}