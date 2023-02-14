using RichOX.Api;
using System;

namespace RichOX.Common
{
    public class DummyRichOXClient : IRichOXClient
    {
        #region IRichOXClient

        public void SetAppVerCode(int appVerCode) { }

        public void SetFissionPlatform(string platform) { }

        public void Init(string appId) { }

        public void Init(string appId, string userId, string deviceId) { }

        public event EventHandler<RichOXEventArgs> OnEvent;

        public event EventHandler<EventArgs> OnBindWeChat;
        public void NotifyBindWeChatResult(bool status, string reason) { }

        public event EventHandler<ItemInfoArgs> OnUpdateItemInfo;

        public void RegisterShareCallback(H5ShareCallback callback) 
        {

        }

        public void OnResultForGen(String shareUrl, int code, String result)
        {

        }

        public void OnResultForShare(int code, String result) 
        {

        }

        public void SetLanguage(string language)
        {
            
        }

        #endregion
    }
}
