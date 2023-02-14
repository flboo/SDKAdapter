using ROXBase.Common;

namespace ROXBase.Platforms
{
    public static class ClientFactory
    {
        public static IRichOXClient RichOXClientInstance()
        {
#if UNITY_EDITOR
            return new DummyRichOXClient();
#elif UNITY_ANDROID
            return ROXBase.Platforms.Android.RichOXClient.Instance;
#elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
            return ROXBase.Platforms.iOS.RichOXClient.Instance;
#else
            return new DummyRichOXClient();
#endif
        }

        public static IROXUser ROXUserInstance()
        {
#if UNITY_EDITOR
            return new DummyROXUserClient();
#elif UNITY_ANDROID
            return ROXBase.Platforms.Android.ROXUserClient.Instance;
#elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
            return ROXBase.Platforms.iOS.RichOXUserClient.Instance;
#else
            return new DummyROXUserClient();
#endif
        }

        public static IRichOXUserManager RichOXUserManagerInstance()
        {
#if UNITY_EDITOR
            return new DummyRichOXUserManager();
#elif UNITY_ANDROID
            return ROXBase.Platforms.Android.RichOXUserManagerClient.Instance;
#elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
            return ROXBase.Platforms.iOS.RichOXUserManagerClient.Instance;
#else
            return new DummyRichOXUserManager();
#endif
        }
    }
}