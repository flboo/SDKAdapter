namespace ROXShare.Api 
{
    // ROX通用错误信息
    public class ROXShareErrorResponse {
        // 错误码
        private int mCode;
        // 错误描述
        private string mMessage;
        public ROXShareErrorResponse(int code, string msg) {
            mCode = code;
            mMessage = msg;
        }

        public int GetCode() {
            return mCode;
        }

        public string GetMessage() {
            return mMessage;
        }
    }
}