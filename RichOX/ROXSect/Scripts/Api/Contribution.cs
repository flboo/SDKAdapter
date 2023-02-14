using UnityEngine;

namespace ROXSect.Api 
{
    public class Contribution 
    {
        /// <summary>
        /// 领取奖励后，当前总的贡献值
        /// <summary> 
        public int TotalContribution {set; get;}

        /// <summary>
        /// 本次奖励的贡献值
        /// <summary> 
        public int ReceivedContribution {set; get;}

        public void ToString() 
        {
            Debug.Log("TotalContribution: " + TotalContribution);
            Debug.Log("ReceivedContribution: " + ReceivedContribution);            
        }
    }
}