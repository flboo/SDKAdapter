using UnityEngine;
using TGCenterSdk.Common;

namespace TGCUdesk.Platforms
{
    public static class ClientFactory {

        public static IUdeskHelperClient UdeskHelperClient() {
            if (Application.platform == RuntimePlatform.Android) {
                return new TGCenterSdk.Platforms.Android.UdeskHelperClient();
            } else if (Application.platform == RuntimePlatform.IPhonePlayer) {
                return new TGCenterSdk.Platforms.iOS.UdeskHelperClient();
            } else {
                return new DummyUdeskHelperClient();
            }
        }
    }
}