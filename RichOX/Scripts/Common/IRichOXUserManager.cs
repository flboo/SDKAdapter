using ROXBase.Api;
using System;
using System.Collections.Generic;


namespace ROXBase.Common
{
    public interface IRichOXUserManager
    {
        void RegisterVisitor(ROXInterface<ROXUserBean> callback);

        void RegisterWithFacebook(string openId, string token, ROXInterface<ROXUserBean> callback);

        void RegisterWithGoogle(String token, ROXInterface<ROXUserBean> callback);

        void RegisterByApple(string appleName, string token, ROXInterface<ROXUserBean> callback);

        void RegisterWithWechat(string wxAppId, string wxCode, ROXInterface<ROXUserBean> callback);

        void StartBindAccount(string type, string appid, string token, ROXInterface<ROXUserBean> callback);

        void GetUserInfo(ROXInterface<ROXUserBean> callback);

        void GetUserInfoByUserId(string userId, ROXInterface<ROXUserBean> callback);

        void StartRetrieveInviter(ROXInterface<ROXUserBeanBase> callback);

        void Logout(ROXInterface<bool> callback);

        void BindInviter(string inviterUid, ROXInterface<bool> callback);

        void GetUserExternalInfo(ROXInterface<ROXUserExternalInfo> callback);

        void GetAPAuthInfo(ROXInterface<string> callback);

        void BindWallet(String type, string walletInfo, ROXInterface<bool> callback);
    }
}