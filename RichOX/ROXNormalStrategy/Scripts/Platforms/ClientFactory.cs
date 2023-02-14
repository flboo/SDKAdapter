using ROXStrategy.Common;

namespace ROXStrategy.Platforms
{
    public static class ClientFactory
    {
        public static IROXNormal RichOXClientInstance()
        {
            #if UNITY_EDITOR
                return new DummyROXNormal();
#elif UNITY_ANDROID
                return ROXStrategy.Platforms.Android.ROXNormalClient.Instance;
#elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new ROXStrategy.Platforms.iOS.ROXNormalClient();
#else
                return new DummyROXNormal();
#endif
        }
    }
}