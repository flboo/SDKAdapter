using UnityEngine;
using AntiAddictionSdk.Common;

namespace AntiAddictionSdk.Platforms {
    public static class ClientFactory {
        public static IAntiAddictionClient AntiAddictionClient() {
            if(Application.platform == RuntimePlatform.Android) {
                return new AntiAddictionSdk.Platforms.Android.AntiAddictionClient();
            } else if(Application.platform == RuntimePlatform.IPhonePlayer) {
                return new AntiAddictionSdk.Platforms.iOS.AntiAddictionClient();
            } else {
                return new DummyAntiAddictionClient();
            }
        }
    }
}