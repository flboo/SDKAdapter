namespace AntiAddictionSdk.Api
{
    public interface HealthGameTipCallback {
        void OnHealthGameTipOpen();

        void OnHealthGameTipOpenFail(string message);

        void OnHealthGameTipClose();
    }
}