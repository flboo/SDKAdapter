using ROXToolbox.Api;
using ROXBase.Api;
using System;
using System.Collections.Generic;


namespace ROXToolbox.Common
{
    public interface IROXToolbox
    {
        void QueryPiggyBankList(ROXInterface<List<PiggyBank>> callback);

        void PiggyBankWithdraw(int piggyId, ROXInterface<bool> callback);

        void Init();

        void SetInterval(int interval);

        void GetGroupInfo(ROXInterface<List<GroupInfo>> callback);

        void GetMessageList(string groupId, int size, ROXInterface<List<ChatMessage>> callback);

        void PostChatMessage(string groupId, string nickName, string avatar, string type, string content, ROXInterface<ChatMessage> callback);

        void SavePrivacyData(string key, string value, ROXInterface<Boolean> callback);

        void QueryPrivacyData(string key, ROXInterface<PrivacyInfo> callback);

        void QueryPrivacyDatas(List<string> keys, ROXInterface<List<PrivacyInfo>> callback);

    }
}