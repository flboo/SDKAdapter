using System;
using UnityEngine;
using System.Collections.Generic;
using WeGameSdk.Api;

namespace WeGameSdk.Common
{
    public class WeGameSdkUtil
    {
        public WeGameSdkUtil()
        {
        }

        public static AndroidJavaObject ToJavaList(String[] list)
        {
            var javaList = new AndroidJavaObject("java.util.ArrayList");


            foreach (String str in list)
            {
                javaList.Call<bool>("add", str);
            }

            return javaList;
        }

        public static AndroidJavaObject AndroidJavaJsonObject(JSONObject jSONObject)
        {
            return new AndroidJavaObject("org.json.JSONObject", jSONObject.ToString());
        }

        public static JSONObject jsonObjectFromJava(AndroidJavaObject androidJavaJsonObject)
        {
            return (JSONObject)JSONNode.Parse(androidJavaJsonObject.Call<String>("toString"));
        }

        public static AndroidJavaObject ToJavaHashMap(Dictionary<String, object> dic)
        {
            var hashMap = new AndroidJavaObject("java.util.HashMap");


            foreach (var kv in dic)
            {
                var vauleObj = ToJavaObject(kv.Value);
                if (vauleObj != null)
                {

                    hashMap.Call<AndroidJavaObject>("put", kv.Key, vauleObj);
                }

            }

            return hashMap;
        }

        public static AndroidJavaObject ToJavaHashMap(Dictionary<string, string> dic)
        {
            var hashMap = new AndroidJavaObject("java.util.HashMap");
            foreach (var entry in dic)
            {
                hashMap.Call<AndroidJavaObject>("put", entry.Key, entry.Value);
            }
            return hashMap;

        }

        private static AndroidJavaObject ToJavaObject(object obj)
        {
            if (obj is int)
            {
                return new AndroidJavaObject("java.lang.Integer", obj);
            }
            else if (obj is long)
            {
                return new AndroidJavaObject("java.lang.Long", obj);
            }

            else if (obj is float)
            {
                return new AndroidJavaObject("java.lang.Float", obj);

            }
            else if (obj is double)
            {
                return new AndroidJavaObject("java.lang.Double", obj);
            }
            else if (obj is string)
            {
                return new AndroidJavaObject("java.lang.String", obj);

            }
            else if (obj is bool)
            {
                return new AndroidJavaObject("java.lang.Integer", Convert.ToInt32((bool)obj));

            }
            else
            {
                Debug.Log("不支持加入" + obj.GetType() + "类型,此kv对被丢弃");
                return null;
            }
        }
    }
}
