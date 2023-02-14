using System.Collections;
using UnityEngine;

namespace ROXSect.Api 
{
    public class ApprenticeInfo 
    {
        /// <summary>
        /// 当前弟子Id
        /// <summary>    
        public string StudentUid {set; get;}

        /// <summary>
        /// 当前弟子的师傅Id
        /// <summary>  
        public string TeacherUid {set; get;}

        /// <summary>
        /// 当前弟子是否已经认证
        /// <summary>  
        public bool HasVerified {set; get;}

        /// <summary>
        /// 当前未领取贡献值
        /// <summary>  
        public int UnclaimedReward {set; get;}

        /// <summary>
        /// 已获得的总贡献值
        /// <summary>  
        public int TotalReward {set; get;}

        /// <summary>
        /// 该弟子在当前宗门的层级
        /// <summary>  
        public int CurrentLevel {set; get;}

        /// <summary>
        /// 当前弟子拥有的弟子总数
        /// <summary>  
        public int TotalStudents {set; get;}

        /// <summary>
        /// 最近两天的贡献值记录
        /// 其中key为日期，样式：2020-08-10, value为对应的贡献值
        /// <summary>  
        public Hashtable DailyRewardMap {set; get;}   

        /// <summary>
        /// 当前弟子昵称
        /// <summary>  
        public string NickName {set; get;}

        /// <summary>
        /// 当前弟子头像
        /// <summary>  
        public string Avatar {set; get;}

         public void ToString() 
         {
            Debug.Log("StudentUid: " + StudentUid);
            Debug.Log("TeacherUid: " + TeacherUid);
            Debug.Log("HasVerified: " + HasVerified); 
            Debug.Log("UnclaimedReward: " + UnclaimedReward);
            Debug.Log("TotalReward: " + TotalReward);
            Debug.Log("CurrentLevel: " + CurrentLevel); 
            Debug.Log("TotalStudents: " + TotalStudents);
            if (DailyRewardMap != null) 
            {
                foreach(string key in DailyRewardMap.Keys) 
                {
                   Debug.Log("daily is: " + key); 
                   Debug.Log("reward is: " + DailyRewardMap[key]);                   
                }
            }
         }    
    }
}