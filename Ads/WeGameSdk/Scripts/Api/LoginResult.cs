using System;
using WeGameSdk.Common;

namespace WeGameSdk.Api
{
    public class LoginResult
    {
        readonly ILoginResultClient mClinet;

        public LoginResult(ILoginResultClient client)
        {
            mClinet = client;
        }

        public string GetUserId() {
            return mClinet.GetUserId();
        }

        public string GetUserName() {
            return mClinet.GetUserName();
        }

        public string GetToken() {
            return mClinet.GetToken();
        }

        public string GetExtension() {
            return mClinet.GetExtension();
        }

        public string GetDescription() {
            return mClinet.GetDescription();
        }
    }
}
