using System;
using UnityEngine;
using System.Collections.Generic;
using WeGameSdk.Common;
using WeGameSdk.Platforms;

namespace WeGameSdk.Api
{
    public class WeGameSDK
    {
        public event EventHandler<LoginEventArgs> OnLogIn;
        public event EventHandler<ExchangeEventArgs> OnExchange;
        public event EventHandler<EventArgs> OnLogout;
        public event EventHandler<ExitEventArgs> OnExit;

        private IWeGameSdkClient mClient;

        private IWeGameSdkClient GetWeGameSdkClient() {
            IWeGameSdkClient client = ClientFactory.GetWeGameSdkClient();

            client.OnLogIn += (sender, args) =>
            {
                if (OnLogIn != null)
                {
                    OnLogIn(this, args);
                }
            };

            client.OnExchange += (sender, args) =>
            {
                if (OnExchange != null)
                {
                    OnExchange(this, args);
                }
            };

            client.OnLogout += (sender, args) =>
            {
                if (OnLogout != null)
                {
                    OnLogout(this, args);
                }
            };

            client.OnExit += (sender, args) =>
            {
                if (OnExit != null)
                {
                    OnExit(this, args);
                }
            };

            return client;
        }

        WeGameSDK()
        {
            mClient = GetWeGameSdkClient();
        }

        static WeGameSDK sInstance = new WeGameSDK();

        public static WeGameSDK Instance
        {
            get
            {
                return sInstance;
            }
        }

        public void Init(bool binded)
        {
            mClient.Init(binded);
        }

        public void Login()
        {
            mClient.Login();
        }

        public void Logout()
        {
            mClient.Logout();
        }

        public void SubmitUserInfo(JSONObject jsonObject)
        {
            mClient.SubmitUserInfo(jsonObject);
        }

        public void ExitGame()
        {
            mClient.ExitGame();
        }
    }
}