using Qarth;

namespace EmbedSDK.Platforms
{
    public class ClientFactory
    {
        public static IEmbedSDKClient GetEmbedSDKClient()
        {
#if UNITY_EDITOR
            return new EmbedSDKDummyClient();
#elif UNITY_ANDROID
            return new EmbedSDK.Platforms.Android.EmbedSDKAndroidClient();
#else
            return new EmbedSDKDummyClient();
#endif
        }
    }
}