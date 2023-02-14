using AntiAddictionSdk.Common;
using UnityEngine;

namespace AntiAddictionSdk.Platforms.Android
{
    public class HealthGameTipClient : IHealthGameTipClient {
        private AndroidJavaObject GameComplianceInfo;
        
        public HealthGameTipClient(AndroidJavaObject gameComplianceInfo) {
            GameComplianceInfo = gameComplianceInfo;
        }
        public string GetTipText() {
            return GameComplianceInfo != null ? GameComplianceInfo.Call<string>("getSplashAdvice") : "";
        }

        public string GetAppInfoText() {
            return GameComplianceInfo != null ? GameComplianceInfo.Call<string>("getSplashInfo") : "";
        }
    }
}