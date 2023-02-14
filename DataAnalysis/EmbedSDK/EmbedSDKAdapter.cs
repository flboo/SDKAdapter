using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using EmbedSDK.Platforms.Android;
using AppsFlyerSDK;

namespace Qarth
{
    public class EmbedSDKAdapter : AbstractSDKAdapter, IDataAnalysisAdapter
    {
        private List<string> m_LstEmbedIgnore;

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
            if (IsIgnoreEvt(eventID))
                return;
            Dictionary<string, string> eventValue = new Dictionary<string, string>();
            if (label != null)
            {
                eventValue.Add("label", label.ToString());
            }
            EmbedSDKMgr.S.ReportCustomEvent(eventID, eventValue);
        }

        public void CustomValueEvent(string eventID, float value, string label = null,
            Dictionary<string, string> dic = null)
        {
            if (IsIgnoreEvt(eventID))
                return;
            if (dic == null)
                dic = new Dictionary<string, string>();

            if (DataAnalysisDefine.W_AD_IMP.Equals(eventID))
            {
                dic.Add(DataAnalysisDefine.AF_SDK_VALUE, value.ToString());
            }
            else
            {
                dic.Add("revenue", value.ToString());
            }

            if (label != null)
            {
                dic.Add("label", label);
            }

            EmbedSDKMgr.S.ReportCustomEvent(eventID, dic);
        }

        public void CustomValueEvent(string eventID, float value, string label = null,
            Dictionary<string, object> dic = null)
        {
            if (IsIgnoreEvt(eventID))
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

            if (label != null)
            {
                strDict.Add("label", label);
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

            EmbedSDKMgr.S.ReportCustomEvent(eventID, strDict);
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
            EmbedSDKConfig embedConfig = adapterConfig as EmbedSDKConfig;
            if (embedConfig.enableAF)
            {
                InitAF(config, embedConfig);
            }

            SetIgnore();
            EmbedSDKMgr.S.Init(embedConfig);
            return true;
        }

        public void SetUserLevel(int level)
        {
        }
        public void CustomEventDic(string eventId, Dictionary<string, string> dic)
        {
            if (IsIgnoreEvt(eventId))
                return;
            EmbedSDKMgr.S.ReportCustomEvent(eventId, dic);
        }

        public void CustomEventDic(string eventID, Dictionary<string, object> dic)
        {
            if (IsIgnoreEvt(eventID))
                return;

            if (dic != null)
            {
                Dictionary<string, string> strDict = new Dictionary<string, string>();
                foreach (var key in dic.Keys)
                {
                    strDict.Add(key, dic[key].ToString());
                }
                EmbedSDKMgr.S.ReportCustomEvent(eventID, strDict);
            }
        }

        public void CheckRemoteConfig()
        {

        }

        public void AddIgnoreEvent(string adapterClassName, List<string> eventIDs)
        {
            if (this.GetType().Name.Contains(adapterClassName))
            {
                EmbedSDKMgr.S.SetBlacklist(eventIDs);
            }
        }

        public void AddWhiteListEvent(string adapterClassName, List<string> eventIDs)
        {
            if (this.GetType().Name.Contains(adapterClassName))
            {
                EmbedSDKMgr.S.SetWhitelist(eventIDs);
            }
        }

        void SetIgnore()
        {
            // 排除掉embedSDK内部逻辑会上报的事件
            m_LstEmbedIgnore = new List<string>()
            {
                // DataAnalysisDefine.W_AD_TRIGGER,
                // DataAnalysisDefine.W_AD_REQUEST,
                // DataAnalysisDefine.W_AD_FILL,
                // DataAnalysisDefine.W_AD_SHOW,
                DataAnalysisDefine.W_AD_IMP,//w_ad_imp
                // DataAnalysisDefine.W_AD_REWARD,
                // DataAnalysisDefine.W_AD_CLOSE,
                // DataAnalysisDefine.W_AD_CLICK,//w_ad_click
                // DataAnalysisDefine.W_APP_START,//w_app_start
            };
        }

        bool IsIgnoreEvt(string eventID)
        {
            return m_LstEmbedIgnore != null && m_LstEmbedIgnore.Contains(eventID);
        }


        void InitAF(SDKConfig config, EmbedSDKConfig embedConfig)
        {
            if (string.IsNullOrEmpty(embedConfig.afAppKey))
                return;

            // AppsFlyer.setAppsFlyerKey(embedConfig.afAppKey);
            if (embedConfig.isDebugMode)
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
            AppsFlyer.initSDK(embedConfig.afAppKey, Application.identifier, AppsFlyerInstance.S);
            //AppsFlyer.setCustomerUserID(AppsFlyer.getAppsFlyerId());
            //AppsFlyer.loadConversionData("AppsFlyerTrackerCallbacks");
#endif
            AppsFlyer.getConversionData("AppsFlyerInstance");
            AppsFlyer.startSDK();
        }
    }
}
