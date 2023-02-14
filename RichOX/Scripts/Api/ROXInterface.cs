namespace ROXBase.Api
{
    public interface ROXInterface<T>
    {
        void OnSuccess(T t);
        void OnFailed(int code, string msg);
    }
    
}