using TaurusXAdSdk.Platforms;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Api
{
    public class ScreenUtil
    {
        private static IScreenUtilClient mClient = GetScreenUtilClient();

        private static IScreenUtilClient GetScreenUtilClient()
        {
            return ClientFactory.ScreenUtilClient();
        }

        public static  bool IsPortrait() {
            return mClient.IsPortrait();
        }

        public static bool IsTablet() {
            return mClient.IsTablet();
        }
    }
}
