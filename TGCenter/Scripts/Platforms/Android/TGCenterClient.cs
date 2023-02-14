using UnityEngine;
using TGCenterSdk.Api;
using TGCenterSdk.Common;

namespace TGCenterSdk.Platforms.Android
{
    public class TGCenterClient : ITGCenterClient {

        static TGCenterClient instance = new TGCenterClient();

        public static TGCenterClient Instance {
            get {
                return instance;
            }
        }

        private readonly AndroidJavaClass tgCenterClass;

        private TGCenterClient() {
            tgCenterClass = new AndroidJavaClass(Utils.Class_TGCenter);
        }
        
        #region ITGCenterClient
        public void Init(InitConfig config) {
            AndroidJavaClass initConfigClass = new AndroidJavaClass(Utils.Class_InitConfig);
            AndroidJavaObject builder = initConfigClass.CallStatic<AndroidJavaObject>("newBuilder");

            builder.Call<AndroidJavaObject>("setDebugMode", config.DebugMode);
            builder.Call<AndroidJavaObject>("setChannel", config.Channel);

            Day1Retention day1Retention = config.Day1Retention;
            if(day1Retention != null) {
                AndroidJavaObject retention = new AndroidJavaObject(Utils.Class_Day1Retention, 
                    Utils.GetRetentionType(day1Retention.RetentionType), day1Retention.StartCount, day1Retention.EndCount);
                builder.Call<AndroidJavaObject>("setDay1Retention", retention);
            }

            AndroidJavaObject configObject = builder.Call<AndroidJavaObject>("build");
            tgCenterClass.CallStatic("init", Utils.GetPlayerActivity(), configObject);
        }
        
        public bool IsUserAgreePolicy() {
            return tgCenterClass.CallStatic<bool>("isUserAgreePolicy", Utils.GetPlayerActivity());
        }

        public void ClearCache() {
            tgCenterClass.CallStatic("clearCache", Utils.GetPlayerActivity());
        }

        public void ShowDebugPage() {
            tgCenterClass.CallStatic("showDebugPage", Utils.GetPlayerActivity());
        }
        #endregion
    }
}