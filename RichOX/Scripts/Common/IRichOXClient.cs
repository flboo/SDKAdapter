using ROXBase.Api;
using System;

namespace ROXBase.Common
{
    public interface IRichOXClient
    {
        void SetDeviceId(string deviceId);
       
        void SetPlatformId(string platformId);
       
        void SetHostUrl(string hostUrl);
       
        void SetAppKey(string appKey);
       
        void SetChannel(string channel);

        void SetExtendInfo(string extendInfo);

        void SetOversea(bool oversea);
       
        void SetVersionCode(int code);

        void Init(string appId, ROXInterface<bool> callback);

        void Init(ROXParams richOXParams);

        void SetTestMode(bool testMode);

        string GetAppId();

        string GetDeviceId();

        string GetUserId();
 
        string GetPlatformId();
 
        string GetChannel();

        string GetFissionHostUrl();

        string GetFissionKey();

        string GetWDExtendInfo();

        void SetUserId(string userId);

        string GenDefaultDeviceId();

        void ReportAppEvent(string eventName);

        void ReportAppEvent(string eventName, string eventValue);

        void QueryEventValue(string eventName, ROXInterface<string> callback);

        event EventHandler<RichOXEventArgs> OnEvent;
    }
}