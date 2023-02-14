using System.Runtime.InteropServices;
using System.Collections.Generic;
using ROXBase.Api;
using ROXBase.Common;
using System;
using AOT;

namespace ROXBase.Platforms.iOS {
    public class RichOXUserClient : IROXUser {

        static RichOXUserClient sInstance = new RichOXUserClient();

        public static RichOXUserClient Instance {
            get {
                return sInstance;
            }
        }

        internal delegate void RichOXUserGetUserDataSuccessCallback(IntPtr userData);
        internal delegate void RichOXUserBindUserSuccessCallback(IntPtr userBindResult);
        internal delegate void RichOXUserGetUsersBasicDataSuccessCallback(IntPtr userLists);

        internal delegate void RichOXUnityCommonSuccessCallback();
        internal delegate void RichOXUnityFailureCallback(int code, string message);

        private ROXInterface<ROXUserInfo> registerVisitorCallback;
        private ROXInterface<ROXUserInfo> startBindAccountCallback;
        private ROXInterface<ROXUserInfo> registerWithWechatCallback;
        private ROXInterface<ROXUserInfo> getUserInfoCallback;
        private ROXInterface<List<ROXUserInfoSimple>> getSpecificUsersInfoCallback;
        private ROXInterface<bool> logOutCallback;
        private ROXInterface<bool> bindInviterCallback;

        public void RegisterVisitor(string source, ROXInterface<ROXUserInfo> callback) {
            registerVisitorCallback = callback;
            Externs.RichOXUserRegisterUserId(source, IntPtr.Zero, userGetUserDataSuccessCallback, userGetUserDataFailedCallback);
        }
        
        public void StartBindAccount(string type, string appid, string token, ROXInterface<ROXUserInfo> callback) {
            startBindAccountCallback = callback;
            Externs.RichOXUserBindUser(appid, type, token,  userBindUserSuccessCallback, userBindUserFailedCallback);
        }

        public void RegisterWithWechat(string wxAppId, string wxCode, string source, ROXInterface<ROXUserInfo> callback) {
            registerWithWechatCallback = callback;
            Externs.RichOXUserRegisterByWeChat(wxAppId, wxCode, source, registerWithWechatSuccessCallback, registerWithWechatFailedCallback);
        }

        public void GetUserInfo(ROXInterface<ROXUserInfo> callback) {
            getUserInfoCallback = callback;
            Externs.RichOXUserGetUserInfo(getUserInfoSuccessCallback, getUserInfoFailedCallback);
        }

        public void GetUserRanking(int count, int accountType, string rankingType, ROXInterface<List<ROXUserInfo>> callback) {

        }

        public void GetSpecificUsersInfo(List<string> userList, ROXInterface<List<ROXUserInfoSimple>> callback) {
            getSpecificUsersInfoCallback = callback;
            Externs.RichOXUserGetUserBaseInfo(userList.ToArray(), userList.Count, getSpecificUsersInfoSuccessCallback, getSpecificUsersInfoFailedCallback);
        }

        public void GetUserToken(ROXInterface<ROXUserToken> callback) {

        }

        public void Logout(ROXInterface<bool> callback) {
            logOutCallback = callback;
            Externs.RichOXUserLogOutUser(logOutSuccessCallback, logOutFailedCallback);
        }

        public void BindInviter(string inviterUid, ROXInterface<bool> callback)
        {
            bindInviterCallback = callback;
            Externs.RichOXUserBindInviter(inviterUid, bindInviterSuccessCallback, bindInviterFailedCallback);
        }

       #region callback methods
        [MonoPInvokeCallback(typeof(RichOXUserGetUserDataSuccessCallback))]
        public static void userGetUserDataSuccessCallback(IntPtr userData) {
            if(Instance.registerVisitorCallback != null) {
                ROXUserInfo userInfo = new ROXUserInfo();
                userInfo.UserId = Externs.RichOXUserDataGetUserId(userData);
                userInfo.Name = Externs.RichOXUserDataGetName(userData);
                userInfo.Avatar = Externs.RichOXUserDataGetAvatar(userData);
                userInfo.DeviceId = Externs.RichOXUserDataGetDeviceId(userData);
                userInfo.WechatAppId = Externs.RichOXUserDataGetWechatAppId(userData);
                userInfo.WechatOpenId = Externs.RichOXUserDataGetWechatOpenId(userData);
                userInfo.WechatBindTime = Externs.RichOXUserDataGetWechatBoundAt(userData);
                userInfo.GetInstallAt = Externs.RichOXUserDataGetInstallAtTime(userData);
                userInfo.IsHasWithdraw = Externs.RichOXUserDataHasWithdraw(userData);
                userInfo.IsNew = Externs.RichOXUserDataGetIsNew(userData);

                Instance.registerVisitorCallback.OnSuccess(userInfo);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        public static void userGetUserDataFailedCallback(int code, string message) {
            if(Instance.registerVisitorCallback != null) {
                Instance.registerVisitorCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUserBindUserSuccessCallback))]
        public static void userBindUserSuccessCallback(IntPtr userBindResult) {
            if(Instance.startBindAccountCallback != null) {
                ROXUserInfo userInfo = new ROXUserInfo();

                IntPtr userData = Externs.RichOXUserBindResultGetUserData(userBindResult);
                userInfo.UserId = Externs.RichOXUserDataGetUserId(userData);
                userInfo.Name = Externs.RichOXUserDataGetName(userData);
                userInfo.Avatar = Externs.RichOXUserDataGetAvatar(userData);
                userInfo.DeviceId = Externs.RichOXUserDataGetDeviceId(userData);
                userInfo.GetInstallAt = Externs.RichOXUserDataGetInstallAtTime(userData);
                userInfo.IsHasWithdraw = Externs.RichOXUserDataHasWithdraw(userData);
                userInfo.IsNew = Externs.RichOXUserDataGetIsNew(userData);
                userInfo.WechatBindTime = Externs.RichOXUserDataGetWechatBoundAt(userData);

                IntPtr wechatInfo = Externs.RichOXUserBindResultGetUserSocailAccountInfo(userBindResult);

                userInfo.WechatAppId = Externs.RichOXUserSocialAccountGetWechatAppId(wechatInfo);
                userInfo.WechatOpenId = Externs.RichOXUserSocialAccountGetWechatUnionId(wechatInfo);
                
                Instance.startBindAccountCallback.OnSuccess(userInfo);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        public static void userBindUserFailedCallback(int code, string message) {
            if(Instance.startBindAccountCallback != null) {
                Instance.startBindAccountCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUserBindUserSuccessCallback))]
        public static void registerWithWechatSuccessCallback(IntPtr userBindResult) {
            if(Instance.registerWithWechatCallback != null) {
                ROXUserInfo userInfo = new ROXUserInfo();

                IntPtr userData = Externs.RichOXUserBindResultGetUserData(userBindResult);
                userInfo.UserId = Externs.RichOXUserDataGetUserId(userData);
                userInfo.Name = Externs.RichOXUserDataGetName(userData);
                userInfo.Avatar = Externs.RichOXUserDataGetAvatar(userData);
                userInfo.DeviceId = Externs.RichOXUserDataGetDeviceId(userData);
                userInfo.GetInstallAt = Externs.RichOXUserDataGetInstallAtTime(userData);
                userInfo.IsHasWithdraw = Externs.RichOXUserDataHasWithdraw(userData);
                userInfo.IsNew = Externs.RichOXUserDataGetIsNew(userData);
                userInfo.WechatBindTime = Externs.RichOXUserDataGetWechatBoundAt(userData);

                IntPtr wechatInfo = Externs.RichOXUserBindResultGetUserSocailAccountInfo(userBindResult);

                userInfo.WechatAppId = Externs.RichOXUserSocialAccountGetWechatAppId(wechatInfo);
                userInfo.WechatOpenId = Externs.RichOXUserSocialAccountGetWechatUnionId(wechatInfo);
                
                Instance.registerWithWechatCallback.OnSuccess(userInfo);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        public static void registerWithWechatFailedCallback(int code, string message) {
            if(Instance.registerWithWechatCallback != null) {
                Instance.registerWithWechatCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUserGetUserDataSuccessCallback))]
        public static void getUserInfoSuccessCallback(IntPtr userData) {
            if(Instance.getUserInfoCallback != null) {
                ROXUserInfo userInfo = new ROXUserInfo();
                userInfo.UserId = Externs.RichOXUserDataGetUserId(userData);
                userInfo.Name = Externs.RichOXUserDataGetName(userData);
                userInfo.Avatar = Externs.RichOXUserDataGetAvatar(userData);
                userInfo.DeviceId = Externs.RichOXUserDataGetDeviceId(userData);
                userInfo.WechatAppId = Externs.RichOXUserDataGetWechatAppId(userData);
                userInfo.WechatOpenId = Externs.RichOXUserDataGetWechatOpenId(userData);
                userInfo.WechatBindTime = Externs.RichOXUserDataGetWechatBoundAt(userData);
                userInfo.GetInstallAt = Externs.RichOXUserDataGetInstallAtTime(userData);
                userInfo.IsHasWithdraw = Externs.RichOXUserDataHasWithdraw(userData);
                userInfo.IsNew = Externs.RichOXUserDataGetIsNew(userData);

                Instance.getUserInfoCallback.OnSuccess(userInfo);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        public static void getUserInfoFailedCallback(int code, string message) {
            if(Instance.getUserInfoCallback != null) {
                Instance.getUserInfoCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUserGetUsersBasicDataSuccessCallback))]
        public static void getSpecificUsersInfoSuccessCallback(IntPtr userLists) {
            if(Instance.getSpecificUsersInfoCallback != null ) {
                List<ROXUserInfoSimple> list = new List<ROXUserInfoSimple>();
                int size = Externs.getUserBasicDataInfoCountFromArray(userLists);
                for (int i=0; i<size; i++) 
                {
                    IntPtr userInfo = Externs.getUserBasicDataInfoFromArray(userLists, i);
                    ROXUserInfoSimple simpleUserInfo = new ROXUserInfoSimple();
                    simpleUserInfo.UserId = Externs.getUserIdFromUserBasicDataInfo(userInfo);
                    simpleUserInfo.Name = Externs.getUserNameFromUserBasicDataInfo(userInfo);
                    simpleUserInfo.Avatar = Externs.getUserAvatarFromUserBasicDataInfo(userInfo);

                    list.Add(simpleUserInfo);
                }

                Instance.getSpecificUsersInfoCallback.OnSuccess(list);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        public static void getSpecificUsersInfoFailedCallback(int code, string message) {
            if(Instance.getSpecificUsersInfoCallback != null) {
                Instance.getSpecificUsersInfoCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityCommonSuccessCallback))]
        public static void logOutSuccessCallback() {
            if(Instance.logOutCallback != null) {
                Instance.logOutCallback.OnSuccess(true);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        public static void logOutFailedCallback(int code, string message) {
            if(Instance.logOutCallback != null) {
                Instance.logOutCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityCommonSuccessCallback))]
        public static void bindInviterSuccessCallback() {
            if(Instance.bindInviterCallback != null) {
                Instance.bindInviterCallback.OnSuccess(true);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        public static void bindInviterFailedCallback(int code, string message) {
            if(Instance.bindInviterCallback != null) {
                Instance.bindInviterCallback.OnFailed(code, message);
            }
        }

        #endregion


        private static RichOXUserClient IntPtrToRichOXUserClient(IntPtr richOXClient)
        {
            GCHandle handle = (GCHandle)richOXClient;
            return handle.Target as RichOXUserClient;
        }
    }
}