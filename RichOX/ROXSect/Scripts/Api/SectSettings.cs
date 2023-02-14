using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXSect.Api 
{
    public class SectSettings 
    {
        /// <summary>
        /// 徒弟层级
        /// <summary>   
        public int Hierarchy  {set; get;}

        /// <summary>
        /// 兑换一次红包消耗的贡献值
        /// <summary>   
        public int TransformContribution  {set; get;}

        /// <summary>
        /// 未领取的最大贡献值
        /// <summary>   
        public int MaxContributionLeft  {set; get;}

        /// <summary>
        /// 宗门各等级对应的邀请人数，下标 0 表示等级 1 ， 以此类推
        /// <summary>   
        public int[] Grades {set; get;}

        /// <summary>
        /// 邀请人数对应奖励信息列表
        /// <summary>   
        public List<InviteAward> AwardSettingsList {set; get;}

        /// <summary>
        /// 不同档位兑换红包消耗的贡献值，key 档位值，value 兑换红包所需消耗的贡献值
        /// <summary>   
        public Hashtable HashSteps {set; get;}

        /// <summary>
        /// 兑换次数红包对应奖励，key：领取该红包需要的兑换次数，value: 各宗门等级对应的奖励金额
        /// <summary>   
        public Hashtable TransformTimesMap {set; get;}

        public void ToString() 
        {
            Debug.Log("Hierarchy : " + Hierarchy);
            Debug.Log("TransformContribution : " + TransformContribution);
            Debug.Log("MaxContributionLeft : " + MaxContributionLeft);

            if (Grades != null) 
            {
                string grades = "";
                foreach(int grade in grades)
                {
                    grades = grades + grade + " ";
                }
                Debug.Log("Grades : " + grades);
            }

            if (AwardSettingsList != null) 
            {
                Debug.Log("invite config is : ");
                foreach(InviteAward inviteAward in AwardSettingsList)
                {
                    inviteAward.ToString();
                }
            }

            if (HashSteps != null) 
            {
                Debug.Log("step is :");
                foreach(int stepKey in HashSteps.Keys)
                {
                    Debug.Log("level is: " + stepKey);
                    Debug.Log("consume is: " + HashSteps[stepKey]);
                }
            }
           
            if (TransformTimesMap != null) 
            {
                foreach(int timesKey in TransformTimesMap.Keys)
                {
                    Debug.Log("times: " + timesKey);
                    double[] rewardlist = (double[]) TransformTimesMap[timesKey];
                    string rewards = "";
                    foreach(double reward in rewardlist)
                    {
                        rewards = rewards + reward + " ";
                    }
                    Debug.Log("rewards : " + rewards);
                }
            }              
        }
    }
}