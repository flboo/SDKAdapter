using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXBase.Api
{
    public class ROXUserBeanBase
    {
        private string m_Id;
        private string m_Name;
        private string m_Avatar;

        /// <summary>
        /// 用户Id
        /// <summary>
        public string Id
        {
            set
            {
                m_Id = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_Id;
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
        /// 如果有，返回用户头像URL
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

        /// <summary>
        /// 用户安装APP时间，unix 时间戳
        /// <summary>
        public long CreateAt { set; get; }

        /// <summary>
        /// 当前服务端时间， unix 时间戳
        /// <summary>
        public long SeverTime { set; get; }


        public string ToString()
        {
            string result = " {"
            + " Id = " + Id + " ,"
            + " Name = " + Name + " ,"
            + " Avatar = " + Avatar + " ,"
            + " CreateAt = " + CreateAt + " ,"
            + " SeverTime = " + SeverTime + " "
            + "}";

            return result;
        }
    }
}