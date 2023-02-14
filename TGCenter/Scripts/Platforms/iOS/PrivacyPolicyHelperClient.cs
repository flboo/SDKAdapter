using System;
using TGCenterSdk.Api;
using TGCenterSdk.Common;
using System.Runtime.InteropServices;
using AOT;

namespace TGCenterSdk.Platforms.iOS
{
    public class PrivacyPolicyHelperClient : IPrivacyPolicyHelperClient {
        
        internal delegate void TGCPrivacyAgreeCallback(IntPtr helperClient);
        internal delegate void TGCPrivacyDisagreeCallback(IntPtr helperClient);


        private IntPtr helperClient;
        private PrivacyPolicyHelper.AgreeListener listener;

        public PrivacyPolicyHelperClient() {
            helperClient = (IntPtr)GCHandle.Alloc(this);
        }


        #region IPrivacyPolicyHelperClient
        public void Init(string appId) {
            Externs.TGCPrivacyInit(appId);
        }

        public void SetAgreeListener(PrivacyPolicyHelper.AgreeListener listener) {
            this.listener = listener;
        }

        public void ShowPrivacyDialog() {
            Externs.TGCPrivacyShowDialog(helperClient, PrivacyAgreeCallback, PrivacyDisagreeCallback);
        }

        public void JumpToUserAgreement() {
            Externs.TGCPrivacyJumpToUserAgreement();
        }

        public void JumpToPrivacyPolicy() {
            Externs.TGCPrivacyJumpToPrivacyPolicy();
        }

        public void HookMacAddress() { }
        
        public void UnHookMacAddress() { }
        #endregion


        #region Callback
        [MonoPInvokeCallback(typeof(TGCPrivacyAgreeCallback))]
        private static void PrivacyAgreeCallback(IntPtr helperClient)
        {
            PrivacyPolicyHelperClient client = IntPtrToPrivacyPolicyHelperClient(helperClient);
            if (client.listener != null) {
                client.listener.OnUserAgree();
            }
        }

        [MonoPInvokeCallback(typeof(TGCPrivacyDisagreeCallback))]
        private static void PrivacyDisagreeCallback(IntPtr helperClient)
        {
            PrivacyPolicyHelperClient client = IntPtrToPrivacyPolicyHelperClient(helperClient);
            if (client.listener != null) {
                client.listener.OnUserDisagree();
            }
        }
        #endregion


        private static PrivacyPolicyHelperClient IntPtrToPrivacyPolicyHelperClient(IntPtr helperClient)
        {
            GCHandle handle = (GCHandle)helperClient;
            return handle.Target as PrivacyPolicyHelperClient;
        }
    }    
}