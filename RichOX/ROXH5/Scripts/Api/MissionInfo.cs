using RichOX.Common;

namespace RichOX.Api
{
    public class MissionInfo
    {
        readonly IMissionInfoClient mClient;
        
        public MissionInfo(IMissionInfoClient client)
        {
            mClient = client;
        }

        public int GetStatus() {
            return mClient.GetStatus();
        }

        public int GetGap() {
            return mClient.GetGap();
        }

        public override string ToString() {
            return "status is " + GetStatus()
                + ", gap is " + GetGap();
        }
    }
}