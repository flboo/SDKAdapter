using System;
using RichOX.Common;

namespace RichOX.Platforms.iOS
{
    public class NativeInfoClient : INativeInfoClient
    {
        private readonly IntPtr mNativeInfo;

        public NativeInfoClient(IntPtr nativeInfo)
        {
            mNativeInfo = nativeInfo;
        }

        #region INativeInfoClient

        public string GetTitle()
        {
            return Externs.ROXGetNativeInfoTitle(mNativeInfo);
        }

        public string GetIconUrl()
        {
            return Externs.ROXGetNativeInfoIconUrl(mNativeInfo);
        }

        public string GetDesc()
        {
            return Externs.ROXGetNativeInfoDesc(mNativeInfo);
        }

        public string GetCTA()
        {
            return Externs.ROXGetNativeInfoCTA(mNativeInfo);
        }

        public string GetMediaUrl()
        {
            return Externs.ROXGetNativeInfoMediaUrl(mNativeInfo);
        }

        #endregion
    }
}
