using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameWish.Game;
using AppsFlyerSDK;
using System.Linq;

namespace Qarth
{
    public class AppsflyerDataAdapter : IDataAnalysisAdapter
    {
        private List<string> m_LstAFIgnore;
        private List<string> m_LstAFWhiteListEvts;
        private int m_EvtExpireDay = -1;

        public string Platform
        {
            get
            {
                return "appsflyer";
            }
        }

        public bool InitWithConfig(SDKConfig config, SDKAdapterConfig adapterConfig)
        {
            AppsflyerConfig appsflyerConfig = adapterConfig as AppsflyerConfig;
            if (appsflyerConfig.isDebugMode)
            {
                AppsFlyer.setIsDebug(true);
            }

#if UNITY_IOS
   /* Mandatory - set your apple app ID
      NOTE: You should enter the number only and not the "ID" prefix */
            AppsFlyer.initSDK(embedConfig.afAppKey, config.iosAppID, AppsFlyerInstance.S);
#elif UNITY_ANDROID
            /* Mandatory - set your Android package name */
            AppsFlyerAndroid.setCollectIMEI(true);
            AppsFlyerAndroid.setCollectOaid(true);
            AppsFlyerAndroid.setCollectAndroidID(true);

            AppsFlyer.setMinTimeBetweenSessions(2);
            /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
            //AppsFlyer.init(appsflyerConfig.appKey, "AppsFlyerTrackerCallbacks");
            AppsFlyer.initSDK(appsflyerConfig.appKey, Application.identifier, AppsFlyerInstance.S);
            //AppsFlyer.setCustomerUserID(AppsFlyer.getAppsFlyerId());
#endif
            AppsFlyer.getConversionData("AppsFlyerInstance");
            AppsFlyer.startSDK();

            //SetIgnore();
            SetWhiteList();
            EventSystem.S.Register(SDKEventID.OnDataAnalysisRemoteConfFetched, OnDataAnalysisRemoteConfFetched);
            OnDataAnalysisRemoteConfFetched(0);
            return true;
        }

        public void CustomEvent(string eventID, object label = null)
        {
            if (NotWhiteListEvt(eventID)
                || IsTimeExpire()
                || IsIgnoreEvt(eventID))
                return;

            Dictionary<string, string> eventValue = new Dictionary<string, string>();
            if (label != null)
            {
                eventValue.Add("description", label.ToString());
            }
            AppsFlyer.sendEvent(eventID, eventValue);
            //Log.e(">>>" + eventID);
        }

        public void CustomValueEvent(string eventID, float value, string label = null,
            Dictionary<string, string> dic = null)
        {
            if (NotWhiteListEvt(eventID)
                || IsTimeExpire()
                || IsIgnoreEvt(eventID))
                return;

            Dictionary<string, string> param = new Dictionary<string, string>();
            if (DataAnalysisDefine.W_AD_IMP.Equals(eventID))
            {
                param.Add(DataAnalysisDefine.AF_SDK_VALUE, value.ToString());
            }
            else
            {
                param.Add("revenue", value.ToString());
            }
            if (!string.IsNullOrEmpty(label))
            {
                param.Add("description", label);
            }
            if (dic != null)
            {
                foreach (var key in dic.Keys)
                {
                    param.Add(key, dic[key]);
                }
            }

            AppsFlyer.sendEvent(eventID, param);
        }

        public void CustomValueEvent(string eventID, float value, string label = null,
                    Dictionary<string, object> dic = null)
        {
            if (NotWhiteListEvt(eventID)
              || IsTimeExpire()
              || IsIgnoreEvt(eventID))
                return;

            var strDict = new Dictionary<string, string>();
            if (DataAnalysisDefine.W_AD_IMP.Equals(eventID))
            {
                strDict.Add(DataAnalysisDefine.AF_SDK_VALUE, value.ToString());
            }
            else
            {
                strDict.Add("revenue", value.ToString());
            }
            if (!string.IsNullOrEmpty(label))
            {
                strDict.Add("description", label);
            }

            if (dic != null)
            {
                foreach (var key in dic.Keys)
                {
                    if (strDict.ContainsKey(key))
                        strDict[key] = dic[key].ToString();
                    else
                        strDict.Add(key, dic[key].ToString());
                }
            }

            AppsFlyer.sendEvent(eventID, strDict);
        }

        public void CustomEventDuration(string eventID, long duration)
        {
        }

        public void CustomEventMapSend(string eventID)
        {
        }

        public void CustomEventMapValue(string key, string value)
        {
        }

        public int GetPriorityScore()
        {
            return 0;
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

        public void OnApplicationQuit()
        {

        }

        public void Pay(double cash, double coin)
        {
            Dictionary<string, string> eventValue = new Dictionary<string, string>();
            eventValue.Add("af_revenue", cash.ToString());
            eventValue.Add("af_content_id", coin.ToString());
            eventValue.Add("af_currency", "USD");

            AppsFlyer.sendEvent("af_purchase", eventValue);
        }

        public void SetUserLevel(int level)
        {

        }

        public void CustomEventDic(string eventID, Dictionary<string, string> dic)
        {
            if (NotWhiteListEvt(eventID)
                || IsTimeExpire()
                || IsIgnoreEvt(eventID))
                return;

            if (dic != null)
            {
                AppsFlyer.sendEvent(eventID, dic);
            }
        }

        public void CustomEventDic(string eventID, Dictionary<string, object> dic)
        {
            if (NotWhiteListEvt(eventID)
              || IsTimeExpire()
              || IsIgnoreEvt(eventID))
                return;

            if (dic != null)
            {
                Dictionary<string, string> strDict = new Dictionary<string, string>();
                foreach (var key in dic.Keys)
                {
                    strDict.Add(key, dic[key].ToString());
                }
                AppsFlyer.sendEvent(eventID, strDict);
            }
        }

        public void CheckRemoteConfig()
        {

        }

        public void AddIgnoreEvent(string adapterClassName, List<string> eventIDs)
        {
            if (this.GetType().Name.Contains(adapterClassName))
            {
                if (m_LstAFIgnore != null)
                {
                    m_LstAFIgnore = m_LstAFIgnore.Union(eventIDs).ToList();
                }
                else
                {
                    m_LstAFIgnore = eventIDs;
                }
            }
        }

        public void AddWhiteListEvent(string adapterClassName, List<string> eventIDs)
        {
            if (this.GetType().Name.Contains(adapterClassName))
            {
                if (m_LstAFWhiteListEvts != null)
                {
                    m_LstAFWhiteListEvts = m_LstAFWhiteListEvts.Union(eventIDs).ToList();
                }
                else
                {
                    m_LstAFWhiteListEvts = eventIDs;
                }
            }
        }

        void SetIgnore()
        {
            m_LstAFIgnore = new List<string>()
            {
                // DataAnalysisDefine.AD_FILL_UNITID_COUNT,//ad_fill_count_unitid
                // DataAnalysisDefine.W_AD_SHOW,//w_ad_show
                "InterfaceCount",
                // DataAnalysisDefine.AD_REQUEST_UNITID_COUNT,//ad_request_count_unitid
                // DataAnalysisDefine.AD_DISPLAY,//ShowAD
                // DataAnalysisDefine.MIXVIEW_VIDEO_IPU,//IPU_MixView
                // DataAnalysisDefine.W_AD_CLOSE,//w_ad_close
                DataAnalysisDefine.TT_AD_CLICK,//ad_button_click
                DataAnalysisDefine.TT_AD_SHOW,//ad_show
                // DataAnalysisDefine.REWARD_VIDEO_IPU,//IPU_RewardVideo
                // DataAnalysisDefine.W_AD_REWARD,//w_ad_reward
                // DataAnalysisDefine.IMPRESSION_VIDEO,//Impression_Video
                // DataAnalysisDefine.AD_REQUEST_PLATFORM_COUNT,//ad_request_count_placement
                DataAnalysisDefine.PANEL_EVENT,//PanelEvent
                DataAnalysisDefine.W_PAGE_VIEW,//w_page_view
                "GetProperty",
            };
        }

        void SetWhiteList()
        {
            m_LstAFWhiteListEvts = new List<string>()
            {
                DataAnalysisDefine.W_AD_IMP,//w_ad_imp
                // DataAnalysisDefine.W_AD_CLICK,//w_ad_click
                // DataAnalysisDefine.W_APP_START,//w_app_start
                // DataAnalysisDefine.W_ENGAGEMENT,//w_engagement
                // DataAnalysisDefine.W_LOG_IN,//w_log_in
                // DataAnalysisDefine.W_GET_COINS,//get_coin
                DataAnalysisDefine.W_FIRST_GET_COINS,//first_get_coin
                // "life_time_minute_10",
                // "life_time_minute_5",
                "day1_retention",
                "Day1 Retention",
            };


#if UNITY_IOS
            m_LstAFWhiteListEvts.Add(DataAnalysisDefine.W_AD_SHOW);
#endif
        }

        bool IsIgnoreEvt(string eventID)
        {
            return m_LstAFIgnore != null && m_LstAFIgnore.Contains(eventID);
        }

        bool NotWhiteListEvt(string eventID)
        {
            return m_LstAFWhiteListEvts != null && !m_LstAFWhiteListEvts.Contains(eventID);
        }

        bool IsTimeExpire()
        {
            return m_EvtExpireDay > 0 && DataAnalysisMgr.S.installDays > m_EvtExpireDay;
        }

        void OnDataAnalysisRemoteConfFetched(int key, params object[] args)
        {
            if (DataAnalysisRemoteConfMgr.S.data == null)
                return;

            if (!string.IsNullOrEmpty(DataAnalysisRemoteConfMgr.S.data.af_evt_expire_day))
            {
                int.TryParse(DataAnalysisRemoteConfMgr.S.data.af_evt_expire_day, out m_EvtExpireDay);
            }
        }
    }
}
