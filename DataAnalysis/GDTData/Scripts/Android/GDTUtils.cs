namespace Tencent.GDT
{
#if UNITY_ANDROID
    using UnityEngine;
    using System.Collections.Generic;
    internal class GDTUtils
    {
        // activity 对象，unity 只有一个 activity
        private static AndroidJavaObject activity;
        public static AndroidJavaObject GetActivity()
        {
            if (activity == null)
            {
                var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            }
            return activity;
        }
    }
#endif
}