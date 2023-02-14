using GameWish.Game;
using RichOX.Api;
using ROXBase.Api;
using ROXSect.Api;
using ROXShare.Api;
using ROXStrategy.Api;
using ROXToolbox.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PiggyBank = ROXToolbox.Api.PiggyBank;

namespace Qarth
{
    public class RichOXMgr : TSingleton<RichOXMgr>
    {
        public bool inited;
        public bool roxH5Inited;
        public bool ready;
        public bool roxShareInited;

        private RichOXSaveDataHandler m_DataHandler;
        public static event RichOXInitResult RichOXInitResultdelegate;
        public delegate void RichOXInitResult(bool result);

        public static event RichOXInitFail RichOXInitFaildelegate;
        public delegate void RichOXInitFail();

        public void Init()
        {
            if (!SDKConfig.S.richOXConfig.isEnable)
                return;

            if (inited)
                return;
            if (SDKConfig.S.richOXConfig.debug)
            {
                RichOXBase.Instance.SetTestMode(true);
            }

            m_DataHandler = new RichOXSaveDataHandler();

            RichOXBase.Instance.OnEvent += (sender, args) =>
            {
                Dictionary<string, string> sendvalue = new Dictionary<string, string>();

                string name = args.RichOXEvent.GetName();
                Debug.Log("onEvent name: " + name);

                string value = args.RichOXEvent.GetValue();
                Debug.Log("onEvent value: " + value);

                sendvalue.Add("event_value", value);

                Dictionary<string, string> param = args.RichOXEvent.GetMapValue();
                if (param != null)
                {
                    foreach (KeyValuePair<string, string> item in param)
                    {
                        string desc = item.Key + ": " + item.Value;
                        Debug.Log("onEvent mapValue: " + desc);
                    }
                }
            };

            string appId = "";

#if UNITY_ANDROID
            appId = SDKConfig.S.adsConfig.taurusXAdsConfig.appIDAndroid;
#elif UNITY_IOS
            appId = SDKConfig.S.adsConfig.taurusXAdsConfig.appIDIos;
#endif

            if (!string.IsNullOrEmpty(SDKConfig.S.richOXConfig.platformId))
            {
                RichOXBase.Instance.SetPlatformId(SDKConfig.S.richOXConfig.platformId);
            }

            string deviceId = "";

#if !UNITY_EDITOR
            if (SDKConfig.S.richOXConfig.customDeviceID)
            {
                deviceId = DeviceID.Get();
            }
            else
            {
                deviceId = RichOXBase.Instance.GenDefaultDeviceId();
            }
#endif
            if (!string.IsNullOrEmpty(deviceId))
            {
                RichOXBase.Instance.SetDeviceId(deviceId);
            }

            RichOXBase.Instance.Init(appId, new InitResponse());

            inited = true;

            Debug.Log("ox_init: userId" + RichOXSaveDataHandler.data.userId);

            //拉取用户信息
            if (!string.IsNullOrEmpty(RichOXSaveDataHandler.data.userId))
            {
                GetUserInfo(OnUserInfoFetched);
            }
        }
        private class InitResponse : ROXInterface<bool>
        {
            public void OnSuccess(bool result)
            {
                if (RichOXInitResultdelegate != null)
                {
                    RichOXInitResultdelegate.Invoke(result);
                }
            }
            public void OnFailed(int code, string msg)
            {
                if (RichOXInitFaildelegate != null)
                {
                    RichOXInitFaildelegate.Invoke();
                }
                Debug.Log("init failed : code " + code + "  msg : " + msg);
            }
        }


        //todo 初始化状态检测
        private void CheckRoxState()
        {
            if (!inited)
                return;
            if (string.IsNullOrEmpty(RichOXSaveDataHandler.data.userId))
                return;

            if (m_UserInfo == null)
            {
                GetUserInfo(OnUserInfoFetched);
            }
            else
            {
                if (m_NormalStrategy == null)
                {
                    FetchAppNormalStrategy(SDKConfig.S.richOXConfig.strategyId, OnNormalStrategyFetched);
                }
            }
        }

        private void InitModule()
        {
            FetchAppNormalStrategy(SDKConfig.S.richOXConfig.strategyId, OnNormalStrategyFetched);
            InitROXH5();
            InitROXShare();
            InitROXSect();
            InitROXChat();
        }

        private void OnUserInfoFetched(RichOXCallback<ROXUserBean>.Respond respond)
        {
            if (respond.success)
            {
                RefreshUserInfo(respond.result);
                InitModule();
            }
            else
            {
                FloatMessage.S.ShowMsg("获取用户信息失败");
            }
        }

        private void OnNormalStrategyFetched(RichOXCallback<NormalStrategyConfig>.Respond respond)
        {
            Debug.Log("ox_strategy_fetch: " + respond.success);

            if (respond.success)
            {
                m_NormalStrategy = respond.result;

                EventSystem.S.Send(OXEventID.OnNormalStrategyFetched);

                QueryAssetInfo(OnNormalAssetInfoFetched);
                QueryPiggyBankList(null);
                if (SDKConfig.S.richOXConfig.debug)
                {
                    Debug.Log("ox_assetInfo: OnNormalStrategyFetched： " + respond.result.ToString());
                }
            }
        }

        private void OnNormalAssetInfoFetched(RichOXCallback<NormalAssetsInfo>.Respond respond)
        {
            Debug.Log("ox_assetInfo_fetch: " + respond.success);

            if (respond.success)
            {
                m_NormalAssetsInfo = respond.result;

                if (!ready)
                {
                    ready = true;
                    EventSystem.S.Send(OXEventID.OnRichOXReady);
                }

                EventSystem.S.Send(OXEventID.OnUserAssetInfoUpdate);

                if (SDKConfig.S.richOXConfig.debug)
                {
                    Debug.Log("ox_assetInfo:" + respond.result.ToString());
                }
            }
        }

        #region User module 用户信息

        private ROXUserBean m_UserInfo;

        public ROXUserBean userInfo
        {
            get
            {
                if (m_UserInfo == null)
                {
                    CheckRoxState();
                    return new ROXUserBean();
                }

                return m_UserInfo;
            }
        }

        private void RefreshUserInfo(ROXUserBean info)
        {
            Debug.Log("ox_refresh_userInfo");
            m_UserInfo = info;

            if (inited)
                RichOXSaveDataHandler.data.SetUserId(m_UserInfo.Id);

            if (SDKConfig.S.richOXConfig.debug)
                Debug.Log("ox_userInfo:" + m_UserInfo.ToString());

            EventSystem.S.Send(OXEventID.OnUserInfoRefresh);
        }

        //接口
        //设置fission的userid（特殊场景：fission和richox一起使用）
        public void SetUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return;

            RichOXBase.Instance.SetUserId(userId);

            if (string.IsNullOrEmpty(RichOXSaveDataHandler.data.userId))
                GetUserInfo(OnUserInfoFetched);
        }

        //注册游客
        public void RegisterVisitor(Action<RichOXCallback<ROXUserBean>.Respond> callback)
        {
            RichOXCallback<ROXUserBean> roxCallback = new RichOXCallback<ROXUserBean>();
            roxCallback.operation = "RegisterVisitor";
            roxCallback.callback = (respond) =>
            {
                if (respond.success)
                {
                    RefreshUserInfo(respond.result);
                    InitModule();
                    DataAnalysisMgr.S.SatoriEvt_UserLogin(respond.result.Id, "visitor");
                }
            };

            roxCallback.callback += callback;
            RichOXUserManager.Instance.RegisterVisitor(roxCallback);
        }
        //绑定微信
        public void StartBindAccount(string type, string appid, string code_or_token, Action<RichOXCallback<ROXUserBean>.Respond> callback)
        {
            RichOXCallback<ROXUserBean> roxCallback = new RichOXCallback<ROXUserBean>();
            roxCallback.operation = "StartBindAccount";
            roxCallback.callback = (respond) =>
            {
                if (respond.success)
                {
                    RefreshUserInfo(respond.result);
                    DataAnalysisMgr.S.SatoriEvt_UserLogin(respond.result.Id, "wehcat");
                }
            };
            roxCallback.callback += callback;
            RichOXUserManager.Instance.StartBindAccount(type, appid, code_or_token, roxCallback);
        }
        //注册微信用户
        public void RegisterWithWechat(string wxAppId, string wxCode, Action<RichOXCallback<ROXUserBean>.Respond> callback)
        {
            RichOXCallback<ROXUserBean> roxCallback = new RichOXCallback<ROXUserBean>();
            roxCallback.operation = "RegisterWithWechat";
            roxCallback.callback = (respond) =>
            {
                RefreshUserInfo(respond.result);
                InitModule();
                DataAnalysisMgr.S.SatoriEvt_UserLogin(respond.result.Id, "wechat");
            };
            roxCallback.callback += callback;
            RichOXUserManager.Instance.RegisterWithWechat(wxAppId, wxCode, roxCallback);
        }
        //获取个人信息
        public void GetUserInfo(Action<RichOXCallback<ROXUserBean>.Respond> callback)
        {
            RichOXCallback<ROXUserBean> roxCallback = new RichOXCallback<ROXUserBean>();
            roxCallback.operation = "GetUserInfo";
            roxCallback.callback = callback;

            RichOXUserManager.Instance.GetUserInfo(roxCallback);
            //RichOXUser.Instance.GetUserInfo(roxCallback);
        }


        //获取排行
        [Obsolete("未实现", true)]
        public void GetUserRanking(int count, int accountType, string rankingType, Action<RichOXCallback<List<ROXUserBean>>.Respond> callback)
        {
            RichOXCallback<List<ROXUserBean>> roxCallback = new RichOXCallback<List<ROXUserBean>>();
            roxCallback.operation = "GetUserRanking";
            roxCallback.callback = callback;
            // RichOXUser.Instance.GetUserRanking(count, accountType, rankingType, roxCallback);
        }

        //获取指定用户信息
        [Obsolete("未实现", true)]
        public void GetSpecificUsersInfo(List<string> userList, Action<RichOXCallback<List<ROXUserInfoSimple>>.Respond> callback)
        {
            RichOXCallback<List<ROXUserInfoSimple>> roxCallback = new RichOXCallback<List<ROXUserInfoSimple>>();
            roxCallback.operation = "GetSpecificUsersInfo";
            roxCallback.callback = callback;
            RichOXUser.Instance.GetSpecificUsersInfo(userList, roxCallback);
        }
        //获取token
        public void GetUserToken(Action<RichOXCallback<ROXUserToken>.Respond> callback)
        {
            RichOXCallback<ROXUserToken> roxCallback = new RichOXCallback<ROXUserToken>();
            roxCallback.operation = "GetUserToken";
            roxCallback.callback = callback;
            RichOXUser.Instance.GetUserToken(roxCallback);
        }
        //注销
        public void Logout(Action<RichOXCallback<bool>.Respond> callback)
        {
            RichOXCallback<bool> roxCallback = new RichOXCallback<bool>();
            roxCallback.operation = "Logout";
            roxCallback.callback = callback;
            RichOXUser.Instance.Logout(roxCallback);
            RichOXUserManager.Instance.Logout(roxCallback);

        }
        //
        public void QueryAppEventValue(string eventName, Action<QueryEventCallback<string>.Respond> _callback)
        {
            QueryEventCallback<string> roxCallback = new QueryEventCallback<string>();
            roxCallback.operation = "QueryAppEventValue";
            roxCallback.callback = _callback;
            RichOXBase.Instance.QueryEventValue(eventName, roxCallback);
        }

        //上报应用内事件
        public void ReportAppEvent(string eventName)
        {
            RichOXBase.Instance.ReportAppEvent(eventName);
        }
        //上报应用内事件带参数
        public void ReportAppEvent(string eventName, string eventValue)
        {
            RichOXBase.Instance.ReportAppEvent(eventName, eventValue);
        }

        #endregion

        #region Strategy module 奖励任务 提现任务 所有资产
        private ROXNormalStrategy m_RoxNormalStrategy;
        private NormalStrategyConfig m_NormalStrategy;//任务，提现配置
        private NormalAssetsInfo m_NormalAssetsInfo;//资产数据 需要更新

        //金币对应的钱
        public double GetMoney()
        {
            double money = GetCoinCount() / GetExchageRate("金币");
            return money;
        }

        //金币
        public double GetCoinCount()
        {
            return GetAssetCount("金币");
        }

        //现金
        public double GetCashCount()
        {
            return GetAssetCount("现金");
        }

        //汇率
        public double GetExchageRate(string assetType = "金币")
        {
            if (m_NormalStrategy == null)
            {
                CheckRoxState();
                return 1;
            }


            foreach (var assetStock in m_NormalStrategy.MissionTaskInfo.StrategyAssetList)
            {
                if (assetStock.AssetName == assetType)
                    return assetStock.ExchangeRate;
            }

            return 1;
        }

        //额外资产
        public double GetAssetCount(string assetType)
        {
            if (m_NormalAssetsInfo == null)
                return 0;

            foreach (var asset in m_NormalAssetsInfo.AssetStockList)
            {
                if (asset.AssetName == assetType)
                {
                    return asset.AssetAmount;
                }
            }

            return 0;
        }

        //任务列表
        public List<StrategyMissionTask> GetRewardMissionTasks()
        {
            if (m_NormalStrategy == null)
            {
                CheckRoxState();
                return new List<StrategyMissionTask>();
            }

            return m_NormalStrategy.MissionTaskInfo.StrategyMissionTaskList;
        }

        //指定任务
        public StrategyMissionTask GetMissionTask(string missionId)
        {
            foreach (var strategyMissionTask in GetRewardMissionTasks())
            {
                if (strategyMissionTask.Id == missionId)
                    return strategyMissionTask;
            }

            return null;
        }

        //提现列表
        public List<NormalStrategyWithdrawTask> GetWithdrawTasks()
        {
            if (m_NormalStrategy == null)
            {
                CheckRoxState();
                return new List<NormalStrategyWithdrawTask>();
            }

            return m_NormalStrategy.NormalWithdrawTaskList;
        }

        //指定提现任务
        public NormalStrategyWithdrawTask GetWithdrawTask(string taskId)
        {
            foreach (var normalStrategyWithdrawTask in GetWithdrawTasks())
            {
                if (normalStrategyWithdrawTask.Id == taskId)
                {
                    return normalStrategyWithdrawTask;
                }
            }

            return null;
        }

        //提现记录
        public List<StrategyWithdrawRecord> GetWithdrawRecords()
        {
            if (m_NormalAssetsInfo == null)
                return new List<StrategyWithdrawRecord>();
            return m_NormalAssetsInfo.Records;
        }

        //更新资产数据
        private void RefreshAssetStockInfo(List<NormalAssetStock> data)
        {
            if (m_NormalAssetsInfo == null)
                return;
            m_NormalAssetsInfo.AssetStockList = data;
            QueryPiggyBankList(null);
            EventSystem.S.Send(OXEventID.OnUserAssetInfoUpdate);
        }

        //接口
        //拉取任务配置
        public void FetchAppNormalStrategy(int strategyId, Action<RichOXCallback<NormalStrategyConfig>.Respond> callback)
        {
            RichOXCallback<NormalStrategyConfig> roxCallback = new RichOXCallback<NormalStrategyConfig>();
            roxCallback.operation = "FetchAppNormalStrategy";
            roxCallback.callback = callback;
            m_RoxNormalStrategy = ROXNormalStrategy.Instance(strategyId);
            m_RoxNormalStrategy.GetStrategyConfig(roxCallback);
        }

        //拉取资产数据
        public void QueryAssetInfo(Action<RichOXCallback<NormalAssetsInfo>.Respond> callback)
        {
            if (m_RoxNormalStrategy != null)
            {
                RichOXCallback<NormalAssetsInfo> roxCallback = new RichOXCallback<NormalAssetsInfo>();
                roxCallback.operation = "QueryAssetInfo";
                roxCallback.callback = callback;
                m_RoxNormalStrategy.QueryAssetInfo(roxCallback);
            }
        }

        //可变奖励任务 最大值限制
        public void DoVariableMission(string taskId, double amount, Action<RichOXCallback<NormalMissionResult>.Respond> callback)
        {
            RichOXCallback<NormalMissionResult> roxCallback = new RichOXCallback<NormalMissionResult>();
            roxCallback.operation = "DoVariableMission";
            roxCallback.callback = (respond) =>
            {
                if (respond.success)
                {
                    RefreshAssetStockInfo(respond.result.AssetList);
                }
            };

            Debug.Log("taskId  DoVariableMission " + taskId + "  amount:  " + amount);
            roxCallback.callback += callback;

            if (m_RoxNormalStrategy != null)
            {
                m_RoxNormalStrategy.DoMission(taskId, amount, roxCallback);
            }
        }

        //固定奖励任务
        public void DoMission(string taskId, Action<RichOXCallback<NormalMissionResult>.Respond> callback)
        {
            RichOXCallback<NormalMissionResult> roxCallback = new RichOXCallback<NormalMissionResult>();
            roxCallback.operation = "DoMission";
            roxCallback.callback = callback;
            roxCallback.callback += (respond) =>
            {
                if (respond.success)
                {
                    RefreshAssetStockInfo(respond.result.AssetList);
                }
            };

            if (m_RoxNormalStrategy != null)
            {
                m_RoxNormalStrategy.DoMission(taskId, roxCallback);
            }
        }

        //策略任务ecpm
        public void DoCustomRulesMission(string taskId, string tid, Action<RichOXCallback<NormalMissionResult>.Respond> callback)
        {
            RichOXCallback<NormalMissionResult> roxCallback = new RichOXCallback<NormalMissionResult>();
            roxCallback.operation = "DoCustomRulesMissionEcpm";
            roxCallback.callback = callback;
            roxCallback.callback += (respond) =>
            {
                if (respond.success)
                {
                    RefreshAssetStockInfo(respond.result.AssetList);
                }
            };

            if (m_RoxNormalStrategy != null)
            {
                m_RoxNormalStrategy.DoCustomRulesMission(taskId, tid, roxCallback);
            }
        }

        //策略任务，自定义规则
        public void DoCustomRulesMission(string taskId, Action<RichOXCallback<NormalMissionResult>.Respond> callback)
        {
            RichOXCallback<NormalMissionResult> roxCallback = new RichOXCallback<NormalMissionResult>();
            roxCallback.operation = "DoCustomRulesMission";
            roxCallback.callback = callback;
            roxCallback.callback += (respond) =>
            {
                if (respond.success)
                {
                    RefreshAssetStockInfo(respond.result.AssetList);
                }
            };

            if (m_RoxNormalStrategy != null)
            {
                m_RoxNormalStrategy.DoCustomRulesMission(taskId, roxCallback);
            }
        }

        public void ExtermeWithdrawNew(string taskId, Action<RichOXCallback<List<NormalAssetStock>>.Respond> callback)
        {
            RichOXCallback<List<NormalAssetStock>> roxCallback = new RichOXCallback<List<NormalAssetStock>>();
            roxCallback.operation = "ExtremeWithdrawNew";

            roxCallback.callback = respond =>
            {
                if (respond.success)
                {

                    int count = 0;
                    foreach (var strategyWithdrawRecord in GetWithdrawRecords())
                    {
                        if (strategyWithdrawRecord.WithdrawTaskId == taskId)
                            count++;
                    }

                    count++;
                    DataAnalysisMgr.S.SatoriEvt_WithdrawSuccess(taskId + "_" + count, (float)GetWithdrawTask(taskId).RewardAmount);

                    QueryAssetInfo(OnNormalAssetInfoFetched);
                    EventSystem.S.Send(OXEventID.OnWithdrawSuccess, taskId);

                }
                else
                {
                    DataAnalysisMgr.S.SatoriEvt_WithdrawFail(taskId, "小额", GetWithdrawTask(taskId).RewardAmount, respond.msg);
                }
            };

            roxCallback.callback += callback;

            if (m_RoxNormalStrategy != null && m_NormalAssetsInfo != null)
            {
                m_RoxNormalStrategy.ExtremeWithdrawNew(taskId, roxCallback);
            }
        }

        public void WithdrawNew(string taskId, string realname, string cardId, string phoneNumber, Action<RichOXCallback<List<NormalAssetStock>>.Respond> callback)
        {
            RichOXCallback<List<NormalAssetStock>> roxCallback = new RichOXCallback<List<NormalAssetStock>>();
            roxCallback.operation = "ExtremeWithdrawNew";

            roxCallback.callback = respond =>
            {
                if (respond.success)
                {
                    int count = 0;
                    foreach (var strategyWithdrawRecord in GetWithdrawRecords())
                    {
                        if (strategyWithdrawRecord.WithdrawTaskId == taskId)
                            count++;
                    }

                    count++;
                    DataAnalysisMgr.S.SatoriEvt_WithdrawSuccess(taskId + "_" + count, (float)GetWithdrawTask(taskId).RewardAmount);
                    EventSystem.S.Send(OXEventID.OnWithdrawSuccess, taskId);

                    m_NormalAssetsInfo.AssetStockList = respond.result;
                }
                else
                {
                    DataAnalysisMgr.S.SatoriEvt_WithdrawFail(taskId, "大额", GetWithdrawTask(taskId).RewardAmount, respond.msg);
                }
            };

            roxCallback.callback += callback;

            if (m_RoxNormalStrategy != null && m_NormalAssetsInfo != null)
            {
                m_RoxNormalStrategy.WithdrawNew(taskId, realname, cardId, phoneNumber, roxCallback);
            }
        }

        public void GeneralWithdraw(string taskId, Hashtable withdrawParam,
            Action<RichOXCallback<List<NormalAssetStock>>.Respond> callback)
        {
            RichOXCallback<List<NormalAssetStock>> richOxCallback = new RichOXCallback<List<NormalAssetStock>>();

            richOxCallback.callback = (respond) =>
            {
                if (respond.success)
                {
                    int count = 0;
                    foreach (var strategyWithdrawRecord in GetWithdrawRecords())
                    {
                        if (strategyWithdrawRecord.WithdrawTaskId == taskId)
                            count++;
                    }

                    count++;
                    DataAnalysisMgr.S.SatoriEvt_WithdrawSuccess(taskId + "_" + count, (double)withdrawParam[ROXNormalStrategy.WITHDRAW_AMOUNT]);

                    QueryAssetInfo(OnNormalAssetInfoFetched);
                    EventSystem.S.Send(OXEventID.OnWithdrawSuccess, taskId);
                }
                else
                {
                    DataAnalysisMgr.S.SatoriEvt_WithdrawFail(taskId, "小额", (double)withdrawParam[ROXNormalStrategy.WITHDRAW_AMOUNT], respond.msg);
                }
            };


            richOxCallback.callback += callback;

            if (m_RoxNormalStrategy != null && m_NormalAssetsInfo != null)
            {
                m_RoxNormalStrategy.GeneralWithdraw(taskId, withdrawParam, richOxCallback);
            }
        }

        public void GlobalWithdraw(string taskId, GlobalWithdrawInfo withdrawIfno,
            Action<RichOXCallback<List<NormalAssetStock>>.Respond> callback)
        {
            RichOXCallback<List<NormalAssetStock>> richOxCallback = new RichOXCallback<List<NormalAssetStock>>();
            richOxCallback.operation = "GlobalWithdraw";
            richOxCallback.callback = respond =>
            {
                if (respond.success)
                {
                    m_NormalAssetsInfo.AssetStockList = respond.result;
                    int count = 0;
                    foreach (var strategyWithdrawRecord in GetWithdrawRecords())
                    {
                        if (strategyWithdrawRecord.WithdrawTaskId == taskId)
                            count++;
                    }

                    count++;
                    DataAnalysisMgr.S.SatoriEvt_WithdrawSuccess(taskId + "_" + count, (float)GetWithdrawTask(taskId).RewardAmount);

                    QueryAssetInfo(OnNormalAssetInfoFetched);
                    EventSystem.S.Send(OXEventID.OnWithdrawSuccess, taskId);
                }
                else
                {
                    DataAnalysisMgr.S.SatoriEvt_WithdrawFail(taskId, "globe", GetWithdrawTask(taskId).RewardAmount, respond.msg);
                }
            };


            richOxCallback.callback += callback;
            if (m_RoxNormalStrategy != null && m_NormalAssetsInfo != null)
            {
                m_RoxNormalStrategy.GlobalWithdraw(taskId, withdrawIfno, richOxCallback);
            }
        }

        //极速提现
        public void ExtremeWithdraw(string taskId, Action<RichOXCallback<bool>.Respond> callback)
        {
            RichOXCallback<bool> roxCallback = new RichOXCallback<bool>();
            roxCallback.operation = "ExtremeWithdraw";
            roxCallback.callback = (respond) =>
            {
                if (respond.success)
                {
                    int count = 0;
                    foreach (var strategyWithdrawRecord in GetWithdrawRecords())
                    {
                        if (strategyWithdrawRecord.WithdrawTaskId == taskId)
                            count++;
                    }

                    count++;
                    DataAnalysisMgr.S.SatoriEvt_WithdrawSuccess(taskId + "_" + count, (float)GetWithdrawTask(taskId).RewardAmount);

                    QueryAssetInfo(OnNormalAssetInfoFetched);
                    EventSystem.S.Send(OXEventID.OnWithdrawSuccess, taskId);
                }
                else
                {
                    DataAnalysisMgr.S.SatoriEvt_WithdrawFail(taskId, "小额", GetWithdrawTask(taskId).RewardAmount, respond.msg);
                }
            };
            roxCallback.callback += callback;

            if (m_RoxNormalStrategy != null && m_NormalAssetsInfo != null)
            {
                m_RoxNormalStrategy.ExtremeWithdraw(taskId, roxCallback);
            }
        }

        //普通提现
        public void Withdraw(string taskId, string realName, string cardId, string phoneNumber, Action<RichOXCallback<bool>.Respond> callback)
        {
            RichOXCallback<bool> roxCallback = new RichOXCallback<bool>();
            roxCallback.operation = "Withdraw";
            roxCallback.callback = (respond) =>
            {
                if (respond.success)
                {
                    int count = 0;
                    foreach (var strategyWithdrawRecord in GetWithdrawRecords())
                    {
                        if (strategyWithdrawRecord.WithdrawTaskId == taskId)
                            count++;
                    }

                    count++;
                    DataAnalysisMgr.S.SatoriEvt_WithdrawSuccess(taskId + "_" + count, (float)GetWithdrawTask(taskId).RewardAmount);

                    EventSystem.S.Send(OXEventID.OnWithdrawSuccess, taskId);

                    QueryAssetInfo(OnNormalAssetInfoFetched);
                }
                else
                {
                    DataAnalysisMgr.S.SatoriEvt_WithdrawFail(taskId, "大额", GetWithdrawTask(taskId).RewardAmount, respond.msg);
                }
            };
            roxCallback.callback += callback;

            if (m_RoxNormalStrategy != null && m_NormalAssetsInfo != null)
            {
                m_RoxNormalStrategy.Withdraw(taskId, realName, cardId, phoneNumber, roxCallback);
            }
        }

        //资产部分兑换
        public void VariableTransform(string exchangeId, double amounts, Action<RichOXCallback<NormalTransformResult>.Respond> callback)
        {
            RichOXCallback<NormalTransformResult> roxCallback = new RichOXCallback<NormalTransformResult>();
            roxCallback.operation = "Transform";
            roxCallback.callback = (respond) =>
            {
                RefreshAssetStockInfo(respond.result.AssetList);
            };
            roxCallback.callback += callback;
            if (m_RoxNormalStrategy != null && m_NormalAssetsInfo != null)
            {
                m_RoxNormalStrategy.Transform(exchangeId, amounts, roxCallback);
            }
        }

        //资产全部兑换
        public void Transform(string exchangeId, Action<RichOXCallback<NormalTransformResult>.Respond> callback)
        {
            RichOXCallback<NormalTransformResult> roxCallback = new RichOXCallback<NormalTransformResult>();
            roxCallback.operation = "Transform";
            roxCallback.callback = (respond) =>
            {
                RefreshAssetStockInfo(respond.result.AssetList);
            };
            roxCallback.callback += callback;

            if (m_RoxNormalStrategy != null && m_NormalAssetsInfo != null)
            {
                m_RoxNormalStrategy.Transform(exchangeId, roxCallback);
            }
        }

        //拉取任务记录（每日任务返回最后一天的数据，全局任务返回总次数）
        public void QueryMissionRecord(string missionId, Action<RichOXCallback<NormalMissionsProgress>.Respond> callback)
        {
            List<string> missions = new List<string>();
            missions.Add(missionId);
            RichOXCallback<NormalMissionsProgress> roxCallback = new RichOXCallback<NormalMissionsProgress>();
            roxCallback.operation = "QueryMissionRecord";
            roxCallback.callback = callback;

            if (m_RoxNormalStrategy != null)
            {
                m_RoxNormalStrategy.QueryProgress(missions, roxCallback);
            }
        }

        //拉取所有任务记录
        public void QueryAllMissionRecord(Action<RichOXCallback<NormalMissionsProgress>.Respond> callback)
        {
            RichOXCallback<NormalMissionsProgress> roxCallback = new RichOXCallback<NormalMissionsProgress>();
            roxCallback.operation = "QueryAllMissionRecord";
            roxCallback.callback = callback;

            if (m_RoxNormalStrategy != null)
            {
                m_RoxNormalStrategy.QueryAllProgress(roxCallback);
            }
        }

        //储蓄罐
        private List<PiggyBank> m_PiggyBanks = new List<PiggyBank>();
        //获取具体储蓄罐
        public PiggyBank GetPiggyBankById(int id)
        {
            if (m_PiggyBanks == null)
                return null;

            for (int i = 0; i < m_PiggyBanks.Count; i++)
            {
                if (m_PiggyBanks[i].PiggyBankId == id)
                    return m_PiggyBanks[i];
            }

            return null;
        }

        public bool CheckPiggyBankCanDraw(int id)
        {
            PiggyBank bank = GetPiggyBankById(id);
            bool canDraw = false;

            if (bank == null)
            {
                Debug.LogError("piggy info is null");
                return false;
            }

            if (bank.UpdateTimeMS == 0) //updatetime==0 没有提交过任务
            {
                canDraw = false;
            }
            else
            {
                DateTime lastUpdateTime = CustomExtensions.GetTimeFromTimestamp(bank.UpdateTimeMS.ToString())
                    .ToLocalTime();

                if (lastUpdateTime.Day != DateTime.Now.Day)
                {
                    canDraw = bank.PrizeAmount > 0;
                }
                else
                {
                    canDraw = false;
                }
            }

            return canDraw;
        }

        //获取储蓄罐列表信息
        public void QueryPiggyBankList(Action<RichOXCallback<List<PiggyBank>>.Respond> callback)
        {
            if (SDKConfig.S.richOXConfig.usePiggyBank)
            {
                RichOXCallback<List<PiggyBank>> roxCallback = new RichOXCallback<List<PiggyBank>>();
                roxCallback.operation = "QueryPiggyBankList";
                roxCallback.callback = callback;
                roxCallback.callback += (respond) =>
                {
                    if (respond.success)
                    {
                        m_PiggyBanks = respond.result;

                        EventSystem.S.Send(OXEventID.OnPiggyBankRefresh);
                    }
                };
                RichOXToolbox.Instance().QueryPiggyBankList(roxCallback);
            }
        }

        //储蓄罐提取
        public void PiggyBankWithdraw(int piggyId, Action<RichOXCallback<bool>.Respond> callback)
        {
            RichOXCallback<bool> roxCallback = new RichOXCallback<bool>();
            roxCallback.operation = "PiggyBankWithdraw";
            roxCallback.callback = callback;
            roxCallback.callback += (respond) =>
            {
                if (respond.success)
                {
                    QueryPiggyBankList(null);
                    QueryAssetInfo(OnNormalAssetInfoFetched);
                    EventSystem.S.Send(OXEventID.OnPiggyBankDraw);
                }
            };
            RichOXToolbox.Instance().PiggyBankWithdraw(piggyId, roxCallback);
        }

        #endregion

        #region ROXH5

        private Dictionary<string, FloatSceneHandler> m_FloatSceneHandlers = new Dictionary<string, FloatSceneHandler>();

        private Dictionary<string, DialogSceneHandler> m_DialogSceneHandlers = new Dictionary<string, DialogSceneHandler>();

        private Dictionary<string, NativeSceneHandler> m_NativeSceneHandlers = new Dictionary<string, NativeSceneHandler>();

        private H5ShareCallback m_H5ShareCallBack;

        private void InitROXH5()
        {
            if (!SDKConfig.S.richOXConfig.roxH5Config.enable)
                return;
            if (roxH5Inited)
                return;
            roxH5Inited = true;

            RichOXManager.Instance.Init();
            RichOXManager.Instance.OnBindWeChat += (sender, args) =>
            {
                // WeShareMgr.S.Login(LoginType.Wechat, (code, token) =>
                // {
                //     if (code == 1)
                //     {
                //         StartBindAccount("wechat", SDKConfig.S.richOXConfig.wechatAppId, token, (respond) =>
                //         {
                //             if (respond.success)
                //             {
                //                 RichOXManager.Instance.NotifyBindWeChatResult(true, "");
                //             }
                //             else
                //             {
                //                 FloatMessage.S.ShowMsg("微信绑定失败");
                //                 RichOXManager.Instance.NotifyBindWeChatResult(false, "bind wechat error: no wechat");
                //             }
                //         });
                //     }
                // });
            };

            RichOXManager.Instance.OnUpdateItemInfo += (sender, args) =>
            {

            };

            m_H5ShareCallBack = new H5ShareCallback();
            m_H5ShareCallBack.RegisterGenShareUrlCallback((url, table) =>
            {
                Debug.Log("ox_h5_gen_url:" + url);
                ROXH5ShareCallback callback = new ROXH5ShareCallback();
                callback.callback = (code, result) =>
                {
                    Debug.Log("ox_h5_gen_callback:" + result);
                    if (code == 0)
                        RichOXManager.Instance.OnResultForGen(result, 0, "success");
                    else
                    {
                        RichOXManager.Instance.OnResultForGen(url, -1, "failed");
                    }
                };
                RichOXShare.Instance().GenShareUrl(url, table, callback);
            });
            m_H5ShareCallBack.RegisterShareContentAction((title, content, bitmap) =>
            {
                // WeShareUtils.SharePicBytesByWXSessionNotUnity(bitmap, (code) =>
                // {
                //     Debug.Log("ox_h5_share:" + code);
                //     if (code == 1)
                //     {
                //         RichOXManager.Instance.OnResultForShare(0, "success");
                //     }
                //     else
                //     {
                //         RichOXManager.Instance.OnResultForShare(-1, "failed");
                //     }
                // });
            });

            RichOXManager.Instance.RegisterShareCallback(m_H5ShareCallBack);

            //s.... m_NativeSceneHandlers
            m_FloatSceneHandlers.Clear();
            m_DialogSceneHandlers.Clear();
            m_NativeSceneHandlers.Clear();
            
//             for (int i = 0; i < TDROXH5SceneTable.count; i++)
//             {
//                 TDROXH5Scene scene = TDROXH5SceneTable.dataList[i];
//                 string id = "";
// #if UNITY_ANDROID
//                 id = scene.iDAndroid;
// #elif UNITY_IOS
//                 id = scene.iDIos;
// #endif
//                 AddSceneHandler(scene.key, id, scene.oXSceneType, scene.width, scene.height);
//             }

            //......
            EventSystem.S.Send(OXEventID.OnRichOXH5Inited);
        }

        //添加scene
        public void AddSceneHandler(string key, string id, string oxSceneType, int width, int height)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(key))
                return;

            if (m_FloatSceneHandlers.ContainsKey(key) || m_DialogSceneHandlers.ContainsKey(key) || m_NativeSceneHandlers.ContainsKey(key))
            {
                Log.w("ox_scene already contains key " + key);
                return;
            }

            OXSceneType sceneType = (OXSceneType)Enum.Parse(typeof(OXSceneType), oxSceneType);
            switch (sceneType)
            {
                case OXSceneType.FloatScene:
                    FloatSceneHandler floatHandler = new FloatSceneHandler(key, id, width, height);
                    if (floatHandler != null)
                        m_FloatSceneHandlers.Add(key, floatHandler);
                    break;
                case OXSceneType.DialogScene:
                    DialogSceneHandler dialogHandler = new DialogSceneHandler(key, id);
                    if (dialogHandler != null)
                        m_DialogSceneHandlers.Add(key, dialogHandler);
                    break;
                case OXSceneType.NativeScene:
                    NativeSceneHandler nativeHandler = new NativeSceneHandler(key, id);
                    if (nativeHandler != null)
                        m_NativeSceneHandlers.Add(key, nativeHandler);
                    break;
            }
        }

        public List<FloatSceneHandler> GetAllFloatSceneHandler()
        {
            if (m_FloatSceneHandlers == null)
                return null;

            return m_FloatSceneHandlers.Values.ToList();
        }

        public List<DialogSceneHandler> GetAllDialogSceneHandlers()
        {
            if (m_DialogSceneHandlers == null)
                return null;

            return m_DialogSceneHandlers.Values.ToList();
        }

        public List<NativeSceneHandler> GetAllNativeSceneHandlers()
        {
            if (m_NativeSceneHandlers == null)
                return null;

            return m_NativeSceneHandlers.Values.ToList();
        }

        public FloatSceneHandler GetFloatSceneHandler(string sceneId)
        {
            if (m_FloatSceneHandlers.ContainsKey(sceneId))
            {
                return m_FloatSceneHandlers[sceneId];
            }

            return null;
        }

        public DialogSceneHandler GetDialogSceneHandler(string sceneId)
        {
            if (m_DialogSceneHandlers.ContainsKey(sceneId))
            {
                return m_DialogSceneHandlers[sceneId];
            }

            return null;
        }

        public NativeSceneHandler GetNativeSceneHandler(string sceneId)
        {
            if (m_NativeSceneHandlers.ContainsKey(sceneId))
            {
                return m_NativeSceneHandlers[sceneId];
            }
            return null;
        }

        #endregion

        #region ROXShare

        private Hashtable m_RestoreParam;

        private void InitROXShare()
        {
            if (!SDKConfig.S.richOXConfig.roxShareConfig.enable)
                return;
            if (roxShareInited)
                return;
            roxShareInited = true;
            RichOXShare.Instance().Init();

            GetInstallParams((respond) =>
            {
                if (respond.success)
                {
                    m_RestoreParam = respond.result;

                    foreach (DictionaryEntry dictionaryEntry in respond.result)
                    {
                        Debug.Log("ox_share:" + dictionaryEntry.Key + "/" + dictionaryEntry.Value);
                    }

                    EventSystem.S.Send(OXEventID.OnROXShareRestoreParam);
                }
            });
        }

        //获取配置参数
        public object GetRestoreParamValue(string key)
        {
            if (m_RestoreParam == null)
                return null;

            if (m_RestoreParam.ContainsKey(key))
            {
                return m_RestoreParam[key];
            }

            return null;
        }

        /// <summary>
        /// 生成无码分享链接
        /// shareUrl : 分享链接
        /// urlParams : 参数信息
        /// <summary>
        public void GenShareUrl(string shareUrl, Hashtable urlParams, Action<RichOXCallback<string>.Respond> callback)
        {
            if (!roxShareInited)
            {
                Debug.LogError("ROXShare has Not initialized before using Function GenShareUrl");
                return;
            }
            RichOXCallback<string> roxCallback = new RichOXCallback<string>();
            roxCallback.operation = "GenShareUrl";
            roxCallback.callback = callback;
            RichOXShare.Instance().GenShareUrl(shareUrl, urlParams, roxCallback);
        }

        /// <summary>
        /// 还原安装函数
        /// <summary>
        public void GetInstallParams(Action<RichOXCallback<Hashtable>.Respond> callback)
        {
            if (!roxShareInited)
            {
                Debug.LogError("ROXShare has Not initialized before using Function GetInstallParams");
                return;
            }
            RichOXCallback<Hashtable> roxCallback = new RichOXCallback<Hashtable>();
            roxCallback.operation = "GetInstallParams";
            roxCallback.callback = callback;
            RichOXShare.Instance().GetInstallParams(roxCallback);
        }

        /// <summary>
        /// 分享链接转换为二维码图象Byte数组
        /// <summary>
        public byte[] GetQRCodeBytes(string shareUrl, int width, int height)
        {
            if (!roxShareInited)
            {
                Debug.LogError("ROXShare has Not initialized before using Function GetQRCodeBytes");
                return null;
            }
            return RichOXShare.Instance().GetQRCodeBytes(shareUrl, width, height);
        }
        /// <summary>
        /// 上报归因关系
        /// <summary>
        public void ReportBindEvent()
        {
            if (!roxShareInited)
            {
                Debug.LogError("ROXShare has Not initialized before using Function ReportBindEvent");
                return;
            }
            RichOXShare.Instance().ReportBindEvent(true);
        }
        ///<summary>
        ///上报分享入口展示
        ///<summary>
        public void ReportShowShare()
        {
            if (!roxShareInited)
            {
                Debug.LogError("ROXShare has Not initialized before using Function ReportShowShare");
                return;
            }
            RichOXShare.Instance().ReportShowShare();
        }
        ///<summary>
        ///上报分享界面打卡打点
        ///<summary>
        public void ReportOpenShare()
        {
            if (!roxShareInited)
            {
                Debug.LogError("ROXShare has Not initialized before using Function ReportOpenShare");
                return;
            }
            RichOXShare.Instance().ReportOpenShare();
        }
        ///<summary>
        ///上报点击分享打点
        ///<summary>
        public void ReportStartShare()
        {
            if (!roxShareInited)
            {
                Debug.LogError("ROXShare has Not initialized before using Function ReportStartShare");
                return;
            }
            RichOXShare.Instance().ReportStartShare();
        }
        ///<summary>
        ///上报分享成功
        ///<summary>
        public void ReportShareSuccess()
        {
            if (!roxShareInited)
            {
                Debug.LogError("ROXShare has Not initialized before using Function ReportShareSuccess");
                return;
            }
            RichOXShare.Instance().ReportShareSuccess();
        }

        #endregion

        #region Sect

        private SectSettings m_SectSetting;
        private SectInfo m_SectInfo;
        public int m_InviteCount = 0;
        private void InitROXSect()
        {
            if (!SDKConfig.S.richOXConfig.roxSectConfig.enable)
                return;

            //绑定师徒关系
            if (m_RestoreParam != null)
            {
                TryToBindMaster();
            }
            else
            {
                EventSystem.S.Register(OXEventID.OnROXShareRestoreParam, (id, args) =>
                {
                    TryToBindMaster();
                });
            }

            GetSectSettings((respond) =>
            {
                if (respond.success)
                {
                    m_SectSetting = respond.result;
                    Debug.Log("ox_sect_setting_fetched");
                }
            });

            GetSectInfo((respond) =>
            {
                if (respond.success)
                {
                    Debug.Log("ox_sect_info_fetched");
                }
            });

            //1 :加服务器过滤  默认为-1 不加过滤
            GetInviterCounts(-1, 1, (result) =>
            {
                if (result.success)
                {
                    m_InviteCount = result.result;
                }
            });
        }

        private void TryToBindMaster()
        {
            string masterId = GetRestoreParamValue(OXSectDefine.restoreIDKey) as string;
            if (!string.IsNullOrEmpty(masterId))
            {
                BindInviter(masterId, (respond) =>
                {
                    if (respond.success)
                    {
                        ReportBindEvent();
                        Debug.Log("ox_sect bind master success:id " + masterId);
                    }
                });
            }
        }

        //数据
        //刷新贡献值
        private void RefreshSectInfoReward(int contribution)
        {
            if (m_SectInfo == null)
                return;
            m_SectInfo.Chief.CurrentReward = contribution;
            EventSystem.S.Send(OXEventID.OnROXSectInfoRefresh);
        }

        //宗门数据
        public ChiefInfo chiefInfo
        {
            get
            {
                if (m_SectInfo == null)
                {
                    ChiefInfo info = new ChiefInfo();
                    info.InviteAwardMap = new Hashtable();
                    return info;
                }

                return m_SectInfo.Chief;
            }
        }
        //宗门配置
        public SectSettings sectSettings
        {
            get
            {
                if (m_SectSetting == null)
                    return new SectSettings();
                return m_SectSetting;
            }
        }

        //当前徒弟列表
        public ApprenticeList GetSectApprenticeList(int level)
        {
            if (m_SectInfo == null)
                return new ApprenticeList();
            if (!m_SectInfo.StudentsMap.ContainsKey(level))
            {
                Debug.LogError("ox_sect_apprentice level out of range");
                return new ApprenticeList();
            }

            return m_SectInfo.StudentsMap[level] as ApprenticeList;
        }

        //配置
        //邀请奖励配置
        public List<InviteAward> GetInviteValidAwardsSetting()
        {
            if (m_SectSetting == null)
                return new List<InviteAward>();

            return m_SectSetting.AwardSettingsList;
        }

        /// <summary>
        /// 拉取宗门信息
        /// <summary>         
        public void GetSectInfo(Action<RichOXCallback<SectInfo>.Respond> callback)
        {
            RichOXCallback<SectInfo> roxCallback = new RichOXCallback<SectInfo>();
            roxCallback.operation = "GetSectInfo";
            roxCallback.callback = (respond) =>
            {
                if (respond.success)
                {
                    m_SectInfo = respond.result;
                    EventSystem.S.Send(OXEventID.OnROXSectInfoRefresh);
                }
            };
            roxCallback.callback += callback;
            ROXSectManager.Instance().GetSectInfo(roxCallback);
        }

        /// <summary>
        /// 当前宗门用户状态
        /// 0: 非宗门用户
        /// 1: 宗门未验证
        /// 2: 宗门已验证
        /// <summary> 
        public void GetUserSectStatus(Action<RichOXCallback<int>.Respond> callback)
        {
            RichOXCallback<int> roxCallback = new RichOXCallback<int>();
            roxCallback.operation = "GetUserSectStatus"; ;
            roxCallback.callback = callback;
            ROXSectManager.Instance().GetUserSectStatus(roxCallback);
        }

        /// <summary>
        /// 获取某级弟子信息
        /// level: 弟子级数 ，1为掌门弟子
        /// <summary> 
        public void GetApprenticeList(int level, Action<RichOXCallback<ApprenticeList>.Respond> callback)
        {
            RichOXCallback<ApprenticeList> roxCallback = new RichOXCallback<ApprenticeList>();
            roxCallback.operation = "GetApprenticeListAll";
            roxCallback.callback = (respond) =>
            {
                if (respond.success)
                {
                    if (m_SectInfo.StudentsMap.ContainsKey(level))
                    {
                        m_SectInfo.StudentsMap[level] = respond.result;
                    }
                }
            };
            roxCallback.callback += callback;
            ROXSectManager.Instance().GetApprenticeList(level, roxCallback);
        }

        /// <summary>
        /// 分页获取某级弟子信息
        /// level: 弟子级数 ，1为掌门弟子
        /// pageSize: 每页大小，非必传字段，默认 -1
        /// currentPage: 当前页，非必传，默认为 -1
        /// <summary> 
        public void GetApprenticeList(int level, int pageSize, int currentPage, Action<RichOXCallback<ApprenticeList>.Respond> callback)
        {
            RichOXCallback<ApprenticeList> roxCallback = new RichOXCallback<ApprenticeList>();
            roxCallback.operation = "GetApprenticeListPage"; ;
            roxCallback.callback = callback;
            ROXSectManager.Instance().GetApprenticeList(level, pageSize, currentPage, roxCallback);
        }

        /// <summary>
        /// 获取某个弟子的详细信息
        /// apprenticeId: 具体弟子id
        /// <summary> 
        public void GetApprenticeInfo(string apprenticeId, Action<RichOXCallback<ApprenticeInfo>.Respond> callback)
        {
            RichOXCallback<ApprenticeInfo> roxCallback = new RichOXCallback<ApprenticeInfo>();
            roxCallback.operation = "GetApprenticeInfo"; ;
            roxCallback.callback = callback;
            ROXSectManager.Instance().GetApprenticeInfo(apprenticeId, roxCallback);
        }
        /// <summary>
        /// 产生贡献值
        /// action: 行为类型，看视频为 0
        /// <summary> 
        public void GenContribution(int action, Action<RichOXCallback<Contribution>.Respond> callback)
        {
            RichOXCallback<Contribution> roxCallback = new RichOXCallback<Contribution>();
            roxCallback.operation = "GenContribution";
            roxCallback.callback = callback;
            ROXSectManager.Instance().GenContribution(action, roxCallback);
        }
        /// <summary>
        /// 领取徒弟的贡献值
        /// studentUid: 某个徒弟的id
        /// <summary> 
        public void GetContribution(string studentUid, Action<RichOXCallback<Contribution>.Respond> callback)
        {
            RichOXCallback<Contribution> roxCallback = new RichOXCallback<Contribution>();
            roxCallback.operation = "GetContribution";
            roxCallback.callback = (respond) =>
            {
                if (respond.success)
                {
                    RefreshSectInfoReward(respond.result.TotalContribution);
                }
            };
            roxCallback.callback += callback;
            ROXSectManager.Instance().GetContribution(studentUid, roxCallback);
        }

        /// <summary>
        /// 一键领取所有徒弟的贡献值
        /// <summary>
        public void GetAllContribution(Action<RichOXCallback<Contribution>.Respond> callback)
        {
            RichOXCallback<Contribution> roxCallback = new RichOXCallback<Contribution>();
            roxCallback.operation = "GetAllContribution";
            roxCallback.callback = (respond) =>
            {
                if (respond.success)
                {
                    RefreshSectInfoReward(respond.result.TotalContribution);
                }
            };
            roxCallback.callback += callback;
            ROXSectManager.Instance().GetAllContribution(roxCallback);
        }

        /// <summary>
        /// 绑定师徒关系
        /// inviterUid: 师傅 ID
        /// <summary>
        public void BindInviter(string inviterUid, Action<RichOXCallback<bool>.Respond> callback)
        {
            RichOXCallback<bool> roxCallback = new RichOXCallback<bool>();
            roxCallback.operation = "BindInviter";
            roxCallback.callback = callback;
            ROXSectManager.Instance().BindInviter(inviterUid, roxCallback);
        }

        /// <summary>
        /// 获取宗门设置
        /// <summary>
        public void GetSectSettings(Action<RichOXCallback<SectSettings>.Respond> callback)
        {
            RichOXCallback<SectSettings> roxCallback = new RichOXCallback<SectSettings>();
            roxCallback.operation = "GetSettings";
            roxCallback.callback = callback;
            ROXSectManager.Instance().GetSettings(roxCallback);
        }

        /// <summary>
        /// 获取邀请排行榜
        /// <summary>
        public void GetInviteRanking(Action<RichOXCallback<List<SectRankingInfo>>.Respond> callback)
        {
            RichOXCallback<List<SectRankingInfo>> roxCallback = new RichOXCallback<List<SectRankingInfo>>();
            roxCallback.operation = "GetInviteRanking";
            roxCallback.callback = callback;
            ROXSectManager.Instance().GetInviteRanking(roxCallback);
        }

        /// <summary>
        /// 获取指定用户某个时间段后裂变邀请人数
        /// lastTime : 精确到毫秒, 不填则默认拉取所有人数
        /// status : 1 则只拉取验证弟子数，默认不过滤
        /// <summary>
        public void GetInviterCounts(long lastTime, int status, Action<RichOXCallback<int>.Respond> callback)
        {
            RichOXCallback<int> roxCallback = new RichOXCallback<int>();
            roxCallback.operation = "GetInviterCounts";
            roxCallback.callback = callback;
            ROXSectManager.Instance().GetInviterCounts(lastTime, status, roxCallback);
        }

        #endregion

        #region toolbox(chat)
        private void InitROXChat()
        {
            if (!SDKConfig.S.richOXConfig.roxChatConfig.enable)
                return;
            //此处不需要走init()方法  去 android studio application 里面调用这个init
            RichOXToolbox.Instance().SetInterval(SDKConfig.S.richOXConfig.roxChatConfig.interval);
            Debug.Log("richox ~ InitROXChat");
        }

        /// <summary>
        /// 拉取群组消息
        /// </summary>
        public void GetGroupInfo(Action<RichOXCallback<List<GroupInfo>>.Respond> callback)
        {
            RichOXCallback<List<GroupInfo>> roxCallback = new RichOXCallback<List<GroupInfo>>();
            roxCallback.operation = "GetGroupInfo";
            roxCallback.callback = callback;
            RichOXToolbox.Instance().GetGroupInfo(roxCallback);
        }

        /// <summary>
        /// 获取聊天红包群消息列表
        /// </summary>
        /// <param name="群组id"></param>
        /// <param name="需要拉取的消息数"></param>
        /// <param name="callback"></param>
        public void GetMessageList(string groupId, int size, Action<RichOXCallback<List<ChatMessage>>.Respond> callback)
        {
            RichOXCallback<List<ChatMessage>> roxCallback = new RichOXCallback<List<ChatMessage>>();
            roxCallback.operation = "GetMessageList";
            roxCallback.callback = callback;
            RichOXToolbox.Instance().GetMessageList(groupId, size, roxCallback);
        }

        /// <summary>
        /// 聊天红包群发送消息
        /// groupId : 群组Id
        /// nickName : 昵称
        /// avatar : 头像
        /// type : 聊天类型 10-普通内容 20-红包
        /// content : 聊天内容
        /// <summary>
        public void PostChatMessage(string groupId, string nickName, string avatar, string type, string content, Action<RichOXCallback<ChatMessage>.Respond> callback)
        {
            RichOXCallback<ChatMessage> roxCallback = new RichOXCallback<ChatMessage>();
            roxCallback.operation = "PostChatMessage";
            roxCallback.callback = callback;
            RichOXToolbox.Instance().PostChatMessage(groupId, nickName, avatar, type, content, roxCallback);
        }

        #endregion

    }

}