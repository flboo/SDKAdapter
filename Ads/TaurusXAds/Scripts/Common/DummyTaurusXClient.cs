using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public class DummyTaurusXClient : ITaurusXClient
    {
        #region ITaurusXClient

        public void Init(string appId) { }

        public void SetGdprConsent(bool consent) { }

        public void SetLogEnable(bool enable) { }

        public void SetTestMode(bool testMode) { }

        public void SetTestServer(string testServer) { }

        public void SetNetworkDebugMode(bool debugMode) { }

        public void SetNetworkDebugMode(Network network, bool debugMode) { }

        public void SetNetworkTestMode(bool testMode) { }

        public void SetNetworkTestMode(Network network, bool testMode) { }

        public void SetiOSAppPauseOnBackground(bool pause) { }

        public void SetGlobalNetworkConfigs(NetworkConfigs networkConfigs) { }

        public void SetSegment(Segment segment) { }

        public void SetDownloadConfirmNetwork(DownloadNetwork network) { }

        public void SetLineItemFilter(LineItemFilter filter) { }

        public string GetUid() { return ""; }

        #endregion
    }
}
