using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXStrategy.Api
{ 
    public class NormalMissionResult
    {  
        /// <summary>
        /// 资产名称
        /// <summary>     
        public string AssetName{set; get;}

        /// <summary>
        /// 资产变化值
        /// <summary>
        public double PrizeDelta{set; get;}

        /// <summary>
        /// 当前资产总量
        /// <summary>
        public double PrizeTotal {set; get;}

        /// <summary>
        /// 资产库存列表
        /// <summary>
        public List<NormalAssetStock> AssetList{set; get; }    

        public string ToString() 
        {
            string result = " {" 
            + " AssetName = " + AssetName + " ," 
            + " PrizeTotal = " + PrizeTotal + " ," 
            + " PrizeDelta = " + PrizeDelta + ", ";

            foreach(NormalAssetStock assetStock in AssetList) 
            {
                result = result + assetStock.ToString() + " ,";
            }           

            result = result.Substring(0, result.Length -1);

            result = result + " }"; 

            return result;
        }      
    }
}