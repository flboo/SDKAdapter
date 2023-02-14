using ROXBase.Api;
using System;
using System.Collections.Generic;

namespace ROXBase.Common
{
    public class DummyRichOXUserManager : IRichOXUserManager
    {
        #region IRichOXUserManager

        public void RegisterVisitor(ROXInterface<ROXUserBean> callback)
        {
            ROXUserBean userInfo = generateDummyUser();
            callback.OnSuccess(userInfo);
        }

        public void RegisterWithFacebook(string openId, string token, ROXInterface<ROXUserBean> callback)
        {
            ROXUserBean userInfo = generateFacebookUser();
            callback.OnSuccess(userInfo);
        }

        public void RegisterWithGoogle(String token, ROXInterface<ROXUserBean> callback)
        {
            ROXUserBean userInfo = generateGoogleUser();
            callback.OnSuccess(userInfo);
        }

        public void RegisterByApple(string appleName, string token, ROXInterface<ROXUserBean> callback)
        {
            ROXUserBean userInfo = generateAppleUser();
            callback.OnSuccess(userInfo);
        }

        public void RegisterWithWechat(string wxAppId, string wxCode, ROXInterface<ROXUserBean> callback)
        {
            ROXUserBean userInfo = generateWechatUser();
            callback.OnSuccess(userInfo);
        }

        public void StartBindAccount(string type, string appid, string token, ROXInterface<ROXUserBean> callback)
        {
            ROXUserBean userInfo = generateGoogleUser();
            callback.OnSuccess(userInfo);
        }

        public void GetUserInfo(ROXInterface<ROXUserBean> callback)
        {
            ROXUserBean userInfo = generateDummyUser();
            callback.OnSuccess(userInfo);
        }

        public void GetUserInfoByUserId(string userId, ROXInterface<ROXUserBean> callback)
        {
            ROXUserBean userInfo = generateDummyUser();
            callback.OnSuccess(userInfo);
        }

        public void StartRetrieveInviter(ROXInterface<ROXUserBeanBase> callback)
        {
            ROXUserBeanBase userInfo = generateDummyUserBase();
            callback.OnSuccess(userInfo);
        }

        public void Logout(ROXInterface<bool> callback)
        {
            callback.OnSuccess(true);
        }
        public void BindInviter(string inviterUid, ROXInterface<bool> callback)
        {
            callback.OnSuccess(true);
        }
        public void GetUserExternalInfo(ROXInterface<ROXUserExternalInfo> callback) 
        {
            ROXUserExternalInfo userInfo = generateDummyUserExtInfo();
            callback.OnSuccess(userInfo);
        }

        public void GetAPAuthInfo(ROXInterface<string> callback) 
        {
            string info = "apiname=com.alipay.account.auth&app_id=2021002112689xx8&app_name=mc&auth_type=AUTHACCOUNT&biz_type=openservice&methodname=alipay.open.auth.sdk.code.get&pid=20885315387xxx721&product_id=APP_FAST_LOGIN&scope=kuaijie&sign_type=RSA2&target_id=1608174601461&sign=kLS23wK0goe6xlBUpVTiXOnXtO%2BdtBm9MsXnXV6ftRvq4CbSIWgQ%2F%2FqCpeHp%2BQOev4UeQ0FBgxv665tQhb%2Fjcu2zdi6q0vcuM48QSS1HmpQY7rYyS%2Bk0ZxTj%2BJVlFStP9TodnYj%2F5wLVkVMXfNiu%2B1bx1%2FZu7Za1W%2FWjc57rsHKTG5gCDDzj9twZRaMB9tFPfQ51s1IIr1LlOuo8GVfdyQcIEiOcqcsoX8Em62%2FyDABKKUOf8VvkFLnT%2B64CcC1f%2FoqXVV8zViXCD%2BfJePE%2BHzUZD%2BI8WMlxW77kiGcTGTWBRyclg9FD%2FxGgzFAHRRSjPbQUkdyuZ5puJU6DvUXAzA%3D%3D";
            callback.OnSuccess(info);
        }

        public void BindWallet(String type, string walletInfo, ROXInterface<bool> callback) 
        {
            callback.OnSuccess(true);
        }


        #endregion

        private ROXUserBeanBase generateDummyUserBase()
        {
            ROXUserBeanBase userInfo = new ROXUserBeanBase();
            userInfo.Id = "c3b609ab75675697";
            userInfo.Name = "testUser";
            userInfo.Avatar = "https://thirdwx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTJAcwWL7LxyaypibxHHR4mFs01hcbBpOMTA4jtIH8ImXIqkbxyNiaWZMdCiamzS2I5bLLbqEeTUvPNsw/132";
            userInfo.CreateAt = 1625897977435;
            userInfo.SeverTime = 1625897977437;
            return userInfo;
        }

        private ROXUserBean generateDummyUser()
        {
            ROXUserBean userInfo = new ROXUserBean();
            userInfo.Id = "c3b609ab75675697";
            userInfo.Name = "testUser";
            userInfo.Avatar = "https://thirdwx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTJAcwWL7LxyaypibxHHR4mFs01hcbBpOMTA4jtIH8ImXIqkbxyNiaWZMdCiamzS2I5bLLbqEeTUvPNsw/132";
            userInfo.DeviceId = "test_zly_user_1";
            userInfo.CountryCode = "CN";
            userInfo.IsHasWithdraw = false;
            userInfo.IsNew = false;
            List<string> walletList = new List<string>();
            walletList.Add("ps");
            userInfo.WalletList = walletList;
            userInfo.CreateAt = 1625897977435;
            userInfo.InstallAt = 1625897977437;
            userInfo.SeverTime = 1625897977437;
            userInfo.InviterId = "";
            userInfo.InvitationCode = "1a381fd7376e4a6b";
            return userInfo;
        }

        private ROXUserBean generateAppleUser()
        {
            ROXUserBean commonUser = generateDummyUser();
            commonUser.AppleInfoCurrent.AppleName = "testApple";
            commonUser.AppleInfoCurrent.AppleSub = "testAppleSub";
            return commonUser;
        }

        private ROXUserBean generateWechatUser()
        {
            ROXUserBean commonUser = generateDummyUser();
            commonUser.WechatInfoCurrent.Avatar = "https://thirdwx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTJAcwWL7LxyaypibxHHR4mFs01hcbBpOMTA4jtIH8ImXIqkbxyNiaWZMdCiamzS2I5bLLbqEeTUvPNsw/132";
            commonUser.WechatInfoCurrent.NickName = "Raunch";
            commonUser.WechatInfoCurrent.WXAppId = "wxf4443d19c4266f6b";
            commonUser.WechatInfoCurrent.OpenId = "0xefeeflwelelidke";
            commonUser.WechatInfoCurrent.UnionId = "124nidefosdniefefsxod32";
            return commonUser;
        }

        private ROXUserBean generateGoogleUser()
        {
            ROXUserBean commonUser = generateDummyUser();
            commonUser.GoogleInfoCurrent.Email = "test@gmail.com";
            commonUser.GoogleInfoCurrent.FamilyName = "Jordan";
            commonUser.GoogleInfoCurrent.GoogleName = "Mike";
            commonUser.GoogleInfoCurrent.GivenName = "Lala";
            commonUser.GoogleInfoCurrent.GoogleSub = "1a381fd7376e4a6b";
            return commonUser;
        }

        private ROXUserBean generateFacebookUser()
        {
            ROXUserBean commonUser = generateDummyUser();
            commonUser.FacebookInfoCurrent.Email = "test@gmail.com";
            commonUser.FacebookInfoCurrent.FBName = "Mike";
            commonUser.FacebookInfoCurrent.FBOpenId = "1dec22edki567kll";
            commonUser.FacebookInfoCurrent.FirstName = "Mike";
            commonUser.FacebookInfoCurrent.LastName = "Jordan";
            return commonUser;
        }

        private ROXUserExternalInfo generateDummyUserExtInfo()
        {
            ROXUserExternalInfo userInfo = new ROXUserExternalInfo();
            List<string> pays = new List<string>();
            pays.Add("11101");
            pays.Add("11102");
            userInfo.PayMethods = pays;
            userInfo.IsBindWX = false;
            userInfo.WxNickName = "wxtest";
            userInfo.IsBindAP = false;
            userInfo.ApNickName = "aptest";
            return userInfo;
        }
    }
}