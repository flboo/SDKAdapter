namespace TaurusXAdSdk.Common
{
    public interface INativeAdDataClient
    {
        bool IsReady();
        
        string GetAdTitle();

        string GetAdSubtitle();

        string GetAdBody();

        string GetAdvertiser();

        string GetCallToAction();

        // icon
        byte[] GetIcon();

        string GetIconUrl();

        // cover
        byte[] GetCover();

        string GetCoverUrl();

        // adchoices
        string GetAdChoicesLinkUrl();

        string GetAdChoicesText();

        string GetAdChoicesImageUr();

        byte[] GetAdChoicesImage();


        double GetRating();

        string GetPrice();

        string GetStore();
    }
}
