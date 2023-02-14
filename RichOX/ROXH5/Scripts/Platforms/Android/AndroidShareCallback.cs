using UnityEngine;
using System;
using RichOX.Api;
using System.Collections;

namespace  RichOX.Platforms.Android {
    class AndroidShareCallback : AndroidJavaProxy {
        public event EventHandler<ROXShareEventArgs> GenShareUrl;
        public event EventHandler<ROXShareEventArgs> ShareContent;

        public AndroidShareCallback() : base("com.richox.sdk.ShareRegisterCallback")
        {
        }

        public void genShareUrl(string hostUrl, AndroidJavaObject shareParams, AndroidJavaObject callback) 
        {
            Debug.Log("go here, genShareUrl");
            Debug.Log("the response is " + hostUrl);
            if (GenShareUrl != null)
            {
                AndroidJavaObject shareObject = shareParams.Call<AndroidJavaObject>("keySet");
                AndroidJavaObject iterator = shareObject.Call<AndroidJavaObject>("iterator");
                AndroidJavaClass stringClass = new AndroidJavaClass("java.lang.String");
                Hashtable shareHash = new Hashtable();
                while(iterator.Call<bool>("hasNext"))
                {
                    string key = iterator.Call<string>("next");
                    AndroidJavaObject vauleObject = shareParams.Call<AndroidJavaObject>("get", key); 
                    shareHash.Add(key, stringClass.CallStatic<string>("valueOf", vauleObject));
                }       
                ROXShareEventArgs args = new ROXShareEventArgs() 
                {
                    Response = new ROXShareResponse(hostUrl, shareHash, null, null, null, callback)
                };
                GenShareUrl(this, args);
            }
        }

        public void shareContent(string title, string content, byte[] bitmap, AndroidJavaObject callback) 
        {
            if (ShareContent != null)
            {
                Debug.Log("go here, shareContent");
                Debug.Log("title is: " + title);

                ROXShareEventArgs args = new ROXShareEventArgs() 
                {
                    Response = new ROXShareResponse(null, null, title, content, bitmap, callback)
                };
                ShareContent(this, args);
            }
        }
    }

}