using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXStrategy.Api
{ 
    public class NormalAssetsInfo
    {   

        /// <summary>
        /// 用户Id
        /// <summary>
        public string Uid{set; get;}

        /// <summary>
        /// 策略Id
        /// <summary>
        public int StrategyId{set; get; }

        /// <summary>
        /// Ab分组名称
        /// <summary>
        public string AbGroup{set; get; }

        /// <summary>
        /// Ab分组Id
        /// <summary>
        public int AbId{set; get; }

        /// <summary>
        /// 当前库存资产列表
        /// <summary>        
        public List<NormalAssetStock> AssetStockList{set; get; }
        
        /// <summary>
        /// 提现记录列表
        /// <summary>  
        public List<StrategyWithdrawRecord> Records{set; get; }

        public string ToString() 
        {
            string result = " {" 
            + " Uid = " + Uid + " ," 
            + " StrategyId = " + StrategyId + ", ";

            foreach(NormalAssetStock assetStock in AssetStockList) 
            {
                result = result + assetStock.ToString() + " ,";
            }

            foreach(StrategyWithdrawRecord strategyWithdrawRecord in Records)
            {
                result = result + strategyWithdrawRecord.ToString() + " ,";
            }

            result = result.Substring(0, result.Length -1);

            result = result + " }"; 

            return result;
        }  
        
    }
}