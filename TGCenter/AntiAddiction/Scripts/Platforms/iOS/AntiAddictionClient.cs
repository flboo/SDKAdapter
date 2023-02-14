using System;
using AntiAddictionSdk.Common;
using AntiAddictionSdk.Api;
using System.Runtime.InteropServices;
using AOT;

namespace AntiAddictionSdk.Platforms.iOS
{
    public class AntiAddictionClient : IAntiAddictionClient, IDisposable {

        private IntPtr mAntiAddictionClientPtr;
        private RealNameCallback realNameCallback;
        private TimeLimitCallback timeLimitCallback;

        internal delegate void AntiAddictionRealNameCallback(IntPtr antiAddictionClient, IntPtr user);
        internal delegate void AntiAddictionTimeLimitCallback(IntPtr antiAddictionClient, IntPtr timeLimit);

        public AntiAddictionClient()
        {
            mAntiAddictionClientPtr = (IntPtr)GCHandle.Alloc(this);
            Externs.AntiAddictionSetAntiAddictionClient(mAntiAddictionClientPtr);
        }

        public User GetUser() {
            return new User(new UserClient(Externs.AntiAddictionGetUser()));
        }
        public void RealName(RealNameCallback callback) {
            realNameCallback = callback;
            Externs.AntiAddictionRealName(iOSRealNameCallback);
        }
        public void RealName(string name, string idNumber, RealNameCallback callback) {
            realNameCallback = callback;
            Externs.AntiAddictionRealNameUseCustomUI(name, idNumber, iOSRealNameCallback);
        }

        #region AntiAddictionRealNameCallback
        [MonoPInvokeCallback(typeof(AntiAddictionRealNameCallback))]
        private static void iOSRealNameCallback(IntPtr antiAddictionClient, IntPtr user)
        {
            AntiAddictionClient client = IntPtrToAntiAddictionClient(antiAddictionClient);
            if (client.realNameCallback != null) {
                client.realNameCallback.OnFinish(new User(new UserClient(user)));
            }
        }
        #endregion

        public void SetAutoShowTimeLimitPage(bool show) {
            Externs.AntiAddictionSetAutoShowTimeLimitPage(show);
        }
        public void SetTimeLimitCallback(TimeLimitCallback callback){
            timeLimitCallback = callback;
            Externs.AntiAddictionSetTimeLimitCallback(iOSTimeLimitCallback);
        }

        #region AntiAddictionTimeLimitCallback
        [MonoPInvokeCallback(typeof(AntiAddictionTimeLimitCallback))]
        private static void iOSTimeLimitCallback(IntPtr antiAddictionClient, IntPtr timeLimit)
        {
            AntiAddictionClient client = IntPtrToAntiAddictionClient(antiAddictionClient);
            if (client.timeLimitCallback != null) {
                client.timeLimitCallback.OnTimeLimit(new TimeLimit(new TimeLimitClient(timeLimit)));
            }
        }
        #endregion

        public TimeLimit CheckTimeLimitStatus() {
            return null;
        }
        public void Logout() {
            Externs.AntiAddictionLogout();
        }


        private AgeTipCallback ageTipCallback;
        internal delegate void AntiAddictionAgeTipShowCallback(IntPtr antiAddictionClient);
        internal delegate void AntiAddictionAgeTipCloseCallback(IntPtr antiAddictionClient);

        public AgeTip GetAgeTip() {
            IntPtr ageTip = Externs.AntiAddictionGetAgeTip();
            return new AgeTip(new AgeTipClient(ageTip));
        }

        public void ShowAgeTipPage(AgeTipCallback callback) {
            ageTipCallback = callback;
            Externs.AntiAddictionShowAgeTip(iOSAgeTipShowCallback, iOSAgeTipCloseCallback);
        }

        #region AgeTipCallback
        [MonoPInvokeCallback(typeof(AntiAddictionAgeTipShowCallback))]
        private static void iOSAgeTipShowCallback(IntPtr antiAddictionClient)
        {
            AntiAddictionClient client = IntPtrToAntiAddictionClient(antiAddictionClient);
            if (client.ageTipCallback != null) {
                client.ageTipCallback.OnAgeTipsOpen();
            }
        }

        [MonoPInvokeCallback(typeof(AntiAddictionAgeTipCloseCallback))]
        private static void iOSAgeTipCloseCallback(IntPtr antiAddictionClient)
        {
            AntiAddictionClient client = IntPtrToAntiAddictionClient(antiAddictionClient);
            if (client.ageTipCallback != null) {
                client.ageTipCallback.OnAgeTipsClose();
            }
        }
        #endregion


        private HealthGameTipCallback healthGameTipCallback;
        internal delegate void AntiAddictionHealthGameTipShowCallback(IntPtr antiAddictionClient);
        internal delegate void AntiAddictionHealthGameTipShowFailedCallback(IntPtr antiAddictionClient, int code, string message);
        internal delegate void AntiAddictionHealthGameTipCloseCallback(IntPtr antiAddictionClient);
        
        public HealthGameTip GetHealthGameTip() {
            IntPtr healthGameTip = Externs.AntiAddictionGetHealthGameTip();
            return new HealthGameTip(new HealthGameTipClient(healthGameTip));
        }

        public void ShowHealthGameTipPage(HealthGameTipCallback callback) {
            healthGameTipCallback = callback;
            Externs.AntiAddictionShowHealthGameTip(
                iOSHealthGameTipShowCallback,
                iOSHealthGameTipShowFailedCallback, 
                iOSHealthGameTipCloseCallback);
        }

        #region HealthGameCallback
        [MonoPInvokeCallback(typeof(AntiAddictionHealthGameTipShowCallback))]
        private static void iOSHealthGameTipShowCallback(IntPtr antiAddictionClient)
        {
            AntiAddictionClient client = IntPtrToAntiAddictionClient(antiAddictionClient);
            if (client.healthGameTipCallback != null) {
                client.healthGameTipCallback.OnHealthGameTipOpen();
            }
        }

        [MonoPInvokeCallback(typeof(AntiAddictionHealthGameTipShowFailedCallback))]
        private static void iOSHealthGameTipShowFailedCallback(IntPtr antiAddictionClient, int code, string message)
        {
            AntiAddictionClient client = IntPtrToAntiAddictionClient(antiAddictionClient);
            if (client.healthGameTipCallback != null) {
                client.healthGameTipCallback.OnHealthGameTipOpenFail(message);
            }
        }

        [MonoPInvokeCallback(typeof(AntiAddictionHealthGameTipCloseCallback))]
        private static void iOSHealthGameTipCloseCallback(IntPtr antiAddictionClient)
        {
            AntiAddictionClient client = IntPtrToAntiAddictionClient(antiAddictionClient);
            if (client.healthGameTipCallback != null) {
                client.healthGameTipCallback.OnHealthGameTipClose();
            }
        }
        #endregion


        public void Dispose()
        {
            ((GCHandle)mAntiAddictionClientPtr).Free();
        }

        ~AntiAddictionClient()
        {
            Dispose();
        }

        private static AntiAddictionClient IntPtrToAntiAddictionClient(IntPtr antiAddictionClient)
        {
            GCHandle handle = (GCHandle)antiAddictionClient;
            return handle.Target as AntiAddictionClient;
        }
    }
}