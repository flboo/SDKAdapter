using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXStrategy.Api
{ 
    public class NormalStrategyWithdrawTask
    {    
        /// <summary>
        /// 提现任务Id
        /// <summary>
        public string Id{set; get;}

        /// <summary>
        /// 提现任务的名称
        /// <summary>
        public string Name{set; get; }

        /// <summary>
        /// 当前提现任务对应的金额
        /// <summary>
        public double RewardAmount{set; get; }

        /// <summary>
        /// 当前提现任务对应的频率类型
        /// 任务限制类型
        /// 1: 每日限制
        /// 2: 每周限制
        /// 3: 全程限制
        /// 4: 无限制
        /// <summary>
        public int FrequencyType{set; get; }

        /// <summary>
        /// 当前提现任务对应的提现类型
        /// <summary>
        public bool IsExtreme{set; get; }

        /// <summary>
        /// 当前提现任务消耗的资产数量
        /// <summary>      
        public double CostAssetsAmount{set; get;}

        /// <summary>
        /// 当前提现任务对应的频率次数
        /// <summary>   
        public int Frequency{set; get;}

        /// <summary>
        /// 当前提现消耗的资产类型
        /// <summary>
        public string AssetName{set; get;}

        /// <summary>
        /// 提现类型
        /// “fixed” : 固定值，字段 RewardAmount  才有意义
        /// “range” : 区间返回，字段 RewardAmount 返回 0 ， 字段 MinCash 和 MaxCash 才有意义
        /// <summary>
        public string WithdrawType{set; get;}

        /// <summary>
        /// 可提现最小值，仅字段 WithdrawType 为 “range” 有意义
        /// <summary>
        public double MinCash{set; get;}

        /// <summary>
        /// 可提现最大值，仅字段 WithdrawType 为 “range” 有意义
        /// <summary>
        public double MaxCash{set; get;}

        public string ToString() 
        {
            string result = " {" 
            + " Id = " + Id + " ," 
            + " Name = " + Name + " ," 
            + " RewardAmount = " + RewardAmount + " ," 
            + " FrequencyType = " + FrequencyType + " ," 
            + " IsExtreme = " + IsExtreme + " ," 
            + " CostAssetsAmount = " + CostAssetsAmount + " ," 
            + " Frequency = " + Frequency + " ," 
            + " AssetName = " + AssetName + " ," 
            + " WithdrawType = " + WithdrawType + " ," 
            + " MinCash = " + MinCash + " ," 
            + " MaxCash = " + MaxCash + " " 
            + "}"; 

            return result;
        }

    }
}