using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Common
{
    public interface IAdContentInfoClient
    {
        LineItem GetLineItem();

        string GetTitle();

        string GetSubTitle();

        string GetBody();

        string GetAdvertiser();

        string GetCallToAction();

        string GetPkgName();

        AdContentInfo.IsApp GetIsApp();

        AdContentInfo.ContentType GetContentType();

        AdContentInfo.RenderType GetRenderType();

        int GetAdMode();

        string GetIconUrl();

        string GetImageUrl();

        string GetClickUrl();

        string GetVideoUrl();
        int GetVideoDuration(); // ms

        string GetRatinig();

        string GetPrice();

        string GetStore();
    }
}
