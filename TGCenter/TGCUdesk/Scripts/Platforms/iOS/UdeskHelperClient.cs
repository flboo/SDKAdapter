using System;
using TGCenterSdk.Api;
using TGCenterSdk.Common;
using System.Runtime.InteropServices;
using AOT;
using TGCUdesk.Platforms.iOS;

namespace TGCenterSdk.Platforms.iOS
{
    public class UdeskHelperClient : IUdeskHelperClient {

        public UdeskHelperClient() {
        }

        #region IUdeskHelperClient
        public void EnterUdesk() {
            TGCUdesk.Platforms.iOS.Externs.TGCUdeskEnter();
        }
        #endregion
    }    
}