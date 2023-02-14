using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms
{
    public static class ClientFactory
    {
        public static ITaurusXClient TaurusXClientInstance()
        {
            #if UNITY_EDITOR
                return new DummyTaurusXClient();
	        #elif UNITY_ANDROID
                return TaurusXAdSdk.Platforms.Android.TaurusXClient.Instance;
	        #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return TaurusXAdSdk.Platforms.iOS.TaurusXClient.Instance;
            #else
                return new DummyTaurusXClient();
            #endif
        }

        public static ITaurusXConfigUtilClient TaurusXConfigUtilClient()
        {
            #if UNITY_EDITOR
                return new DummyTaurusXConfigUtilClient();
            #elif UNITY_ANDROID
                return new TaurusXAdSdk.Platforms.Android.TaurusXConfigUtilClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new DummyTaurusXConfigUtilClient();
            #else
                return new DummyTaurusXConfigUtilClient();
            #endif
        }

        public static ITaurusXTrackerClient TaurusXTrackerClient()
        {
            #if UNITY_EDITOR
                return new DummyTaurusXTrackerClient();
            #elif UNITY_ANDROID
                return new TaurusXAdSdk.Platforms.Android.TaurusXTrackerClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new TaurusXAdSdk.Platforms.iOS.TaurusXTrackerClient();
            #else
                return new DummyTaurusXTrackerClient();
            #endif
        }

        public static IBannerClient BuildBannerClient(string adUnitId)
        {
            #if UNITY_EDITOR
                return new DummyBannerClient();
            #elif UNITY_ANDROID
                return new TaurusXAdSdk.Platforms.Android.BannerClient(adUnitId);
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new TaurusXAdSdk.Platforms.iOS.BannerClient(adUnitId);
            #else
                return new DummyBannerClient();
            #endif
        }

        public static IInterstitialClient BuildInterstitialClient(string adUnitId) {
            #if UNITY_EDITOR
                return new DummyInterstitialClient();
            #elif UNITY_ANDROID
                return new TaurusXAdSdk.Platforms.Android.InterstitialClient(adUnitId);
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new TaurusXAdSdk.Platforms.iOS.InterstitialClient(adUnitId);
            #else
                return new DummyInterstitialClient();
            #endif
        }

        public static IRewardedVideoClient BuildRewardedVideoClient(string adUnitId) {
            #if UNITY_EDITOR
                return new DummyRewardedVideoClient();
            #elif UNITY_ANDROID
                return new TaurusXAdSdk.Platforms.Android.RewardedVideoClient(adUnitId);
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new TaurusXAdSdk.Platforms.iOS.RewardedVideoClient(adUnitId);
            #else
                return new DummyRewardedVideoClient();
            #endif
        }

        public static ISplashClient BuildSplashClient(string adUnitId) {
            #if UNITY_EDITOR
                return new DummySplashClient();
            #elif UNITY_ANDROID
                return new TaurusXAdSdk.Platforms.Android.SplashClient(adUnitId);
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new TaurusXAdSdk.Platforms.iOS.SplashClient(adUnitId);
            #else
                return new DummySplashClient();
            #endif
        }
        
        public static IMixViewClient BuildMixViewClient(string adUnitId) {
            #if UNITY_EDITOR
                return new DummyMixViewClient();
            #elif UNITY_ANDROID
                return new TaurusXAdSdk.Platforms.Android.MixViewClient(adUnitId);
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new TaurusXAdSdk.Platforms.iOS.MixViewClient(adUnitId);
            #else
                return new DummyMixViewClient();
            #endif
        }

        public static INativeAdClient BuildNativeAdClient(string adUnitId)
        {
#if UNITY_EDITOR
            return new DummyNativeAdClient();
#elif UNITY_ANDROID
                return new TaurusXAdSdk.Platforms.Android.NativeAdClient(adUnitId);
#elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new DummyNativeAdClient();
#else
                return new DummyNativeAdClient();
#endif
        }

        public static IMixFullScreenClient BuildMixFullScreenClient(string adUnitId) {
            #if UNITY_EDITOR
                return new DummyMixFullScreenClient();
            #elif UNITY_ANDROID
                return new TaurusXAdSdk.Platforms.Android.MixFullScreenClient(adUnitId);
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new TaurusXAdSdk.Platforms.iOS.MixFullScreenClient(adUnitId);
            #else
                return new DummyMixFullScreenClient();
            #endif
        }

        public static IScreenUtilClient ScreenUtilClient()
        {
            #if UNITY_EDITOR
                return new DummyScreenUtilClient();
            #elif UNITY_ANDROID
                return TaurusXAdSdk.Platforms.Android.ScreenUtilClient.Instance;
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return TaurusXAdSdk.Platforms.iOS.ScreenUtilClient.Instance;
            #else
                return new DummyScreenUtilClient();
            #endif
        }
    }
}
