using TGCenterSdk.Common;
using TGCWeChat.Platforms;

namespace TGCenterSdk.Api
{
    public class WeChatHelper
    {

        public interface LoginListener
        {
            // 登录成功
            void LoginSuccess(string code);
            // 登录失败
            void LoginFailed(string result);
            // 取消登录
            void LoginCancel(string result);
        }

        private static WeChatHelper instance = new WeChatHelper();

        public static WeChatHelper Instance
        {
            get
            {
                return instance;
            }
        }

        private IWeChatHelperClient client;
        private WeChatHelper()
        {
            client = ClientFactory.WeChatHelperClient();
        }

        public void SetLoginListener(LoginListener listener)
        {
            client.SetLoginListener(listener);
        }

        public void Login()
        {
            client.Login();
        }
    }
}