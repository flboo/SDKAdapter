using TGCenterSdk.Common;
using TGCenterSdk.Platforms;

namespace TGCenterSdk.Api
{
    public static class TGCenter {

        private static ITGCenterClient client = GetTGCenterClient();

        private static ITGCenterClient GetTGCenterClient() {
            return ClientFactory.TGCenterClient();
        }

        /**
         * Init TGCenter SDK.
         * @param config InitConfig
         */
        public static void Init(InitConfig config) {
            client.Init(config);
        }

        /**
         * Detect whether user has agree User Agreement & Privacy Policy.
         * @return true: has agree, do Init; false: show policy dialog to user.
         */
        public static bool IsUserAgreePolicy() {
            return client.IsUserAgreePolicy();
        }

        /**
         * Clear cache of TGCenter SDK.
         */
        public static void ClearCache() {
            client.ClearCache();
        }

        public static void ShowDebugPage() {
            client.ShowDebugPage();
        }
    }
}