namespace TaurusXAdSdk.Common
{
    public class DummyScreenUtilClient : IScreenUtilClient
    {
        #region IScreenUtilClient

        public bool IsPortrait() { return false; }

        public bool IsTablet() { return false; }

        #endregion
    }
}
