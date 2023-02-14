using RichOX.Api;
using RichOX.Common;
using System;
using System.Runtime.InteropServices;
using AOT;
using System.Collections;

namespace RichOX.Platforms.iOS
{
    public class RichOXClient : IRichOXClient, IDisposable
    {
        static RichOXClient sInstance = new RichOXClient();

        public static RichOXClient Instance {
            get {
                return sInstance;
            }
        }

        private IntPtr mManagerClientPtr;
        private IntPtr mManagerPtr;


        #region Event callback types

        internal delegate void RichOXEventCallback(IntPtr managerClient, string name, string value, string mapValue);
        internal delegate void RichOXBindWeChatCallback(IntPtr managerClient);
        internal delegate void RichOXGiftUpdateCallback(IntPtr managerClient, int type, string title, int amount);
        internal delegate void ROXUnityGetShareLinkCallback(IntPtr managerClient, string host, IntPtr paramDic);
        internal delegate void ROXUnityShareCallback(IntPtr managerClient, string title, string content, byte[] imageBase64Str, string shareUrl);

        #endregion


        RichOXClient() {
            mManagerClientPtr = (IntPtr)GCHandle.Alloc(this);
            mManagerPtr = Externs.ROXCreateManager(mManagerClientPtr);

            Externs.ROXSetEventCallback(mManagerPtr, EventCallback);
            Externs.ROXSetBindWeChatCallback(mManagerPtr, BindWeChatCallback);
            Externs.ROXSetGiftUpdateCallback(mManagerPtr, GiftUpdateCallback);
        }

        private H5ShareCallback OnShareCallback;

        #region IRichOXClient

        public event EventHandler<RichOXEventArgs> OnEvent;
        public event EventHandler<EventArgs> OnBindWeChat;
        public event EventHandler<ItemInfoArgs> OnUpdateItemInfo;

        public void SetAppVerCode(int appVerCode) {
            Externs.ROXSetAppVerCode(appVerCode);
        }

        public void SetFissionPlatform(string platform) {
            Externs.ROXSetFissionPlatform(platform);
        }

        public void Init(string appId) {
            // Externs.ROXInit(appId);
        }

        public void Init(string appId, string userId, string deviceId) {
            Externs.ROXInit(appId, userId, deviceId);
        }

        public void SetLanguage(string language)
        {
            Externs.ROXSetLanguageCode(language);
        }

        public void NotifyBindWeChatResult(bool status, string reason)
        {
            Externs.ROXNotifyBindWeChatResult(mManagerPtr, status, reason);
        }

        public void RegisterShareCallback(H5ShareCallback callback) 
        {
            Instance.OnShareCallback = callback;

            Externs.ROXSetGetShareLinkCallback(mManagerPtr, GetShareLinkCallback);
            Externs.ROXSetShareCallback(mManagerPtr, ShareCallback);
        }

        public void OnResultForGen(String shareUrl, int code, String result)
        {
            Externs.ROXNotifyGetShareLinkResult(mManagerPtr, shareUrl, code, result);
        }

        public void OnResultForShare(int code, String result) 
        {
            Externs.ROXNotifyShareResult(mManagerPtr, code, result);
        }

        #endregion


        #region IDisposable
        public void Destroy()
        {
            mManagerPtr = IntPtr.Zero;
        }

        public void Dispose()
        {
            Destroy();
            ((GCHandle)mManagerClientPtr).Free();
        }

        ~RichOXClient()
        {
            Dispose();
        }

        #endregion


        #region Event callback methods

        [MonoPInvokeCallback(typeof(RichOXEventCallback))]
        private static void EventCallback(IntPtr managerClient, string name, string value, string mapValue)
        {
            RichOXClient client = IntPtrToRichOXClient(managerClient);
            if (client.OnEvent != null)
            {
                RichOXEventArgs args = new RichOXEventArgs() {
                    RichOXEvent = new RichOXEvent(new RichOXEventClient(name, value, mapValue))
                };
                client.OnEvent(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXBindWeChatCallback))]
        private static void BindWeChatCallback(IntPtr managerClient)
        {
            RichOXClient client = IntPtrToRichOXClient(managerClient);
            if (client.OnBindWeChat != null)
            {
                client.OnBindWeChat(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXGiftUpdateCallback))]
        private static void GiftUpdateCallback(IntPtr managerClient, int type, string title, int amount)
        {
            RichOXClient client = IntPtrToRichOXClient(managerClient);
            if (client.OnUpdateItemInfo != null)
            {
                ItemInfoArgs args = new ItemInfoArgs()
                {
                    ItemInfo = new ItemInfo(new ItemInfoClient(type, title, amount))
                };
                client.OnUpdateItemInfo(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(ROXUnityGetShareLinkCallback))]
        private static void GetShareLinkCallback(IntPtr managerClient, string host, IntPtr paramDic)
        {
            if (Instance.OnShareCallback != null)
            {
                Hashtable table = new Hashtable();
                IntPtr keyArray = Externs.ROXTypeNSDictionaryGetAllKeys(paramDic);
                if (keyArray != null) {
                    int count = Externs.ROXTypeNSArrayGetCount(keyArray);
                    for (int i=0; i<count; i++) {
                        string key = Externs.ROXTypeNSArrayGetItem(keyArray, i);
                        string value = Externs.ROXTypeNSDictionaryGetStringValue(paramDic, key);
                        table.Add(key, value);
                    }
                }

                Instance.OnShareCallback.GenShareUrl(host, table);
            }
        }

        [MonoPInvokeCallback(typeof(ROXUnityShareCallback))]
        private static void ShareCallback(IntPtr managerClient, string title, string content, byte[] imageBase64Str, string shareUrl)
        {
            if (Instance.OnShareCallback != null)
            {
                Instance.OnShareCallback.ShareContent(title, content, imageBase64Str);
            }
        }

        private static RichOXClient IntPtrToRichOXClient(IntPtr RichOXClient)
        {
            GCHandle handle = (GCHandle)RichOXClient;
            return handle.Target as RichOXClient;
        }

        #endregion
    }
}
