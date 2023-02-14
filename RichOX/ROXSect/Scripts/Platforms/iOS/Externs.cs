using System;
using System.Runtime.InteropServices;

namespace ROXSect.Platforms.iOS {
    internal class Externs {
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectRelease(IntPtr sect);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetSectInfo(ROXSectClientManager.RichOXSectGetSectInfoCallback callback, 
        ROXSectClientManager.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXSectDataTypeGetMasterUId(IntPtr sectdata);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXSectDataTypeGetTeacherUId(IntPtr sectdata);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectDataTypeGetContribution(IntPtr sectdata);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectDataTypeGetLevel(IntPtr sectdata);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectDataTypeGetInviteApprenticeCount(IntPtr sectdata);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectDataTypeGetVerifiedApprenticeCount(IntPtr sectdata);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool RichOXSectDataTypeGetVerified(IntPtr sectdata);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectDataTypeGetTransformCount(IntPtr sectdata);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectDataTypeGetTimesPacketCount(IntPtr sectdata);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectDataTypeGetInviteAwardInfo(IntPtr sectdata);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectDataTypeGetApprenticeList(IntPtr sectdata);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectDictionaryTypeGetApprenticeListValue(IntPtr dicPtr, string key);
        
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetSectStatus(ROXSectClientManager.RichOXSectGetSectStatusCallback callback, 
        ROXSectClientManager.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetApprenticeList(int level, int pageSize, int currentPage, ROXSectClientManager.RichOXSectGetApprenticeListCallback callback, 
        ROXSectClientManager.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectApprenticeListTypeGetTotal(IntPtr apprenticeList);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectApprenticeListTypeGetPageSize(IntPtr apprenticeList);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectApprenticeListTypeGetCurrentPage(IntPtr apprenticeList);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectApprenticeListTypeGetApprenticeDataArray(IntPtr apprenticeList);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectApprenticeDataTypeArrayGetCount(IntPtr apprenticeDataArray);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectApprenticeDataTypeArrayGetItem(IntPtr apprenticeDataArray, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXSectApprenticeDataGetApprenticeUId(IntPtr apprenticeData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectApprenticeDataGetContribution(IntPtr apprenticeData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectApprenticeDataGetTotalContribution(IntPtr apprenticeData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool RichOXSectApprenticeDataGetVerified(IntPtr apprenticeData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectApprenticeDataGetDailyContribution(IntPtr apprenticeData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXSectApprenticeDataGetNickName(IntPtr apprenticeData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXSectApprenticeDataGetAvatar(IntPtr apprenticeData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectDictionaryTypeGetAllKeys(IntPtr dicPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectArrayTypeGetCount(IntPtr arrayPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXSectArrayTypeGetKeyItem(IntPtr arrayPtr, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectDictionaryTypeGetIntValue(IntPtr dicPtr, string key);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXSectDictionaryTypeGetStringValue(IntPtr dicPtr, string key);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectDictionaryTypeGetDicValue(IntPtr dicPtr, string key);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetApprenticeInfo(string apprenticeUid, ROXSectClientManager.RichOXSectGetApprenticeInfoCallback callback, 
        ROXSectClientManager.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXSectApprenticeInfoTypeGetTeacherUId(IntPtr apprenticeInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectApprenticeInfoTypeGetLevel(IntPtr apprenticeInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectApprenticeInfoTypeGetInvitedApprenticeCount(IntPtr apprenticeInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetSetting(ROXSectClientManager.RichOXSectGetSettingCallback callback, 
        ROXSectClientManager.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectSettingDataTypeGetHierarchy(IntPtr settingPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectSettingDataTypeGetTransformContribution(IntPtr settingPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectSettingDataTypeGetMaxPoolContribution(IntPtr settingPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectSettingDataTypeGetGrades(IntPtr settingPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectArrayTypeGetIntItem(IntPtr arrayPtr, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectSettingDataTypeGetInviteAwardsSetting(IntPtr settingPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectInviteAwardsSettingDataArrayTypeGetCount(IntPtr arrayPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectInviteAwardsSettingDataArrayTypeGetItem(IntPtr arrayPtr, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectInviteAwardsSettingDataTypeGetCount(IntPtr awardsSettingData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectInviteAwardsSettingDataTypeGetAwardType(IntPtr awardsSettingData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern double RichOXSectInviteAwardsSettingDataTypeGetAwardValue(IntPtr awardsSettingData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectSettingDataTypeGetTransfromSteps(IntPtr settingPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectTransformStepArrayTypeGetCount(IntPtr stepArrayPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectTransformStepArrayTypeGetItem(IntPtr stepArrayPtr, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectTransformStepTypeGetStep(IntPtr stepPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectTransformStepTypeGetTransformContribution(IntPtr stepPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGenContribution(int actionId, ROXSectClientManager.RichOXSectGenContributionCallback callback, 
        ROXSectClientManager.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetContribution(string studentUid, ROXSectClientManager.RichOXSectGetContributionCallback callback, 
        ROXSectClientManager.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetInviteCount(long sinceTime, bool onlyVerified, ROXSectClientManager.RichOXSectGetInviteCountCallback callback, 
        ROXSectClientManager.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetRankingList(ROXSectClientManager.RichOXSectGetRankingListCallback callback, 
        ROXSectClientManager.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectRankingObjectTypeArrayGetCount(IntPtr arrayPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectRankingObjectTypeArrayGetItem(IntPtr arrayPtr, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXSectRankingObjectTypeGetMasterUId(IntPtr rankingPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectRankingObjectTypeGetIndex(IntPtr rankingPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectRankingObjectTypeGetInviteCount(IntPtr rankingPtr);

        //fission sect api
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectGetSectInfo_F(ROXSectClient.RichOXSectGetSectInfoCallback callback, 
        ROXSectClient.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetApprenticeList_F(int level, int pageSize, int currentPage, ROXSectClient.RichOXSectGetApprenticeListCallback callback, 
        ROXSectClient.RichOXFailureCallback failureCallback);

         #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetApprenticeInfo_F(string apprenticeUid, ROXSectClient.RichOXSectGetApprenticeInfoCallback callback, 
        ROXSectClient.RichOXFailureCallback failureCallback); 

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetSetting_F(ROXSectClient.RichOXSectGetSettingCallback callback, 
        ROXSectClient.RichOXFailureCallback failureCallback); 

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGenContribution_F(int actionId, ROXSectClient.RichOXSectGenContributionCallback callback, 
        ROXSectClient.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetContribution_F(string studentUid, ROXSectClient.RichOXSectGetContributionCallback callback, 
        ROXSectClient.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetContributionDayRecord_F(int year, int month, ROXSectClient.RichOXSectGetContributionDayRecordCallback callback, 
        ROXSectClient.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetTransformTimesPacket_F(int count, ROXSectClient.RichOXSectGetTransformTimesPacketCallback callback, 
        ROXSectClient.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern double RichOXSectTransformTimesPacketTypeGetPacket(IntPtr packetPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern double RichOXSectTransformTimesPacketTypeGetCash(IntPtr packetPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern double RichOXSectTransformTimesPacketTypeGetDeltaCash(IntPtr packetPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectTransformTimesPacketTypeGetCount(IntPtr packetPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetRedPacketRecord_F(int pageSize, int curPage, ROXSectClient.RichOXSectGetPacketRecordCallback callback, 
        ROXSectClient.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectRedPacketRecordTypeGetTotal(IntPtr recordPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectRedPacketRecordTypeGetPageSize(IntPtr recordPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectRedPacketRecordTypeGetCurPage(IntPtr recordPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectRedPacketRecordTypeGetRecords(IntPtr recordPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectArrayTypeGetTransformDataItem(IntPtr recordPtr, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXSectTransformDataTypeGetMasterUid(IntPtr dataPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectTransformDataTypeGetPacketType(IntPtr dataPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectTransformDataTypeGetTongLevel(IntPtr dataPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern double RichOXSectTransformDataTypeGetAmount(IntPtr dataPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectTransformDataTypeGetTimesPacketCount(IntPtr dataPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectTransformDataTypeGetTransformCount(IntPtr dataPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXSectTransformDataTypeGetGetTime(IntPtr dataPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectTransform_F(ROXSectClient.RichOXSectTransformCallback callback, 
        ROXSectClient.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectTransformResultTypeGetContribution(IntPtr resultPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectTransformResultTypeGetTransformCount(IntPtr resultPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern double RichOXSectTransformResultTypeGetCash(IntPtr resultPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern double RichOXSectTransformResultTypeGetDeltaCash(IntPtr resultPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectTransformResultTypeGetPackets(IntPtr resultPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern double RichOXSectArrayTypeGetDoubleItem(IntPtr arrayPtr, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetInviteAward_F(int count, ROXSectClient.RichOXSectGetInviteAwardCallback callback, 
        ROXSectClient.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectInviteAwardTypeGetAwardType(IntPtr awardPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectInviteAwardTypeGetContribution(IntPtr awardPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXSectInviteAwardTypeGetDeltaContribution(IntPtr awardPtr);
    
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern double RichOXSectInviteAwardTypeGetCash(IntPtr awardPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetInviteAwardSetting_F(ROXSectClient.RichOXSectGetInviteAwardSettingCallback callback, 
        ROXSectClient.RichOXFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXSectArrayTypeGetInviteAwardsSettingDataItem(IntPtr arrayRef, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXSectGetSectStatus_F(ROXSectClient.RichOXSectGetSectStatusCallback callback, 
        ROXSectClient.RichOXFailureCallback failureCallback);
    }
}