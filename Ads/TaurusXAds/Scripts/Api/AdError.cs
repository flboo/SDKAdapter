using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Api
{
    public class AdError
    {
        public static int ERROR_CODE_INTERNAL_ERROR = 0; // 内部错误
        public static int ERROR_CODE_INVALID_REQUEST = 1; // 无效请求，请求过于频繁等，广告位无效等
        public static int ERROR_CODE_NETWORK_ERROR = 2; // 网络错误
        public static int ERROR_CODE_NO_FILL = 3; // 无广告填充
        public static int ERROR_CODE_TIMEOUT = 4; // 超时
        
        readonly IAdErrorClient mClient;

        public AdError(IAdErrorClient client)
        {
            mClient = client;
        }

        public int GetCode() {
            return mClient.GetCode();
        }

        public string GetMessage() {
            return mClient.GetMessage();
        }

        public override string ToString() {
            return "ErrorCode is [" + GetCode() + "], Message is " + GetMessage();
        }
    }
}
