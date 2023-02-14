namespace Tencent.GDT
{
#if UNITY_ANDROID
    using UnityEngine;
    public class GDTAction
    {
        private static AndroidJavaClass classGDTAction;
        public static void Init(string actionSetId, string secretKey)
        {
            classGDTAction = new AndroidJavaClass("com.qq.gdt.action.GDTAction");
            classGDTAction.CallStatic("init", GDTUtils.GetActivity(), actionSetId, secretKey);
        }

        public static void LogAction(string eventName)
        {
            if (classGDTAction == null)
            {
                return;
            }
            classGDTAction.CallStatic("logAction", eventName);
        }
    }
#endif    
}