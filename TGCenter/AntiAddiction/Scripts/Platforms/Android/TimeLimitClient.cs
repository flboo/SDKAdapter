using UnityEngine;
using AntiAddictionSdk.Common;
using AntiAddictionSdk.Api;

namespace AntiAddictionSdk.Platforms.Android
{
    public class TimeLimitClient : ITimeLimitClient {
        private AndroidJavaObject TimeLimit;
        public TimeLimitClient(AndroidJavaObject timeLimit) {
            TimeLimit = timeLimit;
        }
        public int GetReason() {
            return TimeLimit.Call<int>("getReason");
        }

        public bool IsLimit() {
            return TimeLimit.Call<bool>("isLimit");
        }

        public long GetTimeToLimit() {
            return TimeLimit.Call<long>("getTimeToLimit");
        }

        public LimitTip GetLimitTip() {
            AndroidJavaObject limitTip = TimeLimit.Call<AndroidJavaObject>("getLimitTip");
            return new LimitTip(new LimitTipClient(limitTip));
        }
    }
}