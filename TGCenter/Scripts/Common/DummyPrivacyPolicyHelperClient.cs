using TGCenterSdk.Api;

namespace TGCenterSdk.Common
{
    public class DummyPrivacyPolicyHelperClient : IPrivacyPolicyHelperClient {
        
        public void Init(string appId) { }
        public void SetAgreeListener(PrivacyPolicyHelper.AgreeListener listener) { }
        public void ShowPrivacyDialog() { }
        public void JumpToUserAgreement() { }
        public void JumpToPrivacyPolicy() { }

        public void HookMacAddress() { }
        public void UnHookMacAddress() { }
    }
}