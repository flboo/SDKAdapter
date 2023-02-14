using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXStrategy.Api
{
    public class GlobalWithdrawInfo
    {

        /// <summary>
        /// 用户收款账号，必填
        /// <summary>
        public string PayeeAccount { set; get; }

        /// <summary>
        /// 钱包类型，如果是美国 paypal 提现，此参数传入 paypal ，必填
        /// <summary>        
        public string WalletChannel { set; get; }

        /// <summary>
        /// 用户姓名
        /// <summary>
        public string PayeeName { set; get; }

        /// <summary>
        /// 用户 first name
        /// <summary>
        public string PayeeFirstName { set; get; }

        /// <summary>
        /// 用户 middle name
        /// <summary>
        public string PayeeMiddleName { set; get; }

        /// <summary>
        /// 用户 last name
        /// <summary>        
        public string PayeeLastName { set; get; }

        /// <summary>
        /// 电子钱包扩展参数，JSON格式
        /// <summary>        
        public string ExtendedInfo { set; get; }

        /// <summary>
        /// 支付备注信息，英文字符
        /// <summary>        
        public string PayRemark { set; get; }

        public string ToString()
        {
            string result = " {"
            + " PayeeAccount = " + PayeeAccount + " ,"
            + " PayeeName = " + PayeeName + ", "
            + " PayeeFirstName = " + PayeeFirstName + " ,"
            + " PayeeMiddleName = " + PayeeMiddleName + ", "
            + " PayeeLastName = " + PayeeLastName + " ,"
            + " WalletChannel = " + WalletChannel + ", "
            + " ExtendedInfo = " + ExtendedInfo + " ,"
            + " PayRemark = " + PayRemark + " "
            + " }";

            return result;
        }

    }
}