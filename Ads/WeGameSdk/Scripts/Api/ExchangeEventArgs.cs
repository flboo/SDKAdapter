using System;
namespace WeGameSdk.Api
{
    public class ExchangeEventArgs  : EventArgs
    {
        public bool Success { get; set; }
        public LoginResult LoginResult { get; set; }
    }
}
