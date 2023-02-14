using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXStrategy.Api
{ 
    public class StrategyMissionTask
    {   
        /// <summary>
        /// 任务Id
        /// <summary>    
        public string Id{set; get;}

        /// <summary>
        /// 任务名称
        /// <summary>
        public string Name{set; get; }

        /// <summary>
        /// 当前任务完成后奖励的资产名称
        /// <summary>
        public string AssetName{set; get;}

        /// <summary>
        /// 任务限制次数
        /// <summary>
        public int Frequency{set; get; }

        /// <summary>
        /// 任务限制类型
        /// 1: 每日限制
        /// 2: 每周限制
        /// 3: 全程限制
        /// 4: 无限制
        /// <summary>
        public int FrequencyType{set; get;}

        /// <summary>
        /// 任务奖励数量
        /// <summary>
        public double PrizeAmount{set; get; }

        /// <summary>
        /// 任务奖励数量类型
        /// 1: 每次奖励固定值
        /// 2: 每次奖励最大值
        /// <summary>
        public int PrizeType{set; get; }

        public string ToString() 
        {
            string result = " {" 
            + " Id = " + Id + " ," 
            + " Name = " + Name + " ," 
            + " AssetName = " + AssetName + " ," 
            + " Frequency = " + Frequency + " ," 
            + " FrequencyType = " + FrequencyType + " ," 
            + " PrizeAmount = " + PrizeAmount + " ," 
            + " PrizeType = " + PrizeType + " " 
            + "}"; 

            return result;
        }  
    }
}