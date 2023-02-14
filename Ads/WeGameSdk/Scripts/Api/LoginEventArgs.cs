using System;
namespace WeGameSdk.Api
{
    public class LoginEventArgs  : EventArgs
    {
        public static int LOGIN_SUCCESS = 0;
        public static int LOGIN_FAILED = -1;
        public static int LOGIN_CANCEL = -2;

        public int Code { get; set; }
        public LoginResult LoginResult { get; set; }
    }
}
