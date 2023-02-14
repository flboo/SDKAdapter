using System;
using RichOX.Common;
using RichOX.Platforms;

namespace RichOX.Api
{
    public class NativeScene
    {
        private INativeSceneClient mClient;
    
        public NativeScene(string sceneId)
        {
            mClient = ClientFactory.BuildNativeSceneClient(sceneId);
            ConfigureNativeSceneEvents();
        }

        #region AdListener

        public event EventHandler<EventArgs> OnLoaded;

        public event EventHandler<EventArgs> OnShown;

        public event EventHandler<EventArgs> OnClicked;

        public event EventHandler<EventArgs> OnClosed;

        public event EventHandler<FailedToLoadEventArgs> OnFailedToLoad;

        public event EventHandler<EventArgs> OnRenderSuccess;

        public event EventHandler<FailedToRenderEventArgs> OnFailedToRender;

        public event EventHandler<EventArgs> OnUpdate;

        #endregion

        #region Api

        public void Load() {
            mClient.Load();
        }

        public bool IsReady() {
            return mClient.IsReady();
        }

        public NativeInfo GetNativeInfo() {
            return mClient.GetNativeInfo();
        }

        public void ReportShown() {
            mClient.ReportShown();
        }

        public void HandleClick() {
            mClient.HandleClick();
        }

        /**
         * 判断当前活动是否为特定的交互活动，根据 APP 传进来的名称来匹配当前活动是否为交互活动
         * @param name APP 欲使用的交互活动名称
         * @return 是否为交互活动
         */
        public bool IsInterActive(string name)
        {
            return mClient.IsInterActive(name);
        }

        public void SetInterActiveListener(InterActiveListener listener)
        {
            mClient.SetInterActiveListener(listener);
        }

        /**
         * 加载当前活动配置信息
         * 异步任务，加载成功后会在 InterActiveListener Initialized() 回调中通知给 APP
         */
        public void LoadInterActiveInfo()
        {
            mClient.LoadInterActiveInfo();
        }

        /**
         * 根据当前的任务 ID， 上报活动数据至 ROX 服务器
         * 不同的活动可能有不同的任务，具体使用哪个任务，需要 APP 传递
         * 异步任务，上报结果会在 updateFromServer 回调中，通知给 APP
         * @param taskId 任务 ID
         */
        public void SubmitInterActiveTask(int taskId)
        {
            mClient.SubmitInterActiveTask(taskId);
        }

        /**
         * 获取当前任务状态信息
         * 异步任务，上报结果会在 InterActiveListener.UpdateStatusFormH5 回调中，通知给 APP
         * @param taskId 任务ID
         */
        public void FetchInterActiveTaskStatus(int taskId)
        {
            mClient.FetchInterActiveTaskStatus(taskId);
        }

        
        public void SetActivityMissionListener(ActivityMissionListener listener) {
            mClient.SetActivityMissionListener(listener);
        }
        public void FetchActivityMissionStatus(int taskId, int count) {
            mClient.FetchActivityMissionStatus(taskId, count);
        }


        public void Destroy() {
            mClient.Destroy();
        }

        #endregion

        private void ConfigureNativeSceneEvents()
        {
            mClient.OnLoaded += (sender, args) => 
            {
                if (OnLoaded != null) {
                    OnLoaded(this, args);
                }
            };

            mClient.OnShown += (sender, args) => 
            {
                if (OnShown != null) {
                    OnShown(this, args);
                }
            };

            mClient.OnClicked += (sender, args) => 
            {
                if (OnClicked != null) {
                    OnClicked(this, args);
                }
            };

            mClient.OnClosed += (sender, args) => 
            {
                if (OnClosed != null) {
                    OnClosed(this, args);
                }
            };

            mClient.OnFailedToLoad += (sender, args) => 
            {
                if (OnFailedToLoad != null) {
                    OnFailedToLoad(this, args);
                }
            };

            mClient.OnRenderSuccess += (sender, args) =>
            {
                if (OnRenderSuccess != null) {
                    OnRenderSuccess(this, args);
                }
            };

            mClient.OnFailedToRender += (sender, args) =>
            {
                if (OnFailedToRender != null) {
                    OnFailedToRender(this, args);
                }
            };

            mClient.OnUpdate += (sender, args) =>
            {
                if (OnUpdate != null)
                {
                    OnUpdate(this, args);
                }
            };
        }
    }
}
