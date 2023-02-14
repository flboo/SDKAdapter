using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using ROXBase.Common;
using ROXBase.Platforms;

namespace ROXBase.Api
{
    public class RichOXUserManager
    {
        private static RichOXUserManager mInstance = new RichOXUserManager();
        private IRichOXUserManager mRichOXUserManager;

        public static RichOXUserManager Instance
        {
            get
            {
                return mInstance;
            }
        }

        public RichOXUserManager()
        {
            mRichOXUserManager = ClientFactory.RichOXUserManagerInstance();
        }

        /// <summary>
        /// 游客注册
        /// <summary>  
        public void RegisterVisitor(ROXInterface<ROXUserBean> callback)
        {
            mRichOXUserManager.RegisterVisitor(callback);
        }

        /// <summary>
        /// facebook 账号登录
        /// openId   facebook 用户的 open_id
        /// token    facebook 用户的授权码 token
        /// <summary>  
        public void RegisterWithFacebook(string openId, string token, ROXInterface<ROXUserBean> callback)
        {
            mRichOXUserManager.RegisterWithFacebook(openId, token, callback);
        }

        /// <summary>
        /// google 账号登录
        /// token  google用户的id_token
        /// <summary>  
        public void RegisterWithGoogle(String token, ROXInterface<ROXUserBean> callback)
        {
            mRichOXUserManager.RegisterWithGoogle(token, callback);
        }

        /// <summary>
        /// apple     账号登录
        /// appleName 账户名称
        /// token     当前用户的唯一表示
        /// <summary>  
        public void RegisterByApple(string appleName, string token, ROXInterface<ROXUserBean> callback)
        {
            mRichOXUserManager.RegisterByApple(appleName, token, callback);
        }

        /// <summary>
        /// wxAppId     微信应用app_id
        /// wxCode      微信授权时返回的code
        /// <summary> 
        public void RegisterWithWechat(string wxAppId, string wxCode, ROXInterface<ROXUserBean> callback)
        {
            mRichOXUserManager.RegisterWithWechat(wxAppId, wxCode, callback);
        }

        /// <summary>
        /// 社交账号绑定
        /// type     绑定类型，Facebook 账号： facebook; Google 账号：google; 微信账号：wechat ; 支付宝账号：apy
        /// snsId    绑定 facebook 时，填入 open_id ; 绑定 google 时，填入 google id ; 绑定微信时，填入微信 appid ; 支付宝账号：apy
        /// bindCode facebook时传人 token ; google 时传入 id_token ; 微信时传入授权返回的 code ; 支付宝传入 auth_code
        /// <summary>
        public void StartBindAccount(string type, string appid, string code_or_token, ROXInterface<ROXUserBean> callback)
        {
            mRichOXUserManager.StartBindAccount(type, appid, code_or_token, callback);
        }

        /// <summary>
        /// 获取用户信息
        /// <summary>
        public void GetUserInfo(ROXInterface<ROXUserBean> callback)
        {
            mRichOXUserManager.GetUserInfo(callback);
        }

        /// <summary>
        /// 获取指定用户信息
        /// <summary>
        public void GetUserInfoByUserId(string uid, ROXInterface<ROXUserBean> callback)
        {
            mRichOXUserManager.GetUserInfoByUserId(uid, callback);
        }

        /// <summary>
        /// 获取邀请者信息
        /// <summary>
        public void StartRetrieveInviter(ROXInterface<ROXUserBeanBase> callback)
        {
            mRichOXUserManager.StartRetrieveInviter(callback);
        }

        /// <summary>
        /// 用户注销
        /// <summary>
        public void Logout(ROXInterface<bool> callback)
        {
            mRichOXUserManager.Logout(callback);
        }

        /// <summary>
        /// 绑定师徒关系
        /// <summary>
        public void BindInviter(string inviterUid, ROXInterface<bool> callback)
        {
            mRichOXUserManager.BindInviter(inviterUid, callback);
        }

        /// <summary>
        /// 获取用户的外部存储信息
        /// <summary>
        public void GetUserExternalInfo(ROXInterface<ROXUserExternalInfo> callback)
        {
            mRichOXUserManager.GetUserExternalInfo(callback);
        }

        /// <summary>
        /// 获取支付宝授权请求参数
        /// <summary>
        public void GetAPAuthInfo(ROXInterface<string> callback)
        {
            mRichOXUserManager.GetAPAuthInfo(callback);
        }

        /// <summary>
        /// 绑定电子钱包
        /// type : 钱包类型
        /// walletInfo : 钱包信息
        /// <summary>
        public void BindWallet(String type, string walletInfo, ROXInterface<bool> callback)
        {
            mRichOXUserManager.BindWallet(type, walletInfo, callback);
        }

    }

}
