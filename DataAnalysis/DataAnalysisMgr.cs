using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameWish.Game;
using AppsFlyerSDK;
using TaurusXAdSdk.Api;

namespace Qarth
{
    [TMonoSingletonAttribute("[SDK]/DataAnalysisMgr")]
    public class DataAnalysisMgr : TMonoSingleton<DataAnalysisMgr>, IDataAnalysisAdapter, IPanelEventLogger
    {
        private const string KEY_APP_INSTALL_TIME = "key_install1027";
        private List<IDataAnalysisAdapter> m_Adapters = new List<IDataAnalysisAdapter>();
        private Dictionary<string, bool> m_SingleEventMap = new Dictionary<string, bool>();

        private Dictionary<string, object> m_EvtParamDict = new Dictionary<string, object>();

        //!
        private int m_AnalysisTimer = -1;
        private int[] m_AnalysisFocusMins = { 1, 3, 5, 8, 10, 12, 15, 20, 25, 30, 60, 90, 120 };

        private int[] m_AnalysisLifeFocusMins = { };

        private List<int> m_AnalysisLifeFocusMinsList = new List<int>() { 1, 2, 3, 5, 10, 20, 30, 60, 120 };

        private int m_InstallDays;
        public int installDays
        {
            get { return m_InstallDays; }
        }
        private string m_InstallDaysString;
        private string m_InstallDaysStringMax;
        private bool m_IsAdapterInited = false;
        public bool isAdapterInited
        {
            get { return m_IsAdapterInited; }
        }

        public void Init()
        {
            UIMgr.S.panelEventLogger = this;
            InitDataAnalysisEnv();
            Log.i("Init[DataAnalysisMgr]");
        }

        // call after privacy accept
        public void InitSupplement()
        {
            InitSupportedAdapter(SDKConfig.S);
            m_IsAdapterInited = true;
            m_AnalysisTimer = Timer.S.Post2Really(OnAnalysisTimeTick, 30, -1);

            // CustomEventLifeCircleSingle(DataAnalysisDefine.W_APP_INSTALL);

            Timer.S.Post2Really((count) =>
            {
                CheckRetention();
                // CheckUserRemain();
            }, 5);
        }

        private void InitSupportedAdapter(SDKConfig config)
        {
            if (!config.dataAnalysisConfig.isEnable)
            {
                Log.w("DataAnalysis System Is Not Enable.");
                return;
            }

            if (!config.dataAnalysisConfig.embedsdkConfig.enableAF)
            {
                RegisterAdapter(config, config.dataAnalysisConfig.appsflyerConfig);
            }
            //RegisterAdapter(config, config.dataAnalysisConfig.umengConfig);
            RegisterAdapter(config, config.dataAnalysisConfig.tdConfig);
            RegisterAdapter(config, config.dataAnalysisConfig.gasdkConfig);
            RegisterAdapter(config, config.dataAnalysisConfig.rangersConfig);
            RegisterAdapter(config, config.dataAnalysisConfig.embedsdkConfig);

            Log.i("Init[DataAnalysisMgr] Adapter Registered");
        }

        private void RegisterAdapter(SDKConfig config, SDKAdapterConfig adapterConfig)
        {
            if (!adapterConfig.isEnable)
            {
                return;
            }

            Type type = Type.GetType(adapterConfig.adapterClassName);
            if (type == null)
            {
                Log.w("Not Support DataAnalysis:" + adapterConfig.adapterClassName);
                return;
            }
            IDataAnalysisAdapter adapter = type.Assembly.CreateInstance(adapterConfig.adapterClassName) as IDataAnalysisAdapter;

            if (adapter == null)
            {
                Log.e("DataAnalysis Adapter Create Failed:" + adapterConfig.adapterClassName);
                return;
            }

            if (adapter.InitWithConfig(config, adapterConfig))
            {
                m_Adapters.Add(adapter);

                Log.i("Success Register DataAnalysisAdapter:" + adapterConfig.adapterClassName);
            }
            else
            {
                Log.w("Failed Register DataAnalysisAdapter:" + adapterConfig.adapterClassName);
            }
        }

        protected void InitDataAnalysisEnv()
        {
            string installTime = PlayerPrefs.GetString(KEY_APP_INSTALL_TIME, "");
            if (string.IsNullOrEmpty(installTime))
            {
                installTime = DateTime.Now.ToLongDateString();
                PlayerPrefs.SetString(KEY_APP_INSTALL_TIME, installTime);
                PlayerPrefs.SetString(DataAnalysisDefine.FIRST_INSTALL_TIMESTAMP, CustomExtensions.GetTimeStampUni());
                PlayerPrefs.Save();
            }

            m_InstallDays = GetPassDay(DateTime.Parse(installTime));
            m_InstallDaysString = m_InstallDays.ToString();
            if (m_InstallDays >= 30)
            {
                m_InstallDaysStringMax = "30";
            }
            else
            {
                m_InstallDaysStringMax = m_InstallDaysString;
            }

            // Timer.S.Post2Really((count) =>
            // {
            //     CustomEventDailySingle("Retain", null, false);
            //     CustomEventDailySingle("RetainDaily", "Retain_" + m_InstallDaysStringMax);
            //     // CustomEventLifeCircleSingle("FirstInstall");
            //     CustomEventExtension();
            // }, 15);
        }

        void OnApplicationFocus(bool isFocus)
        {
            if (isFocus)
            {
            }
        }

        public void OnApplicationQuit()
        {
            CancelAnalysisTimer();
            for (int i = 0; i < m_Adapters.Count; ++i)
            {
                m_Adapters[i].OnApplicationQuit();
            }
        }

        public void AddIgnoreEvent(string adapterClassName, List<string> eventIDs)
        {
            for (int i = 0; i < m_Adapters.Count; ++i)
            {
                m_Adapters[i].AddIgnoreEvent(adapterClassName, eventIDs);
            }
        }

        public void AddWhiteListEvent(string adapterClassName, List<string> eventIDs)
        {
            for (int i = 0; i < m_Adapters.Count; ++i)
            {
                m_Adapters[i].AddWhiteListEvent(adapterClassName, eventIDs);
            }
        }

        public void LevelBegin(string levelID)
        {
            for (int i = 0; i < m_Adapters.Count; ++i)
            {
                m_Adapters[i].LevelBegin(levelID);
            }
        }

        public void LevelComplate(string levelID)
        {
            for (int i = 0; i < m_Adapters.Count; ++i)
            {
                m_Adapters[i].LevelComplate(levelID);
            }
        }

        public void LevelFailed(string levelID, string reason)
        {
            for (int i = 0; i < m_Adapters.Count; ++i)
            {
                m_Adapters[i].LevelFailed(levelID, reason);
            }
        }

        public void CustomEvent(string eventID, object label = null)
        {
            for (int i = 0; i < m_Adapters.Count; ++i)
            {
                m_Adapters[i].CustomEvent(eventID, label);
            }
        }

        public void CustomValueEvent(string eventID, float value, string label = null, Dictionary<string, string> dic = null)
        {
            for (int i = 0; i < m_Adapters.Count; ++i)
            {
                m_Adapters[i].CustomValueEvent(eventID, value, label, dic);
            }
        }
        public void CustomValueEvent(string eventID, float value, string label = null, Dictionary<string, object> dic = null)
        {
            for (int i = 0; i < m_Adapters.Count; ++i)
            {
                m_Adapters[i].CustomValueEvent(eventID, value, label, dic);
            }
        }

        public void CustomEventDic(string eventID, Dictionary<string, string> dic)
        {
            for (int i = 0; i < m_Adapters.Count; i++)
            {
                m_Adapters[i].CustomEventDic(eventID, dic);
            }
        }
        public void CustomEventDic(string eventID, Dictionary<string, object> dic)
        {
            for (int i = 0; i < m_Adapters.Count; i++)
            {
                m_Adapters[i].CustomEventDic(eventID, dic);
            }
        }

        public void CustomEventLifeCircleSingle(string eventID, string label = null)
        {
            if (string.IsNullOrEmpty(eventID))
            {
                return;
            }

            if (PlayerPrefs.GetInt(eventID, 0) > 0)
            {
                return;
            }

            PlayerPrefs.SetInt(eventID, 1);

            CustomEvent(eventID, label);
        }

        public void CustomEventWithDate(string eventID, string label = null, bool max = true)
        {

            if (string.IsNullOrEmpty(label))
            {
                if (max)
                {
                    label = m_InstallDaysStringMax;
                }
                else
                {
                    label = m_InstallDaysString;
                }
            }
            else
            {
                if (max)
                {
                    label = string.Format("{0}_{1}", label, m_InstallDaysStringMax);

                }
                else
                {
                    label = string.Format("{0}_{1}", label, m_InstallDaysString);
                }
            }

            for (int i = 0; i < m_Adapters.Count; ++i)
            {
                m_Adapters[i].CustomEvent(eventID, label);
            }
        }

        public void CustomEventSingleton(string eventID, string label = null)
        {
            string key = eventID;
            if (!string.IsNullOrEmpty(label))
            {
                key += label;
            }

            if (m_SingleEventMap.ContainsKey(key))
            {
                return;
            }

            m_SingleEventMap.Add(key, true);

            CustomEvent(eventID, label);
        }

        public void CustomEventDailySingle(string eventID, string label = null, bool max = true)
        {
            string key = eventID;
            if (!string.IsNullOrEmpty(label))
            {
                key += label;
            }

            int day = PlayerPrefs.GetInt(key, -1);
            if (day == m_InstallDays)
            {
                return;
            }

            PlayerPrefs.SetInt(key, m_InstallDays);
            PlayerPrefs.Save();
            CustomEventWithDate(eventID, label, max);
        }

        public void CustomEventDuration(string eventID, long duration)
        {
            for (int i = 0; i < m_Adapters.Count; ++i)
            {
                m_Adapters[i].CustomEventDuration(eventID, duration);
            }
        }

        public void CustomEventMapValue(string key, string value)
        {
            for (int i = 0; i < m_Adapters.Count; ++i)
            {
                m_Adapters[i].CustomEventMapValue(key, value);
            }
        }

        public void Pay(TDPurchase data)
        {
            Pay(data.price, data.itemNum);
        }

        public void CustomEventMapSend(string eventID)
        {
            for (int i = 0; i < m_Adapters.Count; ++i)
            {
                m_Adapters[i].CustomEventMapSend(eventID);
            }
        }

        public int GetPriorityScore()
        {
            throw new NotImplementedException();
        }

        public bool InitWithConfig(SDKConfig config, SDKAdapterConfig adapterConfig)
        {
            throw new NotImplementedException();
        }

        public void Pay(double cash, double coin)
        {
            for (int i = 0; i < m_Adapters.Count; ++i)
            {
                m_Adapters[i].Pay(cash, coin);
            }
        }

        public void LogPanelOpen(string name, PanelEventLogType eventType)
        {
            if (eventType != PanelEventLogType.None)
                SatoriEvt_PageView(name);

            // switch (eventType)
            // {
            //     case PanelEventLogType.Single:
            //         CustomEventSingleton(DataAnalysisDefine.SINGLE_PANEL_EVENT, string.Format("{0}_open", name));
            //         break;
            //     case PanelEventLogType.LifeCircleSingle:
            //         CustomEventLifeCircleSingle(string.Format("{0}_open", name));
            //         break;
            //     case PanelEventLogType.Repeat:
            //         CustomEvent(DataAnalysisDefine.PANEL_EVENT, string.Format("{0}_open", name));
            //         break;
            //     case PanelEventLogType.Mix:
            //         CustomEventSingleton(DataAnalysisDefine.SINGLE_PANEL_EVENT, string.Format("{0}_open", name));
            //         CustomEvent(DataAnalysisDefine.PANEL_EVENT, string.Format("{0}_open", name));
            //         break;
            //     default:
            //         break;
            // }
        }

        public void LogPanelClose(string name, PanelEventLogType eventType)
        {
            // switch (eventType)
            // {
            //     case PanelEventLogType.Single:
            //         CustomEventSingleton(DataAnalysisDefine.SINGLE_PANEL_EVENT, string.Format("{0}_close", name));
            //         break;
            //     case PanelEventLogType.LifeCircleSingle:
            //         CustomEventLifeCircleSingle(string.Format("{0}_close", name));
            //         break;
            //     case PanelEventLogType.Repeat:
            //         CustomEvent(DataAnalysisDefine.PANEL_EVENT, string.Format("{0}_close", name));
            //         break;
            //     case PanelEventLogType.Mix:
            //         CustomEventSingleton(DataAnalysisDefine.SINGLE_PANEL_EVENT, string.Format("{0}_close", name));
            //         CustomEvent(DataAnalysisDefine.PANEL_EVENT, string.Format("{0}_close", name));
            //         break;
            //     default:
            //         break;
            // }
        }

        void OnAnalysisTimeTick(int tickCount)
        {
            // //??????????????????
            // if (tickCount == 20)
            // {
            //     CustomEventDic(DataAnalysisDefine.W_ENGAGEMENT, new Dictionary<string, string>()
            //     {
            //         { "time", "600" }
            //     });
            // }
        }

        void OnAnalysisTimeTickForEvent(int minutes)
        {
            if (m_AnalysisLifeFocusMinsList.Contains(minutes))
            {
                string key = "life_time_minute_" + minutes;
                CustomEventLifeCircleSingle(key);
            }
        }

        void CancelAnalysisTimer()
        {
            if (m_AnalysisTimer > 0)
            {
                Timer.S.Cancel(m_AnalysisTimer);
                m_AnalysisTimer = -1;
            }
        }

        // void CheckUserRemain()
        // {
        //     if (m_InstallDays > 30)
        //     {
        //         return;
        //     }

        //     if (PlayerPrefs.GetInt(DataAnalysisDefine.REMAIN_EVENT_DAY, -1) >= m_InstallDays)
        //     {
        //         return;
        //     }

        //     // Log.e(string.Format(DataAnalysisDefine.REMAIN_EVENT_ID, m_InstallDays));
        //     PlayerPrefs.SetInt(DataAnalysisDefine.REMAIN_EVENT_DAY, m_InstallDays);
        //     PlayerPrefs.Save();
        //     CustomEvent(string.Format(DataAnalysisDefine.REMAIN_EVENT_ID, m_InstallDays), "7");
        // }

        protected int GetPassDay(DateTime time)
        {
            return (DateTime.Parse(DateTime.Now.ToLongDateString()) - time).Days;
        }

        public void SetUserLevel(int level)
        {
            for (int i = 0; i < m_Adapters.Count; ++i)
            {
                m_Adapters[i].SetUserLevel(level);
            }
        }

        public void CheckRemoteConfig()
        {
            for (int i = 0; i < m_Adapters.Count; ++i)
            {
                m_Adapters[i].CheckRemoteConfig();
            }
        }

        public void CheckRetention(bool isPauseCheck = false)
        {
            if (PlayerPrefs.GetInt(DataAnalysisDefine.HAS_RETENTION, 0) == 0)
            {
                var installTime = PlayerPrefs.GetString(DataAnalysisDefine.FIRST_INSTALL_TIMESTAMP, "");
                if (!string.IsNullOrEmpty(installTime))
                {
                    if (CustomExtensions.GetSDKChannel() == "kuaishou" || CustomExtensions.GetSDKChannel() == "chubao")
                    {
                        var nowTime = DateTime.Now.ToUniversalTime().AddHours(8);
                        var lastTime = CustomExtensions.GetTimeFromTimestamp(installTime).AddHours(8);
                        if ((nowTime.Date - lastTime.Date).Days == 1)
                        {
                            DataAnalysisMgr.S.CustomEventSingleton(DataAnalysisDefine.W_RETENTION, "7");
                            DataAnalysisMgr.S.CustomEventSingleton("Day1 Retention", "7");
                            DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.DAY1_RETENTION, "7");
                            PlayerPrefs.SetInt(DataAnalysisDefine.HAS_RETENTION, 1);
                        }
                    }
                    else
                    {
                        long offsetValue = long.Parse(CustomExtensions.GetTimeStampUni()) - long.Parse(installTime);
                        if (offsetValue >= 6 * 1000 * 3600)
                        {
                            DataAnalysisMgr.S.CustomEventSingleton(DataAnalysisDefine.W_RETENTION, "7");
                            DataAnalysisMgr.S.CustomEventSingleton("Day1 Retention", "7");
                            DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.DAY1_RETENTION, "7");
                            PlayerPrefs.SetInt(DataAnalysisDefine.HAS_RETENTION, 1);
                        }
                    }
                }
                else
                {
                    PlayerPrefs.SetString(DataAnalysisDefine.FIRST_INSTALL_TIMESTAMP, CustomExtensions.GetTimeStampUni());
                }
            }
        }

        #region Satori
        /// <summary>
        /// ??????
        /// </summary>
        /// <param name="day">????????????</param>
        /// <param name="scene">????????????</param>
        public void SatoriEvt_Guide(int guideId, string type)
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", guideId);
            m_EvtParamDict.Add("type", type);
            CustomEventDic(DataAnalysisDefine.W_GUIDE, m_EvtParamDict);
        }
        /// <summary>
        /// ??????
        /// </summary>
        /// <param name="day">????????????</param>
        /// <param name="scene">????????????</param>
        public void SatoriEvt_Guide_New(string guideId, string type)
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", guideId);
            m_EvtParamDict.Add("type", type);
            CustomEventDic(DataAnalysisDefine.W_GUIDE, m_EvtParamDict);
        }
        /// <summary>
        /// ??????
        /// </summary>
        /// <param name="day">????????????</param>
        /// <param name="scene">????????????</param>
        public void SatoriEvt_CheckIn(int day, string scene = "")
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", day);
            m_EvtParamDict.Add("source", scene);
            CustomEventDic(DataAnalysisDefine.W_CHECK_IN, m_EvtParamDict);
        }

        /// <summary>
        /// ??????
        /// </summary>
        /// <param name="type">?????????????????????/?????????</param>
        /// <param name="result">??????????????????????????????0????????????1?????????</param>
        /// <param name="description">????????????????????????????????????</param>
        public void SatoriEvt_SignUp(string type, SatoriEvtDefine.CommonResultType result, string description = null)
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("type", type);
            m_EvtParamDict.Add("result", ((int)result).ToString());
            m_EvtParamDict.Add("description", description);
            CustomEventDic(DataAnalysisDefine.W_SIGNUP, m_EvtParamDict);
        }

        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="type">????????????</param>
        /// <param name="value">????????????????????????</param>
        /// <param name="source">?????????????????????</param>
        public void SatoriEvt_CoinAquired(string type, double value, string source = "")
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_type", type);
            m_EvtParamDict.Add("value", value);
            m_EvtParamDict.Add("source", source);
            CustomEventDic(DataAnalysisDefine.W_COIN_ACQUIRED, m_EvtParamDict);
        }

        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="type">????????????</param>
        /// <param name="value">????????????????????????</param>
        public void SatoriEvt_CoinUsed(string type, double value)
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_type", type);
            m_EvtParamDict.Add("value", value);
            CustomEventDic(DataAnalysisDefine.W_COIN_USED, m_EvtParamDict);
        }

        /// <summary>
        /// ???????????????
        /// </summary>
        /// <param name="lv">??????????????????????????????</param>
        /// <param name="source">????????????(??????)??????</param>
        public void SatoriEvt_LevelUp(int value, string source = "")
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", value.ToString());
            m_EvtParamDict.Add("source", source);
            CustomEventDic(DataAnalysisDefine.W_LEVEL_UP, m_EvtParamDict);
        }

        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="type">????????????</param>
        /// <param name="value">????????????</param>
        /// <param name="source">??????</param>
        public void SatoriEvt_ToolAcquire(string type, string value, string source = "")
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_type", type);
            m_EvtParamDict.Add("value", value);
            m_EvtParamDict.Add("source", source);
            CustomEventDic(DataAnalysisDefine.W_TOOL_ACQUIRED, m_EvtParamDict);
        }
        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="type">????????????</param>
        /// <param name="value">??????????????????</param>
        public void SatoriEvt_ToolUpdate(string type, string value)
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_type", type);
            m_EvtParamDict.Add("value", value);
            CustomEventDic(DataAnalysisDefine.W_TOOL_UPDATE, m_EvtParamDict);
        }

        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="type">???????????????</param>
        /// <param name="value">????????????</param>
        public void SatoriEvt_ToolUse(string itemid, string type, string value = "1")
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", itemid);
            m_EvtParamDict.Add("item_type", type);
            m_EvtParamDict.Add("value", value);
            CustomEventDic(DataAnalysisDefine.W_TOOL_USED, m_EvtParamDict);
        }

        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="type">???????????????</param>
        /// <param name="value">????????????</param>
        public void SatoriEvt_ToolSold(string type, string value = "1")
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_type", type);
            m_EvtParamDict.Add("value", value);
            CustomEventDic(DataAnalysisDefine.W_TOOL_SOLD, m_EvtParamDict);
        }

        /// <summary>
        /// ????????????
        /// </summary>
        public void SatoriEvt_FinishGameMatch()
        {
            CustomEvent(DataAnalysisDefine.W_GAME_MATCH);
        }

        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="stageId">??????Id</param>
        public void SatoriEvt_StartPlay(string stageId)
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", stageId);
            CustomEventDic(DataAnalysisDefine.W_START_PLAY, m_EvtParamDict);
        }
        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="stageId">??????Id</param>
        /// <param name="result">??????</param>
        /// <param name="time">?????????</param>
        public void SatoriEvt_EndPlay(string stageId, SatoriEvtDefine.GameResultType result, int time = 0)
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", stageId);
            m_EvtParamDict.Add("result", result.ToString());
            m_EvtParamDict.Add("time", time);
            CustomEventDic(DataAnalysisDefine.W_END_PLAY, m_EvtParamDict);
        }

        /// <summary>
        /// ??????(?????????????????????)
        /// </summary>
        /// <param name="taskId">??????Id</param>
        /// <param name="result">??????:success,fail,uncompleted</param>
        /// <param name="des">????????????</param>
        public void SatoriEvt_Task(int taskId, SatoriEvtDefine.GameResultType result = SatoriEvtDefine.GameResultType.success, string des = "")
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", taskId.ToString());
            m_EvtParamDict.Add("result", result.ToString());
            m_EvtParamDict.Add("description", des);
            CustomEventDic(DataAnalysisDefine.W_TASK, m_EvtParamDict);
        }

        /// <summary>
        /// ????????????????????????
        /// </summary>
        /// <param name="scene">??????Id</param>
        public void SatoriEvt_ADRewardSceneShow(string scene)
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", scene);
            CustomEventDic(DataAnalysisDefine.W_ADSCENE_SHOW, m_EvtParamDict);
        }

        /// <summary>
        /// ??????????????????
        /// </summary>
        /// <param name="clickDesc">????????????</param>
        public void SatoriEvt_ADRewardRequest(string clickDesc)
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", clickDesc);
            CustomEventDic(DataAnalysisDefine.w_ADREWARD_CLICK, m_EvtParamDict);
        }

        /// <summary>
        /// ???????????? ????????????  ????????? ????????????????????????  ?????????????????????
        /// </summary>
        /// <param name="clickDesc">????????????</param>
        public void SatoriEvt_ADReward(string clickDesc)
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", clickDesc);
            CustomEventDic(DataAnalysisDefine.w_AD_REWARD, m_EvtParamDict);
        }

        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="pageId">????????????/??????id</param>
        public void SatoriEvt_PageView(string pageId)
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("page", pageId);
            CustomEventDic(DataAnalysisDefine.W_PAGE_VIEW, m_EvtParamDict);
        }

        /// <summary>
        /// ??????????????????
        /// </summary>
        /// <param name="id">????????????/??????id</param>
        /// <param name="description">?????????????????????</param>
        /// <param name="adTag">???????????????????????????????????????????????????????????????????????????</param>
        public void SatoriEvt_PageItemClick(string pageId, string description, SatoriEvtDefine.PageClickType clickType, string target, string source)
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("page", pageId);
            m_EvtParamDict.Add("description", description);
            m_EvtParamDict.Add("type", clickType.ToString());
            m_EvtParamDict.Add("target", target);
            m_EvtParamDict.Add("source", source);

            CustomEventDic(DataAnalysisDefine.W_PAGE_CLICK, m_EvtParamDict);
        }

        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="userId">??????id,?????????????????????ID</param>
        /// <param name="type">??????????????????????????????????????????</param>
        /// <param name="description">??????????????????</param>
        /// <param name="result">???????????? 0-?????????1-??????</param>
        public void SatoriEvt_UserLogin(string userId, string type = "server",
            SatoriEvtDefine.CommonResultType resultType = SatoriEvtDefine.CommonResultType.Success, string des = "")
        {
            // if (!SDKConfig.S.dataAnalysisConfig.embedsdkConfig.enableAF)
            //     AppsFlyer.setCustomerUserId(userId);
            EmbedSDKMgr.S.SetUserAppId(userId);

            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("type", type);
            m_EvtParamDict.Add("description", des);
            m_EvtParamDict.Add("result", ((int)resultType).ToString());
            CustomEventDic(DataAnalysisDefine.W_LOG_IN, m_EvtParamDict);
        }

        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="userId">??????id,?????????????????????ID</param>
        public void SatoriEvt_UserLogout(string userId)
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", userId);
            CustomEvent(DataAnalysisDefine.W_LOGOUT, m_EvtParamDict);
        }

        /// <summary>
        /// ??????????????????
        /// </summary>
        public void SatoriEvt_OpenShare()
        {
            CustomEvent(DataAnalysisDefine.W_SHARE_OPEN);
        }

        /// <summary>
        /// ??????????????????
        /// </summary>
        public void SatoriEvt_StartShare()
        {
            CustomEvent(DataAnalysisDefine.W_SHARE_START);
        }

        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="withdrawId">????????????id</param>
        /// <param name="value">????????????</param>
        public void SatoriEvt_WithdrawSuccess(string withdrawId, double value)
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", withdrawId);
            m_EvtParamDict.Add("value", value.ToString());
            CustomEventDic(DataAnalysisDefine.W_WITHDRAW, m_EvtParamDict);
        }

        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="withdrawId">????????????id</param>
        /// <param name="type">???????????????????????????????????????</param>
        /// <param name="desc">??????????????????</param>
        public void SatoriEvt_WithdrawFail(string withdrawId, string type, double value, string desc = "")
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", withdrawId);
            m_EvtParamDict.Add("type", type);
            m_EvtParamDict.Add("description", desc);
            m_EvtParamDict.Add("value", value.ToString());
            CustomEventDic(DataAnalysisDefine.W_WITHDRAW_FAIL, m_EvtParamDict);
        }

        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="itemId">??????id</param>
        /// <param name="orderId">??????id</param>
        /// <param name="usdPrice">????????????????????????</param>
        public void SatoriEvt_PurchaseRequest(string itemId, string orderId = "", string usdPrice = "0")
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", itemId);
            m_EvtParamDict.Add("target", orderId);
            m_EvtParamDict.Add("value", usdPrice);
            CustomEventDic(DataAnalysisDefine.W_PURCHASE_REQUEST, m_EvtParamDict);
        }
        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="itemId">??????id</param>
        /// <param name="orderId">??????id</param>
        /// <param name="usdPrice">????????????????????????</param>
        public void SatoriEvt_PurchaseSuccess(string itemId, string orderId = "", string usdPrice = "0")
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", itemId);
            m_EvtParamDict.Add("target", orderId);
            m_EvtParamDict.Add("value", usdPrice);
            CustomEventDic(DataAnalysisDefine.W_PURCHASE_SUCCESS, m_EvtParamDict);
        }
        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="itemId">??????id</param>
        /// <param name="orderId">??????id</param>
        /// <param name="usdPrice">????????????????????????</param>
        public void SatoriEvt_PurchaseCancel(string itemId, string orderId = "", string usdPrice = "0")
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", itemId);
            m_EvtParamDict.Add("target", orderId);
            m_EvtParamDict.Add("value", usdPrice);
            CustomEventDic(DataAnalysisDefine.W_PURCHASE_CANCEL, m_EvtParamDict);
        }
        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="itemId">??????id</param>
        /// <param name="orderId">??????id</param>
        /// <param name="usdPrice">????????????????????????</param>
        /// <param name="desc">????????????</param>
        public void SatoriEvt_PurchaseFailed(string itemId, string orderId = "", string usdPrice = "0", string desc = "")
        {
            m_EvtParamDict.Clear();
            m_EvtParamDict.Add("item_id", itemId);
            m_EvtParamDict.Add("target", orderId);
            m_EvtParamDict.Add("value", usdPrice);
            m_EvtParamDict.Add("description", desc);
            CustomEventDic(DataAnalysisDefine.W_PURCHASE_FAILED, m_EvtParamDict);
        }

        public void SatoriEvt_FirstGetCoin()
        {
            CustomEvent(DataAnalysisDefine.W_FIRST_GET_COINS);
        }

        #endregion

        #region KeyBehavior
        public void CustomEvent_KeyBehavior(int keyIndex, object label = null)
        {
            switch (keyIndex)
            {
                case 1:
                    DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.W_KEY_BEHAVIOR_1, label);
                    break;
                case 2:
                    DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.W_KEY_BEHAVIOR_2, label);
                    break;
                case 4:
                    DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.W_KEY_BEHAVIOR_4, label);
                    break;
                case 5:
                    DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.W_KEY_BEHAVIOR_5, label);
                    break;
                case 6:
                    DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.W_KEY_BEHAVIOR_6, label);
                    break;
                case 7:
                    DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.W_KEY_BEHAVIOR_7, label);
                    break;
                case 8:
                    DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.W_KEY_BEHAVIOR_8, label);
                    break;
                case 9:
                    DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.W_KEY_BEHAVIOR_9, label);
                    break;
                case 10:
                    DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.W_KEY_BEHAVIOR_10, label);
                    break;
                case 11:
                    DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.W_KEY_BEHAVIOR_11, label);
                    break;
                case 3:
                    Log.e("????????????3????????????????????????????????????");
                    break;
                default:
                    Log.e("index???????????????????????????????????????");
                    break;
            }
        }
        #endregion


        #region Embed

        public void EmbedAbTestLog(string des, string ab)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("description", des);
            dic.Add("type", ab);
            CustomEventDic("w_user_segment", dic);
        }

        public void EmbedCustomEventDic(string key, Dictionary<string, object> param)
        {
            CustomEventDic(key, param);
        }

        public void EmbedCustomEventDicLifeCircleSingle(string key, Dictionary<string, object> param, string singleKey)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            if (PlayerPrefs.GetInt(singleKey, 0) > 0)
            {
                return;
            }

            PlayerPrefs.SetInt(singleKey, 1);

            CustomEventDic(key, param);
        }

        public void EmbedCustomEventDicDailySingle(string key, Dictionary<string, object> param, string dayKey)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return;
            }

            int day = PlayerPrefs.GetInt(dayKey, -1);
            if (day == m_InstallDays)
            {
                return;
            }

            PlayerPrefs.SetInt(dayKey, m_InstallDays);

            CustomEventDic(key, param);
        }

        #endregion 

    }
}
