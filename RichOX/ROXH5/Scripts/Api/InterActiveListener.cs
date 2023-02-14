using System;
namespace RichOX.Api
{
    public interface InterActiveListener
    {
        /**
         * 活动配置数据拉取通知
         * @param status 拉取状态
         * @param interActiveInfo 交互活动配置信息
         */
        void Initialized(bool status, InterActiveInfo interActiveInfo);

        /**
         * APP 上报活动任务后，会拉取最新的活动信息，供 APP 更新当前活动状态
         * @param id 上报的任务ID
         * @param status 更新状态
         * @param info 交互活动配置信息
         */
        void UpdateFromServer(int id, bool status, InterActiveInfo interActiveInfo);

        /**
         * 用户在 H5 活动中完成相应的任务，可能会改变交互活动的状态信息
         * 该接口用来通知 APP 某个关联的任务完成，需要更新活动的状态
         * @param id 关联任务ID
         * @param status 关联任务完成状态
         * @param progress 关联任务完成进度
         */
        void UpdateStatusFormH5(int id, bool status, int progress);
    }
}
