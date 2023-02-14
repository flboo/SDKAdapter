using System;
using System.Collections;
using System.Collections.Generic;
using TGCenterSdk.Common;
using TGCenterSdk.Platforms;

namespace TGCenterSdk.Api
{
    public class PermissionHelper {

        public interface PermissionResultListener {
            void OnRequestPermissionsResult(bool isAllGranted,  ArrayList deniedPermissions);
        }

        private static IPermissionHelperClient client = ClientFactory.PermissionHelperClient();

        public static void Init(PermissionResultListener listener) {
            client.Init(listener);
        }

        public static void ShowDefaultPermissionDialog() {
            client.ShowDefaultPermissionDialog();
        }

        public static void ShowPermissionDialog(Dictionary<string, string> permissions) {
            client.ShowPermissionDialog(permissions);
        }
    }
}