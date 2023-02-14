using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXBase.Api
{ 
    public class ROXUserInfoSimple 
    {    
        private string m_UserId;
        private string m_Name;
        private string m_Avatar;

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
        /// 用户名称
        /// <summary>
        public string Name
        {
            set 
            {
                m_Name = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_Name;
            }
        }

        /// <summary>
        /// 用户头像
        /// <summary>
        public string Avatar
        {
            set 
            {
                m_Avatar = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_Avatar;
            }
        }

        public string ToString() 
        {
            string result = " {" 
            + " UserId = " + UserId + " ," 
            + " Name = " + Name + " ," 
            + " Avatar = " + Avatar 
            + "}"; 

            return result;
        }   
    }
}
    