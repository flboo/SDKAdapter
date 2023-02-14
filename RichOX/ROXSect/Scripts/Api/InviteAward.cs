using UnityEngine;

namespace ROXSect.Api 
{
    public class InviteAward 
    {
        /// <summary>
        /// 当前层级对应的邀请人数, “1” 表示邀请1人，“2” 表示邀请2人
        /// <summary>         
        public string Level {set; get;}

        /// <summary>
        /// 奖励类型
        /// 1：现金
        /// 0: 贡献值
        /// <summary>    
        public int AwardType {set; get;}

        /// <summary>
        /// 奖励的数量
        /// <summary>     
        public double AwardAmount {set; get;}

        public void ToString() 
        {
            Debug.Log("Level: " + Level);
            Debug.Log("AwardType: " + AwardType);    
            Debug.Log("AwardAmount: " + AwardAmount.ToString("f2"));         
        }
    }
}