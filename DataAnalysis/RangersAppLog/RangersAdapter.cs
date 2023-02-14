using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using TaurusXAdSdk.Api;

namespace Qarth
{
    public class RangersAdapter : AbstractSDKAdapter, IDataAnalysisAdapter
    {
        public string Platform
        {
            get { return "rangers"; }
        }

        public void OnApplicationQuit()
        {

        }

        public void OnApplicationFocus()
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
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (label == null)
            {
                dic.Add("null", "null");
            }
            else
            {
                dic.Add("label", label);
            }
            RangersClientMgr.S.GetInstance().SendEvent(eventID, dic);
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
            RangersConfig rangersConfig = adapterConfig as RangersConfig;
            if (string.IsNullOrEmpty(rangersConfig.appId))
                return false;

            RangersClientMgr.S.GetInstance().InitConf(rangersConfig.appId,
                string.IsNullOrEmpty(rangersConfig.channel) ?
                    TaurusXConfigUtil.GetChannel() : rangersConfig.channel);
            return true;
        }

        public void SetUserLevel(int level)
        {

        }
        public void CustomEventDic(string eventId, Dictionary<string, string> dic)
        {
            var objDict = new Dictionary<string, object>();
            if (dic != null)
            {
                foreach (var key in dic.Keys)
                {
                    objDict.Add(key, dic[key]);
                }
            }
            RangersClientMgr.S.GetInstance().SendEvent(eventId, objDict);
        }
        public void CustomEventDic(string eventId, Dictionary<string, object> dic)
        {
            RangersClientMgr.S.GetInstance().SendEvent(eventId, dic);
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
