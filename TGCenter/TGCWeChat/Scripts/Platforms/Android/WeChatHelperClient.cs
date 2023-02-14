using UnityEngine;
using TGCenterSdk.Api;
using TGCenterSdk.Common;
using TGCWeChat.Platforms.Android;

namespace TGCenterSdk.Platforms.Android
{
    public class WeChatHelperClient : AndroidJavaProxy, IWeChatHelperClient {
        
        private AndroidJavaClass helperClass;
        private WeChatHelper.LoginListener listener;
        public WeChatHelperClient() : base(TGCWeChat.Platforms.Android.Utils.Class_WeChatLoginCallback) {
            helperClass = new AndroidJavaClass(TGCWeChat.Platforms.Android.Utils.Class_WeChatHelper);
            helperClass.CallStatic("setLoginCallback", this);
        }

        #region IWeChatHelperClient
        public void SetLoginListener(WeChatHelper.LoginListener listener) {
            this.listener = listener;
        }
        public void Login() {
            AndroidJavaClass loginType = new AndroidJavaClass(TGCWeChat.Platforms.Android.Utils.Class_WeChatLoginType);
            AndroidJavaObject weChat = loginType.CallStatic<AndroidJavaObject>("valueOf", "Wechat");
            helperClass.CallStatic("login", weChat);
        }
        #endregion

        #region LoginCallback
        public void loginSuccess(string code) {
            if(listener != null) {
                listener.LoginSuccess(code);
            }
        }
        
        public void loginCancel(string result) {
            if(listener != null) {
                listener.LoginCancel(result);
            }
        }

        public void loginFailed(string result) {
            if(listener != null) {
                listener.LoginFailed(result);
            }
        }
        #endregion
    }
}