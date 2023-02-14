using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Api
{
    public class LoadMode
    {
        public enum Mode
        {
            Serial = 1,
            Parallel = 2,
            Shuffle = 3,
            AutoLoad = 4
        }

        private ILoadModeClient mClient;

        public LoadMode(ILoadModeClient client)
        {
            mClient = client;
        }

        public Mode GetMode()
        {
            return (Mode)mClient.GetMode();
        }

        public string GetModeName(Mode mode)
        {
            switch (mode)
            {
                case Mode.Serial:
                    return "Serial";
                case Mode.Parallel:
                    return "Parallel";
                case Mode.Shuffle:
                    return "Shuffle";
                case Mode.AutoLoad:
                    return "AutoLoad";
                default:
                    return "UnKnown";
            }
        }

        public int GetParallelCount()
        {
            return mClient.GetParallelCount();
        }

        public bool IsUseWaterfallCacheFirst()
        {
            return mClient.IsUseWaterfallCacheFirst();
        }

        public AutoLoadConfig GetAutoLoadConfig() {
            return mClient.GetAutoLoadConfig();
        }

        public override string ToString()
        {
            return "Mode : " + GetMode().ToString()
                + ", ParallelCount: " + GetParallelCount()
                + ", IsUseWaterfallCacheFirst: " + IsUseWaterfallCacheFirst()
                + ", AutoLoadConfig: " + GetAutoLoadConfig();
        }
    }
}