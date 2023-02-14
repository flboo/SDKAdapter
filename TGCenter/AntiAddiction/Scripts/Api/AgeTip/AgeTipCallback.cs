namespace AntiAddictionSdk.Api
{
    public interface AgeTipCallback {
        void OnAgeTipsOpen();

        void OnAgeTipsOpenFail(string message);

        void OnAgeTipsClose();
    }
}