using UnityEngine;
using TGCenterSdk.Api;
using TGCenterSdk.Common;
using TGCUdesk.Platforms.Android;

namespace TGCenterSdk.Platforms.Android
{
    public class UdeskHelperClient : IUdeskHelperClient {
        
        private readonly AndroidJavaClass udeskHelperClass;

        public UdeskHelperClient() {
            udeskHelperClass = new AndroidJavaClass(TGCUdesk.Platforms.Android.Utils.Class_UdeskHelper);
        }
        
        #region IUdeskHelperClient
        public void EnterUdesk() {
            udeskHelperClass.CallStatic("enterUdesk", TGCUdesk.Platforms.Android.Utils.GetPlayerActivity());
        }
        #endregion
    }
}