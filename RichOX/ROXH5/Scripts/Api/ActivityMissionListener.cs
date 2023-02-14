using System;
namespace RichOX.Api
{
    public interface ActivityMissionListener
    {
        /**
         * 更新当前 taskId 对应的Mission信息 
         */
        void Update(int taskId, MissionInfo info);
    }
}
