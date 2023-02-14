using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public interface ITaurusXClient
    {
        void Init(string appId);

        void SetGdprConsent(bool consent);

        void SetLogEnable(bool enable);

        void SetTestMode(bool testMode);

        void SetTestServer(string testServer);

        void SetNetworkDebugMode(bool debugMode);

        void SetNetworkDebugMode(Network network, bool debugMode);

        void SetNetworkTestMode(bool testMode);

        void SetNetworkTestMode(Network network, bool testMode);

        void SetiOSAppPauseOnBackground(bool pause);

        void SetGlobalNetworkConfigs(NetworkConfigs networkConfigs);

        void SetSegment(Segment segment);

        void SetDownloadConfirmNetwork(DownloadNetwork network);

        void SetLineItemFilter(LineItemFilter filter);

        string GetUid();
    }
}