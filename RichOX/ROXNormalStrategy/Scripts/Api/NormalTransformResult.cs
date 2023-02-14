using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXStrategy.Api
{ 
    public class NormalTransformResult
    {      
        /// <summary>
        /// 兑换后原资产信息
        /// <summary>
        public TransformAssetInfo OriginAsset{set; get;}

        /// <summary>
        /// 兑换后目标资产信息
        /// <summary>
        public TransformAssetInfo DestinationAsset{set; get; }

        /// <summary>
        /// 资产库存情况
        /// <summary>
        public List<NormalAssetStock> AssetList{set; get; }

        public string ToString() 
        {
            string result = " {" 
            + OriginAsset.ToString() +  " ,"
            + DestinationAsset.ToString() +  " ,";            

            foreach(NormalAssetStock normalAssetStock in AssetList) 
            {
                result = result + normalAssetStock.ToString() + " ,";
            }

            result = result.Substring(0, result.Length -1);

            result = result + " }"; 

            return result;
        }  
    }
}