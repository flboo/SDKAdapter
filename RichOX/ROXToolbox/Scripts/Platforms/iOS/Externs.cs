using System;
using System.Runtime.InteropServices;

namespace ROXToolbox.Platforms.iOS {
    internal class Externs {
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXToolBoxRelease(IntPtr managerPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXToolBoxQueryPiggyBankList(ROXToolboxClient.RichOXToolBoxQueryPiggyBankListCallback successCallback, ROXToolboxClient.RichOXToolBoxFailedCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXToolBoxPiggyBankObjectTypeArrayGetCount(IntPtr arrayPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXToolBoxPiggyBankObjectTypeArrayGetItem(IntPtr arrayPtr, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXToolBoxPiggyBankObjectTypeGetAppId(IntPtr piggyBankPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXToolBoxPiggyBankObjectTypeGetPiggyBankId(IntPtr piggyBankPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXToolBoxPiggyBankObjectTypeGetPiggyBankName(IntPtr piggyBankPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXToolBoxPiggyBankObjectTypeGetUserId(IntPtr piggyBankPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXToolBoxPiggyBankObjectTypeGetToAssetName(IntPtr piggyBankPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXToolBoxPiggyBankObjectTypeGetSrcPrizeAmount(IntPtr piggyBankPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXToolBoxPiggyBankObjectTypeGetToPrizeAmount(IntPtr piggyBankPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern double RichOXToolBoxPiggyBankObjectTypeGetPrizeAmount(IntPtr piggyBankPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXToolBoxPiggyBankObjectTypeGetUpdateTime(IntPtr piggyBankPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXToolBoxPiggyBankObjectTypeGetLastWithdrawTime(IntPtr piggyBankPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXToolBoxPiggyBankWithdraw(int piggyBankId, ROXToolboxClient.RichOXToolBoxCommonSuccessBlock successCallback, ROXToolboxClient.RichOXToolBoxFailedCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXToolBoxChatGroupSetInterval(int interval);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXToolBoxChatGroupInit();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXToolBoxChatGroupGetGroupInfo(ROXToolboxClient.RichOXToolBoxGetGroupInfoCallback successCallback, ROXToolboxClient.RichOXToolBoxFailedCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXToolBoxGroupInfoTypeArrayGetCount(IntPtr groupArrayPtr);
        
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXToolBoxGroupInfoTypeArrayGetItem(IntPtr groupArrayPtr, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXToolBoxGroupInfoTypeGetGroupId(IntPtr groupPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXToolBoxGroupInfoTypeGetCategory(IntPtr groupPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXToolBoxGroupInfoTypeGetDisplayName(IntPtr groupPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXToolBoxGroupInfoTypeGetName(IntPtr groupPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXToolBoxGroupInfoTypeGetRule(IntPtr groupPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXToolBoxGroupInfoTypeGetLastMessage(IntPtr groupPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXToolBoxChatGroupGetChatMessage(string groupId, int count, ROXToolboxClient.RichOXToolBoxGetChatMessageCallback successCallback, ROXToolboxClient.RichOXToolBoxFailedCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXToolBoxChatMessageTypeArrayGetCount(IntPtr msgArrayPtr);
        
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXToolBoxChatMessageTypeArrayGetItem(IntPtr msgArrayPtr, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXToolBoxChatMessageTypeGetGroupId(IntPtr msgPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXToolBoxChatMessageTypeGetContent(IntPtr msgPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXToolBoxChatMessageTypeGetMessageType(IntPtr msgPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXToolBoxChatMessageTypeGetMessageTime(IntPtr msgPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXToolBoxChatMessageTypeGetMessageId(IntPtr msgPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXToolBoxChatMessageTypeGetSenderId(IntPtr msgPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXToolBoxChatMessageTypeGetSenderName(IntPtr msgPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXToolBoxChatMessageTypeGetSenderImage(IntPtr msgPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXToolBoxChatGroupSendChatMessage(string groupId, string nickName, string avatar, string type, string content, ROXToolboxClient.RichOXToolBoxSendMessageCallback successCallback, ROXToolboxClient.RichOXToolBoxFailedCallback failureCallback);
    
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXToolBoxSavePrivacyData(string key, string value, ROXToolboxClient.RichOXToolBoxCommonSuccessBlock successCallback, ROXToolboxClient.RichOXToolBoxFailedCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXToolBoxQueryPrivacyData(string key, ROXToolboxClient.RichOXToolBoxQueryPrivacyDataCallback successCallback, ROXToolboxClient.RichOXToolBoxFailedCallback failureCallback);
    
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserPrivacyDataTypeGetKey(IntPtr userDataPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserPrivacyDataTypeGetValue(IntPtr userDataPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXUserPrivacyDataTypeGetCreateTime(IntPtr userDataPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXUserPrivacyDataTypeGetUpdateTime(IntPtr userDataPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXToolBoxQueryPrivacyDatas(string[] keys, int keySize, ROXToolboxClient.RichOXToolBoxQueryPrivacyDatasCallback successCallback, ROXToolboxClient.RichOXToolBoxFailedCallback failureCallback);
    
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXUserPrivacyDataArrayGetCount(IntPtr arrayPtr);
        
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXUserPrivacyDataArrayGetItem(IntPtr arrayPtr, int index);
    }
}