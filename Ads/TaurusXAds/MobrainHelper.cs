using UnityEngine;
using System.Collections.Generic;

namespace Qarth
{
    public class MobrainHelper
    {
        // 请确认项目有用到mobrain再调用次脚本中方法
        public static float GetPreEcpm(Dictionary<string, string> extras)
        {
            AndroidJavaClass helperClass = new AndroidJavaClass("com.taurusx.ads.mediation.helper.MobrainHelper");
            if (helperClass == null)
            {
                Log.e(">>>>>>>>>>mobrain helper not found.");
                return 0f;
            }
            return helperClass.CallStatic<float>("getPreEcpm", DictToMap(extras));
        }

        public static string GetAdNetworkRitId(Dictionary<string, string> extras)
        {
            AndroidJavaClass helperClass = new AndroidJavaClass("com.taurusx.ads.mediation.helper.MobrainHelper");
            if (helperClass == null)
            {
                Log.e(">>>>>>>>>>mobrain helper not found.");
                return null;
            }
            return helperClass.CallStatic<string>("getAdNetworkRitId", DictToMap(extras));
        }

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