using RichOX.Common;
using RichOX.Platforms;
using System;

namespace RichOX.Api
{
    /**
     * Class contains logic that applies to the SDK as a whole.
     */
    public class RichOXManager
    {
        static RichOXManager sInstance = new RichOXManager();

        public static RichOXManager Instance
        {
            get
            {
                return sInstance;
            }
        }

        private IRichOXClient mClient;

        RichOXManager()
        {
            mClient = ClientFactory.RichOXClientInstance();
            // mClient.OnEvent += (sender, args) =>
            // {
            //     if (OnEvent != null)
            //     {
            //         OnEvent(this, args);
            //     }
            // };
            mClient.OnBindWeChat += (sender, args) =>
            {
                if (OnBindWeChat != null)
                {
                    OnBindWeChat(this, args);
                }
            };
            mClient.OnUpdateItemInfo += (sender, args) =>
            {
                if (OnUpdateItemInfo != null)
                {
                    OnUpdateItemInfo(this, args);
                }
            };
        }

        /// <summary>
        /// 接入 Fission 系统的提现用的 App 版本号，
        /// 如果不使用 Fission 功能或者 VersionCode是通过去掉 '.' 字符获取（比如 1.3.3 对应 133），
        /// 可以不用调用此方法。
        /// <summary> 
        public void SetAppVerCode(int appVerCode) {
            mClient.SetAppVerCode(appVerCode);
        }

        /// <summary>
        /// 接入 Fission 系统的用的特殊的platform字段值，
        /// 如果不使用 Fission 功能可以不用调用此方法。
        /// <summary> 
        [Obsolete("对接新版本RichOX，该方法已被弃用，请勿在使用")]
        public void SetFissionPlatform(string platform) {
            mClient.SetFissionPlatform(platform);
        }

        /// <summary>
        /// 初始化接口
        /// <summary>
        public void Init() 
        {
            mClient.Init("", "", "");
        }

        /// <summary>
        /// 设置当前应用内使用的语言
        /// <summary>
        public void SetLanguage(string language)
        {
            mClient.SetLanguage(language);            
        }

        [Obsolete("对接新版本RichOX，该方法已被弃用，请勿在使用 init() 方法")]
        public void Init(string appId, string userId, string deviceId) {
            mClient.Init(appId, userId, deviceId);
        }

        [Obsolete("对接新版本RichOX，该方法已被弃用，请勿在使用")]
        public event EventHandler<RichOXEventArgs> OnEvent;

        /// <summary>
        /// 微信绑定回调
        /// <summary>
        public event EventHandler<EventArgs> OnBindWeChat;

        /// <summary>
        /// 通知 H5 绑定结果
        /// <summary>
        public void NotifyBindWeChatResult(bool status, string reason) {
            mClient.NotifyBindWeChatResult(status, reason);
        }

        /// <summary>
        /// H5 改动资产，通知应用更新
        /// <summary>
        public event EventHandler<ItemInfoArgs> OnUpdateItemInfo;

        /// <summary>
        /// 注册分享活动回调接口
        /// 开发者需要在 callback 对应的回调中，完成相应的操作
        /// GenShareUrl 在该回调中根据返回的 hostUrl 和 params 生成分享链接，并在 ShareResultCallback 中告知 SDK
        /// ShareContent 根据返回的title， content，imageContent 完成分享活动，并在 ShareResultCallback 中告知 SDK
        /// <summary>
        public void RegisterShareCallback(H5ShareCallback callback) 
        {
            mClient.RegisterShareCallback(callback);
        }

        /// <summary>
        /// 返回生成分享链接结果
        /// shareUrl: 开发者生成的最终分享链接
        /// code: 结果，0 表示成功，非 0 表示失败
        /// result: 结果描述
        /// <summary> 
        public void OnResultForGen(String shareUrl, int code, String result) 
        {
            mClient.OnResultForGen(shareUrl, code, result);
        }

        /// <summary>
        /// 返回分享结果
        /// code: 结果，0 表示成功，非 0 表示失败
        /// result: 结果描述
        /// <summary> 
        public void OnResultForShare(int code, String result) 
        {
            mClient.OnResultForShare(code, result);
        }

        

    }
}
