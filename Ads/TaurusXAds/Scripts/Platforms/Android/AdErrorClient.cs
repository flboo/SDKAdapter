using UnityEngine;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class AdErrorClient : IAdErrorClient
    {
        private AndroidJavaObject mAdError;

        public AdErrorClient(AndroidJavaObject adError)
        {
            mAdError = adError;
        }

        #region IAdErrorClient

        public int GetCode()
        {
            return mAdError.Call<int>("getCode");
        }

        public string GetMessage()
        {
            return mAdError.Call<string>("getMessage");
        }

        #endregion
    }
}
