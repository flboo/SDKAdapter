using UnityEngine;
using AntiAddictionSdk.Common;

namespace AntiAddictionSdk.Api
{
    public class AgeTip {
        private IAgeTipClient Client;
        public AgeTip(IAgeTipClient client) {
            Client = client;
        }

        // 适龄提示 Icon
        public byte[] GetIconByteArray() {
            return Client.GetIconByteArray();
        }

        // 适龄提示 Icon
        public Texture2D GetIconTexture2D() {
            byte[] iconByteArray = GetIconByteArray();
            if (iconByteArray == null)
            {
                return null;
            }
            return Utils.GetTexture2DFromByteArray(iconByteArray);
        }

        // 适龄提示对话框内容
        public string GetText() {
            return Client.GetText();
        }
    }
}