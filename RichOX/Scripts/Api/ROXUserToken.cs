using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXBase.Api
{ 
    public class ROXUserToken
    {    
        private string m_UserId;
        private string m_Token;

        /// <summary>
        /// 用户Id
        /// <summary>  
        public string UserId
        {
            set 
            {
                m_UserId = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_UserId;
            }
        }

        /// <summary>
        /// 用户Token
        /// <summary>
        public string Token
        {
            set 
            {
                m_Token = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_Token;
            }
        }

        /// <summary>
        /// 刷新时间
        /// <summary>
        public long RefreshTime{set; get; }

        public string ToString() 
        {
            string result = " {" 
            + " UserId = " + UserId + " ," 
            + " Token = " + Token + " ," 
            + " RefreshTime = " + RefreshTime 
            + "}"; 

            return result;
        } 
    }
}
    