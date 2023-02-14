using System;
using System.Collections;
using System.Collections.Generic;
using TGCenterSdk.Api;
using TGCenterSdk.Common;

namespace TGCenterSdk.Platforms.iOS
{
    public class PermissionHelperClient : IPermissionHelperClient {
        
        public void Init(PermissionHelper.PermissionResultListener listener) { }
        public void ShowDefaultPermissionDialog() { }
        public void ShowPermissionDialog(Dictionary<string, string> permissions) { }
    }    
}