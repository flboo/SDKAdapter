using System;
using System.Runtime.InteropServices;
using AntiAddictionSdk.Common;

namespace AntiAddictionSdk.Platforms.iOS
{
    public class RealNameResultClient : IRealNameResultClient {
        private IntPtr Result;
        public RealNameResultClient(IntPtr result) {
            Result = result;
        }

        public bool IsInitial() {
            return Externs.AntiAddictionRealNameResultIsInitial(Result);
        }

        public bool IsSuccess() {
            return Externs.AntiAddictionRealNameResultIsSuccess(Result);
        }

        public bool IsProcessing() {
            return Externs.AntiAddictionRealNameResultIsProcessing(Result);
        }

        public bool IsFail() {
            return Externs.AntiAddictionRealNameResultIsFail(Result);
        }

        public int GetResultCode() {
            return Externs.AntiAddictionRealNameResultGetResultCode(Result);
        }

        public string GetResultMsg() {
            return Externs.AntiAddictionRealNameResultGetResultMsg(Result);
        }

        public string GetPlayerId() {
            return Externs.AntiAddictionRealNameResultGetPlayerId(Result);
        }
    }
}