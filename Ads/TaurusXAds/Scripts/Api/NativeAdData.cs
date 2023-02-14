using UnityEngine;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Api
{
    public class NativeAdData
    {
        INativeAdDataClient mClient;

        public NativeAdData(INativeAdDataClient client) {
            mClient = client;
        }

        public bool IsReady() {
            return mClient.IsReady();
        }

        public string GetAdTitle() {
            return mClient.GetAdTitle();
        }

        public string GetAdSubtitle() {
            return mClient.GetAdSubtitle();
        }

        public string GetAdBody() {
            return mClient.GetAdBody();
        }

        public string GetAdvertiser() {
            return mClient.GetAdvertiser();
        }

        public string GetCallToAction() {
            return mClient.GetCallToAction();
        }

        // icon
        public NativeAdImage GetIcon() {
            NativeAdImage image = new NativeAdImage();
            image.mTexture2D = GetIconTexture2D();
            image.mUrl = GetIconUrl();
            return image;
        }

        private Texture2D GetIconTexture2D() {
            return Utils.GetTexture2DFromByteArray(mClient.GetIcon());
        }

        private string GetIconUrl() {
            return mClient.GetIconUrl();
        }

        // cover
        public NativeAdImage GetCover() {
            NativeAdImage image = new NativeAdImage();
            image.mTexture2D = GetCoverTexture2D();
            image.mUrl = GetCoverUrl();
            return image;
        }

        private Texture2D GetCoverTexture2D() {
            return Utils.GetTexture2DFromByteArray(mClient.GetCover());
        }

        private string GetCoverUrl() {
            return mClient.GetCoverUrl();
        }

        // adchoices
        public NativeAdChoices GetAdChoices() {
            NativeAdChoices adChoices = new NativeAdChoices();
            adChoices.mTexture2D = GetAdChoicesImage();
            adChoices.mImageUrl = GetAdChoicesImageUr();
            adChoices.mText = GetAdChoicesText();
            adChoices.mLinkUr = GetAdChoicesLinkUrl();
            return adChoices;
        }

        private Texture2D GetAdChoicesImage() {
            return Utils.GetTexture2DFromByteArray(mClient.GetAdChoicesImage());
        }

        private string GetAdChoicesImageUr() {
            return mClient.GetAdChoicesImageUr();
        }

        private string GetAdChoicesText() {
            return mClient.GetAdChoicesText();
        }

        private string GetAdChoicesLinkUrl() {
            return mClient.GetAdChoicesLinkUrl();
        }


        public double GetRating() {
            return mClient.GetRating();
        }

        public string GetPrice() {
            return mClient.GetPrice();
        }

        public string GetStore() {
            return mClient.GetStore();
        }
    }
}
