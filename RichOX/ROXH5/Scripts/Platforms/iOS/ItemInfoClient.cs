using RichOX.Common;

namespace RichOX.Platforms.iOS
{
    public class ItemInfoClient : IItemInfoClient
    {
        private int mType;
        private string mTitle;
        private int mAmount; 

        public ItemInfoClient(int type, string title, int amount)
        {
            mType = type;
            mTitle = title;
            mAmount = amount;
        }

        #region IRewardInfoClient

        public int GetInfoType()
        {
            return mType;
        }

        public string GetTitle()
        {
            return mTitle;
        }

        public int GetAmount() {
            return mAmount;
        }

        #endregion
    }
}
