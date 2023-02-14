using System;
using UnityEngine;
using WeGameSdk.Common;

namespace WeGameSdk.Platforms.Android
{
    public class LoginResultClient : ILoginResultClient
    {
        private AndroidJavaObject mLoginResult;

        public LoginResultClient(AndroidJavaObject loginResult)
        {
            mLoginResult = loginResult;
        }

        #region ILoginResultClient
        public string GetUserId()
        {
            if(mLoginResult!=null)
            {
                return mLoginResult.Call<string>("getUserId");
            }
            return "";
        }
        public string GetUserName()
        {
            if (mLoginResult != null)
            {
                return mLoginResult.Call<string>("getUserName");
            }
            return "";
        }
        public string GetToken()
        {
            if (mLoginResult != null)
            {
                return mLoginResult.Call<string>("getToken");
            }
            return "";
        }
        public string GetExtension()
        {
            if (mLoginResult != null)
            {
                return mLoginResult.Call<string>("getExtension");
            }
            return "";
        }
        public string GetDescription()
        {
            if (mLoginResult != null)
            {
                return mLoginResult.Call<string>("getDesc");
            }
            return "";
        }
        #endregion
    }
}
