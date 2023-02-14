using UnityEngine;
using System.Collections;
using AntiAddictionSdk.Api;
using AntiAddictionSdk.Common;

namespace AntiAddictionSdk.Platforms.Android
{
    public class AntiAddictionClient : IAntiAddictionClient {

        private AndroidJavaObject AntiAddiction;
        public AntiAddictionClient() {
            AndroidJavaClass antiAddictionClass = new AndroidJavaClass(Utils.Class_AntiAddiction);
            AntiAddiction = antiAddictionClass.CallStatic<AndroidJavaObject>("getInstance");
        }

        public User GetUser() {
            AndroidJavaObject user = AntiAddiction.Call<AndroidJavaObject>("getUser");
            return new User(new UserClient(user));
        }

        public void RealName(RealNameCallback callback) {
            AntiAddiction.Call("realName", new AndroidRealNameCallback(callback));
        }

        public void RealName(string name, string idNumber, RealNameCallback callback) {
            AntiAddiction.Call("realName", name, idNumber, new AndroidRealNameCallback(callback));
        }

        private class AndroidRealNameCallback : AndroidJavaProxy {
            private RealNameCallback Callback;
            public AndroidRealNameCallback(RealNameCallback callback) : base(Utils.Class_RealNameCallback) {
                Callback = callback;
            }

            #region Class_RealNameCallback
            public void onFinish(AndroidJavaObject user) {
                if(Callback != null) {
                    Callback.OnFinish(new User(new UserClient(user)));
                }
            }
            #endregion
        }

        public void SetAutoShowTimeLimitPage(bool show) {
            AntiAddiction.Call("setAutoShowTimeLimitPage", show);
        }

        public void SetTimeLimitCallback(TimeLimitCallback callback) {
            AndroidTimeLimitCallback androidCallback = new AndroidTimeLimitCallback(callback);
            AntiAddiction.Call("registerTimeLimitCallback", androidCallback);
        }
        private class AndroidTimeLimitCallback : AndroidJavaProxy {
            private TimeLimitCallback Callback;
            public AndroidTimeLimitCallback(TimeLimitCallback callback) : base(Utils.Class_TimeLimitCallback) {
                Callback = callback;
            }

            #region Class_TimeLimitCallback
            public void onTimeLimit(AndroidJavaObject timeLimit) {
                if(Callback != null) {
                    Callback.OnTimeLimit(new TimeLimit(new TimeLimitClient(timeLimit)));
                }
            }
            #endregion
        }

        public TimeLimit CheckTimeLimitStatus() {
            AndroidJavaObject timeLimit = AntiAddiction.Call<AndroidJavaObject>("checkTimeLimitStatus");
            return new TimeLimit(new TimeLimitClient(timeLimit));
        }
        
        public void Logout() {
            AntiAddiction.Call("logout");
        }


        public AgeTip GetAgeTip() {
            byte[] iconByteArray = AntiAddiction.Call<byte[]>("getAgeTipsImageByteArray");
            AndroidJavaObject gameComplianceInfo = AntiAddiction.Call<AndroidJavaObject>("getGameComplianceInfo");
            return new AgeTip(new AgeTipClient(iconByteArray, gameComplianceInfo));
        }

        public void ShowAgeTipPage(AgeTipCallback callback) {
            AndroidAgeTipCallback androidCallback = new AndroidAgeTipCallback(callback);
            AntiAddiction.Call("showAgeTipsPage", androidCallback);
        }

        private class AndroidAgeTipCallback : AndroidJavaProxy {
            private AgeTipCallback Callback;
            public AndroidAgeTipCallback(AgeTipCallback callback) : base(Utils.Class_AgeTipCallback) {
                Callback = callback;
            }

            #region Class_AgeTipCallback
            public void onAgeTipsOpen() {
                if(Callback != null) {
                    Callback.OnAgeTipsOpen();
                }
            }

            public void onAgeTipsOpenFail(string message) {
                if(Callback != null) {
                    Callback.OnAgeTipsOpenFail(message);
                }
            }

            public void onAgeTipsClose() {
                if(Callback != null) {
                    Callback.OnAgeTipsClose();
                }
            }
            #endregion
        }

        public HealthGameTip GetHealthGameTip() {
            AndroidJavaObject gameComplianceInfo = AntiAddiction.Call<AndroidJavaObject>("getGameComplianceInfo");
            return new HealthGameTip(new HealthGameTipClient(gameComplianceInfo));
        }

        public void ShowHealthGameTipPage(HealthGameTipCallback callback) {
            AndroidHealthGameTipCallback androidCallback = new AndroidHealthGameTipCallback(callback);
            AntiAddiction.Call("showHealthGamePage", androidCallback);
        }

        private class AndroidHealthGameTipCallback : AndroidJavaProxy {
            private HealthGameTipCallback Callback;
            public AndroidHealthGameTipCallback(HealthGameTipCallback callback) : base(Utils.Class_HealthGameTipCallback) {
                Callback = callback;
            }

            #region Class_HealthGameTipCallback
            public void onHealthGameTipsOpen() {
                if(Callback != null) {
                    Callback.OnHealthGameTipOpen();
                }
            }

            public void onHealthGameTipsOpenFail(string message) {
                if(Callback != null) {
                    Callback.OnHealthGameTipOpenFail(message);
                }
            }

            public void onHealthGameTipsClose() {
                if(Callback != null) {
                    Callback.OnHealthGameTipClose();
                }
            }
            #endregion
        }        
    }
}