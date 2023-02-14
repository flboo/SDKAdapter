using System;
using UnityEngine;
using RichOX.Api;
using RichOX.Common;

namespace RichOX.Platforms.Android
{
    public class FloatSceneClient : AndroidJavaProxy, IFloatSceneClient
    {
        private AndroidJavaObject mFloatScene;

        public event EventHandler<EventArgs> OnLoaded;
        public event EventHandler<EventArgs> OnShown;
        public event EventHandler<EventArgs> OnClicked;
        public event EventHandler<EventArgs> OnClosed;
        public event EventHandler<FailedToLoadEventArgs> OnFailedToLoad;
        public event EventHandler<EventArgs> OnRenderSuccess;
        public event EventHandler<FailedToRenderEventArgs> OnFailedToRender;

        private AndroidInterActiveListener mAndroidInterActiveListener;
        private AndroidActivityMissionListener mAndroidActivityMissionListener;

        public FloatSceneClient(string sceneId) : base(Utils.SceneListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject container = new AndroidJavaObject(Utils.FrameLayoutClassName, new object[] { activity });

            mFloatScene = new AndroidJavaObject(Utils.FloatSceneClassName, new object[] { activity, sceneId, container});
            mFloatScene.Call("setSceneListener", this);

            mAndroidInterActiveListener = new AndroidInterActiveListener();
            mFloatScene.Call("setInterActiveListener", mAndroidInterActiveListener);

            mAndroidActivityMissionListener = new AndroidActivityMissionListener();
            mFloatScene.Call("setMissionListener", mAndroidActivityMissionListener);
        }

        #region IFloatSceneClient

        public void SetPosition(Position position) {
            mFloatScene.Call("setUnityPosition", (int)position);
        }

        public void SetPosition(int x, int y) {
            mFloatScene.Call("setUnityPosition", x, y);
        }

        public void SetPositionRelative(Position position, int offsetX, int offsetY) {
            mFloatScene.Call("setUnityPosition", (int)position, offsetX, offsetY);
        }

        public void SetSize(int width, int height) {
            mFloatScene.Call("setUnitySize", width, height);
        }

        public void Load() {
            mFloatScene.Call("loadUnity");
        }

        public bool IsReady() {
            return mFloatScene.Call<bool>("isReady");
        }

        public void Show() {
            mFloatScene.Call("showUnity");
        }

        public void Hide() {
            mFloatScene.Call("hideUnity");
        }

        public bool IsInterActive(string name)
        {
            return mFloatScene.Call<bool>("isInterActive", name);
        }

        public void SetInterActiveListener(InterActiveListener listener)
        {
            mAndroidInterActiveListener.mInterActiveListener = listener;
        }

        public void LoadInterActiveInfo()
        {
            mFloatScene.Call("loadInterActiveInfo");
        }

        public void SubmitInterActiveTask(int taskId)
        {
            mFloatScene.Call("submitInterActiveTask", taskId);
        }

        public void FetchInterActiveTaskStatus(int taskId)
        {
            mFloatScene.Call("fetchInterActiveTaskStatus", taskId);
        }

        public void SetActivityMissionListener(ActivityMissionListener listener) {
            mAndroidActivityMissionListener.mActivityMissionListener = listener;
        }

        public void FetchActivityMissionStatus(int taskId, int count) {
            mFloatScene.Call("fetchActivityMissionStatus", taskId, count);
        }

        public void Destroy() {
            mFloatScene.Call("destroy");
        }

        #endregion


        #region SceneListener

        public void onLoaded()
        {
            if (OnLoaded != null)
            {
                OnLoaded(this, EventArgs.Empty);
            }
        }

        public void onShown()
        {
            if (OnShown != null)
            {
                OnShown(this, EventArgs.Empty);
            }
        }

        public void onClick()
        {
            if (OnClicked != null)
            {
                OnClicked(this, EventArgs.Empty);
            }
        }

        public void onClose()
        {
            if (OnClosed != null)
            {
                OnClosed(this, EventArgs.Empty);
            }
        }

        public void onLoadFailed(AndroidJavaObject error)
        {
            if (OnFailedToLoad != null)
            {
                FailedToLoadEventArgs args = new FailedToLoadEventArgs()
                {
                    RichOXError = new RichOXError(new RichOXErrorClient(error))
                };
                OnFailedToLoad(this, args);
            }
        }

        public void onRenderSuccess()
        {
            if (OnRenderSuccess != null)
            {
                OnRenderSuccess(this, EventArgs.Empty);
            }
        }

        public void onRenderFailed(AndroidJavaObject error)
        {
            if (OnFailedToRender != null)
            {
                FailedToRenderEventArgs args = new FailedToRenderEventArgs()
                {
                    RichOXError = new RichOXError(new RichOXErrorClient(error))
                };
                OnFailedToRender(this, args);
            }
        }

        #endregion


        #region InterActiveListener

        class AndroidInterActiveListener : AndroidJavaProxy
        {
            public InterActiveListener mInterActiveListener;

            public AndroidInterActiveListener() : base(Utils.InterActiveListenerClassName)
            {
            }

            public void initialized(bool status, AndroidJavaObject interActiveInfo)
            {
                if (mInterActiveListener != null)
                {
                    InterActiveInfo info = new InterActiveInfo(new InterActiveInfoClient(interActiveInfo));
                    mInterActiveListener.Initialized(status, info);
                }
            }

            public void updateFromServer(int id, bool status, AndroidJavaObject interActiveInfo)
            {
                if (mInterActiveListener != null)
                {
                    InterActiveInfo info = new InterActiveInfo(new InterActiveInfoClient(interActiveInfo));
                    mInterActiveListener.UpdateFromServer(id, status, info);
                }
            }

            public void updateStatusFormH5(int id, bool status, int progress)
            {
                if (mInterActiveListener != null)
                {
                    mInterActiveListener.UpdateStatusFormH5(id, status, progress);
                }
            }
        }

        #endregion

        #region ActivityMissionListener
        class AndroidActivityMissionListener : AndroidJavaProxy 
        {
            public ActivityMissionListener mActivityMissionListener;

            public AndroidActivityMissionListener() : base(Utils.ActivityMissionListenerClassName)
            {
            }

            public void update(int taskId , AndroidJavaObject missionInfo)
            {
                if (mActivityMissionListener != null)
                {
                    MissionInfo info = new MissionInfo(new MissionInfoClient(missionInfo));
                    mActivityMissionListener.Update(taskId, info);
                }
            }

        }
        #endregion
    }
}