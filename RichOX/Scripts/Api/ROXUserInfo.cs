using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXBase.Api
{ 
    public class ROXUserInfo 
    {
        private string m_UserId;
        private string m_Name;
        private string m_Avatar;
        private string m_DeviceId;
        private string m_WechatOpenId;
        private string m_WechatAppId;
        private string m_WechatBindTime;
        private string m_GetInstallAt;
       
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
        /// 用户唯一识别码
        /// <summary>
        public string DeviceId
        {
            set 
            {
                m_DeviceId = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_DeviceId;
            }
        }

        /// <summary>
        /// 返回微信 openId
        /// <summary>
        public string WechatOpenId
        {
            set 
            {
                m_WechatOpenId = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_WechatOpenId;
            }
        }

        /// <summary>
        /// 微信 appId
        /// <summary>
        public string WechatAppId
        {
            set 
            {
                m_WechatAppId = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_WechatAppId;
            }
        }

        /// <summary>
        /// 微信绑定时间
        /// <summary>
        public string WechatBindTime
        {
            set 
            {
                m_WechatBindTime = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_WechatBindTime;
            }
        }

        /// <summary>
        /// 用户安装APP时间
        /// <summary>
        public string GetInstallAt
        {
            set 
            {
                m_GetInstallAt = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_GetInstallAt;
            }
        }

        /// <summary>
        /// 用户是否有效
        /// <summary>
        public bool IsEnabled{set; get;}

        /// <summary>
        /// 用户提现状态
        /// <summary>
        public bool IsHasWithdraw{set; get;}

        /// <summary>
        /// 是否为新用户
        /// <summary>
        public bool IsNew{set; get;}

        public string ToString() 
        {
            string result = " {" 
            + " UserId = " + UserId + " ," 
            + " Name = " + Name + " ," 
            + " Avatar = " + Avatar + " ," 
            + " DeviceId = " + DeviceId + " ," 
            + " WechatOpenId = " + WechatOpenId + " ," 
            + " WechatAppId = " + WechatAppId + " ," 
            + " WechatBindTime = " + WechatBindTime + " ," 
            + " GetInstallAt = " + GetInstallAt + " ,"
            + " IsEnabled = " + IsEnabled + " ,"
            + " IsHasWithdraw = " + IsHasWithdraw + " ,"   
            + " IsNew = " + IsNew + " " 
            + "}"; 

            return result;
        }   
    }
}