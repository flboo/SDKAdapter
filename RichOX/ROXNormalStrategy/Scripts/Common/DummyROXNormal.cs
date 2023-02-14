using ROXStrategy.Api;
using ROXBase.Api;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ROXStrategy.Common
{
    public class DummyROXNormal : IROXNormal
    {
        private double mTotal = 500;
        private double mCashTotal = 10;

        #region IROXNormal

        public void Init(int strategyId)
        {
            Debug.Log("init");
        }

        public void GetStrategyConfig(ROXInterface<NormalStrategyConfig> callback)
        {
            NormalStrategyConfig normalStrategyConfig = generateNormalStrategyConfig();
            callback.OnSuccess(normalStrategyConfig);
        }

        public void DoMission(string taskId, double amount, ROXInterface<NormalMissionResult> callback)
        {
            NormalMissionResult normalMissionResult = generateNormalMissionResult();
            callback.OnSuccess(normalMissionResult);
        }

        public void DoMission(string taskId, ROXInterface<NormalMissionResult> callback)
        {
            NormalMissionResult normalMissionResult = generateNormalMissionResult();
            callback.OnSuccess(normalMissionResult);
        }

        public void QueryAssetInfo(ROXInterface<NormalAssetsInfo> callback)
        {
            NormalAssetsInfo normalAssetsInfo = generateNormalAssetsInfo();
            callback.OnSuccess(normalAssetsInfo);
        }

        public void ExtremeWithdraw(string taskId, ROXInterface<bool> callback)
        {
            callback.OnSuccess(true);
        }

        public void Withdraw(string taskId, String realName, String cardId, String phoneNumber, ROXInterface<bool> callback)
        {
            callback.OnSuccess(true);
        }

        public void Transform(String exchangeId, double amounts, ROXInterface<NormalTransformResult> callback)
        {
            NormalTransformResult normalTransformResult = generateNormalTransformResult();
            callback.OnSuccess(normalTransformResult);
        }

        public void Transform(String exchangeId, ROXInterface<NormalTransformResult> callback)
        {
            NormalTransformResult normalTransformResult = generateNormalTransformResult();
            callback.OnSuccess(normalTransformResult);
        }

        public void QueryProgress(List<string> tasks, ROXInterface<NormalMissionsProgress> callback)
        {
            NormalMissionsProgress normalMissionsProgress = generateNormalMissionsProgress();
            callback.OnSuccess(normalMissionsProgress);
        }

        public void QueryAllProgress(ROXInterface<NormalMissionsProgress> callback)
        {
            NormalMissionsProgress normalMissionsProgress = generateNormalMissionsProgress();
            callback.OnSuccess(normalMissionsProgress);
        }


        public void DoCustomRulesMission(string taskId, string tid, ROXInterface<NormalMissionResult> callback)
        {
            NormalMissionResult normalMissionResult = generateNormalMissionResult();
            callback.OnSuccess(normalMissionResult);
        }

        public void DoCustomRulesMission(string taskId, ROXInterface<NormalMissionResult> callback)
        {
            NormalMissionResult normalMissionResult = generateNormalMissionResult();
            callback.OnSuccess(normalMissionResult);
        }

        public void ExtremeWithdrawNew(string taskId, ROXInterface<List<NormalAssetStock>> callback)
        {
            NormalAssetStock normalAssetStock = generateNormalAssetStock("金币", 100);
            List<NormalAssetStock> list = new List<NormalAssetStock>();
            list.Add(normalAssetStock);
            callback.OnSuccess(list);
        }

        public void WithdrawNew(string taskId, String realName, String cardId, String phoneNumber, ROXInterface<List<NormalAssetStock>> callback)
        {
            NormalAssetStock normalAssetStock = generateNormalAssetStock("金币", 100);
            List<NormalAssetStock> list = new List<NormalAssetStock>();
            list.Add(normalAssetStock);
            callback.OnSuccess(list);
        }

        public void GeneralWithdraw(string taskId, Hashtable withdrawParam, ROXInterface<List<NormalAssetStock>> callback)
        {
            NormalAssetStock normalAssetStock = generateNormalAssetStock("金币", 100);
            List<NormalAssetStock> list = new List<NormalAssetStock>();
            list.Add(normalAssetStock);
            callback.OnSuccess(list);
        }

        public void GlobalWithdraw(string taskId, GlobalWithdrawInfo info, ROXInterface<List<NormalAssetStock>> callback)
        {
            NormalAssetStock normalAssetStock = generateNormalAssetStock("金币", 100);
            List<NormalAssetStock> list = new List<NormalAssetStock>();
            list.Add(normalAssetStock);
            callback.OnSuccess(list);
        }

        #endregion

        private NormalStrategyConfig generateNormalStrategyConfig()
        {
            StrategyAsset asset1 = new StrategyAsset();
            asset1.AssetName = "金币";
            asset1.ExchangeRate = 1000;

            StrategyAsset asset2 = new StrategyAsset();
            asset2.AssetName = "现金";
            asset2.ExchangeRate = 1.0;

            StrategyAsset asset3 = new StrategyAsset();
            asset3.AssetName = "碎片";
            asset3.ExchangeRate = 0.5;

            List<StrategyAsset> list = new List<StrategyAsset>();
            list.Add(asset1);
            list.Add(asset2);
            list.Add(asset3);

            StrategyExchangeTask exchangeTask = new StrategyExchangeTask();
            exchangeTask.ExchangeId = "cointocash";
            exchangeTask.OriginAssetName = "金币";
            exchangeTask.OriginAssetAmount = 1000;
            exchangeTask.ExchangeAssetName = "现金";
            exchangeTask.ExchangeAssetAmount = 1.0;

            StrategyExchangeTask exchangeTask2 = new StrategyExchangeTask();
            exchangeTask2.ExchangeId = "fragtocash";
            exchangeTask2.OriginAssetName = "碎片";
            exchangeTask2.OriginAssetAmount = 1.0;
            exchangeTask2.ExchangeAssetName = "现金";
            exchangeTask2.ExchangeAssetAmount = 2.0;

            List<StrategyExchangeTask> exchangeTaskList = new List<StrategyExchangeTask>();
            exchangeTaskList.Add(exchangeTask);
            exchangeTaskList.Add(exchangeTask2);

            StrategyMissionTask strategyMissionTask = new StrategyMissionTask();
            strategyMissionTask.Id = "dailycash";
            strategyMissionTask.Name = "每日现金";
            strategyMissionTask.AssetName = "现金";
            strategyMissionTask.Frequency = 999;
            strategyMissionTask.FrequencyType = 1;
            strategyMissionTask.PrizeAmount = 1.0;
            strategyMissionTask.PrizeType = 2;

            StrategyMissionTask strategyMissionTask2 = new StrategyMissionTask();
            strategyMissionTask2.Id = "dailyfrag";
            strategyMissionTask2.Name = "每日碎片";
            strategyMissionTask2.AssetName = "碎片";
            strategyMissionTask2.Frequency = 999;
            strategyMissionTask2.FrequencyType = 1;
            strategyMissionTask2.PrizeAmount = 0.4;
            strategyMissionTask2.PrizeType = 1;


            StrategyMissionTask strategyMissionTask3 = new StrategyMissionTask();
            strategyMissionTask3.Id = "dailycoin";
            strategyMissionTask3.Name = "每日金币";
            strategyMissionTask3.AssetName = "金币";
            strategyMissionTask3.Frequency = 999;
            strategyMissionTask3.FrequencyType = 1;
            strategyMissionTask3.PrizeAmount = 500;
            strategyMissionTask3.PrizeType = 1;

            StrategyMissionTask strategyMissionTask4 = new StrategyMissionTask();
            strategyMissionTask4.Id = "dailyconsume";
            strategyMissionTask4.Name = "每日消耗";
            strategyMissionTask4.AssetName = "碎片";
            strategyMissionTask4.Frequency = 999;
            strategyMissionTask4.FrequencyType = 1;
            strategyMissionTask4.PrizeAmount = -0.1;
            strategyMissionTask4.PrizeType = 1;

            List<StrategyMissionTask> missionTaskList = new List<StrategyMissionTask>();
            missionTaskList.Add(strategyMissionTask);
            missionTaskList.Add(strategyMissionTask2);
            missionTaskList.Add(strategyMissionTask3);
            missionTaskList.Add(strategyMissionTask4);

            NormalMissionInfo normalMissionInfo = new NormalMissionInfo();
            normalMissionInfo.StrategyAssetList = list;
            normalMissionInfo.StrategyExchangeTaskList = exchangeTaskList;
            normalMissionInfo.StrategyMissionTaskList = missionTaskList;

            NormalStrategyWithdrawTask normalStrategyWithdrawTask = new NormalStrategyWithdrawTask();
            normalStrategyWithdrawTask.Id = "withdraw3";
            normalStrategyWithdrawTask.Name = "";
            normalStrategyWithdrawTask.RewardAmount = 3.0;
            normalStrategyWithdrawTask.FrequencyType = 1;
            normalStrategyWithdrawTask.IsExtreme = false;
            normalStrategyWithdrawTask.CostAssetsAmount = 3.0;
            normalStrategyWithdrawTask.Frequency = 999;
            normalStrategyWithdrawTask.AssetName = "现金";

            NormalStrategyWithdrawTask normalStrategyWithdrawTask2 = new NormalStrategyWithdrawTask();
            normalStrategyWithdrawTask2.Id = "withdraw03";
            normalStrategyWithdrawTask2.Name = "";
            normalStrategyWithdrawTask2.RewardAmount = 0.3;
            normalStrategyWithdrawTask2.FrequencyType = 1;
            normalStrategyWithdrawTask2.IsExtreme = true;
            normalStrategyWithdrawTask2.CostAssetsAmount = 0.3;
            normalStrategyWithdrawTask2.Frequency = 999;
            normalStrategyWithdrawTask2.AssetName = "现金";

            List<NormalStrategyWithdrawTask> withdrawTaskList = new List<NormalStrategyWithdrawTask>();
            withdrawTaskList.Add(normalStrategyWithdrawTask);
            withdrawTaskList.Add(normalStrategyWithdrawTask2);

            NormalStrategyConfig config = new NormalStrategyConfig();
            config.MissionTaskInfo = normalMissionInfo;
            config.NormalWithdrawTaskList = withdrawTaskList;

            return config;
        }

        private NormalMissionResult generateNormalMissionResult()
        {
            NormalMissionResult normalMissionResult = new NormalMissionResult();
            normalMissionResult.AssetName = "金币";
            normalMissionResult.PrizeDelta = 500;
            mTotal = mTotal + 500;
            normalMissionResult.PrizeTotal = mTotal;

            NormalAssetStock normalAssetStock1 = generateNormalAssetStock("金币", mTotal);
            NormalAssetStock normalAssetStock2 = generateNormalAssetStock("碎片", mTotal);
            NormalAssetStock normalAssetStock3 = generateNormalAssetStock("现金", mTotal);

            List<NormalAssetStock> assetList = new List<NormalAssetStock>();
            assetList.Add(normalAssetStock1);
            assetList.Add(normalAssetStock2);
            assetList.Add(normalAssetStock3);

            normalMissionResult.AssetList = assetList;

            return normalMissionResult;

        }

        private NormalAssetStock generateNormalAssetStock(string name, double amount)
        {
            NormalAssetStock normalAssetStock = new NormalAssetStock();
            normalAssetStock.AssetName = name;
            normalAssetStock.AssetAmount = amount;
            return normalAssetStock;
        }

        private NormalAssetsInfo generateNormalAssetsInfo()
        {
            NormalAssetsInfo normalAssetsInfo = new NormalAssetsInfo();
            normalAssetsInfo.Uid = "r_07aebf7f06dc559d";
            normalAssetsInfo.StrategyId = 159;


            NormalAssetStock normalAssetStock1 = generateNormalAssetStock("金币", mTotal);
            NormalAssetStock normalAssetStock2 = generateNormalAssetStock("碎片", mTotal);
            NormalAssetStock normalAssetStock3 = generateNormalAssetStock("现金", mTotal);

            List<NormalAssetStock> assetList = new List<NormalAssetStock>();
            assetList.Add(normalAssetStock1);
            assetList.Add(normalAssetStock2);
            assetList.Add(normalAssetStock3);
            normalAssetsInfo.AssetStockList = assetList;

            StrategyWithdrawRecord strategyWithdrawRecord = generateStrategyWithdrawRecord();
            List<StrategyWithdrawRecord> withdrawList = new List<StrategyWithdrawRecord>();
            withdrawList.Add(strategyWithdrawRecord);

            normalAssetsInfo.Records = withdrawList;

            return normalAssetsInfo;
        }

        private StrategyWithdrawRecord generateStrategyWithdrawRecord()
        {
            StrategyWithdrawRecord strategyWithdrawRecord = new StrategyWithdrawRecord();
            strategyWithdrawRecord.Id = "6324367395017152143";
            strategyWithdrawRecord.WithdrawTaskId = "coinwithdraw5";
            strategyWithdrawRecord.UserId = "87e746781d621b03";
            strategyWithdrawRecord.PayRemark = "我不是消星星微信提现";
            strategyWithdrawRecord.CashAmount = 5.0;
            strategyWithdrawRecord.RequestDay = "2021-04-23";
            strategyWithdrawRecord.WithdrawChannel = "yzh_wechat";
            strategyWithdrawRecord.CashAmount = 100;

            return strategyWithdrawRecord;
        }

        private NormalTransformResult generateNormalTransformResult()
        {
            NormalTransformResult normalTransformResult = new NormalTransformResult();

            TransformAssetInfo origin = new TransformAssetInfo();

            origin.AssetName = "金币";
            origin.PrizeDelta = -1000;
            mTotal = mTotal - 1000;
            origin.PrizeTotal = mTotal;

            TransformAssetInfo dest = new TransformAssetInfo();

            dest.AssetName = "现金";
            dest.PrizeDelta = 1;
            mCashTotal++;
            dest.PrizeTotal = mCashTotal;

            normalTransformResult.OriginAsset = origin;
            normalTransformResult.DestinationAsset = dest;

            NormalAssetStock normalAssetStock1 = generateNormalAssetStock("金币", mTotal);
            NormalAssetStock normalAssetStock2 = generateNormalAssetStock("碎片", mTotal);
            NormalAssetStock normalAssetStock3 = generateNormalAssetStock("现金", mTotal);

            List<NormalAssetStock> assetList = new List<NormalAssetStock>();
            assetList.Add(normalAssetStock1);
            assetList.Add(normalAssetStock2);
            assetList.Add(normalAssetStock3);
            normalTransformResult.AssetList = assetList;

            return normalTransformResult;
        }

        private NormalMissionsProgress generateNormalMissionsProgress()
        {
            NormalMissionsProgress normalMissionsProgress = new NormalMissionsProgress();
            normalMissionsProgress.Uid = "r_07aebf7f06dc559d";

            MissionProgress progress = new MissionProgress();
            progress.Id = "dailycoin";
            progress.Name = "每日金币";
            progress.Frequency = 999;
            progress.FrequencyType = 1;
            progress.DoneTimes = 1;
            progress.TimeStap = 1619172488000;

            MissionProgress progress2 = new MissionProgress();
            progress2.Id = "dailyconsume2";
            progress2.Name = "每日消耗2";
            progress2.Frequency = 999;
            progress2.FrequencyType = 1;
            progress2.DoneTimes = 0;
            progress2.TimeStap = 0;

            List<MissionProgress> list = new List<MissionProgress>();
            list.Add(progress);
            list.Add(progress2);

            normalMissionsProgress.ProgressList = list;

            return normalMissionsProgress;
        }
    }

}