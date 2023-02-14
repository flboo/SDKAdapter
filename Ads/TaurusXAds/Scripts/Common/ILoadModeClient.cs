using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public interface ILoadModeClient
    {
        LoadMode.Mode GetMode();

        int GetParallelCount();

        bool IsUseWaterfallCacheFirst();

        AutoLoadConfig GetAutoLoadConfig();
    }
}
