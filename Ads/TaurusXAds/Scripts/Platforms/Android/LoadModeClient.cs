using UnityEngine;
using TaurusXAdSdk.Api;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class LoadModeClient : ILoadModeClient
    {
        private AndroidJavaObject mLoadMode;

        public LoadModeClient(AndroidJavaObject loadMode)
        {
            mLoadMode = loadMode;
        }

        #region ILoadModeClient

        public LoadMode.Mode GetMode()
        {
            AndroidJavaObject mode = mLoadMode.Call<AndroidJavaObject>("getMode");
            int modeId = mode.Call<int>("getId");
            switch(modeId)
            {
                case (int)LoadMode.Mode.Parallel:
                    return LoadMode.Mode.Parallel;
                case (int)LoadMode.Mode.Shuffle:
                    return LoadMode.Mode.Shuffle;
                case (int)LoadMode.Mode.AutoLoad:
                    return LoadMode.Mode.AutoLoad;
                case (int)LoadMode.Mode.Serial:
                default:
                    return LoadMode.Mode.Serial;
            }
        }

        public int GetParallelCount()
        {
            return mLoadMode.Call<int>("getParallelCount");
        }

        public bool IsUseWaterfallCacheFirst()
        {
            return mLoadMode.Call<bool>("isUseWaterfallCacheAdFirst");
        }

        public AutoLoadConfig GetAutoLoadConfig()
        {
            return new AutoLoadConfig(new AutoLoadConfigClient(mLoadMode.Call<AndroidJavaObject>("getAutoLoadConfig")));
        }

        #endregion
    }
}
