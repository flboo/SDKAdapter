using TGCenterSdk.Api;

namespace TGCenterSdk.Common
{
    public interface IPrivacyPolicyHelperClient {

        void Init(string appId);
        void SetAgreeListener(PrivacyPolicyHelper.AgreeListener listener);
        void ShowPrivacyDialog();
        void JumpToUserAgreement();
        void JumpToPrivacyPolicy();

        void HookMacAddress();
        void UnHookMacAddress();
    }
}