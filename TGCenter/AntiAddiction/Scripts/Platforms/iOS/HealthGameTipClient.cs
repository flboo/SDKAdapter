using System;
using AntiAddictionSdk.Common;
using AntiAddictionSdk.Api;
using System.Runtime.InteropServices;
using AOT;

namespace AntiAddictionSdk.Platforms.iOS
{
    public class HealthGameTipClient : IHealthGameTipClient {
        private IntPtr HealthGameTipPtr;
        
        public HealthGameTipClient(IntPtr healthGameTipPtr) {
            HealthGameTipPtr = healthGameTipPtr;
        }
        public string GetTipText() {
            return Externs.AntiAddictionHealthGameTipGetTipText(HealthGameTipPtr);
        }

        public string GetAppInfoText() {
            return Externs.AntiAddictionHealthGameTipGetAppInfoText(HealthGameTipPtr);
        }
    }
}