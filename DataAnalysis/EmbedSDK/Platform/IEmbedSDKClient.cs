using System;
using System.Collections.Generic;
using Qarth;

namespace EmbedSDK.Platforms
{
    public interface IEmbedSDKClient
    {
        void Init(EmbedSDKConfig config);
        void SetWhitelist(List<string> evtNames);
        void SetBlacklist(List<string> evtNames);
        void SetUserProperty(string key, string value);
        void SetUserAppId(string userAppId);
        void ReportSignUpSuccess(string type);
        void ReportSignUpFailed(string type, string description);
        void ReportLogInSuccess(string type);
        void ReportLogInFailed(string type, string description);
        void ReportLogOutSuccess(string context);
        void ReportLogOutFailed(string description);
        void ReportUserADReward(string itemId, string itemType, string description, string value);
        void ReportCustomEvent(string eventName, Dictionary<string, string> eventValues);
        void SetEventProperty(string key, string value);
        void RemoveEventProperty(string key);
    }
}
