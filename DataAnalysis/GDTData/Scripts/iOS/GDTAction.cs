namespace Tencent.GDT
{
#if UNITY_IOS
    using System.Runtime.InteropServices;

    public class GDTAction
    {
        [DllImport("__Internal")]
        private static extern void GDT_UnionPlatform_ActionInit(string actionSetId, string secretKey);

        [DllImport("__Internal")]
        private static extern void GDT_UnionPlatform_ActionLog(string eventName);

        public static void Init(string actionSetId, string secretKey)
        {
            GDT_UnionPlatform_ActionInit(actionSetId, secretKey);
        }

        public static void LogAction(string eventName)
        {
            GDT_UnionPlatform_ActionLog(eventName);
        }
    }
#endif    
}
