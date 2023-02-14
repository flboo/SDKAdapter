using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using TGCenterSdk.Api;
using AntiAddictionSdk.Api;

namespace GameWish.Game
{
    public class TGCenterMgr : TSingleton<TGCenterMgr>
    {
        // Start is called before the first frame update

        public void Init()
        {
            var config = new InitConfig();

            config.DebugMode = SDKConfig.S.tGCenterConfig.isDebugMode;


#if UNITY_ANDROID

            if (SDKConfig.S.tGCenterConfig.wechatConfig.isEnable)
            {
                config.WeChatAppId = SDKConfig.S.tGCenterConfig.wechatConfig.wechatAppId;
                WechatMgr.S.SetWechatLoginListener();
            }

            config.AppId = SDKConfig.S.tGCenterConfig.appIDAndroid;
            config.Channel = CustomExtensions.GetSDKChannel();


#elif UNITY_IOS
			config.AppId = SDKConfig.S.tGCenterMgrConfig.appIDIos;
			config.AppleAppID = SDKConfig.S.iosAppID;
            
            if (SDKConfig.S.tGCenterMgrConfig.udeskConfig.isEnable)
            {
                config.UdeskDomain = SDKConfig.S.tGCenterMgrConfig.udeskConfig.domainIOS;
                config.UdeskAppId = SDKConfig.S.tGCenterMgrConfig.udeskConfig.appidIOS;
                config.UdeskAppKey = SDKConfig.S.tGCenterMgrConfig.udeskConfig.appkeyIOS;
            }
#endif

            TGCenter.Init(config);
            //
        }
        // 展示《用户协议和隐私政策》对话框
        public void ShowPolicyDialog(OnEvent privacyAccept)
        {
#if UNITY_EDITOR
            EventSystem.S.Send(SDKEventID.OnPrivacyAccept);
#elif UNITY_ANDROID
            EventSystem.S.Register(SDKEventID.OnPrivacyAccept, privacyAccept);
            PrivacyPolicyHelper.Instance.SetAgreeListener(new PrivacyAgreeListener(this));
            PrivacyPolicyHelper.Instance.ShowPrivacyDialog();
#endif
        }

        /// <summary>
        /// 设置展示自定义的时间限制界面
        /// </summary>
        /// <param name="timeLimitCallBack"></param>
        public void ShowCustomTimeLimitPanel(TimeLimitCallback timeLimitCallBack)
        {
            AntiAddiction.SetAutoShowTimeLimitPage(true);
            AntiAddiction.SetTimeLimitCallback(timeLimitCallBack);
        }


        /// <summary>
        /// 展示实名认证的一些内容
        /// </summary>
        /// <param name="realNameCallback"></param>
        public void RealName(RealNameCallback realNameCallback)
        {
            AntiAddiction.RealName(realNameCallback);
        }

        /// <summary>
        /// 获取适龄提示认证图
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public Sprite GetAgeTipsSprite(int height = 495, int width = 385)
        {
            AgeTip ageTip = AntiAddiction.GetAgeTip();
            if (ageTip == null)
                return null;
            // 适龄提示 Icon，可以使用 Texture2D 或 byte[]
            Texture2D iconTex = ageTip.GetIconTexture2D();
            if (iconTex == null)
                return null;
            return Sprite.Create(iconTex, new Rect(0, 0, iconTex.width, iconTex.height), Vector2.zero);
        }

        /// <summary>
        /// 展示适龄提示界面
        /// </summary>
        /// <param name="ageTipCallback"></param>
        public void ShowAgeTips(AgeTipCallback ageTipCallback)
        {
            AntiAddiction.ShowAgeTipPage(ageTipCallback);
        }

        /// <summary>
        /// 展示健康游戏忠告
        /// </summary>
        /// <param name="healthGameTipCallback"></param>
        public void ShowHealthGameTip(HealthGameTipCallback healthGameTipCallback)
        {
            AntiAddiction.ShowHealthGameTipPage(healthGameTipCallback);
        }

        /// <summary>
        /// 自定义健康游戏忠告时获取提示信息
        /// </summary>
        /// <returns></returns>
        public HealthGameTip ShowCustomHealthGameTip()
        {
            HealthGameTip healthGameTip = AntiAddiction.GetHealthGameTip();
            return healthGameTip;
        }


        // 监听用户的行为
        private class PrivacyAgreeListener : PrivacyPolicyHelper.AgreeListener
        {

            private TGCenterMgr behaviour;
            public PrivacyAgreeListener(TGCenterMgr behaviour)
            {
                this.behaviour = behaviour;
            }

            // 同意
            public void OnUserAgree()
            {
                behaviour.DealDialogAgreeResult(true);
            }
            // 不同意
            public void OnUserDisagree()
            {
                behaviour.DealDialogAgreeResult(false);
            }
        }

        /**
         * 处理用户点击对话框按钮的结果。
         * 用户同意，初始化；用户不同意，进行提示。
         */
        private void DealDialogAgreeResult(bool agree)
        {
            if (agree)
            {
                EventSystem.S.Send(SDKEventID.OnPrivacyAccept);
            }
            else
            {
                Application.Quit();
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }




    }

}