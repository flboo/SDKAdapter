using AntiAddictionSdk.Common;
using AntiAddictionSdk.Platforms;

namespace AntiAddictionSdk.Api
{
    public static class AntiAddiction {

        private static IAntiAddictionClient Client = GetAntiAddictionClient();

        private static IAntiAddictionClient GetAntiAddictionClient() {
            return ClientFactory.AntiAddictionClient();
        }

        // 获取当前的用户信息
        public static User GetUser() {
            return Client.GetUser();
        }
        // 实名认证，使用 SDK 默认的弹窗
        public static void RealName(RealNameCallback callback) {
            Client.RealName(callback);
        }
        // 实名认证，使用 App 的弹窗
        public static void RealName(string name, string idNumber, RealNameCallback callback) {
            Client.RealName(name, idNumber, callback);
        }
        // 是否自动展示时间限制页面，如果不自动展示，App 可以监听 TimeLimitCallback 后自己展示
        public static void SetAutoShowTimeLimitPage(bool show) {
            Client.SetAutoShowTimeLimitPage(show);
        }
        // 注册时间限制回调，App 可以监听此回调，然后自己提示给用户
        public static void SetTimeLimitCallback(TimeLimitCallback callback) {
            Client.SetTimeLimitCallback(callback);
        }
        // // 检查并返回当前时长限制状态
        // public static TimeLimit CheckTimeLimitStatus() {
        //     return Client.CheckTimeLimitStatus();
        // }
        // 用户登出后清除本地数据
        public static void Logout() {
            Client.Logout();
        }

        
        // 获取适龄提示内容
        public static AgeTip GetAgeTip() {
            return Client.GetAgeTip();
        }
        // 展示适龄提示页面
        public static void ShowAgeTipPage(AgeTipCallback callback) {
            Client.ShowAgeTipPage(callback);
        }

        // 获取健康游戏提示内容
        public static HealthGameTip GetHealthGameTip() {
            return Client.GetHealthGameTip();
        }
        // 展示健康游戏提示
        public static void ShowHealthGameTipPage(HealthGameTipCallback callback) {
            Client.ShowHealthGameTipPage(callback);
        }
    }
}