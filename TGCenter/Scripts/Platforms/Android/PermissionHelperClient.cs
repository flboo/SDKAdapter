using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TGCenterSdk.Api;
using TGCenterSdk.Common;

namespace TGCenterSdk.Platforms.Android
{
    public class PermissionHelperClient : AndroidJavaProxy, IPermissionHelperClient {
        
        private AndroidJavaObject helper;
        private PermissionHelper.PermissionResultListener listener;

        public PermissionHelperClient() : base(Utils.Class_IPermissionCallback) {
            helper = new AndroidJavaClass(Utils.Class_PermissionHelper);
        }

        #region IPermissionHelperClient
        public void Init(PermissionHelper.PermissionResultListener resultListener) {
            listener = resultListener;
            helper.CallStatic("init", Utils.GetPlayerActivity(), this);
        }

        public void ShowDefaultPermissionDialog() {
            helper.CallStatic("showDefaultPermissionDialog");
        }

        public void ShowPermissionDialog(Dictionary<string, string> permissions) {
            helper.CallStatic("showPermissionDialog", Utils.CreateMapByDictionary(permissions));
        }
        #endregion

        public void onRequestPermissionsResult(bool isAllGranted, AndroidJavaObject deniedPermissions) {
            if(listener != null) {
                listener.OnRequestPermissionsResult(isAllGranted, Utils.CreateArrayList(deniedPermissions));
            }
        }
    }
}