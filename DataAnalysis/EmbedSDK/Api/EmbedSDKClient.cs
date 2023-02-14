using System;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using EmbedSDK.Platforms;

namespace EmbedSDK.Api
{
    public class EmbedSDKClient
    {
        private IEmbedSDKClient mClient;
        public EmbedSDKClient()
        {
            mClient = ClientFactory.GetEmbedSDKClient();
        }

        #region api
        public void Init(EmbedSDKConfig config)
        {
            mClient.Init(config);
        }

        public void SetWhitelist(List<string> evtNames)
        {
            mClient.SetWhitelist(evtNames);
        }

        public void SetBlacklist(List<string> evtNames)
        {
            mClient.SetBlacklist(evtNames);
        }

        #region 通用属性
        public void SetUserProperty(string key, string value)
        {
            mClient.SetUserProperty(key, value);
        }

        public void SetUserAppId(string userAppId)
        {
            mClient.SetUserAppId(userAppId);
        }
        #endregion

        #region 用户体系事件
        public void ReportSignUpSuccess(string type)
        {
            mClient.ReportSignUpSuccess(type);
        }

        public void ReportSignUpFailed(string type, string description)
        {
            mClient.ReportSignUpFailed(type, description);
        }

        public void ReportLogInSuccess(string type)
        {
            mClient.ReportLogInSuccess(type);
        }

        public void ReportLogInFailed(string type, string description)
        {
            mClient.ReportLogInFailed(type, description);
        }

        public void ReportLogOutSuccess(string context)
        {
            mClient.ReportLogOutSuccess(context);
        }

        public void ReportLogOutFailed(string description)
        {
            mClient.ReportLogOutFailed(description);
        }
        #endregion


        #region 其他事件
        public void ReportUserADReward(string itemId, string itemType, string description, string value)
        {
            mClient.ReportUserADReward(itemId, itemType, description, value);
        }

        public void ReportCustomEvent(string eventName, Dictionary<string, string> eventValues)
        {
            mClient.ReportCustomEvent(eventName, eventValues);
        }
        #endregion

        #region 事件属性
        public void SetEventProperty(string key, string value)
        {
            mClient.SetEventProperty(key, value);
        }

        public void RemoveEventProperty(string key)
        {
            mClient.RemoveEventProperty(key);
        }
        #endregion

        #endregion
    }

}