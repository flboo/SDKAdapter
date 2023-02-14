namespace Tencent.GDT
{
#if UNITY_ANDROID
    using UnityEngine;
    public class GDTSDKManager
    {
        private static string appId;
        private static bool hasInit = false;

        public static bool Init(string appId)
        {
            AndroidJavaObject gdtAdManager = new AndroidJavaClass("com.qq.e.comm.managers.GDTAdSdk");
            hasInit = true;
            gdtAdManager.CallStatic("init", GDTUtils.GetActivity(), appId);
            return hasInit;
        }

        internal static bool CheckInit()
        {
            if (!hasInit)
            {
                Debug.unityLogger.Log("GDT_UNITY_LOG", "请先初始化 SDK ！");
            }
            return hasInit;
        }
    }
#endif    
}