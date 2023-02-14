using UnityEngine;
using TaurusXAdSdk.Common;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Platforms.Android
{
    public class AdContentInfoClient : IAdContentInfoClient
    {
        private AndroidJavaObject mContentInfo;

        public AdContentInfoClient(AndroidJavaObject contentInfo)
        {
            mContentInfo = contentInfo;
        }

        #region IAdContentInfoClient

        public LineItem GetLineItem()
        {
            AndroidJavaObject lineItem = mContentInfo.Call<AndroidJavaObject>("getLineItem");
            return new LineItem(new LineItemClient(lineItem));
        }

        public string GetTitle()
        {
            return mContentInfo.Call<string>("getTitle");
        }

        public string GetSubTitle()
        {
            return mContentInfo.Call<string>("getSubTitle");
        }

        public string GetBody()
        {
            return mContentInfo.Call<string>("getBody");
        }

        public string GetAdvertiser()
        {
            return mContentInfo.Call<string>("getAdvertiser");
        }

        public string GetCallToAction()
        {
            return mContentInfo.Call<string>("getCallToAction");
        }

        public string GetPkgName()
        {
            return mContentInfo.Call<string>("getPkgName");
        }

        public AdContentInfo.IsApp GetIsApp()
        {
            AndroidJavaObject isApp = mContentInfo.Call<AndroidJavaObject>("getIsApp");
            int _isApp = isApp.Call<int>("ordinal");
            return (AdContentInfo.IsApp)_isApp;
        }

        public AdContentInfo.ContentType GetContentType()
        {
            AndroidJavaObject contentType = mContentInfo.Call<AndroidJavaObject>("getContentType");
            int type = contentType.Call<int>("ordinal");
            return (AdContentInfo.ContentType)type;
        }

        public AdContentInfo.RenderType GetRenderType()
        {
            AndroidJavaObject renderType = mContentInfo.Call<AndroidJavaObject>("getRenderType");
            int type = renderType.Call<int>("ordinal");
            return (AdContentInfo.RenderType)type;
        }

        public int GetAdMode()
        {
            return mContentInfo.Call<int>("getAdMode");
        }

        public string GetIconUrl()
        {
            return mContentInfo.Call<string>("getIconUrl");
        }

        public string GetImageUrl()
        {
            return mContentInfo.Call<string>("getImageUrl");
        }

        public string GetClickUrl()
        {
            return mContentInfo.Call<string>("getClickUrl");
        }

        public string GetVideoUrl()
        {
            return mContentInfo.Call<string>("getVideoUrl");
        }

        public int GetVideoDuration()
        {
            return mContentInfo.Call<int>("getVideoDuration");
        }

        public string GetRatinig()
        {
            return mContentInfo.Call<string>("getRatinig");
        }

        public string GetPrice()
        {
            return mContentInfo.Call<string>("getPrice");
        }

        public string GetStore()
        {
            return mContentInfo.Call<string>("getStore");
        }

        #endregion
    }
}
