using UnityEngine;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class NativeAdDataClient : INativeAdDataClient
    {
        private AndroidJavaObject mNativeAdData;

        public NativeAdDataClient(AndroidJavaObject nativeAdData)
        {
            mNativeAdData = nativeAdData;
        }

        #region INativeAdDataClient

        public bool IsReady() {
            return mNativeAdData.Call<bool>("isReady");
        }

        public string GetAdTitle() {
            return mNativeAdData.Call<string>("getAdTitle");
        }

        public string GetAdSubtitle() {
            return mNativeAdData.Call<string>("getAdSubtitle");
        }

        public string GetAdBody() {
            return mNativeAdData.Call<string>("getAdBody");
        }

        public string GetAdvertiser() {
            return mNativeAdData.Call<string>("getAdvertiser");
        }

        public string GetCallToAction() {
            return mNativeAdData.Call<string>("getCallToAction");
        }

        // icon
        public byte[] GetIcon() {
            return mNativeAdData.Call<byte[]>("getIconData");
        }

        public string GetIconUrl() {
            return mNativeAdData.Call<string>("getIconUrl");
        }

        // cover
        public byte[] GetCover() {
            return mNativeAdData.Call<byte[]>("getCoverData");
        }

        public string GetCoverUrl() {
            return mNativeAdData.Call<string>("getCoverUrl");
        }

        // adchoices
        public string GetAdChoicesLinkUrl() {
            return mNativeAdData.Call<string>("getAdChoiceLinkUrl");
        }

        public string GetAdChoicesText() {
            return mNativeAdData.Call<string>("getAdChoicesText");
        }

        public string GetAdChoicesImageUr() {
            return mNativeAdData.Call<string>("getAdChoicesImageUrl");
        }

        public byte[] GetAdChoicesImage() {
            return mNativeAdData.Call<byte[]>("getAdChoicesImageData");
        }


        public double GetRating() {
            return mNativeAdData.Call<double>("getRating");
        }

        public string GetPrice() {
            return mNativeAdData.Call<string>("getPrice");
        }

        public string GetStore() {
            return mNativeAdData.Call<string>("getStore");
        }

        #endregion
    }
}
