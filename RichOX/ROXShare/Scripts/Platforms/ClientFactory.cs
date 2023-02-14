using ROXShare.Common;

namespace ROXShare.Platforms
{
    public static class ClientFactory
    {
        public static IROXShare ROXShareClientInstance()
        {
            #if UNITY_EDITOR
                return new DummyROXShareClient();
	        #elif UNITY_ANDROID
                return ROXShare.Platforms.Android.ROXShareClient.Instance;
	        #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return ROXShare.Platforms.iOS.ROXShareClient.Instance;
            #else
                return new DummyROXShareClient();
            #endif
        }
    }
}