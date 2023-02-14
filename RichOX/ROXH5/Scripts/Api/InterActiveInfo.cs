using RichOX.Common;

namespace RichOX.Api
{
    public class InterActiveInfo
    {
        readonly IInterActiveInfoClient mClient;
        
        public InterActiveInfo(IInterActiveInfoClient client)
        {
            mClient = client;
        }

        public bool HasTriggered() {
            return mClient.HasTriggered();
        }

        public int GetRewardedNumber() {
            return mClient.GetRewardedNumber();
        }

        public int GetMaxNumber() {
            return mClient.GetMaxNumber();
        }

        public int GetCurrentNumber() {
            return mClient.GetCurrentNumber();
        }

        public string GetExtra() {
            return mClient.GetExtra();
        }

        public override string ToString() {
            return "HasTriggered is " + HasTriggered()
                + ", RewardedNumber is " + GetRewardedNumber()
                + ", MaxNumber is " + GetMaxNumber()
                + ", CurrentNumber is " + GetCurrentNumber()
                + ", Extra is " + GetExtra();
        }
    }
}