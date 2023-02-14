using UnityEngine;
using System.Collections;

namespace ROXSect.Api 
{
    public class ChiefInfo 
    {
        /// <summary>
        /// 宗主Id
        /// <summary>  
        public string MasterUid {set; get;}

        /// <summary>
        /// 宗主师傅Id
        /// <summary>  
        public string TeacherUid {set; get;}

        /// <summary>
        /// 是否验证过
        /// <summary>  
        public bool HasVerified  {set; get;}

        /// <summary>
        /// 返回当前宗门贡献值
        /// 所有弟子产生的贡献值，领取后会添加到宗门贡献值中
        /// 用户兑换现金后，宗门贡献值会减去消耗的贡献值
        /// <summary>  
        public int CurrentReward {set; get;}

        /// <summary>
        /// 当前宗门等级
        /// <summary>  
        public int TongLevel {set; get;}

        /// <summary>
        /// 当前宗门所有弟子数
        /// <summary>  
        public int TotalStudents {set; get;}

        /// <summary>
        /// 当前宗门验证过的弟子数
        /// <summary>  
        public int TotalVerifiedStudents {set; get;}

        /// <summary>
        /// 贡献值兑换的次数
        /// <summary>  
        public int TransformCounts {set; get;}

        /// <summary>
        /// 异变次数红包领取个数
        /// <summary>  
        public int TransformPacketCounts {set; get;}

        /// <summary>
        /// 邀请奖励领取状态
        /// key值为层级，value 为领取状态
        /// <summary>  
        public Hashtable InviteAwardMap {set; get;}

        public void ToString() 
        {
            Debug.Log("MasterUid: " + MasterUid);
            Debug.Log("TeacherUid: " + TeacherUid);
            Debug.Log("HasVerified: " + HasVerified);
            Debug.Log("CurrentReward: " + CurrentReward);
            Debug.Log("TongLevel: " + TongLevel);
            Debug.Log("TotalStudents: " + TotalStudents);
            Debug.Log("TotalVerifiedStudents: " + TotalVerifiedStudents);
            Debug.Log("TransformCounts: " + TransformCounts);
            Debug.Log("TransformPacketCounts: " + TransformPacketCounts);
            if (InviteAwardMap != null) 
            {
                foreach(string key in InviteAwardMap.Keys) 
                {
                   Debug.Log("level is: " + key); 
                   Debug.Log("students is :");
                   ApprenticeList apprenticeList = (ApprenticeList)InviteAwardMap[key];
                   apprenticeList.ToString();
                }
            }       
        }
    }
}