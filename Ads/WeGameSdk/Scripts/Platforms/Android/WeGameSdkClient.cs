using System;
using UnityEngine;
using System.Collections.Generic;
using WeGameSdk.Common;
using WeGameSdk.Api;

namespace WeGameSdk.Platforms.Android
{
    public class WeGameSdkClient : IWeGameSdkClient
    {
        #region LoginCallback
        class LoginCallback : AndroidJavaProxy
        {
            private WeGameSdkClient mClient;
            public LoginCallback(WeGameSdkClient client) : base("com.we.game.sdk.core.callback.LoginCallback") {
                mClient = client;
            }

            public void onLogin(int code, AndroidJavaObject result)
            {
                Debug.Log("OnLogIn, code: " + code);
                if (mClient.OnLogIn != null)
                {
                    LoginEventArgs args = new LoginEventArgs()
                    {
                        Code = code,
                        LoginResult = result!=null ? new LoginResult(new LoginResultClient(result)) : null
                    };
                    mClient.OnLogIn(this, args);
                }
            }

            public void onExchange(bool success, AndroidJavaObject result)
            {
                if (mClient.OnExchange != null)
                {
                    ExchangeEventArgs args = new ExchangeEventArgs()
                    {
                        Success = success,
                        LoginResult = result!=null ? new LoginResult(new LoginResultClient(result)) : null
                    };
                    mClient.OnExchange(this, args);
                }
            }

            public void onLogout()
            {
                if (mClient.OnLogout != null)
                {
                    mClient.OnLogout(this, EventArgs.Empty);
                }
            }
        }
        #endregion

        #region ExitCallback
        class ExitCallback : AndroidJavaProxy
        {
            private WeGameSdkClient mClient;
            public ExitCallback(WeGameSdkClient client) : base("com.we.game.sdk.core.callback.ExitCallback")
            {
                mClient = client;
            }

            public void onExit(int type)
            {
                if (mClient.OnExit != null)
                {
                    ExitEventArgs args = new ExitEventArgs()
                    {
                        Type = type
                    };
                    mClient.OnExit(this, args);
                }
            }
        }
        #endregion

        public event EventHandler<LoginEventArgs> OnLogIn;
        public event EventHandler<ExchangeEventArgs> OnExchange;
        public event EventHandler<EventArgs> OnLogout;
        public event EventHandler<ExitEventArgs> OnExit;

        static readonly AndroidJavaObject WeGameSDK = new AndroidJavaClass("com.we.game.sdk.core.WeGameSdk").CallStatic<AndroidJavaObject>("getInstance");
        static readonly AndroidJavaObject Context = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");

        private LoginCallback mLoginCallback;
        private ExitCallback mExitCallback;

        WeGameSdkClient()
        {
            mLoginCallback = new LoginCallback(this);
            mExitCallback = new ExitCallback(this);
        }

        static WeGameSdkClient sInstance = new WeGameSdkClient();

        public static WeGameSdkClient Instance
        {
            get
            {
                return sInstance;
            }
        }

        public void Init(bool binded)
        {
            WeGameSDK.Call("init", Context, binded);
        }

        public void Login()
        {
            WeGameSDK.Call("login", mLoginCallback);
        }

        public void Logout()
        {
            WeGameSDK.Call("logout");
        }

        public void SubmitUserInfo(JSONObject jsonObject)
        {
            WeGameSDK.Call("submitUserInfo", WeGameSdkUtil.AndroidJavaJsonObject(jsonObject));
        }

        public void ExitGame()
        {
            WeGameSDK.Call("exitGame", mExitCallback);
        }
    }
}
