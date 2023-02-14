using RichOX.Common;

namespace RichOX.Api
{
    public class NativeInfo
    {
        readonly INativeInfoClient mClient;

        public NativeInfo(INativeInfoClient client)
        {
            mClient = client;
        }

        public string GetTitle() {
            return mClient.GetTitle();
        }

        // ICON 资源对应的 URL，类似原生广告中 ICON 字段
        public string GetIconUrl() {
            return mClient.GetIconUrl();
        }

        // 描述信息，类似原生广告中 DESC 字段
        public string GetDesc() {
            return mClient.GetDesc();
        }

        // CTA （call_to_action), 类似原生广告中 CTA 字段
        public string GetCTA() {
            return mClient.GetCTA();
        }

        // Media URL
        public string GetMediaUrl() {
            return mClient.GetMediaUrl();
        }

        public override string ToString() {
            return "Title: " + GetTitle()
                + ", IconUrl: " + GetIconUrl()
                + ", Desc: " + GetDesc()
                + ", CTA: " + GetCTA()
                + ", MediaUrl: " + GetMediaUrl();
        }
    }
}