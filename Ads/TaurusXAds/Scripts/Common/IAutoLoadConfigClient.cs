namespace TaurusXAdSdk.Common
{
    public interface IAutoLoadConfigClient
    {
        int GetCacheCount();

        int GetParallelCount();

        int GetMinErrorWaitTime();

        int GetMaxErrorWaitTime();

        int GetMinFreezeWaitTime();

        int GetMaxFreezeWaitTime();

        float GetDelayFactor();
    }
}