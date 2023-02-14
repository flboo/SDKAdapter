using UnityEngine;
using TGCenterSdk.Common;

namespace TGCWeChat.Platforms
{
    public static class ClientFactory {

        public static IWeChatHelperClient WeChatHelperClient() {
            if (Application.platform == RuntimePlatform.Android) {
                return new TGCenterSdk.Platforms.Android.WeChatHelperClient();
            } else if (Application.platform == RuntimePlatform.IPhonePlayer) {
                return new TGCenterSdk.Platforms.iOS.WeChatHelperClient();
            } else {
                return new DummyWeChatHelperClient();
            }
        }
    }
}