using TaurusXAdSdk.Common;
using TaurusXAdSdk.Platforms;

namespace TaurusXAdSdk.Api
{
    public static class TaurusXConfigUtil
    {
        static ITaurusXConfigUtilClient mClient = ClientFactory.TaurusXConfigUtilClient();

        public static string GetAppId()
        {
            return mClient.GetAppId();
        }

        public static string GetAdUnitId(string name)
        {
            return mClient.GetAdUnitId(name);
        }

        public static string GetChannel()
        {
            return mClient.GetChannel();
        }

        public static string GetString(string name)
        {
            return mClient.GetString(name);
        }
    }
}
