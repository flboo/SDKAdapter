using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXToolbox.Api
{ 
    public class PrivacyInfo
    {   
        /// <summary>
        /// 数据存储键值
        /// <summary>
        public string PrivacyKey{set; get; }

        /// <summary>
        /// 数据存储内容
        /// <summary>
        public string PrivacyValue{set; get;}

        /// <summary>
        /// 当前存储数据创建时间，单位:毫秒
        /// <summary>
        public long CreateTime{set; get; }

        /// <summary>
        /// 当前存储数据更新时间，单位:毫秒
        /// <summary>
        public long UpdateTime{set; get; }

        public string ToString() 
        {
            string result = " {" 
            + " PrivacyKey = " + PrivacyKey + " ," 
            + " PrivacyValue = " + PrivacyValue + " ," 
            + " CreateTime = " + CreateTime + " ," 
            + " UpdateTime = " + UpdateTime + " " 
            +  " }"; 
            return result;
        }  
        
    }
}