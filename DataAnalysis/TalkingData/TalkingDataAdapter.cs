using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using TaurusXAdSdk.Api;

namespace Qarth
{
    public class TalkingDataAdapter : AbstractSDKAdapter, IDataAnalysisAdapter
    {



        public void OnApplicationQuit()
        {

        }

        public void LevelBegin(string levelID)
        {
        }

        public void LevelComplate(string levelID)
        {
        }

        public void LevelFailed(string levelID, string reason)
        {
        }

        public void CustomEvent(string eventID, object label = null)
        {
#if !UNITY_EDITOR
            if (label == null || string.IsNullOrEmpty(label.ToString()))
                TalkingDataPlugin.TrackEvent(eventID);
            else
                TalkingDataPlugin.TrackEventWithLabel(eventID, label.ToString());
#endif
        }

        public void CustomValueEvent(string eventID, float value, string label = null,
            Dictionary<string, string> dic = null)
        {
#if !UNITY_EDITOR
            var ps = new Dictionary<string, object>();
            if (dic != null)
            {
                foreach (var key in dic.Keys)
                {
                    ps.Add(key, dic[key]);
                }
            }
            TalkingDataPlugin.TrackEventWithValue(eventID, string.IsNullOrEmpty(label) ? "" : label, ps, value);
#endif
        }

        public void CustomValueEvent(string eventID, float value, string label = null,
            Dictionary<string, object> dic = null)
        {
#if !UNITY_EDITOR
            TalkingDataPlugin.TrackEventWithValue(eventID, string.IsNullOrEmpty(label) ? "" : label, dic, value);
#endif
        }

        public void Pay(double cash, double coin)
        {
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
            TalkingDataConfig conf = adapterConfig as TalkingDataConfig;
#if UNITY_IOS
            TalkingDataPlugin.SessionStarted(conf.appId, "ios");
#elif UNITY_ANDROID && !UNITY_EDITOR
            TalkingDataPlugin.SessionStarted(conf.appId, TaurusXConfigUtil.GetChannel());
            TalkingDataPlugin.SetExceptionReportEnabled(true);
#endif

            return true;
        }

        public void SetUserLevel(int level)
        {
        }
        public void CustomEventDic(string eventId, Dictionary<string, string> dic)
        {
#if !UNITY_EDITOR
            Dictionary<string, object> objDict = new Dictionary<string, object>();
            foreach (var key in dic.Keys)
            {
                objDict.Add(key, dic[key]);
            }
            TalkingDataPlugin.TrackEventWithParameters(eventId, "label", objDict);
#endif
        }

        public void CustomEventDic(string eventId, Dictionary<string, object> dic)
        {
#if !UNITY_EDITOR
            TalkingDataPlugin.TrackEventWithParameters(eventId, "label", dic);
#endif
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
