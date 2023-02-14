using UnityEngine;

namespace TGCUdesk.Platforms.Android
{
    public static class Utils {
        public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";
        
        public const string Class_UdeskHelper = "com.tgcenter.unified.sdk.h.UdeskHelper";

        public static AndroidJavaObject GetPlayerActivity() {
            AndroidJavaClass playerClass = new AndroidJavaClass(UnityActivityClassName);
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            return activity;
        }
    }
}