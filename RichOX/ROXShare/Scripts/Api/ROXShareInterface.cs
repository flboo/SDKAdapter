namespace ROXShare.Api
{
    public interface ROXShareInterface<T>
    {
        void OnSuccess(T t);
        void OnFailed(int code, string msg);
    }
    
}