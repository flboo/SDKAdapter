using UnityEngine;
using RichOX.Common;

namespace RichOX.Platforms.Android
{
    public class NativeInfoClient : INativeInfoClient
    {
        private AndroidJavaObject mNativeInfo;

        public NativeInfoClient(AndroidJavaObject nativeInfo)
        {
            mNativeInfo = nativeInfo;
        }

        #region INativeInfoClient

        public string GetTitle()
        {
            return mNativeInfo.Call<string>("getTitle");
        }

        public string GetIconUrl()
        {
            return mNativeInfo.Call<string>("getIconUrl");
        }

        public string GetDesc()
        {
            return mNativeInfo.Call<string>("getDesc");
        }

        public string GetCTA()
        {
            return mNativeInfo.Call<string>("getCTA");
        }

        public string GetMediaUrl()
        {
            return mNativeInfo.Call<string>("getMediaUrl");
        }

        #endregion
    }
}
