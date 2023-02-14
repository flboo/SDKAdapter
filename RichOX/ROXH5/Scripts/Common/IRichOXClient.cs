using RichOX.Api;
using System;

namespace RichOX.Common
{
    public interface IRichOXClient
    {
        void SetAppVerCode(int appVerCode);

        void SetFissionPlatform(string platform);

        void Init(string appId);

        void Init(string appId, string userId, string deviceId);

        event EventHandler<RichOXEventArgs> OnEvent;

        event EventHandler<EventArgs> OnBindWeChat;
        void NotifyBindWeChatResult(bool status, string reason);

        event EventHandler<ItemInfoArgs> OnUpdateItemInfo;

        void RegisterShareCallback(H5ShareCallback callback);

        void OnResultForGen(String shareUrl, int code, String result);

        void OnResultForShare(int code, String result);

        void SetLanguage(string language);
    }
}