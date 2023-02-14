using System;
using System.Runtime.InteropServices;

namespace ROXStrategy.Platforms.iOS {
    internal class Externs {
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyRelease(IntPtr strategy);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyGetStrategyInstance(IntPtr client, int strategyId);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXNormalStrategySyncList(IntPtr strategy, ROXNormalClient.RichOXGetNormalStrategySettingCallback callback, 
        ROXNormalClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXNormalStrategyTypeSettingGetStrategyId(IntPtr strategySetting);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXNormalStrategyTypeSettingGetStrategyType(IntPtr strategySetting);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeSettingGetStrategyVersion(IntPtr strategySetting);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeSettingGetStrategyAppId(IntPtr strategySetting);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeSettingGetStrategyName(IntPtr strategySetting);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeSettingGetStrategyPayremark(IntPtr strategySetting);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeSettingGetStrategyStartTime(IntPtr strategySetting);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeSettingGetStrategyEndTime(IntPtr strategySetting);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeSettingGetStrategyABGroup(IntPtr strategySetting);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXNormalStrategyTypeSettingGetStrategyABId(IntPtr strategySetting);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool RichOXNormalStrategyTypeSettingGetStrategyIsIndefinite(IntPtr strategySetting);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyTypeSettingGetStrategyTaskInfo(IntPtr strategySetting);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyTypeTaskInfoGetAssetInfoArray(IntPtr taskInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXNormalStrategyTypeAssetInfoArrayGetCount(IntPtr assetInfoArray);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyTypeAssetInfoArrayGetItem(IntPtr assetInfoArray, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyAssetInfoGetName(IntPtr assetInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern float RichOXNormalStrategyAssetInfoGetExchangeRate(IntPtr assetInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyTypeTaskInfoGetAssetExchangeArrayInfoArray(IntPtr taskInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXNormalStrategyTypeAssetExchangeArrayGetCount(IntPtr assetExchangeArray);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyTypeAssetExchangeInfoArrayGetItem(IntPtr assetExchangeArray, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyAssetExchangeInfoGetExchangeId(IntPtr assetExchange);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyAssetExchangeInfoGetFromAssetName(IntPtr assetExchange);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern float RichOXNormalStrategyAssetExchangeInfoGetFromPrizeAmount(IntPtr assetExchange);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyAssetExchangeInfoGetToAssetName(IntPtr assetExchange);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern float RichOXNormalStrategyAssetExchangeInfoGetToPrizeAmount(IntPtr assetExchange);


        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyTypeTaskInfoGetAssetTaskArrayInfoArray(IntPtr taskInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXNormalStrategyTypeTaskArrayGetCount(IntPtr taskArray);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyTypeTaskArrayGetItem(IntPtr taskArray, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTaskGetTaskId(IntPtr task);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTaskGetName(IntPtr task);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTaskGetAssetName(IntPtr task);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXNormalStrategyTaskGetFrequency(IntPtr task);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXNormalStrategyTaskGetFrequencyType(IntPtr task);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXNormalStrategyTaskGetPrizeType(IntPtr task);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXNormalStrategyTaskGetRewardType(IntPtr task);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern float RichOXNormalStrategyTaskGetPrizeAmount(IntPtr task);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyTypeSettingGetStrategyWithdrawSetting(IntPtr setting);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXNormalStrategyTypeItemArrayGetCount(IntPtr withdrawArray);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyTypeItemArrayGetItem(IntPtr withdrawArray, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeItemGetPackageId(IntPtr withdrawItem);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeItemGetName(IntPtr withdrawItem);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern double RichOXNormalStrategyTypeItemGetAmount(IntPtr withdrawItem);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXNormalStrategyTypeItemGetFrequency(IntPtr withdrawItem);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXNormalStrategyTypeItemGetFrequencyType(IntPtr withdrawItem);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXNormalStrategyTypeItemGetWithdrawType(IntPtr withdrawItem);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeItemGetAssetName(IntPtr withdrawItem);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern float RichOXNormalStrategyTypeItemGetCostAsset(IntPtr withdrawItem);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXNormalStrategyTypeItemGetAmountType(IntPtr withdrawItem);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeItemGetCurrency(IntPtr withdrawItem);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeItemGetWithdrawAmountType(IntPtr withdrawItem);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern double RichOXNormalStrategyTypeItemGetMinCash(IntPtr withdrawItem);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern double RichOXNormalStrategyTypeItemGetMaxCash(IntPtr withdrawItem);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXNormalStrategySyncCurrentPrize(IntPtr strategy, ROXNormalClient.RichOXGetNormalStrategySyncPrizeCallback successCallback, ROXNormalClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeStatusGetUId(IntPtr strategyStatus);
        
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXNormalStrategyTypeStatusGetStrategyId(IntPtr strategyStatus);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeStatusGetABGroup(IntPtr strategyStatus);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXNormalStrategyTypeStatusGetABId(IntPtr strategyStatus);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyTypeStatusGetAssetStatusArray(IntPtr strategyStatus);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXNormalStrategyTypeAssetStatusArrayGetCount(IntPtr assetStatusArray);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyTypeAssetStatusArrayGetItem(IntPtr assetStatusArray, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyAssetStatusGetName(IntPtr assetStatus);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern float RichOXNormalStrategyAssetStatusGetAmount(IntPtr assetStatus);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyTypeStatusGetWithdrawRecordDataArray(IntPtr assetStatus);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXNormalStrategyTypeWithdrawRecordDataArrayGetCount(IntPtr withdrawRecordArray);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyTypeWithdrawRecordDataArrayGetItem(IntPtr withdrawRecordArray, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeWithdrawRecordDataGetTaskId(IntPtr withdrawRecord);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeWithdrawRecordDataGetWithdrawId(IntPtr withdrawRecord);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeWithdrawRecordDataGetUserId(IntPtr withdrawRecord);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeWithdrawRecordDataGetPayRemark(IntPtr withdrawRecord);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeWithdrawRecordDataGetRequestDay(IntPtr withdrawRecord);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeWithdrawRecordDataGetWithdrawChannel(IntPtr withdrawRecord);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTypeWithdrawRecordDataGetStatusComment(IntPtr withdrawRecord);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXNormalStrategyTypeWithdrawRecordDataGetCoin(IntPtr withdrawRecord);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXNormalStrategyTypeWithdrawRecordDataGetStatus(IntPtr withdrawRecord);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern float RichOXNormalStrategyTypeWithdrawRecordDataGetAmount(IntPtr withdrawRecord);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool RichOXNormalStrategyTypeWithdrawRecordDataIsRefunded(IntPtr withdrawRecord);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXNormalStrategyDoMission(IntPtr strategy, string taskId, float prizeAmount, string tid, ROXNormalClient.RichOXGetNormalStrategyDoMissionCallback successCallback, ROXNormalClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTaskResultGetAssetName(IntPtr taskResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern float RichOXNormalStrategyTaskResultGetDeltaPrizeValue(IntPtr taskResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern float RichOXNormalStrategyTaskResultGetTotalPrizeValue(IntPtr taskResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyTaskResultGetGetAssetStatusArray(IntPtr taskResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXNormalStrategyExchangeAsset(IntPtr strategy, string exchangeId, float coin, ROXNormalClient.RichOXGetNormalStrategyAssetExchangeCallback successCallback, ROXNormalClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyExchangeResultGetFromAssetName(IntPtr exchangeResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern float RichOXNormalStrategyExchangeResultGetFromAssetDeltaPrizeValue(IntPtr exchangeResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern float RichOXNormalStrategyExchangeResultGetFromAssetTotalPrizeValue(IntPtr exchangeResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyExchangeResultGetToAssetName(IntPtr exchangeResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern float RichOXNormalStrategyExchangeResultGetToAssetDeltaPrizeValue(IntPtr exchangeResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern float RichOXNormalStrategyExchangeResultGetToAssetTotalPrizeValue(IntPtr exchangeResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyExchangeResultGetAssetStatusArray(IntPtr exchangeResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXNormalStrategyGetTaskProcessInfo(IntPtr strategy, string[] taskIds, int taskIdCount, ROXNormalClient.RichOXGetNormalStrategyGetTaskProcessInfoCallback successCallback, ROXNormalClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXNormalStrategyTaskProcessResultGetUId(IntPtr taskProcessResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyTaskProcessResultGetTaskProcessArray(IntPtr taskProcessResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXNormalStrategyTaskProcessArrayGetCount(IntPtr taskProcessArray);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyTaskProcessArrayGetItem(IntPtr taskProcessArray, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXNormalStrategyTaskProcessGetUpdateTimes(IntPtr taskProcess);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXNormalStrategyTaskProcessGetLastUpdateTime(IntPtr taskProcess);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXNormalStrategyWithdraw(IntPtr strategy, string packetId, IntPtr withdrawInfo, ROXNormalClient.RichOXGetNormalStrategyWithdrawCallback successCallback, ROXNormalClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXNormalStrategyWithdrawResultGetAssetStatusArray(IntPtr withdrawResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXNormalStrategyReplenish(IntPtr strategy, string taskId, IntPtr replenishInfo, ROXNormalClient.RichOXGetNormalStrategyWithdrawCallback successCallback, ROXNormalClient.RichOXUnityFailureCallback failureCallback);

        
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr createGlobalWithdrawInfo(string walletChannel, string account);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXGlobalWithdrawInfoSetExtendedInfo(IntPtr withdrawInfoPtr, string extendedInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXGlobalWithdrawInfoSetPayRemark(IntPtr withdrawInfoPtr, string payRemark);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXGlobalWithdrawInfoSetName(IntPtr withdrawInfoPtr, string name); 

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXGlobalWithdrawInfoSetFirstName(IntPtr withdrawInfoPtr, string firstName);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXGlobalWithdrawInfoSetLastName(IntPtr withdrawInfoPtr, string lastName);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXGlobalWithdrawInfoSetMiddleName(IntPtr withdrawInfoPtr, string middleName);
        
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXNormalStrategyGlobalWithdraw(IntPtr strategy, string packetId, IntPtr globalWithdrawInfo, ROXNormalClient.RichOXGetNormalStrategyWithdrawCallback successCallback, ROXNormalClient.RichOXUnityFailureCallback failureCallback);

    }
}