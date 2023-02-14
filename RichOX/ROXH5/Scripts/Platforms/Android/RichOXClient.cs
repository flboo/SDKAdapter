using UnityEngine;
using System;
using RichOX.Api;
using RichOX.Common;
using System.Collections;

namespace RichOX.Platforms.Android
{
    public class RichOXClient : AndroidJavaProxy, IRichOXClient
    {
        static RichOXClient sInstance = new RichOXClient();

        public static RichOXClient Instance {
            get {
                return sInstance;
            }
        }

        private AndroidJavaClass mRichOXClass;
        private AndroidJavaObject mAppContext;

        private AndroidJavaObject mShareCallback;

        private BindWeChatListener mBindWeChatListener;
        private UpdateItemInfoListener mUpdateItemInfoListener;

        public event EventHandler<RichOXEventArgs> OnEvent;
        public event EventHandler<EventArgs> OnBindWeChat;

        public event EventHandler<ItemInfoArgs> OnUpdateItemInfo;

        RichOXClient() : base(Utils.Object) {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            mAppContext = activity.Call<AndroidJavaObject>("getApplication");

            mRichOXClass = new AndroidJavaClass(Utils.RichOXClassName);

            // mRichOXClass.CallStatic("registerEventCallback", this);

            mBindWeChatListener = new BindWeChatListener();
            mBindWeChatListener.OnBindWeChat += (sender, args) =>
            {
                if (OnBindWeChat != null)
                {
                    OnBindWeChat(this, args);
                }
            };
            mRichOXClass.CallStatic("registerWeChatCallback", mBindWeChatListener);

            mUpdateItemInfoListener = new UpdateItemInfoListener();
            mUpdateItemInfoListener.OnUpdateItemInfo += (sender, args) =>
            {
                if (OnUpdateItemInfo != null)
                {
                    OnUpdateItemInfo(this, args);
                }
            };
            mRichOXClass.CallStatic("registerInfoUpdateCallback", mUpdateItemInfoListener);
        }


        #region IRichOXClient

        public void SetAppVerCode(int appVerCode) {
            // Keep empty, Android Sdk can get appVerCode
        }

        public void SetFissionPlatform(string platform) {
            // mRichOXClass.CallStatic("setFissionPlatform", platform);
        }

        public void Init(string appId) {
            // mRichOXClass.CallStatic("init", mAppContext, appId);
            mRichOXClass.CallStatic("init", mAppContext);
        }

        public void Init(string appId, string userId, string deviceId) {
            // mRichOXClass.CallStatic("init", mAppContext, appId, userId, deviceId);
            mRichOXClass.CallStatic("init", mAppContext);
        }

        public void SetLanguage(string language)
        {
            mRichOXClass.CallStatic("setLanguage", language);
        }

        public void NotifyBindWeChatResult(bool status, string reason) {
            mBindWeChatListener.WeChatResultCallback.Call("onResult", status, reason);
        }

        public void RegisterShareCallback(H5ShareCallback callback) 
        {
            AndroidShareCallback androidShareCallback = new AndroidShareCallback();
            androidShareCallback.GenShareUrl += (sender, args) =>
            {
                string host = args.Response.GetHostUrl();
                Hashtable shareParams = args.Response.GetShareParams();
                mShareCallback = (AndroidJavaObject) args.Response.getCallbackObject();
                callback.GenShareUrl(host, shareParams); 
            };

            androidShareCallback.ShareContent += (sender, args) =>
            {                
                string title = args.Response.GetShareTitle();
                string content = args.Response.GetShareContent();
                byte[] bitmapBytes = args.Response.GetBitmpaBytes();
                mShareCallback = (AndroidJavaObject) args.Response.getCallbackObject();
                callback.ShareContent(title, content, bitmapBytes); 
            };

            mRichOXClass.CallStatic("registerShareCallback", androidShareCallback);
        }

        public void OnResultForGen(String shareUrl, int code, String result)
        {
            if (mShareCallback != null) 
            {
                mShareCallback.Call("onResultForGen", shareUrl, code, result);  
            }
        }

        public void OnResultForShare(int code, String result) 
        {
            if (mShareCallback != null) 
            {
                mShareCallback.Call("onResultForShare", code, result); 
            }
        }

        #endregion


        #region EventListener

        // public void onEvent(string name) {
        //     if (OnEvent != null)
        //     {
        //         RichOXEventArgs args = new RichOXEventArgs()
        //         {
        //             RichOXEvent = new RichOXEvent(new RichOXEventClient(name, "", ""))
        //         };
        //         OnEvent(this, args);
        //     }
        // }

        // public void onEvent(string name, string value) {
        //     if (OnEvent != null)
        //     {
        //         RichOXEventArgs args = new RichOXEventArgs()
        //         {
        //             RichOXEvent = new RichOXEvent(new RichOXEventClient(name, value, ""))
        //         };
        //         OnEvent(this, args);
        //     }
        // }

        // public void onEvent(string name, AndroidJavaObject mapValue)
        // {
        //     // keep empty
        // }

        // public void onEventJson(string name, string mapValue)
        // {
        //     if (OnEvent != null)
        //     {
        //         RichOXEventArgs args = new RichOXEventArgs()
        //         {
        //             RichOXEvent = new RichOXEvent(new RichOXEventClient(name, "", mapValue))
        //         };
        //         OnEvent(this, args);
        //     }
        // }

        #endregion


        #region BindWeChat

        class BindWeChatListener : AndroidJavaProxy {

            public event EventHandler<EventArgs> OnBindWeChat;

            public AndroidJavaObject WeChatResultCallback;

            public BindWeChatListener() : base(Utils.WeChatRegisterCallbackClassName)
            {
            }

            // WeChatResultCallback
            public void registerWeChat(AndroidJavaObject callback)
            {
                WeChatResultCallback = callback;
                if (OnBindWeChat != null) 
                {
                    OnBindWeChat(this, EventArgs.Empty);
                }
            }
        }

        #endregion


        #region UpdateRewardInfo

        class UpdateItemInfoListener : AndroidJavaProxy
        {
            public event EventHandler<ItemInfoArgs> OnUpdateItemInfo;

            public UpdateItemInfoListener() : base(Utils.InfoUpdateCallbackClassName)
            {
            }

            public void updateInfo(int type, string title, int count) {
                if (OnUpdateItemInfo != null) 
                {
                    ItemInfoArgs args = new ItemInfoArgs()
                    {
                        ItemInfo = new ItemInfo(new ItemInfoClient(type, title, count))
                    };
                    OnUpdateItemInfo(this, args);
                }
            }
        }

        #endregion
    }
}