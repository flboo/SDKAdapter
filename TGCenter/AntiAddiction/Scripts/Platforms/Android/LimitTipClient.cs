using UnityEngine;
using AntiAddictionSdk.Common;

namespace AntiAddictionSdk.Platforms.Android
{
    public class LimitTipClient : ILimitTipClient {
        private AndroidJavaObject LimitTip;
        public LimitTipClient(AndroidJavaObject limitTip) {
            LimitTip = limitTip;
        }

        public string GetTitle() {
            return LimitTip.Call<string>("getTitle");
        }

        public string GetDesc() {
            return LimitTip.Call<string>("getDesc");
        }

        public string GetButton() {
            return LimitTip.Call<string>("getButton");
        }

        public bool CanRealName() {
            return LimitTip.Call<bool>("canRealName");
        }
    }
}