using UnityEngine;
using TGCenterSdk.Api;
using System.Collections;
using System.Collections.Generic;

namespace TGCenterSdk.Platforms.Android
{
    public static class Utils {
        public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";
        
        public const string Class_TGCenter = "com.tgcenter.unified.sdk.api.TGCenter";
        public const string Class_InitConfig = "com.tgcenter.unified.sdk.api.InitConfig";
        public const string Class_Day1Retention = "com.tgcenter.unified.sdk.api.Day1Retention";

        public const string Class_PrivacyPolicyHelperBuilder = "com.nefarian.privacy.policy.PrivacyPolicyHelper$Builder";
        public const string Class_IPrivacyPolicyCallback = "com.nefarian.privacy.policy.IPrivacyPolicyCallback";
        public const string Class_HookUtils = "com.nefarian.privacy.policy.utils.HookUtils";

        public const string Class_IPermissionCallback = "com.nefarian.privacy.policy.permissioncheck.PermissionResultListener";
        public const string Class_PermissionHelper = "com.nefarian.privacy.policy.permissioncheck.PermissionHelper";

        public static AndroidJavaObject GetPlayerActivity() {
            AndroidJavaClass playerClass = new AndroidJavaClass(UnityActivityClassName);
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            return activity;
        }

        public static string GetRetentionType(Day1Retention.RetentionTypes type) {
            switch(type) {
                case Day1Retention.RetentionTypes.Hour:
                    return "hour";
                case Day1Retention.RetentionTypes.NatureDay:
                    return "nature_day";
                default:
                    return "";
            }
        }

        public static AndroidJavaObject CreateMapByDictionary(Dictionary<string, string> dictionary)
        {
            if (dictionary == null) {
                return null;
            }

            AndroidJavaObject map = new AndroidJavaObject("java.util.HashMap");
            foreach(KeyValuePair<string, string> pair in dictionary) {
                map.Call<string>("put", pair.Key, pair.Value);
            }
            return map;
        }

        public static ArrayList CreateArrayList(AndroidJavaObject list) {
            if (list == null) {
                return null;
            }

            ArrayList result = new ArrayList();
            int size = list.Call<int>("size");
            for(int i = 0; i < size; i++) {
                result.Add(list.Call<string>("get", i));
            }
            
            return result;
        }
    }
}