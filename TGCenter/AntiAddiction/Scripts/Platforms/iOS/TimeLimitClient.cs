using System;
using System.Runtime.InteropServices;
using AntiAddictionSdk.Common;
using AntiAddictionSdk.Api;

namespace AntiAddictionSdk.Platforms.iOS
{
    public class TimeLimitClient : ITimeLimitClient {
        private IntPtr TimeLimit;
        public TimeLimitClient(IntPtr timeLimit) {
            TimeLimit = timeLimit;
        }
        public int GetReason() {
            return Externs.AntiAddictionTimeLimitGetReason(TimeLimit);
        }

        public bool IsLimit() {
            return Externs.AntiAddictionTimeLimitIsLimit(TimeLimit);
        }

        public long GetTimeToLimit() {
            return Externs.AntiAddictionTimeLimitGetTimeToLimit(TimeLimit);
        }

        public LimitTip GetLimitTip() {
            return new LimitTip(new LimitTipClient(Externs.AntiAddictionTimeLimitGetLimitTip(TimeLimit)));
        }
    }
}