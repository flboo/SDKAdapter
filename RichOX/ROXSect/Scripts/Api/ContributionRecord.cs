using System.Collections;
using UnityEngine;

namespace ROXSect.Api 
{
    public class ContributionRecord 
    {
        /// <summary>
        /// 当天总贡献值
        /// <summary> 
        public int Total {set; get;}  

        /// <summary>
        /// 贡献值分布情况，KEY值为等级
        /// <summary> 
        public Hashtable RecordMap {set; get;}  

        public void ToString() 
        {
            Debug.Log("Total: " + Total);
            if (RecordMap != null) 
            {
                foreach(string key in RecordMap.Keys) 
                {
                   Debug.Log("level is: " + key); 
                   Debug.Log("contribution is: " + RecordMap[key]);                   
                }
            }           
        } 
    }
}