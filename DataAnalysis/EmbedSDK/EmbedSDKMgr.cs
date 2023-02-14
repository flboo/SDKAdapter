using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmbedSDK.Api;

namespace Qarth
{
    public class EmbedSDKMgr : TSingleton<EmbedSDKMgr>
    {
        private EmbedSDKClient m_Client;

        public void Init(EmbedSDKConfig config)
        {
            m_Client = new EmbedSDKClient();
            m_Client.Init(config);
        }

        public void SetWhitelist(List<string> evtNames)
        {
            m_Client.SetWhitelist(evtNames);
        }

        public void SetBlacklist(List<string> evtNames)
        {
            m_Client.SetBlacklist(evtNames);
        }

        #region 通用属性
        public void SetUserProperty(string key, string value)
        {
            m_Client.SetUserProperty(key, value);
        }

        public void SetUserAppId(string userAppId)
        {
            m_Client.SetUserAppId(userAppId);
        }
        #endregion

        #region 用户体系事件
        public void ReportSignUpSuccess(string type)
        {
            m_Client.ReportSignUpSuccess(type);
        }

        public void ReportSignUpFailed(string type, string description)
        {
            m_Client.ReportSignUpFailed(type, description);
        }

        public void ReportLogInSuccess(string type)
        {
            m_Client.ReportLogInSuccess(type);
        }

        public void ReportLogInFailed(string type, string description)
        {
            m_Client.ReportLogInFailed(type, description);
        }

        public void ReportLogOutSuccess(string context)
        {
            m_Client.ReportLogOutSuccess(context);
        }

        public void ReportLogOutFailed(string description)
        {
            m_Client.ReportLogOutFailed(description);
        }
        #endregion


        #region 其他事件
        public void ReportUserADReward(string itemId, string itemType, string description, string value)
        {
            m_Client.ReportUserADReward(itemId, itemType, description, value);
        }

        public void ReportCustomEvent(string eventName, Dictionary<string, string> eventValues)
        {
            m_Client.ReportCustomEvent(eventName, eventValues);
        }
        #endregion

        #region 事件属性
        public void SetEventProperty(string key, string value)
        {
            m_Client.SetEventProperty(key, value);
        }

        public void RemoveEventProperty(string key)
        {
            m_Client.RemoveEventProperty(key);
        }
        #endregion
    }
}