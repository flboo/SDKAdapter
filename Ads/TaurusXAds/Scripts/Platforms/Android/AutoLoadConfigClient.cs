using UnityEngine;
using TaurusXAdSdk.Api;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class AutoLoadConfigClient : IAutoLoadConfigClient
    {
        private AndroidJavaObject mAutoLoad;

        public AutoLoadConfigClient(AndroidJavaObject autoLoad)
        {
            mAutoLoad = autoLoad;
        }

        #region IAutoLoadConfigClient

        public int GetCacheCount()
        {
            return mAutoLoad.Call<int>("getCacheCount");
        }

        public int GetParallelCount()
        {
            return mAutoLoad.Call<int>("getParallelCount");
        }

        public int GetMinErrorWaitTime()
        {
            return mAutoLoad.Call<int>("getMinErrorWaitTime");
        }

        public int GetMaxErrorWaitTime()
        {
            return mAutoLoad.Call<int>("getMaxErrorWaitTime");
        }

        public int GetMinFreezeWaitTime()
        {
            return mAutoLoad.Call<int>("getMinFreezeWaitTime");
        }

        public int GetMaxFreezeWaitTime()
        {
            return mAutoLoad.Call<int>("getMaxFreezeWaitTime");
        }

        public float GetDelayFactor()
        {
            return mAutoLoad.Call<float>("getDelayFactor");
        }

        #endregion
    }
}
