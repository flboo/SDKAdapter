using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using GameAnalyticsSdk.Api;
using TaurusXAdSdk.Api;
using System.Linq;

namespace Qarth
{
    public class GASdkAdapter : AbstractSDKAdapter, IDataAnalysisAdapter
    {
        private bool m_IsCloseListCtrl;
        private List<string> m_LstWhiteListEvts = new List<string>();
        private List<string> m_LstIgnore = new List<string>();

        public void OnApplicationQuit()
        {

        }

        public void LevelBegin(string levelID)
        {
            GASdk.StartLevel(levelID);
        }

        public void LevelComplate(string levelID)
        {
            GASdk.FinishLevel(levelID);
        }

        public void LevelFailed(string levelID, string reason)
        {
            GASdk.FailLevel(levelID);
        }

        public void CustomEvent(string eventID, object label = null)
        {
            if (!m_IsCloseListCtrl && (NotWhiteListEvt(eventID) || IsIgnoreEvt(eventID)))
                return;
            Log.i("gasdk_" + eventID);

#if UNITY_ANDROID && !UNITY_EDITOR
            if (TaurusXConfigUtil.GetChannel() == "toutiao"
                || TaurusXConfigUtil.GetChannel() == "bytedance")
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (label == null || string.IsNullOrEmpty(label.ToString()))
                {
                    dic.Add("null", "null");
                    GASdk.EventObject(eventID, dic);
                }
                else
                {
                    dic.Add("label", label);
                    GASdk.EventObject(eventID, dic);
                }
            }
            return;
#endif

            if (label == null || string.IsNullOrEmpty(label.ToString()))
            {
                GASdk.Event(eventID);
            }
            else
            {
                GASdk.Event(eventID, label.ToString());
            }
        }

        public void CustomValueEvent(string eventID, float value, string label = null,
            Dictionary<string, string> dic = null)
        {
            if (!m_IsCloseListCtrl && (NotWhiteListEvt(eventID) || IsIgnoreEvt(eventID)))
                return;
            Log.i("gasdk_" + eventID);
            //计算事件
            GASdk.Event(eventID, dic, (int)value);
        }

        public void CustomValueEvent(string eventID, float value, string label = null,
           Dictionary<string, object> dic = null)
        {
            if (!m_IsCloseListCtrl && (NotWhiteListEvt(eventID) || IsIgnoreEvt(eventID)))
                return;
            Log.i("gasdk_" + eventID);
            if (dic.ContainsKey("__ct__"))
            {
                dic["__ct__"] = value;
            }
            else
            {
                dic.Add("__ct__", value);
            }

            //计算事件
            GASdk.EventObject(eventID, dic);
        }

        public void Pay(double cash, double coin)
        {
#if UNITY_IPHONE
            GASdk.Pay(cash, PaySource.AppStore, coin);
#elif UNITY_ANDROID
            GASdk.Pay(cash, PaySource.Paypal, coin);
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
            GASdkConfig gaConf = adapterConfig as GASdkConfig;
            m_IsCloseListCtrl = gaConf.closeCtrlList;
            GASdk.ApplicaitonInit();
            GASdk.Init();
            return true;
        }

        public void SetUserLevel(int level)
        {
            GASdk.SetUserLevel(level);
        }
        public void CustomEventDic(string eventId, Dictionary<string, string> dic)
        {
            if (!m_IsCloseListCtrl && (NotWhiteListEvt(eventId) || IsIgnoreEvt(eventId)))
                return;
            Log.i("gasdk_" + eventId);
            GASdk.Event(eventId, dic);
        }

        public void CustomEventDic(string eventId, Dictionary<string, object> dic)
        {
            if (!m_IsCloseListCtrl && (NotWhiteListEvt(eventId) || IsIgnoreEvt(eventId)))
                return;
            Log.i("gasdk_" + eventId);
            GASdk.EventObject(eventId, dic);
        }

        public void CheckRemoteConfig()
        {

        }

        public void AddIgnoreEvent(string adapterClassName, List<string> eventIDs)
        {
            if (this.GetType().Name.Contains(adapterClassName))
            {
                if (m_LstIgnore != null)
                {
                    m_LstIgnore = m_LstIgnore.Union(eventIDs).ToList();
                }
                else
                {
                    m_LstIgnore = eventIDs;
                }
            }
        }

        public void AddWhiteListEvent(string adapterClassName, List<string> eventIDs)
        {
            if (this.GetType().Name.Contains(adapterClassName))
            {
                if (m_LstWhiteListEvts != null)
                {
                    m_LstWhiteListEvts = m_LstWhiteListEvts.Union(eventIDs).ToList();
                }
                else
                {
                    m_LstWhiteListEvts = eventIDs;
                }
            }
        }

        bool NotWhiteListEvt(string eventID)
        {
            return m_LstWhiteListEvts != null && !m_LstWhiteListEvts.Contains(eventID);
        }

        bool IsIgnoreEvt(string eventID)
        {
            return m_LstIgnore != null && m_LstIgnore.Contains(eventID);
        }
    }
}
