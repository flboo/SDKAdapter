using TGCenterSdk.Api;

namespace TGCenterSdk.Common
{
    public class DummyTGCenterClient : ITGCenterClient {
        
        public void Init(InitConfig config) { }

        public bool IsUserAgreePolicy() {
            return false;
        }

        public void ClearCache() { }

        public void ShowDebugPage() { }
    }    
}