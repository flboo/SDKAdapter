using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameWish.Game;
using TaurusXAdSdk.Api;
using TGCenterSdk.Api;
using AntiAddictionSdk.Api;
namespace Qarth
{
    [TMonoSingletonAttribute("[SDK]/SDKMgr")]
    public class SDKMgr : TMonoSingleton<SDKMgr>
    {
        private bool m_PermissionChecked;

        public bool shopCheckFetched;
        public bool isChecking;

        public void Init()
        {
            DataAnalysisMgr.S.Init();
            EventSystem.S.Register(SDKEventID.OnPrivacyAccept, OnPrivacyAccept);
            PlayerPrefs.SetInt("channel_exit_key", 1);

#if UNITY_ANDROID && !UNITY_EDITOR
            // set hume channel
            var humeChannel = SplitAppHandler.GetChannelId();
            Debug.Log("hume channel: "+humeChannel);
            if (!string.IsNullOrEmpty(humeChannel))
            {
                var seg = new Segment();
                seg.SetChannel(humeChannel);
                TaurusXAds.SetSegment(seg);
            }
#endif
            Log.i("Init[SDKMgr]");
        }

        void OnPrivacyAccept(int key, params object[] args)
        {
            //hume channel
#if UNITY_ANDROID && !UNITY_EDITOR
            var humeChannel = SplitAppHandler.GetChannelId();
            Debug.Log("hume channel: " + humeChannel);
            if (!string.IsNullOrEmpty(humeChannel))
            {
                var seg = new Segment();
                seg.SetChannel(humeChannel);
                TaurusXAds.SetSegment(seg);
            }
#endif

            //#if UNITY_EDITOR
            InitSDKAfterRequestPermission();
            //#else
            //LebianMgr.S.Init();
            bool requestPermissions = false;
            string timeString = PlayerPrefs.GetString("firstStartGame", "");
            DateTime lastSignDate;
            if (!string.IsNullOrEmpty(timeString))
            {
                if (DateTime.TryParse(timeString, out lastSignDate))
                {
                    DateTime today = DateTime.Today;
                    TimeSpan pass = today - lastSignDate;

                    if (pass.TotalDays >= 2)
                    {
                        PlayerPrefs.SetString("firstStartGame", DateTime.Today.ToShortDateString());
                        requestPermissions = true;
                    }
                }
                else
                {
                    PlayerPrefs.SetString("firstStartGame", DateTime.Today.ToShortDateString());
                }
            }
            else
            {
                PlayerPrefs.SetString("firstStartGame", DateTime.Today.ToShortDateString());
                requestPermissions = true;
            }
            if (requestPermissions)
            {
                CustomExtensions.RequestPermissions(new string[]
                    {
                    // AndroidPermissionDefine.READ_PHONE_STATE,
                    // AndroidPermissionDefine.WRITE_EXTERNAL_STORAGE,
                    // AndroidPermissionDefine.ACCESS_COARSE_LOCATION
                    },
                    (p) => { InitSDKAfterRequestPermission(); },
                    (p) => { InitSDKAfterRequestPermission(); },
                    (p) => { InitSDKAfterRequestPermission(); });
            }
            else
            {
                InitSDKAfterRequestPermission();
            }
            //#endif
            Log.i("Init[SDKMgr] After Privacy");
        }

        private void ShopCheck()
        {
            Dictionary<string, string> m_Headers = new Dictionary<string, string>();
            if (!m_Headers.ContainsKey("Content-Encoding"))
                m_Headers.Add("Content-Encoding", "gzip");
            else
                m_Headers["Content-Encoding"] = "gzip";

            //for shop check
            CustomExtensions.FetchRemoteConfParams(
                SDKConfig.S.remoteConfAppName,
                "app_shop_check_key",
                OnShopCheckRemoteFetched,
                OnShopCheckRemoteFailed,
                "all",
                SDKConfig.S.remoteConfUrl,
                m_Headers);
        }
        private void OnShopCheckRemoteFetched(string value)
        {
            shopCheckFetched = true;
            ShopCheckInfo remoteData = null;
            try
            {
                remoteData = LitJson.JsonMapper.ToObject<ShopCheckInfo>(value);
            }
            catch (Exception e)
            {
                Debug.LogError("shop ad remote config error:" + e);
            }

            if (remoteData != null)
            {
                isChecking = remoteData.IsCheking(CustomExtensions.GetSDKChannel(), Application.version);

                if (!isChecking)
                {
                    AdsMgr.S.PreloadAllAd();
                }
                Log.i("shop is checking " + isChecking);
            }
            else
            {
                AdsMgr.S.PreloadAllAd();
            }

            EventSystem.S.Send(SDKEventID.OnShopCheckFetched);
        }

        private void OnShopCheckRemoteFailed()
        {
            shopCheckFetched = true;
            AdsMgr.S.PreloadAllAd();
            EventSystem.S.Send(SDKEventID.OnShopCheckFetched);
        }

        public void InitSDKAfterRequestPermission()
        {
            if (m_PermissionChecked)
                return;
            m_PermissionChecked = true;

            DataAnalysisMgr.S.InitSupplement();
            DataAnalysisRemoteConfMgr.S.Init();
            DataAnalysisMgr.S.CustomEventDailySingle("InitAfterPermission", "init");

            BuglyMgr.S.Init();
            SocialMgr.S.Init();

            NotificationMgr.S.Init();
            JPushMgr.S.Init();
            RichOXMgr.S.Init();
            AccountMgr.S.Init();

            AdsMgr.S.Init();
            AdsMgr.S.InitAllAdData();
            if (SDKConfig.S.shopCheckCtrl)
                ShopCheck();
            else
            {
                AdsMgr.S.PreloadAllAd();
            }

            if (SDKConfig.S.dataAnalysisConfig.secMetaConfig.isEnable)
            {
                MetaSecHelper.S.Init(SDKConfig.S.dataAnalysisConfig.secMetaConfig.secMetaLiscense);
            }

            if (SDKConfig.S.dataAnalysisConfig.GDTActionConfig.isEnable)
            {
                GDTDataMgr.S.InitSDK(SDKConfig.S.dataAnalysisConfig.GDTActionConfig.appId, SDKConfig.S.dataAnalysisConfig.GDTActionConfig.userActionSetId, SDKConfig.S.dataAnalysisConfig.GDTActionConfig.appSecretKey);
                GDTDataMgr.S.ReportBehavior(GDTActionDefine.GDT_GAME_INIT);
                GDTDataMgr.S.ReportBehavior(GDTActionDefine.GDT_REALNAME_START);
            }

            if (SDKConfig.S.tGCenterConfig.isEnable)
            {
                TGCenterMgr.S.Init();
                TGCenterMgr.S.RealName(new RealNameListener());
            }


            EventSystem.S.Send(SDKEventID.OnPemissionCallBack);
            Log.i("Init[SDKMgr] After Permission");
        }

        public void RequestRealNameSys(string url, string appName)
        {
            RealNameRemoteConfMgr.S.Init(url, appName, CustomExtensions.GetSDKChannel());
        }

        public void CheckPrivacy(OnEvent privacyAccept)
        {
            EventSystem.S.Register(SDKEventID.OnPrivacyAccept, privacyAccept);
            if (TGCenter.IsUserAgreePolicy())
            {
                EventSystem.S.Send(SDKEventID.OnPrivacyAccept);
            }
            else
            {
                TGCenterMgr.S.ShowPolicyDialog(privacyAccept);
            }
        }

        void OnApplicationFocus(bool focusStatus)
        {
            if (focusStatus && DataAnalysisMgr.S.isAdapterInited)
            {
                DataAnalysisMgr.S.CheckRetention(true);

                // if (SDKConfig.S.dataAnalysisConfig.GDTActionConfig.isEnable)
                // {
                //     GDTAction.S.LogAction(GDTActionDefine.GDT_ACTIONTYPE_START_APP);
                // }
            }
        }

        public void SetNotificationState(bool state)
        {
            PlayerPrefs.SetInt("sdkmgr_close_notify_state", state ? 0 : 1);


            if (state)
            {
                NotificationMgr.S.ReopenNotification();
#if !UNITY_EDITOR && UNITY_ANDROID
                JPushMgr.S.ResumePush();
#endif
            }
            else
            {
                NotificationMgr.S.CleanNotification();
#if !UNITY_EDITOR && UNITY_ANDROID
                JPushMgr.S.StopPush();
#endif
            }
        }

        public void CheckNotificationState()
        {
            SetNotificationState(PlayerPrefs.GetInt("sdkmgr_close_notify_state", 0) == 0);
        }

        private class RealNameListener : RealNameCallback
        {
            public void OnFinish(User user)
            {
                Debug.Log("RealNameCallback " + user.GetAge() + " " + user.IsTourist() + " " + user.IsChild() + " " + user.IsAdult());
                if (user.GetAge() == 0 && user.IsAdult() == true)
                {
                    //没拉取到配置（默认为成年，无限制）
                }
                else
                {
                    if (SDKConfig.S.dataAnalysisConfig.GDTActionConfig.isEnable)
                    {
                        //实名认证成功
                        GDTDataMgr.S.ReportBehavior(GDTActionDefine.GDT_REALNAME_SUCCESS);
                    }
                }
            }
        }

    }
}
