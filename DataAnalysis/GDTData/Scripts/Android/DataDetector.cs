namespace Tencent.GDT
{
#if UNITY_ANDROID
    using UnityEngine;
    using System.Collections.Generic;

    public sealed class DataDetector : IDataDetector
    {
        private AndroidJavaObject detectorProxy;

        public DataDetector()
        {
            if (!GDTSDKManager.CheckInit())
            {
                return;
            }
            AndroidJavaClass detectorClass = new AndroidJavaClass("com.qq.e.comm.datadetect.GDTDataDetector");
            detectorProxy = detectorClass.CallStatic<AndroidJavaObject>("getInstance");
        }

        public void Report(string eventName)
        {
            Report(eventName, null);
        }

        public void Report(string eventName, Dictionary<string, string> param)
        {
            AndroidJavaObject eventObj = this.GenerateGDTEvent(eventName, param);
            detectorProxy.Call("report", eventObj);
        }

        private AndroidJavaObject GenerateGDTEvent(string eventName, Dictionary<string, string> param)
        {
            if(eventName == null || eventName.Length == 0)
            {
                return null;
            }
            AndroidJavaObject eventObj = new AndroidJavaObject("com.qq.e.comm.datadetect.GDTDetectEvent", eventName);
            if(param != null && param.Count != 0)
            {
                foreach(KeyValuePair<string, string> pair in param)
                {
                    eventObj.Call("put", pair.Key, pair.Value);
                    Debug.unityLogger.Log("howielog", pair.Key + "=" + pair.Value);
                }
            }
            return eventObj;
        }
    }
#endif
}