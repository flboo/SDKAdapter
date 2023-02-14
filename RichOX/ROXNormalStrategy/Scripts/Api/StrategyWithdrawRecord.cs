using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXStrategy.Api
{ 
    public class StrategyWithdrawRecord
    {       
        /// <summary>
        /// 提现记录ID
        /// <summary>
        public string Id{set; get;}
        /// <summary>
        /// 提现任务ID
        /// <summary>
        public string WithdrawTaskId{set; get; }
        /// <summary>
        /// 提现用户Id
        /// <summary>
        public string UserId{set; get;}
        /// <summary>
        /// 提现备注信息
        /// <summary>
        public string PayRemark{set; get; }
        /// <summary>
        /// 提现金额
        /// <summary>
        public double CashAmount{set; get;}
        /// <summary>
        /// 当前提现发起时间时间，格式为： 2021-03-03
        /// <summary>
        public string RequestDay{set; get; }
        /// <summary>
        /// 当前提现渠道
        /// <summary>
        public string WithdrawChannel{set; get; }
        /// <summary>
        /// 提现状态
        /// 1: 等待审核
        /// 2: 审核通过
        /// 3: 审核驳回
        /// 4: 正在打款
        /// 5: 提现失败
        /// 100: 提现成功
        /// <summary>
        public int Status{set; get; }

        public string ToString() 
        {
            string result = " {" 
            + " Id = " + Id + " ," 
            + " WithdrawTaskId = " + Id + " ," 
            + " UserId = " + UserId + " ," 
            + " PayRemark = " + PayRemark + " ," 
            + " CashAmount = " + CashAmount + " ," 
            + " RequestDay = " + RequestDay + " ," 
            + " Status = " + Status + " ," 
            + " WithdrawChannel = " + WithdrawChannel + " " 
            + "}"; 

            return result;
        }    
    }
}