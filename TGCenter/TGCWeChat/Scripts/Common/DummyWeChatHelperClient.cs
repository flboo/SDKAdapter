using TGCenterSdk.Api;

namespace TGCenterSdk.Common
{
    public class DummyWeChatHelperClient : IWeChatHelperClient {
        public void SetLoginListener(WeChatHelper.LoginListener listener) { }
        
        public void Login() { }
    }
}