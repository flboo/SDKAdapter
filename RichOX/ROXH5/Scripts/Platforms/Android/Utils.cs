using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RichOX.Api;
using System;
using System.Runtime.InteropServices;

namespace RichOX.Platforms.Android
{
    public static class Utils
    {
        #region RichOX class names
        public const string RichOXClassName = "com.richox.sdk.RichOXH5";

        public const string FloatSceneClassName = "com.richox.sdk.core.scene.FloatScene";
        public const string DialogSceneClassName = "com.richox.sdk.core.scene.DialogScene";

        public const string NativeSceneClassName = "com.richox.sdk.core.scene.NativeScene";
        public const string NativeInfoUpdateCallbackClassName = "com.richox.sdk.core.scene.NativeInfoUpdateCallback";

        public const string SceneListenerClassName = "com.richox.sdk.core.scene.SceneListener";
        public const string InterActiveListenerClassName = "com.richox.sdk.core.interactive.InterActiveListener";

        public const string MissionInfoClassName = "com.richox.sdk.core.interactive.MissionInfo";
        public const string ActivityMissionListenerClassName = "com.richox.sdk.core.interactive.ActivityMissionListener";

        public const string WeChatRegisterCallbackClassName = "com.richox.sdk.core.WeChatRegisterCallback";
        public const string InfoUpdateCallbackClassName = "com.richox.sdk.core.InfoUpdateCallback";

        public const string EventCallbackClassName = "com.richox.sdk.core.EventCallback";

        public const string FrameLayoutClassName = "android.widget.FrameLayout";

        public const string Object = "java.lang.Object";
        #endregion

        #region Unity class names
        public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";
        #endregion

        public const string ArrayClassName = "java.lang.reflect.Array";

        public static Dictionary<string, string> ToCSharpDictionary(AndroidJavaObject map)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            if (map != null)
            {
                AndroidJavaObject entrySet = map.Call<AndroidJavaObject>("entrySet");
                if (entrySet != null)
                {
                    AndroidJavaObject entryArray = entrySet.Call<AndroidJavaObject>("toArray");
                    AndroidJavaClass arrayClass = new AndroidJavaClass(ArrayClassName);
                    int length = arrayClass.CallStatic<int>("getLength", entryArray);
                    for (int i = 0; i < length; i++)
                    {
                        AndroidJavaObject entry = arrayClass.CallStatic<AndroidJavaObject>("get", entryArray, i);
                        if (entry != null)
                        {
                            string key = entry.Get<string>("key");
                            string value = entry.Get<string>("value");
                            dictionary.Add(key, value);
                        }
                    }
                }
            }

            return dictionary;
        }
    }
}