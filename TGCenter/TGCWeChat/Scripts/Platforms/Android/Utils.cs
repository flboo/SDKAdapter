using UnityEngine;

namespace TGCWeChat.Platforms.Android
{
    public static class Utils {
        public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";
        
        public const string Class_WeChatHelper = "com.tgcenter.unified.sdk.h.WeChatHelper";
        public const string Class_WeChatLoginCallback = "com.we.modoo.callback.LoginCallback";
        public const string Class_WeChatLoginType = "com.we.modoo.core.LoginType";

        public static AndroidJavaObject GetPlayerActivity() {
            AndroidJavaClass playerClass = new AndroidJavaClass(UnityActivityClassName);
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            return activity;
        }
    }
}