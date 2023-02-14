using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Api
{
    /**
     * Segment 可以标记当前用户的属性。
     * 后台会根据 Segment 返回对应的 AdUnit 配置。
     */
    public class Segment
    {
        public Segment() { }

        private string mChannel;

        /**
         * 设置渠道。
         */
        public void SetChannel(string channel) {
            mChannel = channel;
        }



        private ISegmentClient Client;

        public Segment(ISegmentClient client)
        {
            Client = client;
        }

        public string GetId()
        {
            return Client.GetId();
        }

        public int GetPriority()
        {
            return Client.GetPriority();
        }

        public string GetChannel()
        {
            if(mChannel != null && !mChannel.Equals(""))
            {
                return mChannel;
            }
            else if(Client != null)
            {
                return Client.GetChannel();
            }
            return "";
        }

        public string GetCondition()
        {
            return Client.GetCondition();
        }

        public override string ToString()
        {
            return "Id: " + GetId()
                + ", Priority: " + GetPriority()
                + ", Channel: " + GetChannel()
                + ", Condition: " + GetCondition();
        }
    }
}