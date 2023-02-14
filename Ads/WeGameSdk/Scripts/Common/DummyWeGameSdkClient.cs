using System;
using System.Collections.Generic;
using WeGameSdk.Api;

namespace WeGameSdk.Common
{
    public class DummyWeGameSdkClient : IWeGameSdkClient
    {
        public event EventHandler<LoginEventArgs> OnLogIn;
        public event EventHandler<ExchangeEventArgs> OnExchange;
        public event EventHandler<EventArgs> OnLogout;
        public event EventHandler<ExitEventArgs> OnExit;

        public void Init(bool binded) {
        }

        public void Login() {
        }

        public void Logout() {
        }

        public void SubmitUserInfo(JSONObject jsonObject) {
        }

        public void ExitGame() {
        }
    }
}
