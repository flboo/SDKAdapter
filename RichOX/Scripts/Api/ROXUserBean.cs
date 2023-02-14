using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXBase.Api
{
    public class ROXUserBean : ROXUserBeanBase
    {
        private string m_DeviceId;
        private string m_CountryCode;
        private string m_InviterId;
        private string m_InvitationCode;

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
        /// 用户注册对应的国家码
        /// <summary>
        public string CountryCode
        {
            set
            {
                m_CountryCode = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_CountryCode;
            }
        }

        /// <summary>
        /// 用户提现状态
        /// <summary>
        public bool IsHasWithdraw { set; get; }

        /// <summary>
        /// 安装时间， unix 时间戳
        /// <summary>
        public long InstallAt { set; get; }

        /// <summary>
        /// 邀请者Id
        /// <summary>
        public string InviterId
        {
            set
            {
                m_InviterId = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_InviterId;
            }
        }

        /// <summary>
        /// 邀请码
        /// <summary>
        public string InvitationCode
        {
            set
            {
                m_InvitationCode = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_InvitationCode;
            }
        }

        /// <summary>
        /// 是否为新用户
        /// <summary>
        public bool IsNew {set; get;}

        /// <summary>
        /// 用户注册时，服务端检测的IP类型
        /// 1：vpn用户；2：白名单用户；3：普通IP；4：服务器注册；99：IP信息未知
        /// <summary>
        public int IpType{set; get;}

        public List<string> WalletList {set; get;}

        public AppleInfo AppleInfoCurrent { set; get; }

        public FacebookInfo FacebookInfoCurrent { set; get; }

        public GoogleInfo GoogleInfoCurrent { set; get; }

        public WechatInfo WechatInfoCurrent { set; get; }

        public APInfo APInfoCurrent { set; get; }

        public string ToString()
        {
            string result = " {"
            + " Id = " + Id + " ,"
            + " Name = " + Name + " ,"
            + " Avatar = " + Avatar + " ,"
            + " DeviceId = " + DeviceId + " ,"
            + " CountryCode = " + CountryCode + " ,"
            + " IsHasWithdraw = " + IsHasWithdraw + " ,"
            + " InstallAt = " + InstallAt + " ,"
            + " CreateAt = " + CreateAt + " ,"
            + " SeverTime = " + SeverTime + " ,"
            + " InviterId = " + InviterId + " ,"
            + " InvitationCode = " + InvitationCode + " ,"
            + " IsNew = " + IsNew + " ,"
            + " IpType = " + IpType + " ,";

            string walletInfo = "[";
            if (WalletList != null) 
            {
                foreach(string wallet in WalletList)
                {
                    walletInfo = walletInfo + wallet + ",";
                }
            }
            walletInfo = walletInfo + "]";

            result = result + " WalletList : " + walletInfo + " ,";

            if (AppleInfoCurrent != null)
            {
                result = result + " AppleInfo : " + AppleInfoCurrent.ToString() + " ,";
            }

            if (GoogleInfoCurrent != null)
            {
                result = result + " GoogleInfo : " + GoogleInfoCurrent.ToString() + " ,";
            }

            if (FacebookInfoCurrent != null)
            {
                result = result + " FacebookInfo : " + FacebookInfoCurrent.ToString() + " ,";
            }

            if (WechatInfoCurrent != null)
            {
                result = result + " WechatInfo : " + WechatInfoCurrent.ToString() + " ,";
            }

            if (APInfoCurrent != null)
            {
                result = result + " APInfo : " + APInfoCurrent.ToString() + " ,";
            }

            if (result.EndsWith(","))
            {
                result = result.Substring(0, result.Length - 1);
            }

            result = result + "}";

            return result;
        }
    }

    public class AppleInfo
    {
        private string m_AppleName;
        private string m_AppleSub;

        /// <summary>
        /// 当前账号名称
        /// <summary>
        public string AppleName
        {
            set
            {
                m_AppleName = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_AppleName;
            }
        }


        /// <summary>
        /// 当前用户的唯一表示
        /// <summary>
        public string AppleSub
        {
            set
            {
                m_AppleSub = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_AppleSub;
            }
        }

        public string ToString()
        {
            string result = " {"
            + " AppleName = " + AppleName + " ,"
            + " AppleSub = " + AppleSub + " "
            + "}";

            return result;
        }
    }

    public class APInfo
    {
        private string m_APName;
        private string m_APId;

        /// <summary>
        /// 支付宝账号昵称
        /// <summary>
        public string APName
        {
            set
            {
                m_APName = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_APName;
            }
        }


        /// <summary>
        /// 支付宝账号 ID
        /// <summary>
        public string APId
        {
            set
            {
                m_APId = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_APId;
            }
        }

        public string ToString()
        {
            string result = " {"
            + " APName = " + APName + " ,"
            + " APId = " + APId + " "
            + "}";

            return result;
        }
    }

    public class FacebookInfo
    {
        private string m_Email;
        private string m_FBName;
        private string m_FBOpenId;
        private string m_FirstName;
        private string m_LastName;

        /// <summary>
        /// 当前账号注册邮箱
        /// <summary>
        public string Email
        {
            set
            {
                m_Email = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_Email;
            }
        }

        /// <summary>
        /// 当前用户名称
        /// <summary>
        public string FBName
        {
            set
            {
                m_FBName = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_FBName;
            }
        }

        /// <summary>
        /// Facebook 对应 open_id
        /// <summary>
        public string FBOpenId
        {
            set
            {
                m_FBOpenId = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_FBOpenId;
            }
        }

        /// <summary>
        /// Facebook first_name
        /// <summary>
        public string FirstName
        {
            set
            {
                m_FirstName = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_FirstName;
            }
        }

        /// <summary>
        /// Facebook last_name
        /// <summary>
        public string LastName
        {
            set
            {
                m_LastName = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_LastName;
            }
        }

        public string ToString()
        {
            string result = " {"
            + " Email = " + Email + " ,"
            + " FBName = " + FBName + " ,"
            + " FBOpenId = " + FBOpenId + " ,"
            + " FirstName = " + FirstName + " ,"
            + " LastName = " + LastName + " "
            + "}";

            return result;
        }
    }

    public class GoogleInfo
    {
        private string m_Email;
        private string m_FamilyName;
        private string m_GivenName;
        private string m_GoogleName;
        private string m_GoogleSub;

        /// <summary>
        /// google 注册邮箱
        /// <summary>
        public string Email
        {
            set
            {
                m_Email = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_Email;
            }
        }

        /// <summary>
        /// google family name
        /// <summary>
        public string FamilyName
        {
            set
            {
                m_FamilyName = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_FamilyName;
            }
        }

        /// <summary>
        /// google given name
        /// <summary>
        public string GivenName
        {
            set
            {
                m_GivenName = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_GivenName;
            }
        }

        /// <summary>
        /// google name
        /// <summary>
        public string GoogleName
        {
            set
            {
                m_GoogleName = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_GoogleName;
            }
        }

        /// <summary>
        /// google sub
        /// <summary>
        public string GoogleSub
        {
            set
            {
                m_GoogleSub = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_GoogleSub;
            }
        }

        public string ToString()
        {
            string result = " {"
            + " Email = " + Email + " ,"
            + " FamilyName = " + FamilyName + " ,"
            + " GivenName = " + GivenName + " ,"
            + " GoogleName = " + GoogleName + " ,"
            + " GoogleSub = " + GoogleSub + " "
            + "}";

            return result;
        }
    }


    public class WechatInfo
    {
        private string m_WXAppId;
        private string m_NickName;
        private string m_Avatar;
        private string m_OpenId;
        private string m_UnionId;

        /// <summary>
        /// 微信 AppId
        /// <summary>
        public string WXAppId
        {
            set
            {
                m_WXAppId = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_WXAppId;
            }
        }

        /// <summary>
        /// 微信用户昵称
        /// <summary>
        public string NickName
        {
            set
            {
                m_NickName = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_NickName;
            }
        }

        /// <summary>
        /// 微信用户头像
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
        /// 微信 openid
        /// <summary>
        public string OpenId
        {
            set
            {
                m_OpenId = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_OpenId;
            }
        }

        /// <summary>
        /// 微信 unionid
        /// <summary>
        public string UnionId
        {
            set
            {
                m_UnionId = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_UnionId;
            }
        }

        public string ToString()
        {
            string result = " {"
            + " WXAppId = " + WXAppId + " ,"
            + " NickName = " + NickName + " ,"
            + " Avatar = " + Avatar + " ,"
            + " OpenId = " + OpenId + " ,"
            + " UnionId = " + UnionId + " "
            + "}";

            return result;
        }
    }
}