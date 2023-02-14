using RichOX.Common;

namespace RichOX.Api
{
    public class ItemInfo
    {
        public static int TYPE_COIN = 0; // 金币
        public static int TYPE_CHANGE = 1; // 零钱
        public static int TYPE_POINTS = 2; // 积分

        readonly IItemInfoClient mClient;

        public ItemInfo(IItemInfoClient client)
        {
            mClient = client;
        }

        public int GetInfoType() {
            return mClient.GetInfoType();
        }

        public string GetTitle() {
            return mClient.GetTitle();
        }

        public int GetAmount() {
            return mClient.GetAmount();
        }

        public override string ToString() {
            return "Type is " + GetInfoType() 
                + ", Title is " + GetTitle() 
                + ", Amount is " + GetAmount();
        }
    }
}
