using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXStrategy.Api
{ 
    public class PiggyBank
    {   

        /// <summary>
        /// 储蓄罐Id
        /// <summary>
        public int PiggyBankId{set; get;}

        /// <summary>
        /// 当前应用Id
        /// <summary>
        public string AppId{set; get; }

        /// <summary>
        /// 储蓄罐名称
        /// <summary>
        public string PiggyBankName{set; get; }

        /// <summary>
        /// 用户Id
        /// <summary>
        public string UserId{set; get; }

        /// <summary>
        /// 提取资产的名称
        /// <summary>
        public string ToAssetName{set; get; }

        /// <summary>
        /// 储蓄罐值，源资产值。和to_prize_amount 一起决定储蓄罐值和目的资产的提取比率。
        /// <summary>
        public int SrcPrizeAmount{set; get; }

        /// <summary>
        /// 储蓄罐值，源资产值。和to_prize_amount 一起决定储蓄罐值和目的资产的提取比率。
        /// <summary>
        public int ToPrizeAmount{set; get; }

        /// <summary>
        /// 资产值, 最多支持小数点2位
        /// <summary>
        public double PrizeAmount{set; get; }

        /// <summary>
        /// 用户上次做任务的时间(unix时间戳). 如果没做做去过任务，则为0
        /// <summary>
        public long UpdateTimeMS{set; get; }

        /// <summary>
        /// 最近提取时间. 如果没有提取过，则为0
        /// <summary>
        public long LastWithdrawTimeMS{set; get; }

        public string ToString() 
        {
            string result = " {" 
            + " PiggyBankId = " + PiggyBankId + " ," 
            + " AppId = " + AppId + " ," 
            + " PiggyBankName = " + PiggyBankName + " ," 
            + " UserId = " + UserId + " ," 
            + " ToAssetName = " + ToAssetName + " ," 
            + " SrcPrizeAmount = " + SrcPrizeAmount + " ," 
            + " ToPrizeAmount = " + ToPrizeAmount + " ," 
            + " PrizeAmount = " + PrizeAmount + " ," 
            + " UpdateTimeMS = " + UpdateTimeMS + " ," 
            + " LastWithdrawTimeMS = " + LastWithdrawTimeMS 
            +  " }"; 

            return result;
        }  
        
    }
}