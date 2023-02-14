using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROXBase.Api;
using System;
using System.Runtime.InteropServices;

namespace ROXBase.Platforms.Android
{
    public static class RichOXUtils
    {
        public static ROXUserInfo GenerateUser(AndroidJavaObject userInfo)
        {
            string userId = userInfo.Call<string>("getId");
            string name = userInfo.Call<string>("getName");
            string avatar = userInfo.Call<string>("getAvatar");
            string deviceId = userInfo.Call<string>("getDeviceId");
            string wechatOpenId = userInfo.Call<string>("getWechatOpenId");
            string wechatAppId = userInfo.Call<string>("getWechatAppId");
            string wechatBindTime = userInfo.Call<string>("getWechatBindTime");
            string getInstallAt = userInfo.Call<string>("getInstallAt");
            bool isEnabled = userInfo.Call<bool>("isEnabled");
            bool isHasWithdraw = userInfo.Call<bool>("isHasWithdraw");
            bool isNew = userInfo.Call<bool>("isNew");

            ROXUserInfo roxUserInfo = new ROXUserInfo();
            roxUserInfo.UserId = userId;
            roxUserInfo.Name = name;
            roxUserInfo.Avatar = avatar;
            roxUserInfo.DeviceId = deviceId;
            roxUserInfo.WechatAppId = wechatAppId;
            roxUserInfo.WechatOpenId = wechatOpenId;
            roxUserInfo.WechatBindTime = wechatBindTime;
            roxUserInfo.GetInstallAt = getInstallAt;
            roxUserInfo.IsEnabled = isEnabled;
            roxUserInfo.IsHasWithdraw = isHasWithdraw;
            roxUserInfo.IsNew = isNew;
            return roxUserInfo;
        }

        public static ROXUserInfoSimple GenerateSimpleUser(AndroidJavaObject userInfo)
        {
            string userId = userInfo.Call<string>("getUserId");
            string name = userInfo.Call<string>("getName");
            string avatar = userInfo.Call<string>("getAvatar");


            ROXUserInfoSimple roxSimpleUserInfo = new ROXUserInfoSimple();
            roxSimpleUserInfo.UserId = userId;
            roxSimpleUserInfo.Name = name;
            roxSimpleUserInfo.Avatar = avatar;
            return roxSimpleUserInfo;

        }

        public static ROXUserToken GenerateUserToken(AndroidJavaObject tokenInfo)
        {
            string userId = tokenInfo.Call<string>("getUserId");
            string token = tokenInfo.Call<string>("getToken");
            long refreshTime = tokenInfo.Call<long>("getRefreshTime");


            ROXUserToken roxToken = new ROXUserToken();
            roxToken.UserId = userId;
            roxToken.Token = token;
            roxToken.RefreshTime = refreshTime;
            return roxToken;

        }


        public static ROXUserBean GenerateROXUserBean(AndroidJavaObject userInfo)
        {
            ROXUserBean roxUserInfo = new ROXUserBean();
            string userId = userInfo.Call<string>("getId");
            string name = userInfo.Call<string>("getName");
            string avatar = userInfo.Call<string>("getAvatar");
            string deviceId = userInfo.Call<string>("getDeviceId");
            string countryCode = userInfo.Call<string>("getCountryCode");
            bool isHasWithdraw = userInfo.Call<bool>("isHasWithdraw");
            long installAt = userInfo.Call<long>("getInstallAt");
            long createAt = userInfo.Call<long>("getCreateAt");
            long serverTime = userInfo.Call<long>("getSeverTime");
            string inviterId = userInfo.Call<string>("getInviterId");
            string invitationCode = userInfo.Call<string>("getInvitationCode");
            bool isNew = userInfo.Call<bool>("isNew");
            int ipType = userInfo.Call<int>("getIpType");
            AndroidJavaObject walletListObject = userInfo.Call<AndroidJavaObject>("getWalletList");
            List<string> walletList = new List<string>(); 
            if (walletListObject != null) 
            {
                int size = walletListObject.Call<int>("size");
                for (int i=0; i<size; i++) 
                {
                    string wallet = walletListObject.Call<string>("get", i);
                    walletList.Add(wallet);
                }
            }
            roxUserInfo.Id = userId;
            roxUserInfo.Name = name;
            roxUserInfo.Avatar = avatar;
            roxUserInfo.DeviceId = deviceId;
            roxUserInfo.CountryCode = countryCode;
            roxUserInfo.IsHasWithdraw = isHasWithdraw;
            roxUserInfo.InstallAt = installAt;
            roxUserInfo.CreateAt = createAt;
            roxUserInfo.SeverTime = serverTime;
            roxUserInfo.InviterId = inviterId;
            roxUserInfo.InvitationCode = invitationCode;
            roxUserInfo.IsNew = isNew;
            roxUserInfo.IpType = ipType;
            roxUserInfo.WalletList = walletList;

            AndroidJavaObject googleInfoObject = userInfo.Call<AndroidJavaObject>("getGoogleInfo");
            if (googleInfoObject != null)
            {
                GoogleInfo googleInfo = new GoogleInfo();
                googleInfo.Email = googleInfoObject.Call<string>("getEmail");
                googleInfo.FamilyName = googleInfoObject.Call<string>("getFamilyName");
                googleInfo.GoogleName = googleInfoObject.Call<string>("getGoogleName");
                googleInfo.GivenName = googleInfoObject.Call<string>("getGivenName");
                googleInfo.GoogleSub = googleInfoObject.Call<string>("getGoogleSub");
                roxUserInfo.GoogleInfoCurrent = googleInfo;
            }

            AndroidJavaObject facebookInfoObject = userInfo.Call<AndroidJavaObject>("getFBInfo");
            if (facebookInfoObject != null)
            {
                FacebookInfo facebookInfo = new FacebookInfo();
                facebookInfo.Email = facebookInfoObject.Call<string>("getEmail");
                facebookInfo.FBName = facebookInfoObject.Call<string>("getFBName");
                facebookInfo.FBOpenId = facebookInfoObject.Call<string>("getFBOpenId");
                facebookInfo.FirstName = facebookInfoObject.Call<string>("getFirstName");
                facebookInfo.LastName = facebookInfoObject.Call<string>("getLastName");
                roxUserInfo.FacebookInfoCurrent = facebookInfo;
            }

            AndroidJavaObject wechatInfoObject = userInfo.Call<AndroidJavaObject>("getWechatInfo");
            if (wechatInfoObject != null)
            {
                WechatInfo wechatInfo = new WechatInfo();
                wechatInfo.WXAppId = wechatInfoObject.Call<string>("getWXAppId");
                wechatInfo.NickName = wechatInfoObject.Call<string>("getNickName");
                wechatInfo.Avatar = wechatInfoObject.Call<string>("getAvatar");
                wechatInfo.OpenId = wechatInfoObject.Call<string>("getOpenId");
                wechatInfo.UnionId = wechatInfoObject.Call<string>("getUnionId");
                roxUserInfo.WechatInfoCurrent = wechatInfo;
            }

            AndroidJavaObject apInfoObject = userInfo.Call<AndroidJavaObject>("getAPInfo");
            if (apInfoObject != null)
            {
                APInfo apInfo = new APInfo();
                apInfo.APName = apInfoObject.Call<string>("getAPName");
                apInfo.APId = apInfoObject.Call<string>("getAPId");
                roxUserInfo.APInfoCurrent = apInfo;
            }
            return roxUserInfo;
        }


        public static ROXUserBeanBase GenerateROXUserBeanBase(AndroidJavaObject userInfoBase)
        {
            ROXUserBeanBase roxUserInfoBase = new ROXUserBeanBase();
            string userId = userInfoBase.Call<string>("getId");
            string name = userInfoBase.Call<string>("getName");
            string avatar = userInfoBase.Call<string>("getAvatar");
            long createAt = userInfoBase.Call<long>("getCreateAt");
            long serverTime = userInfoBase.Call<long>("getSeverTime");

            roxUserInfoBase.Id = userId;
            roxUserInfoBase.Name = name;
            roxUserInfoBase.Avatar = avatar;
            roxUserInfoBase.CreateAt = createAt;
            roxUserInfoBase.SeverTime = serverTime;

            return roxUserInfoBase;
        }

        public static ROXUserExternalInfo GenerateROXUserExternalInfo(AndroidJavaObject externalInfo)
        {
            ROXUserExternalInfo externalUserInfo = new ROXUserExternalInfo();
            Debug.Log("create externalUserInfo");
            AndroidJavaObject payList = externalInfo.Call<AndroidJavaObject>("getPayMethods");
            if (payList != null) {
                int size = payList.Call<int>("size");
                List<string> list = new List<string>();
                for (int i=0; i<size; i++) 
                {
                    string payMethod = payList.Call<string>("get", i);
                    list.Add(payMethod);
                }
                externalUserInfo.PayMethods = list;
            }
            externalUserInfo.IsBindWX = externalInfo.Call<bool>("isBindWX");
            externalUserInfo.WxNickName = externalInfo.Call<string>("getWXNickName");
            externalUserInfo.IsBindAP = externalInfo.Call<bool>("isBindAP");
            externalUserInfo.ApNickName = externalInfo.Call<string>("getAPNickName");
            
            return externalUserInfo;
        }
    }
}