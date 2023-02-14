
namespace AntiAddictionSdk.Common
{
    public interface IRealNameResultClient {
        bool IsInitial();

        bool IsSuccess();

        bool IsProcessing();

        bool IsFail();

        int GetResultCode();

        string GetResultMsg();

        string GetPlayerId();
    }
}