using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXStrategy.Api
{ 
    public class NormalMissionInfo
    {   
        /// <summary>
        /// 所有资产兑换现金汇率列表
        /// <summary>    
        public List<StrategyAsset> StrategyAssetList {set; get;}

        /// <summary>
        /// 所有兑换任务列表
        /// <summary>
        public List<StrategyExchangeTask> StrategyExchangeTaskList{set; get; }  

        /// <summary>
        /// 奖励发放任务列表
        /// <summary>
        public List<StrategyMissionTask> StrategyMissionTaskList{set; get; }   

        public string ToString() 
        {
            string result = " { " ;           

            foreach(StrategyAsset strategyAsset in StrategyAssetList) 
            {
                result = result + strategyAsset.ToString() + " ,";
            }

            foreach(StrategyExchangeTask strategyExchangeTask in StrategyExchangeTaskList)
            {
                result = result + strategyExchangeTask.ToString() + " ,";
            }

            foreach(StrategyMissionTask strategyMissionTask in StrategyMissionTaskList)
            {
                result = result + strategyMissionTask.ToString() + " ,";
            }

            result = result.Substring(0, result.Length -1);

            result = result + " }"; 

            return result;
        }  


    }
}