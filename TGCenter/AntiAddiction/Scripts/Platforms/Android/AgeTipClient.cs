using AntiAddictionSdk.Common;
using UnityEngine;

namespace AntiAddictionSdk.Platforms.Android
{
    public class AgeTipClient : IAgeTipClient {

        private byte[] IconByteArray;
        private AndroidJavaObject GameComplianceInfo;
        
        public AgeTipClient(byte[] iconByteArray, AndroidJavaObject gameComplianceInfo) {
            IconByteArray = iconByteArray;
            GameComplianceInfo = gameComplianceInfo;
        }

        public byte[] GetIconByteArray() {
            return IconByteArray;
        }
        public string GetText() {
            return GameComplianceInfo != null ? GameComplianceInfo.Call<string>("getAgeTips") : "";
        }
    }
}