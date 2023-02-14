using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXStrategy.Api
{ 
    public class NormalAssetStock
    {       
        /// <summary>
        /// 资产名称
        /// <summary>
        public string AssetName{set; get;}
        /// <summary>
        /// 资产总数
        /// <summary>
        public double AssetAmount{set; get;}   

        public string ToString() 
        {
            string result = " {" 
            + " AssetName = " + AssetName + " ," 
            + " AssetAmount = " + AssetAmount + " " 
            + "}"; 

            return result;
        }    
    }
}