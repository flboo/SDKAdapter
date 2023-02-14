using AntiAddictionSdk.Api;

namespace AntiAddictionSdk.Common
{
    public interface IUserClient {
        RealNameResult GetRealNameResult();

        int GetAge();

        bool IsTourist();

        bool IsChild();

        bool IsAdult();
    }
}