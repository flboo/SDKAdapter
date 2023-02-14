using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace Qarth
{
    public class AndroidAccountAdapter : AbstractSDKAdapter, IAccountAdapter
    {
        public static AndroidJavaObject GetActivity()
        {
            AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            return playerClass.GetStatic<AndroidJavaObject>("currentActivity");
        }
        public string GetOpenUdid()
        {
            AndroidJavaClass unityPlayerActivity = new AndroidJavaClass(Application.identifier + ".UnityPlayerActivity");
            string openudid = unityPlayerActivity.CallStatic<string>("GetOpenUdid", GetActivity());
            return openudid;
        }
        public byte[] Encrypt(string _json)
        {
            AndroidJavaClass unityPlayerActivity = new AndroidJavaClass(Application.identifier + ".UnityPlayerActivity");
            byte[] compress = unityPlayerActivity.CallStatic<byte[]>("HttpCompress", GetActivity(), _json);
            byte[] encrypt = unityPlayerActivity.CallStatic<byte[]>("HttpEncrypt", GetActivity(), compress);
            return encrypt;
        }
        public byte[] Decrypt(byte[] _data)
        {
            AndroidJavaClass unityPlayerActivity = new AndroidJavaClass(Application.identifier + ".UnityPlayerActivity");
            //byte[] compress = unityPlayerActivity.CallStatic<byte[]>("HttpCompress", GetActivity(), _data);
            byte[] encrypt = unityPlayerActivity.CallStatic<byte[]>("HttpDecrypt", GetActivity(), _data);
            return encrypt;
        }
    }
}