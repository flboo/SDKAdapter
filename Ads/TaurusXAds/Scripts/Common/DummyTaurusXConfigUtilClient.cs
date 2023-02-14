namespace TaurusXAdSdk.Common
{
    public class DummyTaurusXConfigUtilClient : ITaurusXConfigUtilClient
    {
        #region ITaurusXConfigUtilClient

        public string GetAppId() {
            return "";
        }

        public string GetAdUnitId(string name) {
            return "";
        }

        public string GetChannel() {
            return "";
        }

        public string GetString(string name) {
            return "";
        }

        #endregion
    }
}
