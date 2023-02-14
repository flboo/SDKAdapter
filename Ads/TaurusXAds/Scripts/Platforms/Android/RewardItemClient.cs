using UnityEngine;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class RewardItemClient : IRewardItemClient
    {
        private AndroidJavaObject mRewardItem;

        public RewardItemClient(AndroidJavaObject rewardItem)
        {
            mRewardItem = rewardItem;
        }

        #region IRewardItemClient

        public string GetRewardType()
        {
            if (mRewardItem != null) {
                return mRewardItem.Call<string>("getType");                
            } else {
                return "";
            }
        }

        public int GetAmount()
        {
            if (mRewardItem != null) {
                return mRewardItem.Call<int>("getAmount");
            } else {
                return 0;
            }
        }

        #endregion
    }
}
