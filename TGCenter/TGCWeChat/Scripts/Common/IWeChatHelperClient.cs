using TGCenterSdk.Api;

namespace TGCenterSdk.Common
{
    public interface IWeChatHelperClient {

        void SetLoginListener(WeChatHelper.LoginListener listener);

        void Login();
    }
}