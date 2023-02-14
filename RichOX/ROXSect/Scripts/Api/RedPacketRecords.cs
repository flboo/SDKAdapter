using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROXSect.Api 
{
    public class RedPacketRecords 
    {
        /// <summary>
        /// 红包记录总数
        /// <summary>  
        public int Total {set; get;}

        /// <summary>
        /// 分页展示，当前分页大小
        /// <summary>  
        public int PageSize {set; get;}

        /// <summary>
        /// 分页展示，当前分页索引
        /// <summary>  
        public int CurrentPage {set; get;}

        /// <summary>
        /// 记录列表
        /// <summary>  
        public List<Item> RecordList {set; get;}

        public void ToString() 
        {
            Debug.Log("Total: " + Total);
            Debug.Log("PageSize: " + PageSize);    
            Debug.Log("CurrentPage: " + CurrentPage);     
            if (RecordList != null)   
            {
                Debug.Log("the record is : ");     
                foreach(Item item in RecordList)
                {
                    item.ToString();
                }
            } 
        }
    }

    public class Item 
    {
        /// <summary>
        /// 宗主 Id
        /// <summary>  
        public string MasterId {set; get;}

        /// <summary>
        /// 红包类型
        /// 0：兑换红包
        /// 1：次数红包
        /// <summary>  
        public int Type {set; get;}

        /// <summary>
        /// 宗门等级
        /// <summary>  
        public int TongLevel {set; get;}

        /// <summary>
        /// 红包金额
        /// <summary>  
        public double Amount {set; get;}

        /// <summary>
        /// 次数红包对应的次数
        /// <summary>  
        public int TimePacketCount {set; get;}

        /// <summary>
        /// 贡献值兑换次数
        /// <summary>  
        public int TransformCount {set; get;}

        /// <summary>
        /// 红包获取时间
        /// <summary>  
        public long PacketGetTime {set; get;}

        public void ToString() 
        {
            Debug.Log("MasterId: " + MasterId);
            Debug.Log("Type: " + Type);    
            Debug.Log("TongLevel: " + TongLevel);   
            Debug.Log("Amount: " + Amount);
            Debug.Log("TimePacketCount: " + TimePacketCount);    
            Debug.Log("TransformCount: " + TransformCount); 
            Debug.Log("PacketGetTime: " + PacketGetTime);
        }
    }    
}