using TaurusXAdSdk.Common;
using TaurusXAdSdk.Platforms;

namespace TaurusXAdSdk.Api
{
    /**
     * Class contains logic that applies to the SDK as a whole.
     */
    public static class TaurusXAds
    {
        private static ITaurusXClient mClient = GetTaurusXClient();

        private static ITaurusXClient GetTaurusXClient()
        {
            return ClientFactory.TaurusXClientInstance();
        }

        /**
         * Init TaurusX with AppId.
         */
        public static void Init(string appId)
        {
            mClient.Init(appId);
        }

        /**
         * Set whether user consent GDPR.
         * You can SetGdprConsent at anytime if user changed their choices.
         */
        public static void SetGdprConsent(bool consent)
        {
            mClient.SetGdprConsent(consent);
        }

        /**
         * Set whether to print log.
         */
        public static void SetLogEnable(bool enable)
        {
            mClient.SetLogEnable(enable);
        }

        /**
         * Set whether to request test ads from Marketplace.
         */
        public static void SetTestMode(bool testMode)
        {
            mClient.SetTestMode(testMode);
        }

        public static void SetTestServer(string testServer)
        {
            mClient.SetTestServer(testServer);
        }

        /**
         * Set all Network debug mode switch.
         */
        public static void SetNetworkDebugMode(bool debugMode) {
            mClient.SetNetworkDebugMode(debugMode);
        }

        /**
         * Set specified Network debug mode switch.
         * This setting will override setting in SetNetworkDebugMode() for this Network.
         */
        public static void SetNetworkDebugMode(Network network, bool debugMode) {
            mClient.SetNetworkDebugMode(network, debugMode);
        }

        /**
         * Set all Network test mode switch.
         */
        public static void SetNetworkTestMode(bool testMode) {
            mClient.SetNetworkTestMode(testMode);
        }

        /**
         * Set specified Network test mode switch.
         * This setting will override setting in SetNetworkTestMode() for this Network.
         */
        public static void SetNetworkTestMode(Network network, bool testMode) {
            mClient.SetNetworkTestMode(network, testMode);
        }

        /**
         * Set whether to pause iOS App when show fullscreen ads.
         * Fullscreen Ads includes InterstitialAd, RewardedVideoAd, MixFullScreenAd.
         */
        public static void SetiOSAppPauseOnBackground(bool pause)
        {
            mClient.SetiOSAppPauseOnBackground(pause);
        }

        /**
         * Set global NetworkConfigs, the config will be sent to Network SDK.
         */
        public static void SetGlobalNetworkConfigs(NetworkConfigs networkConfigs)
        {
            mClient.SetGlobalNetworkConfigs(networkConfigs);
        }

        /**
         * Set user segment, SDK will request AdUnit Config using this segment.
         */
        public static void SetSegment(Segment segment)
        {
            mClient.SetSegment(segment);
        }

        /**
         * Specific network to download Android apk directly.
         * Default is All.
         * @param network
         */
        public static void SetDownloadConfirmNetwork(DownloadNetwork network)
        {
            mClient.SetDownloadConfirmNetwork(network);
        }

        /**
         * Use LineItemFilter to request specified LineItem in AdUnit.
         * This is useful for local test.
         * @param filter
         */
        public static void SetLineItemFilter(LineItemFilter filter)
        {
            mClient.SetLineItemFilter(filter);
        }

        /**
         * Get TaurusX generated user id.
         */
        public static string GetUid()
        {
            return mClient.GetUid();
        }
    }
}
