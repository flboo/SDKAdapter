using System;
using GameAnalyticsSdk.Common;

namespace GameAnalyticsSdk.Platforms
{
    public class ClientFactory
    {
        public ClientFactory()
        {
        }

        public static IGASdkClient GASdkClientInstance()
        {
            #if UNITY_EDITOR
                return new DummyGASdkClient();
            #elif UNITY_ANDROID
                return GameAnalyticsSdk.Platforms.Android.GASdkClient.Instance;
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new DummyGASdkClient();
            #else
                return new DummyGASdkClient();
            #endif
        }
    }
}