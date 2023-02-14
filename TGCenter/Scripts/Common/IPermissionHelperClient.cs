using System;
using System.Collections;
using System.Collections.Generic;
using TGCenterSdk.Api;

namespace TGCenterSdk.Common
{
    public interface IPermissionHelperClient {

        void Init(PermissionHelper.PermissionResultListener listener);
        void ShowDefaultPermissionDialog();
        void ShowPermissionDialog(Dictionary<string, string> permissions);
    }
}