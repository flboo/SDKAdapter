using AntiAddictionSdk.Common;

namespace AntiAddictionSdk.Api
{
    public class HealthGameTip {
        private IHealthGameTipClient Client;
        public HealthGameTip(IHealthGameTipClient client) {
            Client = client;
        }

        // 居中展示的提示内容
        public string GetTipText() {
            return Client.GetTipText();
        }

        // 底部展示的产品信息
        public string GetAppInfoText() {
            return Client.GetAppInfoText();
        }
    }
}