using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Api
{
    public class AutoLoadConfig
    {
        private IAutoLoadConfigClient mClient;

        public AutoLoadConfig(IAutoLoadConfigClient client)
        {
            mClient = client;
        }

        public int GetCacheCount()
        {
            return mClient.GetCacheCount();
        }

        public int GetParallelCount()
        {
            return mClient.GetParallelCount();
        }

        public int GetMinErrorWaitTime()
        {
            return mClient.GetMinErrorWaitTime();
        }

        public int GetMaxErrorWaitTime()
        {
            return mClient.GetMaxErrorWaitTime();
        }

        public int GetMinFreezeWaitTime()
        {
            return mClient.GetMinFreezeWaitTime();
        }

        public int GetMaxFreezeWaitTime()
        {
            return mClient.GetMaxFreezeWaitTime();
        }

        public float GetDelayFactor()
        {
            return mClient.GetDelayFactor();
        }

        public override string ToString()
        {
            return "CacheCount: " + GetCacheCount()
                    + ", ParallelCount: " + GetParallelCount()

                    + ", MixErrorTime: " + GetMinErrorWaitTime()
                    + ", MaxErrorTime: " + GetMaxErrorWaitTime()

                    + ", MinFreezeTime: " + GetMinFreezeWaitTime()
                    + ", MaxFreezeTime: " + GetMaxFreezeWaitTime()

                    + ", DelayFactor: " + GetDelayFactor();
        }
    }
}
