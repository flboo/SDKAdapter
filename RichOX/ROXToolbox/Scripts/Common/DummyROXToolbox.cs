using ROXToolbox.Api;
using ROXBase.Api;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ROXToolbox.Common
{
    public class DummyROXToolbox : IROXToolbox
    {

        #region IROXToolbox

        public void QueryPiggyBankList(ROXInterface<List<PiggyBank>> callback)
        {
            // TODO
        }

        public void PiggyBankWithdraw(int piggyId, ROXInterface<bool> callback)
        {
            // TODO
        }

        public void Init()
        {
            // TODO
        }

        public void SetInterval(int interval)
        {
            // TODO
        }

        public void GetGroupInfo(ROXInterface<List<GroupInfo>> callback)
        {
            // TODO
        }

        public void GetMessageList(string groupId, int size, ROXInterface<List<ChatMessage>> callback)
        {
            // TODO
        }

        public void PostChatMessage(string groupId, string nickName, string avatar, string type, string content, ROXInterface<ChatMessage> callback)
        {
            // TODO
        }

        public void SavePrivacyData(string key, string value, ROXInterface<Boolean> callback)
        {
            // TODO 
        }
        public void QueryPrivacyData(string key, ROXInterface<PrivacyInfo> callback)
        {
            // TODO
        }

        public void QueryPrivacyDatas(List<string> keys, ROXInterface<List<PrivacyInfo>> callback)
        {
            // TODO
        }

        #endregion
    }

}