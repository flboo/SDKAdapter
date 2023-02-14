using TGCenterSdk.Common;
using TGCUdesk.Platforms;

namespace TGCenterSdk.Api
{
    public class UdeskHelper {

        private static UdeskHelper instance = new UdeskHelper();

        public static UdeskHelper Instance {
            get {
                return instance;
            }
        }

        private IUdeskHelperClient client;
        private UdeskHelper() {
            client = ClientFactory.UdeskHelperClient();
        }

        public void EnterUdesk() {
            client.EnterUdesk();
        }
    }
}