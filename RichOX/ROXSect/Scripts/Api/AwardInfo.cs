using System.Collections;
using UnityEngine;

namespace ROXSect.Api 
{
    public class AwardInfo 
    {
        /// <summary>
        /// 奖励类型
        /// 1：现金
        /// 0: 贡献值
        /// <summary>  
        public int AwardType {set; get;}

        /// <summary>
        /// 领取奖励后，当前总的贡献值
        /// <summary>  
        public int TotalContribution {set; get;}

        /// <summary>
        /// 本次奖励的贡献值
        /// <summary>  
        public int ReceivedAccount {set; get;}

        /// <summary>
        /// 现金的账户余额
        /// <summary>  
        public double TotalCash {set; get;}

        public void ToString() 
         {
            Debug.Log("AwardType: " + AwardType);
            Debug.Log("TotalContribution: " + TotalContribution);
            Debug.Log("ReceivedAccount: " + ReceivedAccount); 
            Debug.Log("TotalCash: " + TotalCash); 
         }
    }
}