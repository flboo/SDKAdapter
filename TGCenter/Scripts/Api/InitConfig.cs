namespace TGCenterSdk.Api
{
    public class InitConfig {

        public InitConfig() { }


        #region Common Config
        // Show debug log in test process.
        public bool DebugMode;

        // Custom Day1Retention
        public Day1Retention Day1Retention;
        #endregion


        #region Android Config
        // Channel for Umeng, RangersAppLog
        public string Channel;
        #endregion
        

        #region  iOS Config
        // ModooPlay AppId
        public string AppId;
        
        // iOS Apple AppID
        public string AppleAppID;

        // Umeng AppKey
        public string UmengAppKey;

        // AppsFlyer DevKey
        public string AppsFlyerDevKey;

        // RangersAppLog AppId
        public string RangersAppLogAppId;
        // RangersAppLog AppName
        public string RangersAppLogAppName;


        // WeChat Login
        public string WeChatAppId;
        public string WeChatUniversalLink;

        // Udesk
        public string UdeskDomain;
        public string UdeskAppKey;
        public string UdeskAppId;
        #endregion
    }
}