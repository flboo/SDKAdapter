using Advertisers.Common;
using Advertisers.Platforms;
using TaurusXAdSdk.Api;

namespace Advertisers.Platforms
{
    public class ClientFactory
    {
        public static IProbeManager ProbeManagerInstance()
        {
            #if UNITY_EDITOR
                return new DummyProbeManager();
	        #elif UNITY_ANDROID
                return new Advertisers.Platforms.Android.ProbeManagerClient();
	        // #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
            //     return Dsp.Platforms.iOS.WeSdkClient.Instance;
            #else
                return new DummyProbeManager();
            #endif
        }
    }
}