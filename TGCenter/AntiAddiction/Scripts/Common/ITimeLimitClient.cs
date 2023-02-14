using AntiAddictionSdk.Api;

namespace AntiAddictionSdk.Common
{
    public interface ITimeLimitClient {
        int GetReason();

        bool IsLimit();

        long GetTimeToLimit();

        LimitTip GetLimitTip();
    }
}