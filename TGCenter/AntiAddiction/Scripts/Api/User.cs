using AntiAddictionSdk.Common;

namespace AntiAddictionSdk.Api
{
    public class User {
        private IUserClient mClient;

        public User(IUserClient client) {
            mClient = client;
        }

        public RealNameResult GetRealNameResult() {
            return mClient.GetRealNameResult();
        }

        public int GetAge() {
            return mClient.GetAge();
        }

        // 用户类型：游客、未成年人、成年人
        // 未实名认证、认证中都是游客
        public bool IsTourist() {
            return mClient.IsTourist();
        }

        public bool IsChild() {
            return mClient.IsChild();
        }

        public bool IsAdult() {
            return mClient.IsAdult();
        }
    }
}