using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using ROXBase.Common;
using ROXBase.Platforms;

namespace ROXBase.Api
{ 
    public class RichOXUser 
    {
        private static RichOXUser mInstance = new RichOXUser();
        private IROXUser mROXUser;

        public static RichOXUser Instance
        {
            get
            {
                return mInstance;
            }
        }

        public RichOXUser () {
            mROXUser = ClientFactory.ROXUserInstance();
        }

        /// <summary>
        /// 游客注册
        /// source ：默认为""
        /// <summary>  
        public void RegisterVisitor(string source, ROXInterface<ROXUserInfo> callback) 
        {
            mROXUser.RegisterVisitor(source, callback);
        }

        /// <summary>
        /// 用户绑定，微信绑定，type
        /// 微信绑定，type 对应： wechat
        /// appid： 微信开发平台对应的AppId
        /// token：微信登陆返回的授权码
        /// <summary> 
        public void StartBindAccount(string type, string appid, string token, ROXInterface<ROXUserInfo> callback)
        {
            mROXUser.StartBindAccount(type, appid, token, callback);
        }

        /// <summary>
        /// 微信注册
        /// appid： 微信开发平台对应的AppId
        /// token：微信登陆返回的授权码
        /// <summary> 
        public void RegisterWithWechat(string wxAppId, string wxCode, string source, ROXInterface<ROXUserInfo> callback)
        {
            mROXUser.RegisterWithWechat(wxAppId, wxCode, source, callback);
        }

        /// <summary>
        /// 获取用户信息
        /// <summary> 
        public void GetUserInfo(ROXInterface<ROXUserInfo> callback) 
        {
            mROXUser.GetUserInfo(callback);
        }

        /// <summary>
        /// 获取用户排行
        /// count：排行榜数量
        /// accountType：0: 返回所有用户, 1: 仅返回绑定社交账户用户
        /// rankingType：coin: 按金币余额排行, cash: 按现金余额排行
        /// <summary> 

        public void GetUserRanking(int count, int accountType, string rankingType, ROXInterface<List<ROXUserInfo>> callback)
        {
            mROXUser.GetUserRanking(count, accountType, rankingType, callback);
        }

        /// <summary>
        /// 获取指定用户信息
        /// userList：指定用户列表
        /// <summary> 
        public void GetSpecificUsersInfo(List<string> userList, ROXInterface<List<ROXUserInfoSimple>> callback) 
        {
            mROXUser.GetSpecificUsersInfo(userList, callback);
        }

        /// <summary>
        /// 获取用户 token
        /// <summary> 
        public void GetUserToken(ROXInterface<ROXUserToken> callback) 
        {
            mROXUser.GetUserToken(callback);
        }

        /// <summary>
        /// 用户注销
        /// <summary> 
        public void Logout(ROXInterface<bool> callback) 
        {
            mROXUser.Logout(callback);
        }

        public void BindInviter(string inviterUid, ROXInterface<bool> callback)
        {
            mROXUser.BindInviter(inviterUid, callback);
        }
    }
}
