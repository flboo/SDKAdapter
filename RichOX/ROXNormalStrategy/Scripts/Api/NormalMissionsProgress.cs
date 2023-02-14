using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXStrategy.Api
{ 
    public class NormalMissionsProgress
    {   

        /// <summary>
        /// 用户Id
        /// <summary>
        public string Uid{set; get;}
        /// <summary>
        /// 任务进度列表
        /// <summary>      
        public List<MissionProgress> ProgressList{set; get; }
        

        public string ToString() {
            string result = " { " 
            + " Uid = " + Uid + " ," ;
            
            foreach(MissionProgress missionProgress in ProgressList) 
            {
                result = result + missionProgress.ToString() + ",";
            }
            result = result.Substring(0, result.Length - 1);

            result = result + " }";
            return result;
        }
    }

    public class MissionProgress 
    {
        /// <summary>
        /// 任务ID
        /// <summary>
        public string Id {set; get;}
        /// <summary>
        /// 任务名称
        /// <summary>
        public string Name {set; get;}
        /// <summary>
        /// 任务频次
        /// <summary>
        public int Frequency {set; get;}
        /// <summary>
        /// 任务限制类型
        /// <summary>
        public int FrequencyType {set; get;}
        /// <summary>
        /// 任务已完成次数
        /// <summary>
        public int DoneTimes {set; get;}
        /// <summary>
        /// 任务上一次更细的时间戳
        /// <summary>
        public long TimeStap {set; get;}

        public string ToString() 
        {
            string result = " {" 
            + " Id = " + Id + " ," 
            + " Name = " + Name + " ," 
            + " Frequency = " + Frequency + " ," 
            + " FrequencyType = " + FrequencyType + " ," 
            + " DoneTimes = " + DoneTimes + " ," 
            + " TimeStap = " + TimeStap + " "
            + "}"; 
            return result;
        }
    }
}