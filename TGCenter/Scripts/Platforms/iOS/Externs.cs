using System;
using System.Runtime.InteropServices;

namespace TGCenterSdk.Platforms.iOS
{
    internal class Externs
    {

        #region Common
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCRelease(IntPtr obj);
        #endregion
        

        #region TGCenter
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCInit(IntPtr initConfig);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool TGCIsUserAgreePolicy();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool TGCClearCache();
        #endregion


        #region TGCInitConfig
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr TGCCreateInitConfig();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCSetInitConfig_DebugMode(IntPtr config, bool debugMode);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCSetInitConfig_AppId(IntPtr config, string appId);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCSetInitConfig_AppleAppID(IntPtr config, string appleAppID);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCSetInitConfig_UmengAppKey(IntPtr config, string umengAppKey);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCSetInitConfig_AppsFlyerDevKey(IntPtr config, string appsFlyerDevKey);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCSetInitConfig_RangersAppLogAppId(IntPtr config, string rangersAppLogAppId);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCSetInitConfig_RangersAppLogAppName(IntPtr config, string rangersAppLogAppName);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCSetInitConfig_WeChatAppId(IntPtr config, string weChatAppId);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCSetInitConfig_WeChatUniversalLink(IntPtr config, string weChatUniversalLink);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCSetInitConfig_UdeskDomain(IntPtr config, string udeskDomain);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCSetInitConfig_UdeskAppKey(IntPtr config, string udeskAppKey);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCSetInitConfig_UdeskAppId(IntPtr config, string udeskAppId);

        #endregion


        // #region TGCDay1Retention
        // #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        // [DllImport("__Internal")]
        // #endif
        // internal static extern IntPtr TGCCreateDay1Retention();

        // #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        // [DllImport("__Internal")]
        // #endif
        // internal static extern void TGCSetDay1Retention_Type(IntPtr retention, int type);

        // #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        // [DllImport("__Internal")]
        // #endif
        // internal static extern void TGCSetDay1Retention_StartCount(IntPtr retention, int startCount);

        // #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        // [DllImport("__Internal")]
        // #endif
        // internal static extern void TGCSetDay1Retention_EndCount(IntPtr retention, int endCount);
        // #endregion


        #region PrivacyPolicyHelper
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCPrivacyInit(string appId);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCPrivacyShowDialog(IntPtr clientPtr, 
            PrivacyPolicyHelperClient.TGCPrivacyAgreeCallback agreeCallback,
            PrivacyPolicyHelperClient.TGCPrivacyDisagreeCallback disagreeCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCPrivacyJumpToUserAgreement();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCPrivacyJumpToPrivacyPolicy();
        #endregion
    }
}
