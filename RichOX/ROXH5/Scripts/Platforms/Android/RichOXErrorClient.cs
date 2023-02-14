using UnityEngine;
using RichOX.Common;

namespace RichOX.Platforms.Android
{
    public class RichOXErrorClient : IRichOXErrorClient
    {
        private AndroidJavaObject mError;

        public RichOXErrorClient(AndroidJavaObject error)
        {
            mError = error;
        }

        #region IRichOXErrorClient

        public int GetCode()
        {
            return mError.Call<int>("getCode");
        }

        public string GetMessage()
        {
            return mError.Call<string>("getMessage");
        }

        #endregion
    }
}
