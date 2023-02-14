using System;
using UnityEngine;
using RichOX.Api;
using RichOX.Common;

namespace RichOX.Platforms.Android
{
    public class NativeSceneClient : AndroidJavaProxy, INativeSceneClient
    {
        public event EventHandler<EventArgs> OnLoaded;
        public event EventHandler<EventArgs> OnShown;
        public event EventHandler<EventArgs> OnClicked;
        public event EventHandler<EventArgs> OnClosed;
        public event EventHandler<FailedToLoadEventArgs> OnFailedToLoad;
        public event EventHandler<EventArgs> OnRenderSuccess;
        public event EventHandler<FailedToRenderEventArgs> OnFailedToRender;
        public event EventHandler<EventArgs> OnUpdate;

        private AndroidJavaObject mNativeScene;
        private AndroidJavaObject mActivity;

        private NativeInfoUpdateCallback mNativeInfoUpdateCallback;

        private AndroidInterActiveListener mAndroidInterActiveListener;
        private AndroidActivityMissionListener mAndroidActivityMissionListener;

        public NativeSceneClient(string sceneId) : base(Utils.SceneListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            mActivity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            mNativeScene = new AndroidJavaObject(Utils.NativeSceneClassName, new object[] { mActivity, sceneId });
            mNativeScene.Call("setSceneListener", this);

            mNativeInfoUpdateCallback = new NativeInfoUpdateCallback();
            mNativeInfoUpdateCallback.OnUpdate += (sender, args) =>
            {
                if (OnUpdate != null)
                {
                    OnUpdate(this, args);
                }
            };
            mNativeScene.Call("registerNativeInfoUpdateCallback", mNativeInfoUpdateCallback);

            mAndroidInterActiveListener = new AndroidInterActiveListener();
            mNativeScene.Call("setInterActiveListener", mAndroidInterActiveListener);

            mAndroidActivityMissionListener = new AndroidActivityMissionListener();
            mNativeScene.Call("setMissionListener", mAndroidActivityMissionListener);
        }

        #region INativeSceneClient

        public void Load() {
            mNativeScene.Call("load");
        }

        public bool IsReady() {
            return mNativeScene.Call<bool>("isReady");
        }

        public NativeInfo GetNativeInfo() {
            return new NativeInfo(new NativeInfoClient(mNativeScene.Call<AndroidJavaObject>("getNativeInfo")));
        }

        public void ReportShown() {
            mNativeScene.Call("reportSceneShown");
        }

        public void HandleClick() {
            mNativeScene.Call("showScene");
        }

        public bool IsInterActive(string name)
        {
            return mNativeScene.Call<bool>("isInterActive", name);
        }

        public void SetInterActiveListener(InterActiveListener listener)
        {
            mAndroidInterActiveListener.mInterActiveListener = listener;
        }

        public void LoadInterActiveInfo()
        {
            mNativeScene.Call("loadInterActiveInfo");
        }

        public void SubmitInterActiveTask(int taskId)
        {
            mNativeScene.Call("submitInterActiveTask", taskId);
        }

        public void FetchInterActiveTaskStatus(int taskId)
        {
            mNativeScene.Call("fetchInterActiveTaskStatus", taskId);
        }
        
        public void SetActivityMissionListener(ActivityMissionListener listener) {
            mAndroidActivityMissionListener.mActivityMissionListener = listener;
        }

        public void FetchActivityMissionStatus(int taskId, int count) {
            mNativeScene.Call("fetchActivityMissionStatus", taskId, count);
        }

        public void Destroy() {
            mNativeScene.Call("destroy");
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


        #region NativeInfoUpdateCallback

        class NativeInfoUpdateCallback : AndroidJavaProxy
        {
            public event EventHandler<EventArgs> OnUpdate;

            public NativeInfoUpdateCallback() : base(Utils.NativeInfoUpdateCallbackClassName)
            {
            }

            public void onUpdate()
            {
                if (OnUpdate != null)
                {
                    OnUpdate(this, EventArgs.Empty);
                }
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