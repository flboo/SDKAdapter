using System;
using System.Collections.Generic;
using WeGameSdk.Api;

namespace WeGameSdk.Common
{
    public interface IWeGameSdkClient
    {
        event EventHandler<LoginEventArgs> OnLogIn;
        event EventHandler<ExchangeEventArgs> OnExchange;
        event EventHandler<EventArgs> OnLogout;
        event EventHandler<ExitEventArgs> OnExit;

        void Init(bool binded);

        void Login();

        void Logout();

        void SubmitUserInfo(JSONObject jsonObject);

        void ExitGame();
    }
}