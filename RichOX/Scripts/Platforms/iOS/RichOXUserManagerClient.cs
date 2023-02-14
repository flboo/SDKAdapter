using System.Collections.Generic;
using System.Runtime.InteropServices;
using ROXBase.Api;
using System;
using AOT;
using ROXBase.Common;

namespace ROXBase.Platforms.iOS
{
    public class RichOXUserManagerClient : IRichOXUserManager
    {

        static RichOXUserManagerClient sInstance = new RichOXUserManagerClient();

        public static RichOXUserManagerClient Instance
        {
            get
            {
                return sInstance;
            }
        }

        internal delegate void RichOXUserGetUserInfoSuccessCallback(IntPtr userData);
        internal delegate void RichOXUserGetInviterInfoSuccessCallback(IntPtr inviterInfo);

        internal delegate void RichOXUnityCommonSuccessCallback();
        internal delegate void RichOXUnityFailureCallback(int code, string message);

        internal delegate void RichOXUserGetUserExternalInfoSuccessCallback(IntPtr extInfo);
        internal delegate void RichOXUserGetAPAuthInfoSuccessCallback(string authInfo);

        private ROXInterface<ROXUserBean> registerVisitorCallback;
        private ROXInterface<ROXUserBean> registerWithFacebookCallback;
        private ROXInterface<ROXUserBean> registerWithGoogleCallback;
        private ROXInterface<ROXUserBean> registerByAppleCallback;
        private ROXInterface<ROXUserBean> registerByWechatCallback;
        private ROXInterface<ROXUserBean> startBindAccountCallback;
        private ROXInterface<ROXUserBean> getUserInfoCallback;
        private ROXInterface<ROXUserBean> getSpecialUserInfoCallback;
        private ROXInterface<ROXUserBeanBase> getInviterCallback;
        private ROXInterface<bool> logOutCallback;
        private ROXInterface<bool> bindInviterCallback;
        private ROXInterface<ROXUserExternalInfo> getUserExtInfoCallback;
        private ROXInterface<string> getAPAuthCallback;
        private ROXInterface<bool> bindWalletCallback;


        public void RegisterVisitor(ROXInterface<ROXUserBean> callback)
        {
            registerVisitorCallback = callback;
            Externs.RichOXUserManagerRegisterUserId(registerVisitorSuccessCallback, registerVisitorFailedCallback);
        }

        public void RegisterWithFacebook(string openId, string token, ROXInterface<ROXUserBean> callback)
        {
            registerWithFacebookCallback = callback;
            Externs.RichOXUserManagerRegisterByFacebook(openId, token, registerWithFacebookSuccessCallback, registerWithFacebookFailedCallback);
        }

        public void RegisterWithGoogle(String token, ROXInterface<ROXUserBean> callback)
        {
            registerWithGoogleCallback = callback;
            Externs.RichOXUserManagerRegisterByGoogle(token, registerWithGoogleSuccessCallback, registerWithGoogleFailedCallback);
        }

        public void RegisterByApple(string appleName, string token, ROXInterface<ROXUserBean> callback)
        {
            registerByAppleCallback = callback;
            Externs.RichOXUserManagerRegisterByApple(appleName, token, registerByAppleSuccessCallback, registerByAppleFailedCallback);
        }

        public void RegisterWithWechat(string wxAppId, string wxCode, ROXInterface<ROXUserBean> callback)
        {
            registerByWechatCallback = callback;
            Externs.RichOXUserManagerRegisterByWeChat(wxAppId, wxCode, registerByWechatSuccessCallback, registerByWechatFailedCallback);
        }

        public void StartBindAccount(string type, string appid, string token, ROXInterface<ROXUserBean> callback)
        {
            startBindAccountCallback = callback;
            Externs.RichOXUserManagerBindUser(appid, type, token, userBindUserSuccessCallback, userBindUserFailedCallback);
        }

        public void GetUserInfo(ROXInterface<ROXUserBean> callback)
        {
            getUserInfoCallback = callback;
            Externs.RichOXUserManagerGetUserInfo(getUserInfoSuccessCallback, getUserInfoFailedCallback);
        }

        public void GetUserInfoByUserId(string userId, ROXInterface<ROXUserBean> callback) {
            getSpecialUserInfoCallback = callback;
            Externs.RichOXUserManagerGetUserInfoByUserId(userId, getUserInfoByUserIdSuccessCallback, getUserInfoByUserIdFailedCallback);
        }

        public void StartRetrieveInviter(ROXInterface<ROXUserBeanBase> callback)
        {
            getInviterCallback = callback;
            Externs.RichOXUserManagerGetInviter(getInviterInfoSuccessCallback, getInviterInfoFailedCallback);
        }

        public void Logout(ROXInterface<bool> callback)
        {
            logOutCallback = callback;
            Externs.RichOXUserManagerLogOutUser(logOutSuccessCallback, logOutFailedCallback);
        }

        public void BindInviter(string inviterUid, ROXInterface<bool> callback)
        {
            bindInviterCallback = callback;
            Externs.RichOXUserManagerBindInviter(inviterUid, bindInviterSuccessCallback, bindInviterFailedCallback);
        }

        public void GetUserExternalInfo(ROXInterface<ROXUserExternalInfo> callback)
        {
            getUserExtInfoCallback = callback;
            Externs.RichOXUserManagerGetUserExternalInfo(getUserExternalInfoSuccessCallback, getUserExternalInfoFailedCallback);
        }

        public void GetAPAuthInfo(ROXInterface<string> callback) 
        {
            getAPAuthCallback = callback;
            Externs.RichOXUserManagerGetAPAuthInfo(getAPAuthSuccessCallback, getAPAuthFailedCallback);
        }

        public void BindWallet(string type, string walletInfo, ROXInterface<bool> callback) 
        {
            bindWalletCallback = callback;
            Externs.RichOXUserManagerBindWallet(type, walletInfo, bindWalletSuccessCallback, bindWalletFailedCallback);
        }

        private static ROXUserBean GenerateROXUserBean(IntPtr userInfo)
        {
            ROXUserBean roxUserInfo = new ROXUserBean();
            roxUserInfo.Id = Externs.RichOXUserObjectGetUserId(userInfo);
            roxUserInfo.Name = Externs.RichOXUserObjectGetName(userInfo);
            roxUserInfo.Avatar = Externs.RichOXUserObjectGetAvatar(userInfo);
            roxUserInfo.DeviceId = Externs.RichOXUserObjectGetDeviceId(userInfo);
            roxUserInfo.CountryCode = Externs.RichOXUserObjectGetConutryCode(userInfo);
            roxUserInfo.IsHasWithdraw = Externs.RichOXUserObjectGetHasWithdraw(userInfo);
            roxUserInfo.InstallAt = Externs.RichOXUserObjectGetInstallAt(userInfo);
            roxUserInfo.CreateAt = Externs.RichOXUserObjectGetCreateAt(userInfo);
            roxUserInfo.SeverTime = Externs.RichOXUserObjectGetServerNow(userInfo);
            roxUserInfo.InviterId = Externs.RichOXUserObjectGetInviterId(userInfo);
            roxUserInfo.InvitationCode = Externs.RichOXUserObjectGetInvitationCode(userInfo);

            IntPtr wallets = Externs.RichOXUserObjectGetWallets(userInfo);
            List<string> walletList = new List<string>(); 
            if (wallets != null) 
            {
                int size = Externs.RichOXArrayGetCount(wallets);
                for (int i=0; i<size; i++) 
                {
                    string wallet = Externs.RichOXArrayGetStringItem(wallets, i);
                    walletList.Add(wallet);
                }
            }

            roxUserInfo.WalletList = walletList;

            IntPtr googleObjectInfo = Externs.RichOXUserObjectGetGoogleAccountInfo(userInfo);
            if (googleObjectInfo != null)
            {
                GoogleInfo googleInfo = new GoogleInfo();
                googleInfo.Email = Externs.RichOXGoogleAccountInfoGetEmail(googleObjectInfo);
                googleInfo.FamilyName = Externs.RichOXGoogleAccountInfoGetFamilyName(googleObjectInfo);
                googleInfo.GoogleName = Externs.RichOXGoogleAccountInfoGetGoogleName(googleObjectInfo);
                googleInfo.GivenName = Externs.RichOXGoogleAccountInfoGetGivenName(googleObjectInfo);
                googleInfo.GoogleSub = Externs.RichOXGoogleAccountInfoGetGoogleSub(googleObjectInfo);
                roxUserInfo.GoogleInfoCurrent = googleInfo;
            }

            IntPtr facebookInfoObject = Externs.RichOXUserObjectGetFacebookAccountInfo(userInfo);
            if (facebookInfoObject != null)
            {
                FacebookInfo facebookInfo = new FacebookInfo();
                facebookInfo.Email = Externs.RichOXFacebookAccountInfoGetEmail(facebookInfoObject);
                facebookInfo.FBName = Externs.RichOXFacebookAccountInfoGetFBName(facebookInfoObject);
                facebookInfo.FBOpenId = Externs.RichOXFacebookAccountInfoGetOpenId(facebookInfoObject);
                facebookInfo.FirstName = Externs.RichOXFacebookAccountInfoGetFirstName(facebookInfoObject);
                facebookInfo.LastName = Externs.RichOXFacebookAccountInfoGetLastName(facebookInfoObject);
                roxUserInfo.FacebookInfoCurrent = facebookInfo;
            }

            IntPtr appleInfoObject = Externs.RichOXUserObjectGetAppleAccountInfo(userInfo);
            if (appleInfoObject != null)
            {
                AppleInfo appleInfo = new AppleInfo();
                appleInfo.AppleName = Externs.RichOXAppleAccountInfoGetAppleName(appleInfoObject);
                appleInfo.AppleSub = Externs.RichOXAppleAccountInfoGetAppleSub(appleInfoObject);
                roxUserInfo.AppleInfoCurrent = appleInfo;
            }

            IntPtr wxInfoObject = Externs.RichOXUserObjectGetWechatAccountInfo(userInfo); 
            if (wxInfoObject != null) {
                WechatInfo wechatInfo = new WechatInfo();
                wechatInfo.WXAppId = Externs.RichOXWechatAccountInfoGetAppId(wxInfoObject);
                wechatInfo.NickName = Externs.RichOXWechatAccountInfoGetNickName(wxInfoObject);
                wechatInfo.Avatar = Externs.RichOXWechatAccountInfoGetAvatar(wxInfoObject);
                wechatInfo.OpenId = Externs.RichOXWechatAccountInfoGetOpenId(wxInfoObject);
                wechatInfo.UnionId = Externs.RichOXWechatAccountInfoGetUnionId(wxInfoObject);
                roxUserInfo.WechatInfoCurrent = wechatInfo;
            }

            IntPtr apyInfoObject = Externs.RichOXUserObjectGetAPAccountInfo(userInfo); 
            if (apyInfoObject != null) {
                APInfo apInfo = new APInfo();
                apInfo.APName = Externs.RichOXAPAccountInfoGetAPName(apyInfoObject);
                apInfo.APId = Externs.RichOXAPAccountInfoGetAPId(apyInfoObject);
                roxUserInfo.APInfoCurrent = apInfo;
            }

            return roxUserInfo;
        }

        private static ROXUserBeanBase GenerateROXUserBeanBase(IntPtr userInfo)
        {
            ROXUserBeanBase roxUserInfo = new ROXUserBeanBase();
            roxUserInfo.Id = Externs.RichOXUserObjectGetUserId(userInfo);
            roxUserInfo.Name = Externs.RichOXUserObjectGetName(userInfo);
            roxUserInfo.Avatar = Externs.RichOXUserObjectGetAvatar(userInfo);
            roxUserInfo.CreateAt = Externs.RichOXUserObjectGetCreateAt(userInfo);
            roxUserInfo.SeverTime = Externs.RichOXUserObjectGetServerNow(userInfo);

            return roxUserInfo;
        }

        #region callback methods
        [MonoPInvokeCallback(typeof(RichOXUserGetUserInfoSuccessCallback))]
        private static void registerVisitorSuccessCallback(IntPtr userData)
        {
            if (Instance.registerVisitorCallback != null)
            {
                ROXUserBean userInfo = GenerateROXUserBean(userData);
                Instance.registerVisitorCallback.OnSuccess(userInfo);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        private static void registerVisitorFailedCallback(int code, string message)
        {
            if (Instance.registerVisitorCallback != null)
            {
                Instance.registerVisitorCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUserGetUserInfoSuccessCallback))]
        private static void registerWithFacebookSuccessCallback(IntPtr userData)
        {
            if (Instance.registerWithFacebookCallback != null)
            {
                ROXUserBean userInfo = GenerateROXUserBean(userData);
                Instance.registerWithFacebookCallback.OnSuccess(userInfo);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        private static void registerWithFacebookFailedCallback(int code, string message)
        {
            if (Instance.registerWithFacebookCallback != null)
            {
                Instance.registerWithFacebookCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUserGetUserInfoSuccessCallback))]
        private static void registerWithGoogleSuccessCallback(IntPtr userData)
        {
            if (Instance.registerWithGoogleCallback != null)
            {
                ROXUserBean userInfo = GenerateROXUserBean(userData);
                Instance.registerWithGoogleCallback.OnSuccess(userInfo);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        private static void registerWithGoogleFailedCallback(int code, string message)
        {
            if (Instance.registerWithGoogleCallback != null)
            {
                Instance.registerWithGoogleCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUserGetUserInfoSuccessCallback))]
        private static void registerByAppleSuccessCallback(IntPtr userData)
        {
            if (Instance.registerByAppleCallback != null)
            {
                ROXUserBean userInfo = GenerateROXUserBean(userData);
                Instance.registerByAppleCallback.OnSuccess(userInfo);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        private static void registerByAppleFailedCallback(int code, string message)
        {
            if (Instance.registerByAppleCallback != null)
            {
                Instance.registerByAppleCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUserGetUserInfoSuccessCallback))]
        private static void registerByWechatSuccessCallback(IntPtr userData)
        {
            if (Instance.registerByWechatCallback != null)
            {
                ROXUserBean userInfo = GenerateROXUserBean(userData);
                Instance.registerByWechatCallback.OnSuccess(userInfo);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        private static void registerByWechatFailedCallback(int code, string message)
        {
            if (Instance.registerByWechatCallback != null)
            {
                Instance.registerByWechatCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUserGetUserInfoSuccessCallback))]
        private static void userBindUserSuccessCallback(IntPtr userData)
        {
            if (Instance.startBindAccountCallback != null)
            {
                ROXUserBean userInfo = GenerateROXUserBean(userData);
                Instance.startBindAccountCallback.OnSuccess(userInfo);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        private static void userBindUserFailedCallback(int code, string message)
        {
            if (Instance.startBindAccountCallback != null)
            {
                Instance.startBindAccountCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUserGetUserInfoSuccessCallback))]
        private static void getUserInfoSuccessCallback(IntPtr userData)
        {
            if (Instance.getUserInfoCallback != null)
            {
                ROXUserBean userInfo = GenerateROXUserBean(userData);
                Instance.getUserInfoCallback.OnSuccess(userInfo);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        private static void getUserInfoFailedCallback(int code, string message)
        {
            if (Instance.getUserInfoCallback != null)
            {
                Instance.getUserInfoCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUserGetUserInfoSuccessCallback))]
        private static void getUserInfoByUserIdSuccessCallback(IntPtr userData)
        {
            if (Instance.getSpecialUserInfoCallback != null)
            {
                ROXUserBean userInfo = GenerateROXUserBean(userData);
                Instance.getSpecialUserInfoCallback.OnSuccess(userInfo);
            }
        }
        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        private static void getUserInfoByUserIdFailedCallback(int code, string message)
        {
            if (Instance.getSpecialUserInfoCallback != null)
            {
                Instance.getSpecialUserInfoCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUserGetInviterInfoSuccessCallback))]
        private static void getInviterInfoSuccessCallback(IntPtr userData)
        {
            if (Instance.getInviterCallback != null)
            {
                ROXUserBeanBase userInfo = GenerateROXUserBeanBase(userData);

                Instance.getInviterCallback.OnSuccess(userInfo);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        private static void getInviterInfoFailedCallback(int code, string message)
        {
            if (Instance.getInviterCallback != null)
            {
                Instance.getInviterCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityCommonSuccessCallback))]
        private static void logOutSuccessCallback()
        {
            if (Instance.logOutCallback != null)
            {
                Instance.logOutCallback.OnSuccess(true);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        private static void logOutFailedCallback(int code, string message)
        {
            if (Instance.logOutCallback != null)
            {
                Instance.logOutCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityCommonSuccessCallback))]
        private static void bindInviterSuccessCallback()
        {
            if (Instance.bindInviterCallback != null)
            {
                Instance.bindInviterCallback.OnSuccess(true);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        private static void bindInviterFailedCallback(int code, string message)
        {
            if (Instance.bindInviterCallback != null)
            {
                Instance.bindInviterCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUserGetUserExternalInfoSuccessCallback))]
        private static void getUserExternalInfoSuccessCallback(IntPtr extInfo)
        {
            if (Instance.getUserExtInfoCallback != null)
            {
                ROXUserExternalInfo info = new ROXUserExternalInfo();
                List<string> list = new List<string>();

                IntPtr pays = Externs.RichOXUserExternalInfoGetPayMethods(extInfo);
                if (pays != null) {
                    int count = Externs.RichOXArrayGetCount(pays);
                    for (int i=0; i<count; i++) {
                        list.Add(Externs.RichOXArrayGetStringItem(pays, i));
                    }
                }
                info.PayMethods = list;

                info.IsBindWX = Externs.RichOXUserExternalInfoGetIsBindWX(extInfo);
                info.WxNickName = Externs.RichOXUserExternalInfoGetWXNickName(extInfo);
                info.IsBindAP = Externs.RichOXUserExternalInfoGetIsBindAP(extInfo);
                info.ApNickName = Externs.RichOXUserExternalInfoGetAPNickName(extInfo);

                Instance.getUserExtInfoCallback.OnSuccess(info);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        private static void getUserExternalInfoFailedCallback(int code, string message)
        {
            if (Instance.getUserExtInfoCallback != null)
            {
                Instance.getUserExtInfoCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUserGetAPAuthInfoSuccessCallback))]
        private static void getAPAuthSuccessCallback(string authInfo)
        {
            if (Instance.getAPAuthCallback != null)
            {
                Instance.getAPAuthCallback.OnSuccess(authInfo);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        private static void getAPAuthFailedCallback(int code, string message)
        {
            if (Instance.getAPAuthCallback != null)
            {
                Instance.getAPAuthCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityCommonSuccessCallback))]
        private static void bindWalletSuccessCallback()
        {
            if (Instance.bindWalletCallback != null)
            {
                Instance.bindWalletCallback.OnSuccess(true);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXUnityFailureCallback))]
        private static void bindWalletFailedCallback(int code, string message)
        {
            if (Instance.bindWalletCallback != null)
            {
                Instance.bindWalletCallback.OnFailed(code, message);
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