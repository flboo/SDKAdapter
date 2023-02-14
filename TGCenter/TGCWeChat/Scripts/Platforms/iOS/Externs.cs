using System;
using System.Runtime.InteropServices;
using TGCenterSdk.Platforms.iOS;

namespace TGCWeChat.Platforms.iOS
{
    internal class Externs
    {
        #region WeChatHelper
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCWeChatSetCallback(IntPtr clientPtr,
                          WeChatHelperClient.TGCWeChatLoginSuccessCallback successCallback,
                          WeChatHelperClient.TGCWeChatLoginFailedCallback failedCallback,
                          WeChatHelperClient.TGCWeChatLoginCancelCallback cancelCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCWeChatLogin();
        #endregion

    }
}
