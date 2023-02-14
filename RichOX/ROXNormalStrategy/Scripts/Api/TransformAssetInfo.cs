using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXStrategy.Api
{ 
    public class TransformAssetInfo
    {       
        /// <summary>
        /// 资产名称
        /// <summary>
        public string AssetName{set; get;}

        /// <summary>
        /// 资产变化值
        /// <summary>
        public double PrizeDelta{set; get; }

        /// <summary>
        /// 资产总数
        /// <summary>
        public double PrizeTotal{set; get; }

        public string ToString() 
        {
            string result = " {" 
            + " AssetName = " + AssetName + " ," 
            + " PrizeTotal = " + PrizeTotal + " ," 
            + " PrizeDelta = " + PrizeDelta + " " 
            + "}"; 

            return result;
        }    
    }
}