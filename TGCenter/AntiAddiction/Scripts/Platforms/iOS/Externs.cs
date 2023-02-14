using System;
using System.Runtime.InteropServices;

namespace AntiAddictionSdk.Platforms.iOS
{
    internal class Externs
    {

        #region Common
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void AntiAddictionRelease(IntPtr obj);
        #endregion
        

        #region AntiAddiction
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr AntiAddictionSetAntiAddictionClient(IntPtr antiAddictionClient);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr AntiAddictionGetUser();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void AntiAddictionRealName(AntiAddictionClient.AntiAddictionRealNameCallback callback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void AntiAddictionRealNameUseCustomUI(
            string name, 
            string idNumber,
            AntiAddictionClient.AntiAddictionRealNameCallback callback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void AntiAddictionSetAutoShowTimeLimitPage(bool show);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void AntiAddictionSetTimeLimitCallback(AntiAddictionClient.AntiAddictionTimeLimitCallback callback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr AntiAddictionCheckTimeLimitStatus();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr AntiAddictionLogout();
        #endregion


        #region User
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr AntiAddictionUserGetRealNameResult(IntPtr user);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int AntiAddictionUserGetAge(IntPtr user);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool AntiAddictionUserIsTourist(IntPtr user);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool AntiAddictionUserIsChild(IntPtr user);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool AntiAddictionUserIsAdult(IntPtr user);
        #endregion


        #region RealNameResult
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool AntiAddictionRealNameResultIsInitial(IntPtr realNameResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool AntiAddictionRealNameResultIsSuccess(IntPtr realNameResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool AntiAddictionRealNameResultIsProcessing(IntPtr realNameResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool AntiAddictionRealNameResultIsFail(IntPtr realNameResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int AntiAddictionRealNameResultGetResultCode(IntPtr realNameResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string AntiAddictionRealNameResultGetResultMsg(IntPtr realNameResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string AntiAddictionRealNameResultGetPlayerId(IntPtr realNameResult);
        #endregion


        #region TimeLimit
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int AntiAddictionTimeLimitGetReason(IntPtr timeLimit);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool AntiAddictionTimeLimitIsLimit(IntPtr timeLimit);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long AntiAddictionTimeLimitGetTimeToLimit(IntPtr timeLimit);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr AntiAddictionTimeLimitGetLimitTip(IntPtr timeLimit);
        #endregion


        #region LimitTip
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string AntiAddictionLimitTipGetTitle(IntPtr limitTip);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string AntiAddictionLimitTipGetDesc(IntPtr limitTip);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string AntiAddictionLimitTipGetButton(IntPtr limitTip);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool AntiAddictionLimitTipCanRealName(IntPtr limitTip);
        #endregion


        #region AgeTip
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr AntiAddictionGetAgeTip();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string AntiAddictionAgeTipGetIconByteArrayString(IntPtr ageTip);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string AntiAddictionAgeTipGetText(IntPtr ageTip);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void AntiAddictionShowAgeTip(
            AntiAddictionClient.AntiAddictionAgeTipShowCallback showCallback,
            AntiAddictionClient.AntiAddictionAgeTipCloseCallback closeCallback);
        #endregion


        #region HealthGameTip
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr AntiAddictionGetHealthGameTip();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string AntiAddictionHealthGameTipGetTipText(IntPtr healthGameTip);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string AntiAddictionHealthGameTipGetAppInfoText(IntPtr healthGameTip);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void AntiAddictionShowHealthGameTip(
            AntiAddictionClient.AntiAddictionHealthGameTipShowCallback showCallback,
            AntiAddictionClient.AntiAddictionHealthGameTipShowFailedCallback showFailedCallback,
            AntiAddictionClient.AntiAddictionHealthGameTipCloseCallback closeCallback);
        #endregion
    }
}