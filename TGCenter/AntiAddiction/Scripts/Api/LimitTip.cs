using AntiAddictionSdk.Common;

namespace AntiAddictionSdk.Api
{
    // 达到时间限制时候的提示
    public class LimitTip {
        private ILimitTipClient Client;
        public LimitTip(ILimitTipClient client) {
            Client = client;
        }

        // 标题
        public string GetTitle() {
            return Client.GetTitle();
        }

        // 描述
        public string GetDesc() {
            return Client.GetDesc();
        }

        // 按钮
        public string GetButton() {
            return Client.GetButton();
        }

        // 是否能够实名认证，如果能，还需要再显示一个“去实名”的按钮
        public bool CanRealName() {
            return Client.CanRealName();
        }
    }
}