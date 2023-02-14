using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXStrategy.Api
{ 
    public class StrategyAsset
    {       
        /// <summary>
        /// 资产名称
        /// <summary>  
        public string AssetName{set; get;}

        /// <summary>
        /// 兑换汇率
        /// <summary>  
        public double ExchangeRate{set; get; }

        public string ToString() 
        {
            string result = " {" 
            + " AssetName = " + AssetName + " ," 
            + " ExchangeRate = " + ExchangeRate + " " 
            + "}"; 

            return result;
        } 
    }
}