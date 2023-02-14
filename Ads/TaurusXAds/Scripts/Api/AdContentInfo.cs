using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Api
{
    public class AdContentInfo
    {
        // 广告是否是 App
        public enum IsApp {
            YES = 0, // 是
            NO, // 不是
            UNkNOWN // 未知
        }

        // 广告主内容类型
        public enum ContentType
        {
            UNKNOWN = 0, // 未知类型
            LARGE_IMAGE, // 大图
            SMALL_IMAGE, // 小图
            SMALL_IMAGE_VERTICAL, // 竖图
            GROUP_IMAGE, // 组图
            VIDEO // 视频
        }

        public enum RenderType
        {
            UNkNOWN = 0,
            CUSTOM, // 自渲染
            EXPRESS // 模版渲染
        }

        readonly IAdContentInfoClient mClient;

        public AdContentInfo(IAdContentInfoClient client)
        {
            mClient = client;
        }

        public LineItem GetLineItem()
        {
            return mClient.GetLineItem();
        }

        public string GetTitle()
        {
            return mClient.GetTitle();
        }

        public string GetSubTitle()
        {
            return mClient.GetSubTitle();
        }

        public string GetBody()
        {
            return mClient.GetBody();
        }

        public string GetAdvertiser()
        {
            return mClient.GetAdvertiser();
        }

        public string GetCallToAction()
        {
            return mClient.GetCallToAction();
        }

        public string GetPkgName()
        {
            return mClient.GetPkgName();
        }

        public IsApp GetIsApp()
        {
            return mClient.GetIsApp();
        }

        public ContentType GetContentType()
        {
            return mClient.GetContentType();
        }

        public RenderType GetRenderType()
        {
            return mClient.GetRenderType();
        }

        public int GetAdMode()
        {
            return mClient.GetAdMode();
        }

        public string GetIconUrl()
        {
            return mClient.GetIconUrl();
        }

        public string GetImageUrl()
        {
            return mClient.GetImageUrl();
        }

        public string GetClickUrl()
        {
            return mClient.GetClickUrl();
        }

        public string GetVideoUrl()
        {
            return mClient.GetVideoUrl();
        }

        public int GetVideoDuration()
        {
            return mClient.GetVideoDuration();
        }

        public string GetRatinig()
        {
            return mClient.GetRatinig();
        }

        public string GetPrice()
        {
            return mClient.GetPrice();
        }

        public string GetStore()
        {
            return mClient.GetStore();
        }

        public override string ToString()
        {
            return "Title: " + GetTitle()
                    + ", SubTitle: " + GetSubTitle()
                    + ", Body: " + GetBody()
                    + ", Advertiser: " + GetAdvertiser()
                    + ", CallToAction: " + GetCallToAction()
                    + ", PkgName: " + GetPkgName()

                    + ", IsApp: " + GetIsApp()
                    + ", ContentType: " + GetContentType()
                    + ", RenderType: " + GetRenderType()
                    + ", AdMode: " + GetAdMode()

                    + ", IconUrl: " + GetIconUrl()
                    + ", ImageUrl: " + GetImageUrl()
                    + ", ClickUrl: " + GetClickUrl()

                    + ", VideoUrl: " + GetVideoUrl()
                    + ", VideoDuration: " + GetVideoDuration() + "ms"

                    + ", Rating: " + GetRatinig()
                    + ", Price: " + GetPrice()
                    + ", Store: " + GetStore();
        }
    }
}
