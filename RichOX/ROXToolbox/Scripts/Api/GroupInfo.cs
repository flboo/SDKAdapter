using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXToolbox.Api
{
    public class GroupInfo
    {

        /// <summary>
        /// 群组类型
        /// 10 - GUIDE  #引导群
        /// 20 - WORLD  #引导群
        /// 30 - REGION #省份群
        /// 40 - AREA   #大区群
        /// 50 - UNKNOWN_REGION #未获取到地址位置的群
        /// 60 - FAMILY_MY   #未使用
        /// 70 - FAMILY_TEACHER #未使用
        /// 80 - FAMILY_TEACHER_TEACHER #未使用
        /// 10000 - SYSTEM #系统消息群
        /// <summary>
        public string Category { set; get; }

        /// <summary>
        /// 默认群组名称，服务端提供，如华东大区群
        /// <summary>
        public string DisplayName { set; get; }

        /// <summary>
        /// 群组Id
        /// <summary>
        public string GroupId { set; get; }

        /// <summary>
        /// 该群最新消息
        /// <summary>
        public ChatMessage LastChatMessage { set; get; }

        /// <summary>
        /// 群名称简写，对应默认名称如华东大区群，该字段返回华东，客户端可自行定制
        /// <summary>
        public string Name { set; get; }

        /// <summary>
        /// * 发送规则
        /// 0-不可以收，也不可以发，比如引导群和系统消息群
        /// 1-只可以收消息，暂时未使用
        /// 2-只可以发消息，暂时未使用
        /// 3-可以收，也可以发，比如同城群、大区群
        /// <summary>
        public int Rule { set; get; }

        public string ToString()
        {
            string result = " {"
            + " Category = " + Category + " ,"
            + " DisplayName = " + DisplayName + " ,"
            + " GroupId = " + GroupId + " ,"
            + " LastChatMessage : " + LastChatMessage.ToString() + " ,"
            + " Name = " + Name + " ,"
            + " Rule = " + Rule
            + " }";

            return result;
        }

    }
}