using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using ROXToolbox.Common;
using ROXToolbox.Platforms;
using ROXBase.Api;

namespace ROXToolbox.Api
{
    public class RichOXToolbox
    {
        private static RichOXToolbox mInstance;
        private static IROXToolbox mROXToolbox;

        public static RichOXToolbox Instance()
        {
            if (mInstance != null)
            {
                return mInstance;
            }
            else
            {
                mInstance = new RichOXToolbox();
                return mInstance;
            }
        }

        /// <summary>
        /// 获取实例
        /// <summary>  
        public RichOXToolbox()
        {
            mROXToolbox = ClientFactory.RichOXClientInstance();
        }

        /// <summary>
        /// 获取应用内储蓄罐信息
        /// <summary>
        public void QueryPiggyBankList(ROXInterface<List<PiggyBank>> callback)
        {
            mROXToolbox.QueryPiggyBankList(callback);
        }

        /// <summary>
        /// 储蓄罐提取 
        /// <summary>
        public void PiggyBankWithdraw(int piggyId, ROXInterface<bool> callback)
        {
            mROXToolbox.PiggyBankWithdraw(piggyId, callback);
        }

        /// <summary>
        /// 初始化，聊天红包群需要用到该方法，Android中请务必在Application中调用该方法
        /// <summary>
        public void Init()
        {
            mROXToolbox.Init();
        }

        /// <summary>
        /// 设置聊天红包群，自动去服务区拉取消息时间间隔，单位毫秒
        /// <summary>
        public void SetInterval(int interval)
        {
            mROXToolbox.SetInterval(interval);
        }

        /// <summary>
        /// 获取聊天红包群群组信息
        /// <summary>
        public void GetGroupInfo(ROXInterface<List<GroupInfo>> callback)
        {
            mROXToolbox.GetGroupInfo(callback);
        }

        /// <summary>
        /// 获取聊天红包群消息列表
        /// groupId ： 群组Id
        /// size ： 获取消息条数
        /// <summary>
        public void GetMessageList(string groupId, int size, ROXInterface<List<ChatMessage>> callback)
        {
            mROXToolbox.GetMessageList(groupId, size, callback);
        }

        /// <summary>
        /// 聊天红包群发送消息
        /// groupId : 群组Id
        /// nickName : 昵称
        /// avatar : 头像
        /// type : 聊天类型 10-普通内容 20-红包
        /// content : 聊天内容
        /// <summary>
        public void PostChatMessage(string groupId, string nickName, string avatar, string type, string content, ROXInterface<ChatMessage> callback)
        {
            mROXToolbox.PostChatMessage(groupId, nickName, avatar, type, content, callback);
        }

        /// <summary>
        /// 存储私有数据
        /// key ： 键值
        /// value ： 存储数据
        /// <summary>
        public void SavePrivacyData(string key, string value, ROXInterface<Boolean> callback)
        {
            mROXToolbox.SavePrivacyData(key, value, callback);
        }

        /// <summary>
        /// 查询单个私有数据
        /// key ： 键值
        /// <summary>
        public void QueryPrivacyData(string key, ROXInterface<PrivacyInfo> callback)
        {
            mROXToolbox.QueryPrivacyData(key, callback);
        }

        /// <summary>
        /// 查询多个私有数据
        /// keys ： 键值数组
        /// keySize ： 键值个数，不超过100
        /// <summary>
        public void QueryPrivacyDatas(List<string> keys, ROXInterface<List<PrivacyInfo>> callback)
        {
            mROXToolbox.QueryPrivacyDatas(keys, callback);
        }
    }
}
