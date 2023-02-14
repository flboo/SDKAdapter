using UnityEngine;
using RichOX.Common;

namespace RichOX.Platforms.Android
{
    public class MissionInfoClient : IMissionInfoClient
    {
        private AndroidJavaObject mMissionInfo;

        public MissionInfoClient(AndroidJavaObject missionInfo)
        {
            mMissionInfo = missionInfo;
        }

        #region IMissionInfoClient

        public int GetStatus() {
            return mMissionInfo.Call<int>("getStatus");
        }

        public int GetGap() {
            return mMissionInfo.Call<int>("getGap");
        }

        #endregion
    }
}
