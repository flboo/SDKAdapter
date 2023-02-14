using System;
using TGCenterSdk.Api;
using TGCenterSdk.Common;
using System.Runtime.InteropServices;
using AOT;
using TGCWeChat.Platforms.iOS;

namespace TGCenterSdk.Platforms.iOS
{
    public class WeChatHelperClient : IWeChatHelperClient {
        
        internal delegate void TGCWeChatLoginSuccessCallback(IntPtr helperClient, string code);
        internal delegate void TGCWeChatLoginFailedCallback(IntPtr helperClient, string result);
        internal delegate void TGCWeChatLoginCancelCallback(IntPtr helperClient, string result);

        private IntPtr helperClient;
        private WeChatHelper.LoginListener listener;

        public WeChatHelperClient() {
            helperClient = (IntPtr)GCHandle.Alloc(this);
            TGCWeChat.Platforms.iOS.Externs.TGCWeChatSetCallback(helperClient,
                                        WeChatLoginSuccessCallback,
                                        WeChatLoginFailedCallback,
                                        WeChatLoginCancelCallback);
        }


        #region IWeChatHelperClient
        public void SetLoginListener(WeChatHelper.LoginListener listener) {
            this.listener = listener;
        }
        public void Login() {
            TGCWeChat.Platforms.iOS.Externs.TGCWeChatLogin();
        }
        #endregion


        #region LoginCallback
        [MonoPInvokeCallback(typeof(TGCWeChatLoginSuccessCallback))]
        private static void WeChatLoginSuccessCallback(IntPtr helperClient, string code)
        {
            WeChatHelperClient client = IntPtrToWeChatHelperClient(helperClient);
            if (client.listener != null) {
                client.listener.LoginSuccess(code);
            }
        }

        [MonoPInvokeCallback(typeof(TGCWeChatLoginFailedCallback))]
        private static void WeChatLoginFailedCallback(IntPtr helperClient, string result)
        {
            WeChatHelperClient client = IntPtrToWeChatHelperClient(helperClient);
            if (client.listener != null) {
                client.listener.LoginFailed(result);
            }
        }

        [MonoPInvokeCallback(typeof(TGCWeChatLoginCancelCallback))]
        private static void WeChatLoginCancelCallback(IntPtr helperClient, string result)
        {
            WeChatHelperClient client = IntPtrToWeChatHelperClient(helperClient);
            if (client.listener != null) {
                client.listener.LoginCancel(result);
            }
        }
        #endregion


        private static WeChatHelperClient IntPtrToWeChatHelperClient(IntPtr helperClient)
        {
            GCHandle handle = (GCHandle)helperClient;
            return handle.Target as WeChatHelperClient;
        }
    }    
}