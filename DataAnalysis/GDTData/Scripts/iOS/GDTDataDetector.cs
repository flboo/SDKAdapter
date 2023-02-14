namespace Tencent.GDT
{
#if UNITY_IOS
    using System;
    using System.Runtime.InteropServices;
    using System.Collections.Generic;

    public sealed class DataDetector : IDataDetector
    {
        [DllImport("__Internal")]
        private static extern void GDT_UnionPlatform_DataDetectorReport(string eventName, IntPtr paras);

        [DllImport("__Internal")]
        private static extern void GDT_UnionPlatform_DataDetectorReportAddParams(IntPtr paras, string key, string value);

        [DllImport("__Internal")]
        private static extern IntPtr GDT_UnionPlatform_DataDetectorReportDictionary();

        public void Report(string eventName)
        {
            Report(eventName, null);
        }

        public void Report(string eventName, Dictionary<string, string> param)
        {
            IntPtr dict = GDT_UnionPlatform_DataDetectorReportDictionary();
            foreach (KeyValuePair<string, string> pair in param)
            {
                GDT_UnionPlatform_DataDetectorReportAddParams(dict, pair.Key, pair.Value);
            }
            GDT_UnionPlatform_DataDetectorReport(eventName, dict);
        }
    }
#endif
}