using UnityEngine;
using AntiAddictionSdk.Common;

namespace AntiAddictionSdk.Platforms.Android
{
    public class RealNameResultClient : IRealNameResultClient {
        private AndroidJavaObject Result;
        public RealNameResultClient(AndroidJavaObject result) {
            Result = result;
        }
        public bool IsInitial() {
            return Result.Call<bool>("isInitial");
        }
        public bool IsSuccess() {
            return Result.Call<bool>("isSuccess");
        }
        public bool IsProcessing() {
            return Result.Call<bool>("isProcessing");
        }
        public bool IsFail() {
            return Result.Call<bool>("isFail");
        }
        public int GetResultCode() {
            return Result.Call<int>("getResultCode");
        }
        public string GetResultMsg() {
            return Result.Call<string>("getResultMsg");
        }
        public string GetPlayerId() {
            return Result.Call<string>("getPlayerId");
        }
    }
}