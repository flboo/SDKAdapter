using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXSect.Api 
{
    public class TransformInfo 
    {
        /// <summary>
        /// 列表形式返回随机红包
        /// <summary> 
        public List<double> PacketList {set; get;}

        /// <summary>
        /// 兑换后剩余贡献值
        /// <summary> 
        public int CurrentContribution {set; get;}

        /// <summary>
        /// 已完成的兑换次数
        /// <summary> 
        public int TransformTimes {set; get;}

        /// <summary>
        /// 此次兑换的红包金额
        /// <summary>         
        public double CashDelta {set; get;}

        /// <summary>
        /// 当前红包账户总金额
        /// <summary> 
        public double CurrentCash {set; get;}
        
    }
}