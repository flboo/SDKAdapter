
using System;
using ROXBase.Api;
using ROXToolbox.Api;
using ROXToolbox.Common;
using AOT;
using System.Collections.Generic;

namespace ROXToolbox.Platforms.iOS
{
    public class ROXToolboxClient : IROXToolbox
    {
        static ROXToolboxClient sInstance = new ROXToolboxClient();

        public static ROXToolboxClient Instance
        {
            get
            {
                return sInstance;
            }
        }

        #region callback types

        internal delegate void RichOXToolBoxQueryPiggyBankListCallback(IntPtr arrayPtr);

        internal delegate void RichOXToolBoxCommonSuccessBlock();
        internal delegate void RichOXToolBoxFailedCallback(int code, string message);
        internal delegate void RichOXToolBoxGetGroupInfoCallback(IntPtr arrayPtr);
        internal delegate void RichOXToolBoxGetChatMessageCallback(IntPtr arrayPtr);
        internal delegate void RichOXToolBoxSendMessageCallback(IntPtr messagePtr);
        internal delegate void RichOXToolBoxQueryPrivacyDataCallback(IntPtr dataPtr);
        internal delegate void RichOXToolBoxQueryPrivacyDatasCallback(IntPtr dataPtr);

        #endregion

        private ROXInterface<List<PiggyBank>> OnGetPiggyBankListCallback;
        private ROXInterface<bool> OnWithdrawCallback;
        private ROXInterface<List<GroupInfo>> OnGetGroupInfoCallback;
        private ROXInterface<List<ChatMessage>> OnGetChatMessageCallback;
        private ROXInterface<ChatMessage> OnSendChatMessageCallback;
        private ROXInterface<Boolean> OnSavePrivacyDataCallback;
        private ROXInterface<PrivacyInfo> OnQueryPrivacyDataCallback;
        private ROXInterface<List<PrivacyInfo>> OnQueryPrivacyDatasCallback;

        public void QueryPiggyBankList(ROXInterface<List<PiggyBank>> callback)
        {
            OnGetPiggyBankListCallback = callback;
            Externs.RichOXToolBoxQueryPiggyBankList(getPiggyBankListSuccessCallback, getPiggyBankListFailedCallback);
        }

        public void PiggyBankWithdraw(int piggyId, ROXInterface<bool> callback)
        {
            OnWithdrawCallback = callback;
            Externs.RichOXToolBoxPiggyBankWithdraw(piggyId, withdrawSuccessCallback, withdrawFailedCallback);
        }

        public void Init()
        {
            Externs.RichOXToolBoxChatGroupInit();
        }

        public void SetInterval(int interval)
        {
            Externs.RichOXToolBoxChatGroupSetInterval(interval);
        }

        public void GetGroupInfo(ROXInterface<List<GroupInfo>> callback)
        {
            OnGetGroupInfoCallback = callback;
            Externs.RichOXToolBoxChatGroupGetGroupInfo(getGroupInfoSuccessCallback, getGroupInfoFailedCallback);
        }

        public void GetMessageList(string groupId, int size, ROXInterface<List<ChatMessage>> callback)
        {
            OnGetChatMessageCallback = callback;
            Externs.RichOXToolBoxChatGroupGetChatMessage(groupId, size, getChatMessageSuccessCallback, getChatMessageFailedCallback);
        }

        public void PostChatMessage(string groupId, string nickName, string avatar, string type, string content, ROXInterface<ChatMessage> callback)
        {
            OnSendChatMessageCallback = callback;
            Externs.RichOXToolBoxChatGroupSendChatMessage(groupId, nickName, avatar, type, content, sendChatMessageSuccessCallback, sendChatMessageFailedCallback);
        }

        public void SavePrivacyData(string key, string value, ROXInterface<Boolean> callback)
        {
            OnSavePrivacyDataCallback = callback;
            Externs.RichOXToolBoxSavePrivacyData(key, value, savePrivacyDataSuccessCallback, savePrivacyDataFailedCallback);
        }
        public void QueryPrivacyData(string key, ROXInterface<PrivacyInfo> callback)
        {
            OnQueryPrivacyDataCallback = callback;
            Externs.RichOXToolBoxQueryPrivacyData(key, queryPrivacyDataSuccessCallback, queryPrivacyDataFailedCallback);
        }

        public void QueryPrivacyDatas(List<string> keys, ROXInterface<List<PrivacyInfo>> callback)
        {
            OnQueryPrivacyDatasCallback = callback;
            Externs.RichOXToolBoxQueryPrivacyDatas(keys.ToArray(), keys.Count, queryPrivacyDatasSuccessCallback, queryPrivacyDatasFailedCallback);
        }

        private static ChatMessage transChatMessage(IntPtr messagePtr)
        {
            ChatMessage message = new ChatMessage();
            message.GroupId = Externs.RichOXToolBoxChatMessageTypeGetGroupId(messagePtr);
            message.MessageContent = Externs.RichOXToolBoxChatMessageTypeGetContent(messagePtr);
            message.MessageType = Externs.RichOXToolBoxChatMessageTypeGetMessageType(messagePtr);
            message.MessageTime = Externs.RichOXToolBoxChatMessageTypeGetMessageTime(messagePtr);
            message.MessageId = Externs.RichOXToolBoxChatMessageTypeGetMessageId(messagePtr);
            message.SenderId = Externs.RichOXToolBoxChatMessageTypeGetSenderId(messagePtr);
            message.SenderName = Externs.RichOXToolBoxChatMessageTypeGetSenderName(messagePtr);
            message.SenderImage = Externs.RichOXToolBoxChatMessageTypeGetSenderImage(messagePtr);

            return message;
        }

        #region callback implement

        [MonoPInvokeCallback(typeof(RichOXToolBoxQueryPiggyBankListCallback))]
        private static void getPiggyBankListSuccessCallback(IntPtr arrayPtr)
        {
            if (Instance.OnGetPiggyBankListCallback != null)
            {
                List<PiggyBank> unityAssetList = new List<PiggyBank>();
                int size = Externs.RichOXToolBoxPiggyBankObjectTypeArrayGetCount(arrayPtr);
                for (int i = 0; i < size; i++)
                {
                    IntPtr piggyBankObject = Externs.RichOXToolBoxPiggyBankObjectTypeArrayGetItem(arrayPtr, i);
                    PiggyBank piggyBank = new PiggyBank();
                    piggyBank.PiggyBankId = Externs.RichOXToolBoxPiggyBankObjectTypeGetPiggyBankId(piggyBankObject);
                    piggyBank.AppId = Externs.RichOXToolBoxPiggyBankObjectTypeGetAppId(piggyBankObject);
                    piggyBank.PiggyBankName = Externs.RichOXToolBoxPiggyBankObjectTypeGetPiggyBankName(piggyBankObject);
                    piggyBank.UserId = Externs.RichOXToolBoxPiggyBankObjectTypeGetUserId(piggyBankObject);
                    piggyBank.ToAssetName = Externs.RichOXToolBoxPiggyBankObjectTypeGetToAssetName(piggyBankObject);
                    piggyBank.SrcPrizeAmount = Externs.RichOXToolBoxPiggyBankObjectTypeGetSrcPrizeAmount(piggyBankObject);
                    piggyBank.ToPrizeAmount = Externs.RichOXToolBoxPiggyBankObjectTypeGetToPrizeAmount(piggyBankObject);
                    piggyBank.PrizeAmount = Externs.RichOXToolBoxPiggyBankObjectTypeGetPrizeAmount(piggyBankObject);
                    piggyBank.UpdateTimeMS = Externs.RichOXToolBoxPiggyBankObjectTypeGetUpdateTime(piggyBankObject);
                    piggyBank.LastWithdrawTimeMS = Externs.RichOXToolBoxPiggyBankObjectTypeGetLastWithdrawTime(piggyBankObject);
                    unityAssetList.Add(piggyBank);
                }

                Instance.OnGetPiggyBankListCallback.OnSuccess(unityAssetList);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXToolBoxFailedCallback))]
        private static void getPiggyBankListFailedCallback(int code, string message)
        {
            if (Instance.OnGetPiggyBankListCallback != null)
            {
                Instance.OnGetPiggyBankListCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXToolBoxCommonSuccessBlock))]
        private static void withdrawSuccessCallback()
        {
            if (Instance.OnWithdrawCallback != null)
            {
                Instance.OnWithdrawCallback.OnSuccess(true);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXToolBoxFailedCallback))]
        private static void withdrawFailedCallback(int code, string message)
        {
            if (Instance.OnWithdrawCallback != null)
            {
                Instance.OnWithdrawCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXToolBoxGetGroupInfoCallback))]
        private static void getGroupInfoSuccessCallback(IntPtr arrayPtr)
        {
            if (Instance.OnGetGroupInfoCallback != null)
            {
                List<GroupInfo> unityGroupList = new List<GroupInfo>();
                int size = Externs.RichOXToolBoxGroupInfoTypeArrayGetCount(arrayPtr);
                for (int i = 0; i < size; i++)
                {
                    IntPtr groupPtr = Externs.RichOXToolBoxGroupInfoTypeArrayGetItem(arrayPtr, i);
                    GroupInfo group = new GroupInfo();
                    group.GroupId = Externs.RichOXToolBoxGroupInfoTypeGetGroupId(groupPtr);
                    group.Category = Externs.RichOXToolBoxGroupInfoTypeGetCategory(groupPtr);
                    group.DisplayName = Externs.RichOXToolBoxGroupInfoTypeGetDisplayName(groupPtr);
                    group.Name = Externs.RichOXToolBoxGroupInfoTypeGetName(groupPtr);
                    group.Rule = Externs.RichOXToolBoxGroupInfoTypeGetRule(groupPtr);
                    IntPtr lastMessagePtr = Externs.RichOXToolBoxGroupInfoTypeGetLastMessage(groupPtr);
                    if (lastMessagePtr != null)
                    {
                        group.LastChatMessage = transChatMessage(lastMessagePtr);
                    }
                    unityGroupList.Add(group);
                }

                Instance.OnGetGroupInfoCallback.OnSuccess(unityGroupList);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXToolBoxFailedCallback))]
        private static void getGroupInfoFailedCallback(int code, string message)
        {
            if (Instance.OnGetGroupInfoCallback != null)
            {
                Instance.OnGetGroupInfoCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXToolBoxGetChatMessageCallback))]
        private static void getChatMessageSuccessCallback(IntPtr arrayPtr)
        {
            if (Instance.OnGetChatMessageCallback != null)
            {
                List<ChatMessage> unityMsgList = new List<ChatMessage>();
                int size = Externs.RichOXToolBoxChatMessageTypeArrayGetCount(arrayPtr);
                for (int i = 0; i < size; i++)
                {
                    IntPtr msgPtr = Externs.RichOXToolBoxChatMessageTypeArrayGetItem(arrayPtr, i);
                    ChatMessage message = transChatMessage(msgPtr);
                    unityMsgList.Add(message);
                }

                Instance.OnGetChatMessageCallback.OnSuccess(unityMsgList);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXToolBoxFailedCallback))]
        private static void getChatMessageFailedCallback(int code, string message)
        {
            if (Instance.OnGetChatMessageCallback != null)
            {
                Instance.OnGetChatMessageCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXToolBoxSendMessageCallback))]
        private static void sendChatMessageSuccessCallback(IntPtr messagePtr)
        {
            if (Instance.OnSendChatMessageCallback != null)
            {
                ChatMessage message = transChatMessage(messagePtr);
                Instance.OnSendChatMessageCallback.OnSuccess(message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXToolBoxFailedCallback))]
        private static void sendChatMessageFailedCallback(int code, string message)
        {
            if (Instance.OnSendChatMessageCallback != null)
            {
                Instance.OnSendChatMessageCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXToolBoxCommonSuccessBlock))]
        private static void savePrivacyDataSuccessCallback()
        {
            if (Instance.OnSavePrivacyDataCallback != null)
            {
                Instance.OnSavePrivacyDataCallback.OnSuccess(true);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXToolBoxFailedCallback))]
        private static void savePrivacyDataFailedCallback(int code, string message)
        {
            if (Instance.OnSavePrivacyDataCallback != null)
            {
                Instance.OnSavePrivacyDataCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXToolBoxQueryPrivacyDataCallback))]
        private static void queryPrivacyDataSuccessCallback(IntPtr dataPtr)
        {
            if (Instance.OnQueryPrivacyDataCallback != null)
            {
                PrivacyInfo privacyInfo = new PrivacyInfo();
                privacyInfo.PrivacyKey = Externs.RichOXUserPrivacyDataTypeGetKey(dataPtr);
                privacyInfo.PrivacyValue = Externs.RichOXUserPrivacyDataTypeGetValue(dataPtr);
                privacyInfo.CreateTime = Externs.RichOXUserPrivacyDataTypeGetCreateTime(dataPtr);
                privacyInfo.UpdateTime = Externs.RichOXUserPrivacyDataTypeGetUpdateTime(dataPtr);
                Instance.OnQueryPrivacyDataCallback.OnSuccess(privacyInfo);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXToolBoxFailedCallback))]
        private static void queryPrivacyDataFailedCallback(int code, string message)
        {
            if (Instance.OnQueryPrivacyDataCallback != null)
            {
                Instance.OnQueryPrivacyDataCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXToolBoxQueryPrivacyDatasCallback))]
        private static void queryPrivacyDatasSuccessCallback(IntPtr arrayPtr)
        {
            if (Instance.OnQueryPrivacyDatasCallback != null)
            {
                List<PrivacyInfo> infoList = new List<PrivacyInfo>();
                int size = Externs.RichOXUserPrivacyDataArrayGetCount(arrayPtr);
                for (int i = 0; i < size; i++) {
                    IntPtr dataPtr = Externs.RichOXUserPrivacyDataArrayGetItem(arrayPtr, i);
                    PrivacyInfo privacyInfo = new PrivacyInfo();
                    privacyInfo.PrivacyKey = Externs.RichOXUserPrivacyDataTypeGetKey(dataPtr);
                    privacyInfo.PrivacyValue = Externs.RichOXUserPrivacyDataTypeGetValue(dataPtr);
                    privacyInfo.CreateTime = Externs.RichOXUserPrivacyDataTypeGetCreateTime(dataPtr);
                    privacyInfo.UpdateTime = Externs.RichOXUserPrivacyDataTypeGetUpdateTime(dataPtr);
                    infoList.Add(privacyInfo);
                }
                
                Instance.OnQueryPrivacyDatasCallback.OnSuccess(infoList);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXToolBoxFailedCallback))]
        private static void queryPrivacyDatasFailedCallback(int code, string message)
        {
            if (Instance.OnQueryPrivacyDatasCallback != null)
            {
                Instance.OnQueryPrivacyDatasCallback.OnFailed(code, message);
            }
        }

        #endregion
    }
}