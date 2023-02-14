using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXStrategy.Api
{ 
    public class NormalStrategyConfig
    {       
        /// <summary>
        /// 策略Id
        /// <summary>
        public int StrategyId{set; get;}

        /// <summary>
        /// 策略类型
        /// 1: 通用策略
        /// 2: 阶梯策略
        /// <summary>
        public int StrategyType{set; get; }

        /// <summary>
        /// 策略名称
        /// <summary>
        public string StrategyName{set; get; }

        /// <summary>
        /// 开始时间
        /// <summary>
        public string StartTime{set; get; }

        /// <summary>
        /// 结束时间
        /// <summary>
        public string EndTime{set; get; }

        /// <summary>
        /// 应用在 Harbor 平台的 AppId
        /// <summary>
        public string AppId{set; get; }

        /// <summary>
        /// 支付说明，客户端展示用
        /// <summary>
        public string PayRemark{set; get; }

        /// <summary>
        /// Ab分组名称
        /// <summary>
        public string AbGroup{set; get; }

        /// <summary>
        /// Ab分组Id
        /// <summary>
        public int AbId{set; get; }
        /// <summary>
        /// 当前策略对应的任务信息
        /// <summary>
        public NormalMissionInfo MissionTaskInfo{set; get;}

        /// <summary>
        /// 当前策略对应的提现任务列表
        /// <summary>
        public List<NormalStrategyWithdrawTask> NormalWithdrawTaskList{set; get; }

        public string ToString() 
        {
            string result = " {" 
            +  " StrategyId = " + StrategyId + " ," 
            +  " StrategyType = " + StrategyType + " ," 
            +  " StrategyName = " + StrategyName + " ," 
            +  " StartTime = " + StartTime + " ," 
            +  " EndTime = " + EndTime + " ," 
            +  " AppId = " + AppId + " ," 
            +  " PayRemark = " + PayRemark + " ," 
            +  " AbGroup = " + AbGroup + " ," 
            +  " AbId = " + AbId + " ," 
            + MissionTaskInfo.ToString() +  " ,";           

            foreach(NormalStrategyWithdrawTask normalStrategyWithdrawTask in NormalWithdrawTaskList) 
            {
                result = result + normalStrategyWithdrawTask.ToString() + " ,";
            }

            result = result.Substring(0, result.Length -1);

            result = result + " }"; 

            return result;
        }  
    }
}