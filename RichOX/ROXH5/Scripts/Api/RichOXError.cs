using RichOX.Common;

namespace RichOX.Api
{
    public class RichOXError
    {
        public static int ERROR_CODE_INVALID_PARAMS = 1000; // 从服务获取配置失败等
        public static int ERROR_CODE_INTERNAL_ERROR = 2000; // 内部错误
        public static int ERROR_CODE_RENDER_ERROR = 3000; // 渲染错误
        public static int ERROR_CODE_NETWORK_ERROR = 4000; // 网络错误

        readonly IRichOXErrorClient mClient;

        public RichOXError(IRichOXErrorClient client)
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
