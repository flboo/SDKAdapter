using UnityEngine;
using RichOX.Common;

namespace RichOX.Platforms.Android
{
    public class ItemInfoClient : IItemInfoClient
    {
        private int mType;
        private string mTitle;
        private int mAmount; 

        public ItemInfoClient(int type, string title, int count)
        {
            mType = type;
            mTitle = title;
            mAmount = count;
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
