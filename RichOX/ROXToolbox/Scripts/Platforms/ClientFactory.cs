using ROXToolbox.Common;

namespace ROXToolbox.Platforms
{
    public static class ClientFactory
    {
        public static IROXToolbox RichOXClientInstance()
        {
            #if UNITY_EDITOR
                return new DummyROXToolbox();
	        #elif UNITY_ANDROID
                return ROXToolbox.Platforms.Android.ROXToolboxClient.Instance;
	        #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return ROXToolbox.Platforms.iOS.ROXToolboxClient.Instance;
            #else
                return new DummyROXToolbox();
            #endif
        }
    }
}