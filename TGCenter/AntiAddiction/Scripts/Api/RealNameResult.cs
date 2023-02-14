using AntiAddictionSdk.Common;
namespace AntiAddictionSdk.Api
{
    public class RealNameResult {
        private IRealNameResultClient mClient;

        public RealNameResult(IRealNameResultClient client) {
            mClient = client;
        }

        public bool IsInitial() {
            return mClient.IsInitial();
        }

        public bool IsSuccess() {
            return mClient.IsSuccess();
        }

        public bool IsProcessing() {
            return mClient.IsProcessing();
        }

        public bool IsFail() {
            return mClient.IsFail();
        }

        public int GetResultCode() {
            return mClient.GetResultCode();
        }

        public string GetResultMsg() {
            return mClient.GetResultMsg();
        }

        public string GetPlayerId() {
            return mClient.GetPlayerId();
        }
    }
}