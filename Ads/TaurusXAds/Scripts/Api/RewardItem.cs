using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Api
{
    public class RewardItem
    {
        readonly IRewardItemClient mClient;

        public RewardItem(IRewardItemClient client)
        {
            mClient = client;
        }

        public string GetRewardType() {
            return mClient.GetRewardType();
        }

        public int GetAmount() {
            return mClient.GetAmount();
        }

        public override string ToString() {
            return "Type: " + GetRewardType() + ", Amount: " + GetAmount();
        }
    }
}
