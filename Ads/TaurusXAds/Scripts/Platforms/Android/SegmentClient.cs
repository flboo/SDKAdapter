using UnityEngine;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class SegmentClient : ISegmentClient
    {
        private AndroidJavaObject mSegment;

        public SegmentClient(AndroidJavaObject segment)
        {
            mSegment = segment;
        }

        #region ISegmentClient

        public string GetId()
        {
            return mSegment.Call<string>("getId");
        }

        public int GetPriority()
        {
            return mSegment.Call<int>("getPriority");
        }

        public string GetChannel()
        {
            return mSegment.Call<string>("getChannel");
        }

        public string GetCondition()
        {
            return mSegment.Call<string>("getCondition");
        }

        #endregion
    }
}
