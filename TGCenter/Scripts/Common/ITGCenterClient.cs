using TGCenterSdk.Api;

namespace TGCenterSdk.Common
{
    public interface ITGCenterClient {

        void Init(InitConfig config);

        bool IsUserAgreePolicy();

        void ClearCache();

        void ShowDebugPage();
    }
}