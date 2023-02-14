using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using TaurusXAdSdk.Api;
using Umeng;

namespace Qarth
{
    public class UmengAdapter : AbstractSDKAdapter, IDataAnalysisAdapter
    {



        public void OnApplicationQuit()
        {

        }

        public void LevelBegin(string levelID)
        {
            GA.StartLevel(levelID);
        }

        public void LevelComplate(string levelID)
        {
            GA.FinishLevel(levelID);
        }

        public void LevelFailed(string levelID, string reason)
        {
            GA.FailLevel(levelID);
        }

        public void CustomEvent(string eventID, object label = null)
        {
            if (label == null || string.IsNullOrEmpty(label.ToString()))
            {
                GA.Event(eventID);
            }
            else
            {
                GA.Event(eventID, label.ToString());
            }
        }

        public void CustomValueEvent(string eventID, float value, string label = null,
            Dictionary<string, string> dic = null)
        {

        }

        public void CustomValueEvent(string eventID, float value, string label = null,
            Dictionary<string, object> dic = null)
        {

        }

        public void Pay(double cash, double coin)
        {
#if UNITY_IPHONE
            GA.Pay(cash, GA.PaySource.AppStore, coin);
#elif UNITY_ANDROID
            GA.Pay(cash, GA.PaySource.Paypal, coin);
#elif UNITY_EDITOR
             
#endif
        }

        public void CustomEventDuration(string eventID, long duration)
        {

        }

        public void CustomEventMapValue(string key, string value)
        {

        }

        public void CustomEventMapSend(string eventID)
        {

        }

        protected override bool DoAdapterInit(SDKConfig config, SDKAdapterConfig adapterConfig)
        {
            UmengConfig umengConfg = adapterConfig as UmengConfig;


#if UNITY_IOS
            GA.StartWithAppKeyAndChannelId(umengConfg.iosAppKey.Trim(), umengConfg.appChannelId);
#elif UNITY_ANDROID //&& !UNITY_EDITOR
            GA.StartWithAppKeyAndChannelId(umengConfg.androidAppKey.Trim(), CustomExtensions.GetSDKChannel());
#endif
            return true;
        }

        public void SetUserLevel(int level)
        {
            GA.SetUserLevel(level);
        }
        public void CustomEventDic(string eventId, Dictionary<string, string> dic)
        {

        }

        public void CustomEventDic(string eventId, Dictionary<string, object> dic)
        {

        }

        public void CheckRemoteConfig()
        {

        }

        public void AddIgnoreEvent(string adapterClassName, List<string> eventIDs)
        {

        }

        public void AddWhiteListEvent(string adapterClassName, List<string> eventIDs)
        {

        }
    }
}
