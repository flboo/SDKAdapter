using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmbedSDK.Platforms.Android
{
    public static class Utils
    {

        public const string EmbedMgrClassName = "we.studio.embed.EmbedSDK";

        #region Unity class names
        public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";
        #endregion


        public static AndroidJavaObject DictToMap(Dictionary<string, string> dictionary)
        {
            if (dictionary == null)
            {
                return null;
            }
            AndroidJavaObject map = new AndroidJavaObject("java.util.HashMap");
            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                map.Call<string>("put", pair.Key, pair.Value);
            }
            return map;
        }
    }
}