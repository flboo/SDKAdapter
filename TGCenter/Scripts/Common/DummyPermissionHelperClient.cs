using System;
using System.Collections;
using System.Collections.Generic;
using TGCenterSdk.Api;

namespace TGCenterSdk.Common
{
    public class DummyPermissionHelperClient : IPermissionHelperClient {
        
        public void Init(PermissionHelper.PermissionResultListener listener) { }
        public void ShowDefaultPermissionDialog() { }
        public void ShowPermissionDialog(Dictionary<string, string> permissions) { }
    }
}