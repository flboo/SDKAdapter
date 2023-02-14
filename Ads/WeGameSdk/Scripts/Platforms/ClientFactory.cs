using System;
using WeGameSdk.Common;

namespace WeGameSdk.Platforms
{
    public class ClientFactory
    {
        public ClientFactory()
        {
        }

        public static IWeGameSdkClient GetWeGameSdkClient()
        {
            #if UNITY_EDITOR
                return new DummyWeGameSdkClient();
            #elif UNITY_ANDROID
                return WeGameSdk.Platforms.Android.WeGameSdkClient.Instance;
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new DummyWeGameSdkClient();
            #else
                return new DummyWeGameSdkClient();
            #endif
        }
    }
}