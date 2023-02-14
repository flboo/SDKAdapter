using UnityEngine;
using RichOX.Common;

namespace RichOX.Platforms.Android
{
    public class InterActiveInfoClient : IInterActiveInfoClient
    {
        private AndroidJavaObject mInterActiveInfo;

        public InterActiveInfoClient(AndroidJavaObject interActiveInfo)
        {
            mInterActiveInfo = interActiveInfo;
        }

        #region IInterActiveInfoClient

        public bool HasTriggered() {
            return mInterActiveInfo.Call<bool>("hasTriggered");
        }

        public int GetRewardedNumber() {
            return mInterActiveInfo.Call<int>("getRewardedNumber");
        }

        public int GetMaxNumber() {
            return mInterActiveInfo.Call<int>("getMaxNumber");
        }

        public int GetCurrentNumber() {
            return mInterActiveInfo.Call<int>("getCurrentNumber");
        }

        public string GetExtra() {
            return mInterActiveInfo.Call<string>("getExtra");
        }

        #endregion
    }
}
