using System;
using System.Runtime.InteropServices;

namespace ROXBase.Platforms.iOS {
    internal class Externs {

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXBaseCreateManager(IntPtr managerClient);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXBaseInit(IntPtr manager, string appId, RichOXClient.RichOXBaseInitSuccessCallback callback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXBaseSetOverSea();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXBaseSetDeviceId(string deviceId);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXBaseSetUserId(string userId);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXBaseSetFissionPlatform(string platform);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXBaseSetFissionHost(string host);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXBaseSetFissionKey(string key);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXBaseSetExtendInfo(string extendInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXBaseGetDeviceId();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXBaseGetUserId();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXBaseGetAppId();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXBaseGetFissionHost();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXBaseGetFissionKey();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXBaseGetFissionPlatform();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXBaseGetExtendInfo();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXBaseSetAppVerCode(int appVerCode);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXBaseSetTestMode(bool testMode);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXReportAppEvent(string eventName, string eventValue);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXGetAPPEventValue(IntPtr manager, string eventName, RichOXClient.RichOXBaseAPPEventValueCallback callback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXBaseSetEventCallback(IntPtr manager, RichOXClient.RichOXBaseEventCallback callback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXUserCreateInitUserData(long coin, float cash, bool isRisky);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserRegisterUserId(string inviterId, IntPtr initUserData, RichOXUserClient.RichOXUserGetUserDataSuccessCallback successCallback, RichOXUserClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserDataGetUserId(IntPtr userData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserDataGetName(IntPtr userData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserDataGetAvatar(IntPtr userData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserDataGetDeviceId(IntPtr userData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserDataGetWechatOpenId(IntPtr userData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserDataGetWechatAppId(IntPtr userData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserDataGetWechatBoundAt(IntPtr userData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserDataGetInstallAtTime(IntPtr userData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserDataGetInviterId(IntPtr userData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool RichOXUserDataGetIsNew(IntPtr userData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool RichOXUserDataHasWithdraw(IntPtr userData);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool RichOXUserBindUser(string appId, string bindType, string bindCode, RichOXUserClient.RichOXUserBindUserSuccessCallback successCallback, RichOXUserClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXUserBindResultGetCreateAtTime(IntPtr bindResult);


        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXUserBindResultGetUserData(IntPtr bindResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXUserBindResultGetBindType(IntPtr bindResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXUserBindResultGetUserSocailAccountInfo(IntPtr bindResult);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserSocialAccountGetWechatAppId(IntPtr socailAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserSocialAccountGetWechatNickName(IntPtr socailAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserSocialAccountGetWechatAvatar(IntPtr socailAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserSocialAccountGetWechatUnionId(IntPtr socailAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserSocialAccountGetWechatCountry(IntPtr socailAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserSocialAccountGetWechatProvince(IntPtr socailAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserSocialAccountGetWechatCity(IntPtr socailAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXUserSocialAccountGetWechatGender(IntPtr socailAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserRegisterByWeChat(string appId, string bindCode, string inviterId, RichOXUserClient.RichOXUserBindUserSuccessCallback successCallback, RichOXUserClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserGetUserInfo(RichOXUserClient.RichOXUserGetUserDataSuccessCallback successCallback, RichOXUserClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserGetUserBaseInfo(string[] targetUserId, int count, RichOXUserClient.RichOXUserGetUsersBasicDataSuccessCallback successCallback, RichOXUserClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int getUserBasicDataInfoCountFromArray(IntPtr basicInfos);
        
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr getUserBasicDataInfoFromArray(IntPtr basicInfos, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string getUserIdFromUserBasicDataInfo(IntPtr basicInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string getUserNameFromUserBasicDataInfo(IntPtr basicInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string getUserAvatarFromUserBasicDataInfo(IntPtr basicInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserLogOutUser(RichOXUserClient.RichOXUnityCommonSuccessCallback successCallback, RichOXUserClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserBindInviter(string inviterId, RichOXUserClient.RichOXUnityCommonSuccessCallback successCallback, RichOXUserClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserManagerRegisterUserId(RichOXUserManagerClient.RichOXUserGetUserInfoSuccessCallback successCallback, RichOXUserManagerClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserObjectGetUserId(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserObjectGetName(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserObjectGetAvatar(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXUserObjectGetCreateAt(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXUserObjectGetServerNow(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserObjectGetDeviceId(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserObjectGetConutryCode(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserObjectGetInviterId(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserObjectGetInvitationCode(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXUserObjectGetInstallAt(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool RichOXUserObjectGetIsNew(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool RichOXUserObjectGetHasWithdraw(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXUserObjectGetWallets(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXUserObjectGetAppleAccountInfo(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXAppleAccountInfoGetAppleName(IntPtr appleAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXAppleAccountInfoGetAppleSub(IntPtr appleAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXUserObjectGetFacebookAccountInfo(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXFacebookAccountInfoGetOpenId(IntPtr fbAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXFacebookAccountInfoGetFBName(IntPtr appAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXFacebookAccountInfoGetFirstName(IntPtr appAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXFacebookAccountInfoGetLastName(IntPtr appAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXFacebookAccountInfoGetEmail(IntPtr appAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXUserObjectGetGoogleAccountInfo(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXGoogleAccountInfoGetGoogleName(IntPtr googleAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXGoogleAccountInfoGetGivenName(IntPtr googleAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXGoogleAccountInfoGetGoogleSub(IntPtr googleAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXGoogleAccountInfoGetFamilyName(IntPtr googleAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXGoogleAccountInfoGetEmail(IntPtr googleAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXUserObjectGetWechatAccountInfo(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXWechatAccountInfoGetOpenId(IntPtr wxAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXWechatAccountInfoGetAppId(IntPtr wxAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXWechatAccountInfoGetNickName(IntPtr wxAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXWechatAccountInfoGetAvatar(IntPtr wxAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXWechatAccountInfoGetUnionId(IntPtr wxAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXWechatAccountInfoGetCountry(IntPtr wxAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXWechatAccountInfoGetProvince(IntPtr wxAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXWechatAccountInfoGetCity(IntPtr wxAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXWechatAccountInfoGetGender(IntPtr wxAccountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserManagerRegisterByFacebook(string openId, string accessToken, RichOXUserManagerClient.RichOXUserGetUserInfoSuccessCallback successCallback, RichOXUserManagerClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserManagerRegisterByGoogle(string idToken, RichOXUserManagerClient.RichOXUserGetUserInfoSuccessCallback successCallback, RichOXUserManagerClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserManagerRegisterByApple(string appleName, string appleToken, RichOXUserManagerClient.RichOXUserGetUserInfoSuccessCallback successCallback, RichOXUserManagerClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserManagerRegisterByWeChat(string wxAppId, string wxCode, RichOXUserManagerClient.RichOXUserGetUserInfoSuccessCallback successCallback, RichOXUserManagerClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserManagerBindUser(string appleId, string bindType, string bindCode, RichOXUserManagerClient.RichOXUserGetUserInfoSuccessCallback successCallback, RichOXUserManagerClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserManagerGetUserInfo(RichOXUserManagerClient.RichOXUserGetUserInfoSuccessCallback successCallback, RichOXUserManagerClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserManagerGetUserInfoByUserId(string userId, RichOXUserManagerClient.RichOXUserGetUserInfoSuccessCallback successCallback, RichOXUserManagerClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserManagerLogOutUser(RichOXUserManagerClient.RichOXUnityCommonSuccessCallback successCallback, RichOXUserManagerClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserManagerGetInviter(RichOXUserManagerClient.RichOXUserGetInviterInfoSuccessCallback successCallback, RichOXUserManagerClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXBasicUserObjectGetUserId(IntPtr userBasicInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXBasicUserObjectGetName(IntPtr userBasicInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXBasicUserObjectGetAvatar(IntPtr userBasicInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXBasicUserObjectGetCreateAt(IntPtr userBasicInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long RichOXBasicUserObjectGetServerNow(IntPtr userBasicInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserManagerBindInviter(string inviterId, RichOXUserManagerClient.RichOXUnityCommonSuccessCallback successCallback, RichOXUserManagerClient.RichOXUnityFailureCallback failureCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserManagerGetUserExternalInfo(RichOXUserManagerClient.RichOXUserGetUserExternalInfoSuccessCallback successCallback, RichOXUserManagerClient.RichOXUnityFailureCallback failureCallback);

         #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserManagerGetAPAuthInfo(RichOXUserManagerClient.RichOXUserGetAPAuthInfoSuccessCallback successCallback, RichOXUserManagerClient.RichOXUnityFailureCallback failureCallback);

        
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXUserExternalInfoGetPayMethods(IntPtr extInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int RichOXArrayGetCount(IntPtr arrayPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXArrayGetStringItem(IntPtr arrayPtr, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool RichOXUserExternalInfoGetIsBindWX(IntPtr extInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserExternalInfoGetWXNickName(IntPtr extInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool RichOXUserExternalInfoGetIsBindAP(IntPtr extInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXUserExternalInfoGetAPNickName(IntPtr extInfo);
        
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXCreateWithdrawInfo(string payRemark);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXWithdrawInfoSetComment(IntPtr withdrawInfo, string comment);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXWithdrawInfoSetRealName(IntPtr withdrawInfo, string realName);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXWithdrawInfoSetIdCard(IntPtr withdrawInfo, string idCard);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXWithdrawInfoSetPhoneNo(IntPtr withdrawInfo, string phobeNo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXWithdrawInfoSetWithdrawChannel(IntPtr withdrawInfo, string channel);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXWithdrawInfoSetWithdrawAmount(IntPtr withdrawInfo, double amount);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXWithdrawInfoSetWithdrawWay(IntPtr withdrawInfo, string withdrawWay);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXCreateMutableDictionaryInfo();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXTypeMutableDictionarySetIntValue(IntPtr dicPtr, string key, int intValue);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXTypeMutableDictionarySetLongValue(IntPtr dicPtr, string key, long longValue);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXTypeMutableDictionarySetBoolValue(IntPtr dicPtr, string key, bool boolValue);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXTypeMutableDictionarySetStringValue(IntPtr dicPtr, string key, string stringValue);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXTypeMutableDictionarySetDictionaryValue(IntPtr dicPtr, string key, IntPtr dicValue);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXWithdrawInfoSetExtParam(IntPtr withdrawInfo, IntPtr extParam);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXCreateReplenishInfo();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXReplenishInfoSetPhoneNo(IntPtr replenishInfo, string phoneNo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXReplenishInfoSetProductCode(IntPtr replenishInfo, string productCode);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXUserObjectGetAPAccountInfo(IntPtr userObject);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXAPAccountInfoGetAPName(IntPtr accountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string RichOXAPAccountInfoGetAPId(IntPtr accountInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXUserManagerBindWallet(string type, string info, RichOXUserManagerClient.RichOXUnityCommonSuccessCallback successCallback, RichOXUserManagerClient.RichOXUnityFailureCallback failureCallback);

    }
}