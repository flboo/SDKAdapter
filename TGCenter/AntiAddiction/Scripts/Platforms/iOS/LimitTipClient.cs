using System;
using System.Runtime.InteropServices;
using AntiAddictionSdk.Common;

namespace AntiAddictionSdk.Platforms.iOS
{
    public class LimitTipClient : ILimitTipClient {
        private IntPtr LimitTip;
        public LimitTipClient(IntPtr limitTip) {
            LimitTip = limitTip;
        }

        public string GetTitle() {
            return Externs.AntiAddictionLimitTipGetTitle(LimitTip);
        }

        public string GetDesc() {
            return Externs.AntiAddictionLimitTipGetDesc(LimitTip);
        }

        public string GetButton() {
            return Externs.AntiAddictionLimitTipGetButton(LimitTip);
        }

        public bool CanRealName() {
            return Externs.AntiAddictionLimitTipCanRealName(LimitTip);
        }
    }
}