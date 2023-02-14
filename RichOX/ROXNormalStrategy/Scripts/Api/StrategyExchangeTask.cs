namespace ROXStrategy.Api
{ 
    public class StrategyExchangeTask
    {       
        /// <summary>
        /// 兑换任务ID
        /// <summary>
        public string ExchangeId{set; get;}

        /// <summary>
        /// 当前兑换资产名称
        /// <summary>
        public string OriginAssetName{set; get; }

        /// <summary>
        /// 当前兑换资产数量
        /// <summary>
        public double OriginAssetAmount{set; get;}

        /// <summary>
        /// 当前换取资产名称
        /// <summary>
        public string ExchangeAssetName{set; get; }

        /// <summary>
        /// 当前获取资产数量
        /// <summary>
        public double ExchangeAssetAmount{set; get;}

        public string ToString() 
        {
            string result = " {" 
            + " ExchangeId = " + ExchangeId + " ," 
            + " OriginAssetName = " + OriginAssetName + " ," 
            + " OriginAssetAmount = " + OriginAssetAmount + " ," 
            + " ExchangeAssetName = " + ExchangeAssetName + " ," 
            + " ExchangeAssetAmount = " + ExchangeAssetAmount + " " 
            + "}"; 

            return result;
        }    
    }
}