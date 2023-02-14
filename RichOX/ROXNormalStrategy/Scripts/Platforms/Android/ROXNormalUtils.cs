using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROXStrategy.Api;
using System;
using System.Runtime.InteropServices;

namespace ROXStrategy.Platforms.Android
{
    public static class ROXNormalUtils
    {
        public static NormalStrategyConfig GenerateNormalStrategyConfig(AndroidJavaObject normalConfig) 
        {
            NormalStrategyConfig config = new NormalStrategyConfig();

            NormalMissionInfo missionInfo = new NormalMissionInfo(); 

            AndroidJavaObject missionTaskInfo = normalConfig.Call<AndroidJavaObject>("getMissionTaskInfo");

            config.StrategyId = normalConfig.Call<int>("getStrategyId");
            config.StrategyType = normalConfig.Call<int>("getStrategyType");
            config.StrategyName = normalConfig.Call<string>("getName");
            config.StartTime = normalConfig.Call<string>("getBeginTime");
            config.EndTime = normalConfig.Call<string>("getEndTime");
            config.AppId = normalConfig.Call<string>("getAppId");
            config.AbId = normalConfig.Call<int>("getAbId");
            config.AbGroup = normalConfig.Call<string>("getAbGroup");

            AndroidJavaObject assetList = missionTaskInfo.Call<AndroidJavaObject>("getStrategyAssetList");
            if (assetList != null) {
                List<StrategyAsset> unityAssetList = new List<StrategyAsset>();
                int size = assetList.Call<int>("size");
                for (int i=0; i<size; i++) {
                    AndroidJavaObject assetObject = assetList.Call<AndroidJavaObject>("get", i);
                    StrategyAsset asset = new StrategyAsset();
                    asset.AssetName = assetObject.Call<string>("getAssetName");
                    asset.ExchangeRate = assetObject.Call<double>("getExchangeRate");
                    unityAssetList.Add(asset);
                }
                missionInfo.StrategyAssetList = unityAssetList;
            }       

            AndroidJavaObject exchangeTaskList = missionTaskInfo.Call<AndroidJavaObject>("getStrategyExchangeTaskList");
            if (exchangeTaskList != null) {
                List<StrategyExchangeTask> unityExchangeTaskList = new List<StrategyExchangeTask>();
                int size = exchangeTaskList.Call<int>("size");
                for (int i=0; i<size; i++) {
                    AndroidJavaObject exchangeTaskObject = exchangeTaskList.Call<AndroidJavaObject>("get", i);
                    StrategyExchangeTask exchangeTask = new StrategyExchangeTask();
                    exchangeTask.ExchangeId = exchangeTaskObject.Call<string>("getExchangeId");
                    exchangeTask.OriginAssetName = exchangeTaskObject.Call<string>("getOriginAssetName");
                    exchangeTask.OriginAssetAmount = exchangeTaskObject.Call<double>("getOriginAssetAmount");
                    exchangeTask.ExchangeAssetName = exchangeTaskObject.Call<string>("getExchangeAssetName");
                    exchangeTask.ExchangeAssetAmount = exchangeTaskObject.Call<double>("getExchangeAssetAmount");
                    unityExchangeTaskList.Add(exchangeTask);
                }
                missionInfo.StrategyExchangeTaskList = unityExchangeTaskList;
            } 

            AndroidJavaObject missionTaskList = missionTaskInfo.Call<AndroidJavaObject>("getMissionTaskList");
            if (missionTaskList != null) {
                List<StrategyMissionTask> unityMissionTaskList = new List<StrategyMissionTask>();
                int size = missionTaskList.Call<int>("size");
                for (int i=0; i<size; i++) {
                    AndroidJavaObject missionTaskObject = missionTaskList.Call<AndroidJavaObject>("get", i);
                    StrategyMissionTask missionTask = new StrategyMissionTask();
                    missionTask.Id = missionTaskObject.Call<string>("getId");
                    missionTask.Name = missionTaskObject.Call<string>("getName");
                    missionTask.AssetName = missionTaskObject.Call<string>("getAssetName");
                    missionTask.Frequency = missionTaskObject.Call<int>("getFrequency");
                    missionTask.FrequencyType = missionTaskObject.Call<int>("getFrequencyType");
                    missionTask.PrizeAmount = missionTaskObject.Call<double>("getPrizeAmount");
                    missionTask.PrizeType = missionTaskObject.Call<int>("getPrizeType");                   
                    unityMissionTaskList.Add(missionTask);
                }
                missionInfo.StrategyMissionTaskList = unityMissionTaskList;
            } 

            config.MissionTaskInfo = missionInfo;


            AndroidJavaObject withdrawTaskList = normalConfig.Call<AndroidJavaObject>("getWithdrawTaskList");
            if (withdrawTaskList != null) {
                List<NormalStrategyWithdrawTask> unityWithdrawTaskList = new List<NormalStrategyWithdrawTask>();
                int size = withdrawTaskList.Call<int>("size");
                for (int i=0; i<size; i++) {
                    AndroidJavaObject withdrawTaskObject = withdrawTaskList.Call<AndroidJavaObject>("get", i);
                    NormalStrategyWithdrawTask withdrawTask = new NormalStrategyWithdrawTask();
                    withdrawTask.Id = withdrawTaskObject.Call<string>("getId");
                    withdrawTask.Name = withdrawTaskObject.Call<string>("getName");
                    withdrawTask.RewardAmount = withdrawTaskObject.Call<double>("getRewardAmount");
                    withdrawTask.FrequencyType = withdrawTaskObject.Call<int>("getFrequencyType");
                    withdrawTask.IsExtreme = withdrawTaskObject.Call<bool>("isExtreme");

                    withdrawTask.CostAssetsAmount = withdrawTaskObject.Call<double>("getCostAssets");
                    withdrawTask.Frequency = withdrawTaskObject.Call<int>("getFrequency");
                    withdrawTask.AssetName = withdrawTaskObject.Call<string>("getAssetName");
                    withdrawTask.WithdrawType = withdrawTaskObject.Call<string>("getWithdrawType");
                    withdrawTask.MinCash = withdrawTaskObject.Call<double>("getMinCash");
                    withdrawTask.MaxCash = withdrawTaskObject.Call<double>("getMaxCash");          
                    unityWithdrawTaskList.Add(withdrawTask);
                }
                config.NormalWithdrawTaskList = unityWithdrawTaskList;
            } 
         
            return config;
        }

        public static NormalMissionResult generateMissionResult(AndroidJavaObject androidObject) {
            NormalMissionResult missionResult = new NormalMissionResult();
            string assetName = androidObject.Call<string>("getAssetName");
            double prizeDelta = androidObject.Call<double>("getDeltaAmount");
            double prizeTotal = androidObject.Call<double>("getTotalAmount");

            missionResult.AssetName = assetName;
            missionResult.PrizeDelta = prizeDelta;
            missionResult.PrizeTotal = prizeTotal;

            AndroidJavaObject assetListObject = androidObject.Call<AndroidJavaObject>("getAssetList");
            if (assetListObject != null) {
                List<NormalAssetStock> unityAssetList = new List<NormalAssetStock>();
                int size = assetListObject.Call<int>("size");
                for (int i=0; i<size; i++) {
                    AndroidJavaObject assetObject = assetListObject.Call<AndroidJavaObject>("get", i);
                    NormalAssetStock stock = new NormalAssetStock();
                    stock.AssetName = assetObject.Call<string>("getAssetName");
                    stock.AssetAmount = assetObject.Call<double>("getAssetAmount");
                    unityAssetList.Add(stock);
                }
                missionResult.AssetList = unityAssetList;
            }
            return missionResult;
        }

        public static NormalAssetsInfo generateAssetInfo(AndroidJavaObject androidObject) 
        {
            NormalAssetsInfo assetInfo = new NormalAssetsInfo();
            string uid = androidObject.Call<string>("getUid");
            int strategyId = androidObject.Call<int>("getStrategyId");

            assetInfo.Uid = uid;
            assetInfo.StrategyId = strategyId;

            AndroidJavaObject assetListObject = androidObject.Call<AndroidJavaObject>("getAssetStockList");
            if (assetListObject != null) {
                List<NormalAssetStock> unityAssetList = new List<NormalAssetStock>();
                int size = assetListObject.Call<int>("size");
                for (int i=0; i<size; i++) {
                    AndroidJavaObject assetObject = assetListObject.Call<AndroidJavaObject>("get", i);
                    NormalAssetStock stock = new NormalAssetStock();
                    stock.AssetName = assetObject.Call<string>("getAssetName");
                    stock.AssetAmount = assetObject.Call<double>("getAssetAmount");
                    unityAssetList.Add(stock);
                }
                assetInfo.AssetStockList = unityAssetList;
            }

            AndroidJavaObject recordListObject = androidObject.Call<AndroidJavaObject>("getWithdrawRecords");
            if (recordListObject != null) {
                List<StrategyWithdrawRecord> unityRecordList = new List<StrategyWithdrawRecord>();
                int size = recordListObject.Call<int>("size");
                for (int i=0; i<size; i++) {
                    AndroidJavaObject recordObject = recordListObject.Call<AndroidJavaObject>("get", i);
                    StrategyWithdrawRecord record = new StrategyWithdrawRecord();
                    record.Id = recordObject.Call<string>("getId");
                    record.WithdrawTaskId = recordObject.Call<string>("getWithdrawTaskId");        
                    record.UserId = recordObject.Call<string>("getUserId");
                    record.PayRemark = recordObject.Call<string>("getPayRemark");
                    record.CashAmount = recordObject.Call<double>("getCashAmount");
                    record.RequestDay = recordObject.Call<string>("getRequestDay");
                    record.WithdrawChannel = recordObject.Call<string>("getWithdrawChannel");
                    record.Status = recordObject.Call<int>("getStatus");
                    unityRecordList.Add(record);
                }
                assetInfo.Records = unityRecordList;
            }
            return assetInfo;
        }

        public static NormalTransformResult generateTransformResult(AndroidJavaObject androidObject) 
        {
            NormalTransformResult transformResult = new NormalTransformResult();

            AndroidJavaObject originObject = androidObject.Call<AndroidJavaObject>("getOriginAsset");
            if (originObject != null) {
                TransformAssetInfo originInfo = new TransformAssetInfo();
                originInfo.AssetName = originObject.Call<string>("getAssetName");
                originInfo.PrizeDelta = originObject.Call<double>("getPrizeDelta");
                originInfo.PrizeTotal = originObject.Call<double>("getPrizeTotal");
                transformResult.OriginAsset = originInfo;
            }

            AndroidJavaObject destObject = androidObject.Call<AndroidJavaObject>("getDestinationAsset");
            if (destObject != null) {
                TransformAssetInfo destInfo = new TransformAssetInfo();
                destInfo.AssetName = destObject.Call<string>("getAssetName");
                destInfo.PrizeDelta = destObject.Call<double>("getPrizeDelta");
                destInfo.PrizeTotal = destObject.Call<double>("getPrizeTotal");
                transformResult.DestinationAsset = destInfo;
            }

            AndroidJavaObject assetListObject = androidObject.Call<AndroidJavaObject>("getAssetList");
            if (assetListObject != null) {
                List<NormalAssetStock> unityAssetList = new List<NormalAssetStock>();
                int size = assetListObject.Call<int>("size");
                for (int i=0; i<size; i++) {
                    AndroidJavaObject assetObject = assetListObject.Call<AndroidJavaObject>("get", i);
                    NormalAssetStock stock = new NormalAssetStock();
                    stock.AssetName = assetObject.Call<string>("getAssetName");
                    stock.AssetAmount = assetObject.Call<double>("getAssetAmount");
                    unityAssetList.Add(stock);
                }
                transformResult.AssetList = unityAssetList;
            }
            return transformResult;            
        }

        public static NormalMissionsProgress generateNormalMissionsProgress(AndroidJavaObject androidObject) 
        {
            NormalMissionsProgress normalMissionsProgress = new NormalMissionsProgress();
            normalMissionsProgress.Uid = androidObject.Call<string>("getUid");
            AndroidJavaObject missionListObject = androidObject.Call<AndroidJavaObject>("getMissionList");
            if (missionListObject != null) {
                int size = missionListObject.Call<int>("size");
                List<MissionProgress> missionList = new List<MissionProgress>();                
                for (int index = 0 ; index < size; index ++ ) 
                {
                    AndroidJavaObject mission = missionListObject.Call<AndroidJavaObject>("get", index);
                    MissionProgress missionProgress = new MissionProgress();
                    missionProgress.Id = mission.Call<string>("getId");
                    missionProgress.Name = mission.Call<string>("getName");
                    missionProgress.Frequency = mission.Call<int>("getFrequency");
                    missionProgress.FrequencyType = mission.Call<int>("getFrequencyType");
                    missionProgress.DoneTimes = mission.Call<int>("getTimesDone");
                    missionProgress.TimeStap = mission.Call<long>("getTimeStamp");
                    missionList.Add(missionProgress);
                }
                normalMissionsProgress.ProgressList = missionList;
            }
            return normalMissionsProgress;
        }

        public static List<NormalAssetStock> generateNormalAssetStockList(AndroidJavaObject androidObject) 
        {
            List<NormalAssetStock> unityAssetList = new List<NormalAssetStock>();
            int size = androidObject.Call<int>("size");
            for (int i=0; i<size; i++) {
                AndroidJavaObject assetObject = androidObject.Call<AndroidJavaObject>("get", i);
                NormalAssetStock stock = new NormalAssetStock();
                stock.AssetName = assetObject.Call<string>("getAssetName");
                stock.AssetAmount = assetObject.Call<double>("getAssetAmount");
                unityAssetList.Add(stock);
            }
            return unityAssetList;
        }

    }    
}