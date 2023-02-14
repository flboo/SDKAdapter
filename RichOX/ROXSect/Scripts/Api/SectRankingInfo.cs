using System.Collections;
using UnityEngine;

namespace ROXSect.Api
{
    public class SectRankingInfo
    {
        /// <summary>
        /// 当前用户邀请排名
        /// <summary>    
        public int Index { set; get; }

        /// <summary>
        /// 当前用户 Id
        /// <summary>  
        public string MasterId { set; get; }

        /// <summary>
        /// 当前用户邀请人数
        /// <summary>  
        public int Counts { set; get; }

        public string ToString()
        {
            string result = " {"
            + " Index = " + Index + " ,"
            + " MasterId = " + MasterId + " ,"
            + " Counts = " + Counts + " "
            + "}";

            return result;
        }
    }
}