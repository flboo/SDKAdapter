using System;
using System.Runtime.InteropServices;
using AntiAddictionSdk.Common;
using AntiAddictionSdk.Api;

namespace AntiAddictionSdk.Platforms.iOS
{
    public class UserClient : IUserClient {

        private IntPtr User;
        private RealNameResult Result;
        public UserClient(IntPtr user) {
            User = user;

            IntPtr result = Externs.AntiAddictionUserGetRealNameResult(user);
            Result = new RealNameResult(new RealNameResultClient(result));
        }

        public RealNameResult GetRealNameResult() {
            return Result;
        }

        public int GetAge() {
            return Externs.AntiAddictionUserGetAge(User);
        }
        
        public bool IsTourist() {
            return Externs.AntiAddictionUserIsTourist(User);
        }

        public bool IsChild() {
            return Externs.AntiAddictionUserIsChild(User);
        }

        public bool IsAdult() {
            return Externs.AntiAddictionUserIsAdult(User);
        }
    }
}