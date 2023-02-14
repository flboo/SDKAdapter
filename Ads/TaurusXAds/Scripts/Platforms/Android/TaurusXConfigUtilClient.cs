using UnityEngine;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class TaurusXConfigUtilClient : ITaurusXConfigUtilClient
    {
        AndroidJavaClass playerClass;
        AndroidJavaObject activity;
        AndroidJavaClass gameConfigUtilClass;

        public TaurusXConfigUtilClient()
        {
            playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            gameConfigUtilClass = new AndroidJavaClass(Utils.GameConfigClassName);
        }

        public string GetAppId()
        {
            return gameConfigUtilClass.CallStatic<string>("getAppId", activity);
        }

        public string GetAdUnitId(string name)
        {
            return gameConfigUtilClass.CallStatic<string>("getAdUnitId", activity, name);
        }

        public string GetChannel()
        {
            return gameConfigUtilClass.CallStatic<string>("getChannel", activity);
        }

        public string GetString(string name)
        {
            return gameConfigUtilClass.CallStatic<string>("getString", activity, name);
        }
    }
}
