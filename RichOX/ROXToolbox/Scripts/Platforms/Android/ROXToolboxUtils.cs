using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROXToolbox.Api;
using System;
using System.Runtime.InteropServices;

namespace ROXToolbox.Platforms.Android
{
    public static class ROXToolboxUtils
    {
        public static List<PiggyBank> generatePiggyBankList(AndroidJavaObject androidObject)
        {
            List<PiggyBank> unityAssetList = new List<PiggyBank>();
            int size = androidObject.Call<int>("size");
            for (int i = 0; i < size; i++)
            {
                AndroidJavaObject bankObject = androidObject.Call<AndroidJavaObject>("get", i);
                PiggyBank piggyBank = new PiggyBank();
                piggyBank.PiggyBankId = bankObject.Call<int>("getPiggyBankId");
                piggyBank.AppId = bankObject.Call<string>("getAppId");
                piggyBank.PiggyBankName = bankObject.Call<string>("getPiggyBankName");
                piggyBank.UserId = bankObject.Call<string>("getUserId");
                piggyBank.ToAssetName = bankObject.Call<string>("getToAssetName");
                piggyBank.SrcPrizeAmount = bankObject.Call<int>("getSrcPrizeAmount");
                piggyBank.ToPrizeAmount = bankObject.Call<int>("getToPrizeAmount");
                piggyBank.PrizeAmount = bankObject.Call<double>("getPrizeAmount");
                piggyBank.UpdateTimeMS = bankObject.Call<long>("getUpdateTimeMS");
                piggyBank.LastWithdrawTimeMS = bankObject.Call<long>("getLastWithdrawTimeMS");
                unityAssetList.Add(piggyBank);
            }
            return unityAssetList;
        }

        public static ChatMessage generateChatMessage(AndroidJavaObject androidObject)
        {
            if (androidObject == null)
            {
                return null;
            }
            ChatMessage chatMessage = new ChatMessage();
            chatMessage.GroupId = androidObject.Call<string>("getGroupId");
            chatMessage.MessageContent = androidObject.Call<string>("getMessageContent");
            chatMessage.MessageId = androidObject.Call<long>("getMessageId");
            chatMessage.MessageTime = androidObject.Call<long>("getMessageTime");
            chatMessage.MessageType = androidObject.Call<string>("getMessageType");
            chatMessage.SenderId = androidObject.Call<string>("getSenderId");
            chatMessage.SenderName = androidObject.Call<string>("getSenderName");
            chatMessage.SenderImage = androidObject.Call<string>("getSenderImage");
            chatMessage.Status = androidObject.Call<int>("getStatus");
            return chatMessage;
        }

        public static List<ChatMessage> generateChatMessageList(AndroidJavaObject androidObject)
        {
            List<ChatMessage> unityChatMessageList = new List<ChatMessage>();
            int size = androidObject.Call<int>("size");
            for (int i = 0; i < size; i++)
            {
                AndroidJavaObject chatMessageObject = androidObject.Call<AndroidJavaObject>("get", i);
                ChatMessage chatMessage = generateChatMessage(chatMessageObject);
                unityChatMessageList.Add(chatMessage);
            }
            return unityChatMessageList;
        }

        public static List<GroupInfo> generateGroupinfoList(AndroidJavaObject androidObject)
        {
            List<GroupInfo> unityGroupList = new List<GroupInfo>();
            int size = androidObject.Call<int>("size");
            for (int i = 0; i < size; i++)
            {
                AndroidJavaObject groupObject = androidObject.Call<AndroidJavaObject>("get", i);
                GroupInfo groupInfo = new GroupInfo();
                groupInfo.Category = groupObject.Call<string>("getCategory");
                groupInfo.DisplayName = groupObject.Call<string>("getDisplayName");
                groupInfo.GroupId = groupObject.Call<string>("getGroupId");
                AndroidJavaObject lastMessageObject = groupObject.Call<AndroidJavaObject>("getLastMessage");
                groupInfo.LastChatMessage = generateChatMessage(lastMessageObject);
                groupInfo.Name = groupObject.Call<string>("getName");
                groupInfo.Rule = groupObject.Call<int>("getRule");
                unityGroupList.Add(groupInfo);
            }
            return unityGroupList;
        }

        public static PrivacyInfo generatePrivacyInfo(AndroidJavaObject androidObject)
        {
            if (androidObject == null)
            {
                return null;
            }
            PrivacyInfo privacyInfo = new PrivacyInfo();
            privacyInfo.PrivacyKey = androidObject.Call<string>("getPrivacyKey");
            privacyInfo.PrivacyValue = androidObject.Call<string>("getPrivacyValue");
            privacyInfo.CreateTime = androidObject.Call<long>("getCreateTime");
            privacyInfo.UpdateTime = androidObject.Call<long>("getUpdateTime");
            return privacyInfo;
        }

        public static List<PrivacyInfo> generatePrivacyInfoList(AndroidJavaObject androidObject)
        {
            if (androidObject == null)
            {
                return null;
            }
            List<PrivacyInfo> privacyInfos = new List<PrivacyInfo>();
            int size = androidObject.Call<int>("size");
            for (int i = 0; i < size; i++)
            {
                AndroidJavaObject privacyObject = androidObject.Call<AndroidJavaObject>("get", i);
                if (privacyObject != null)
                {
                    PrivacyInfo privacyInfo = new PrivacyInfo();
                    privacyInfo.PrivacyKey = privacyObject.Call<string>("getPrivacyKey");
                    privacyInfo.PrivacyValue = privacyObject.Call<string>("getPrivacyValue");
                    privacyInfo.CreateTime = privacyObject.Call<long>("getCreateTime");
                    privacyInfo.UpdateTime = privacyObject.Call<long>("getUpdateTime");
                    privacyInfos.Add(privacyInfo);
                }
            }
            return privacyInfos;
        }
    }
}