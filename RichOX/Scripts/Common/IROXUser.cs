using ROXBase.Api;
using System;
using System.Collections.Generic;


namespace ROXBase.Common
{
    public interface IROXUser
    {
        void RegisterVisitor(string source, ROXInterface<ROXUserInfo> callback);
        
        void StartBindAccount(string type, string appid, string token, ROXInterface<ROXUserInfo> callback);

        void RegisterWithWechat(string wxAppId, string wxCode, string source, ROXInterface<ROXUserInfo> callback);

        void GetUserInfo(ROXInterface<ROXUserInfo> callback);

        void GetUserRanking(int count, int accountType, string rankingType, ROXInterface<List<ROXUserInfo>> callback);

        void GetSpecificUsersInfo(List<string> userList, ROXInterface<List<ROXUserInfoSimple>> callback);

        void GetUserToken(ROXInterface<ROXUserToken> callback);

        void Logout(ROXInterface<bool> callback);

        void BindInviter(string inviterUid, ROXInterface<bool> callback);
    }
}