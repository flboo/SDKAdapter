using ROXBase.Api;
using System;
using System.Collections.Generic;

namespace ROXBase.Common
{
    public class DummyROXUserClient : IROXUser
    {
        #region IROXUser

        public void RegisterVisitor(string source, ROXInterface<ROXUserInfo> callback) 
        {   
            ROXUserInfo userInfo = generateDummyUser();
            callback.OnSuccess(userInfo);
        }

        public void StartBindAccount(string type, string appid, string token, ROXInterface<ROXUserInfo> callback)
        {
            ROXUserInfo userInfo = generateDummyUser();
            callback.OnSuccess(userInfo);
        }

        public void RegisterWithWechat(string wxAppId, string wxCode, string source, ROXInterface<ROXUserInfo> callback)
        {
            ROXUserInfo userInfo = generateDummyUser();
            callback.OnSuccess(userInfo);
        }

        public void GetUserInfo(ROXInterface<ROXUserInfo> callback) 
        {
            ROXUserInfo userInfo = generateDummyUser();
            callback.OnSuccess(userInfo);
        }

        public void GetUserRanking(int count, int accountType, string rankingType, ROXInterface<List<ROXUserInfo>> callback)
        {
            ROXUserInfo userInfo = generateDummyUser();

            List<ROXUserInfo> list = new List<ROXUserInfo>();
            list.Add(userInfo);
            callback.OnSuccess(list);
        }

        public void GetSpecificUsersInfo(List<string> userList, ROXInterface<List<ROXUserInfoSimple>> callback) 
        {
            ROXUserInfoSimple simpleUser = generateDummyROXUserInfoSimple();

            List<ROXUserInfoSimple> list = new List<ROXUserInfoSimple>();
            list.Add(simpleUser);
            ROXUserInfoSimple simple2 = new ROXUserInfoSimple();
            simple2.UserId = "6823c6e402f8b373";
            simple2.Name = "池梓立";
            simple2.Avatar = "https://thirdwx.qlogo.cn/mmopen/vi_32/L0xciabg2sBvFiavgbwuba81vIibhtEWoPwwqOlliaqoVnjDwKd5B4UDAxgOcjpo3EnqoqhhWRQ64K0Kkpz88qFYQQ/132";
            list.Add(simple2);
            callback.OnSuccess(list);
        }

        public void GetUserToken(ROXInterface<ROXUserToken> callback) 
        {
            ROXUserToken token = new ROXUserToken();
            token.UserId = "c3b609ab75675697";
            token.Token = "a74060f759a27130";
            token.RefreshTime = 1619089760;
            callback.OnSuccess(token);
        }

        public void Logout(ROXInterface<bool> callback) 
        {
            callback.OnSuccess(true);
        }

        public void BindInviter(string inviterUid, ROXInterface<bool> callback)
        {
            callback.OnSuccess(true);
        }

        #endregion

        private ROXUserInfo generateDummyUser() 
        {
            ROXUserInfo userInfo = new ROXUserInfo();
            userInfo.UserId = "c3b609ab75675697";
            userInfo.Name = "游客vjguqwx8";
            userInfo.Avatar = "";
            userInfo.DeviceId = "test_zly_user_1";
            userInfo.WechatOpenId = "";
            userInfo.WechatAppId = "";
            userInfo.WechatBindTime = "";
            userInfo.GetInstallAt = "1970-01-19T15:13:09.135Z";

            userInfo.IsEnabled = true;
            userInfo.IsHasWithdraw = true;
            userInfo.IsNew = true;
            return userInfo;
        }

        private ROXUserInfo generateDummyUser2() 
        {
            ROXUserInfo userInfo = new ROXUserInfo();
            userInfo.UserId = "1a381fd7376e4a6b";
            userInfo.Name = "Xsheen";
            userInfo.Avatar = "https://thirdwx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTJAcwWL7LxyaypibxHHR4mFs01hcbBpOMTA4jtIH8ImXIqkbxyNiaWZMdCiamzS2I5bLLbqEeTUvPNsw/132";
            userInfo.DeviceId = "test_zly_user_1";
            userInfo.WechatOpenId = "";
            userInfo.WechatAppId = "";
            userInfo.WechatBindTime = "";
            userInfo.GetInstallAt = "1970-01-19T15:13:09.135Z";

            userInfo.IsEnabled = true;
            userInfo.IsHasWithdraw = true;
            userInfo.IsNew = true;
            return userInfo;
        }

        private ROXUserInfoSimple generateDummyROXUserInfoSimple() 
        {
            ROXUserInfoSimple simple = new ROXUserInfoSimple();
            simple.UserId = "1a381fd7376e4a6b";
            simple.Name = "Xsheen";
            simple.Avatar = "https://thirdwx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTJAcwWL7LxyaypibxHHR4mFs01hcbBpOMTA4jtIH8ImXIqkbxyNiaWZMdCiamzS2I5bLLbqEeTUvPNsw/132";
            return simple;
        }
    }
}