using UnityEngine;
using TGCenterSdk.Api;
using TGCenterSdk.Common;

namespace TGCenterSdk.Platforms.Android
{
    public class PrivacyPolicyHelperClient : AndroidJavaProxy, IPrivacyPolicyHelperClient {
        
        private AndroidJavaObject helper;
        private PrivacyPolicyHelper.AgreeListener listener;
        public PrivacyPolicyHelperClient() : base((Utils.Class_IPrivacyPolicyCallback)) {
            AndroidJavaObject builder = new AndroidJavaObject(Utils.Class_PrivacyPolicyHelperBuilder, Utils.GetPlayerActivity());
            builder.Call<AndroidJavaObject>("callback", this);
            helper = builder.Call<AndroidJavaObject>("build");
        }

        #region IPrivacyPolicyHelperClient
        public void Init(string appId) { }
        
        public void SetAgreeListener(PrivacyPolicyHelper.AgreeListener listener) {
            this.listener = listener;
        }
        public void ShowPrivacyDialog() {
            helper.Call("showDialog");
        }
        public void JumpToUserAgreement() {
            helper.Call("jumpToUserAgreement");
        }
        public void JumpToPrivacyPolicy() {
            helper.Call("jumpToPrivacyPolicy");
        }

        public void HookMacAddress() {
            GetHookUtils().CallStatic("hookMacAddress", "PrivacyDialog", Utils.GetPlayerActivity());
        }
        public void UnHookMacAddress() {
            GetHookUtils().CallStatic("unHookMacAddress", "PrivacyDialog", Utils.GetPlayerActivity());
        }

        public AndroidJavaClass GetHookUtils() {
            return new AndroidJavaClass(Utils.Class_HookUtils);
        }
        #endregion

        #region IPrivacyPolicyCallback
        public void onUserAgree() {
            if(listener != null) {
                listener.OnUserAgree();
            }
        }

        public void onUserDisagree() {
            if(listener != null) {
                listener.OnUserDisagree();
            }
        }
        #endregion
    }
}