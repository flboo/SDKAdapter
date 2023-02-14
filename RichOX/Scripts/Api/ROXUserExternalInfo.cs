using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXBase.Api
{
    public class ROXUserExternalInfo
    {
        private string m_wxNickName;
        private string m_apNickName;

        /// <summary>
        /// pay methods
        /// <summary>
        public List<string> PayMethods { set; get; }

        /// <summary>
        /// 是否绑定WX
        /// <summary>
        public bool IsBindWX { set; get; }

        /// <summary>
        /// WX昵称
        /// <summary>
        public string WxNickName
        {
            set
            {
                m_wxNickName = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_wxNickName;
            }
        }

        /// <summary>
        /// 是否绑定AP
        /// <summary>
        public bool IsBindAP { set; get; }

        /// <summary>
        /// AP昵称
        /// <summary>
        public string ApNickName
        {
            set
            {
                m_apNickName = RichOXStringUtil.ValueOf(value);
            }
            get
            {
                return m_apNickName;
            }
        }

        public string ToString()
        {
            string pays = "[";
            if (PayMethods != null) 
            {
                foreach(string method in PayMethods)
                {
                    pays = pays + method + ",";
                }
            }
            pays = pays + "]";
            
            string result = " {"
            + " PayMehtods = " + pays + " ,"
            + " IsBindWX = " + IsBindWX + " ,"
            + " WxNickName = " + WxNickName + " ,"
            + " IsBindAP = " + IsBindAP + " ,"
            + " ApNickName = " + ApNickName + " ,";

            result = result + "}";

            return result;
        }
    }
}