namespace ROXBase.Api 
{
    // 通用返回信息，返回信息为泛型，这里使用object来处理
    public class ROXCommonResponse {
        private object mResponse;
        public ROXCommonResponse(object response) {
            mResponse = response;
        }

        public object GetResponse() {
            return mResponse;
        }
    }
}