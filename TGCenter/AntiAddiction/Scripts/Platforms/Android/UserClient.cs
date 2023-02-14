using UnityEngine;
using AntiAddictionSdk.Common;
using AntiAddictionSdk.Api;

namespace AntiAddictionSdk.Platforms.Android
{
    public class UserClient : IUserClient {

        private AndroidJavaObject User;
        private RealNameResult Result;
        public UserClient(AndroidJavaObject user) {
            User = user;

            AndroidJavaObject result = User.Call<AndroidJavaObject>("getRealNameResult");
            RealNameResultClient resultClient = new RealNameResultClient(result);
            Result = new RealNameResult(resultClient);
        }

        public RealNameResult GetRealNameResult() {
            return Result;
        }

        public int GetAge() {
            return User.Call<int>("getAge");
        }
        
        public bool IsTourist() {
            return User.Call<bool>("isTourist");
        }

        public bool IsChild() {
            return User.Call<bool>("isChild");
        }

        public bool IsAdult() {
            return User.Call<bool>("isAdult");
        }
    }
}