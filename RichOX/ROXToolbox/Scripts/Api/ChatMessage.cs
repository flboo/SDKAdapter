using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXToolbox.Api
{ 
    public class ChatMessage
    {   
        /// <summary>
        /// 群组Id
        /// <summary>
        public string GroupId{set; get; }

        /// <summary>
        /// 当前消息内容
        /// <summary>
        public string MessageContent{set; get;}

        /// <summary>
        /// 消息 Id
        /// <summary>
        public long MessageId{set; get; }

        /// <summary>
        /// 消息生成时间
        /// <summary>
        public long MessageTime{set; get; }

        /// <summary>
        /// 消息类型：10-消息；20-红包
        /// <summary>
        public string MessageType{set; get; }

        /// <summary>
        /// 发送者用户Id
        /// <summary>
        public string SenderId{set; get; }

        /// <summary>
        /// 发送者用户名
        /// <summary>
        public string SenderName{set; get; }

        /// <summary>
        /// 发送者头像
        /// <summary>
        public string SenderImage{set; get; }

        /// <summary>
        /// 消息状态
        /// <summary>
        public int Status{set; get; }

        public string ToString() 
        {
            string result = " {" 
            + " GroupId = " + GroupId + " ," 
            + " MessageContent = " + MessageContent + " ," 
            + " MessageId = " + MessageId + " ," 
            + " MessageTime = " + MessageTime + " ," 
            + " MessageType = " + MessageType + " ," 
            + " SenderId = " + SenderId + " ," 
            + " SenderName = " + SenderName + " ," 
            + " SenderImage = " + SenderImage + " ," 
            + " Status = " + Status + " ," 
            +  " }"; 
            return result;
        }  
        
    }
}