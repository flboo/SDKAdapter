using System;
using UnityEngine;
using RichOX.Api;
using RichOX.Common;

namespace RichOX.Platforms.Android
{
    public class DialogSceneClient : AndroidJavaProxy, IDialogSceneClient
    {
        private AndroidJavaObject mDialogScene;
        private AndroidJavaObject mActivity;

        public event EventHandler<EventArgs> OnLoaded;
        public event EventHandler<EventArgs> OnShown;
        public event EventHandler<EventArgs> OnClicked;
        public event EventHandler<EventArgs> OnClosed;
        public event EventHandler<FailedToLoadEventArgs> OnFailedToLoad;
        public event EventHandler<EventArgs> OnRenderSuccess;
        public event EventHandler<FailedToRenderEventArgs> OnFailedToRender;

        private AndroidInterActiveListener mAndroidInterActiveListener;

        private AndroidActivityMissionListener mAndroidActivityMissionListener;

        public DialogSceneClient(string sceneId) : base(Utils.SceneListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            mActivity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            mDialogScene = new AndroidJavaObject(Utils.DialogSceneClassName, new object[] { mActivity, sceneId });
            mDialogScene.Call("setSceneListener", this);

            mAndroidInterActiveListener = new AndroidInterActiveListener();
            mDialogScene.Call("setInterActiveListener", mAndroidInterActiveListener);

            mAndroidActivityMissionListener = new AndroidActivityMissionListener();
            mDialogScene.Call("setMissionListener", mAndroidActivityMissionListener);
        }

        #region IDialogSceneClient

        public void Load() {
            mDialogScene.Call("load");
        }

        public bool IsReady() {
            return mDialogScene.Call<bool>("isReady");
        }

        public void Show() {
            mDialogScene.Call("showDialog");
        }

        public bool IsInterActive(string name)
        {
            return mDialogScene.Call<bool>("isInterActive", name);
        }

        public void SetInterActiveListener(InterActiveListener listener)
        {
            mAndroidInterActiveListener.mInterActiveListener = listener;
        }

        public void LoadInterActiveInfo()
        {
            mDialogScene.Call("loadInterActiveInfo");
        }

        public void SubmitInterActiveTask(int taskId)
        {
            mDialogScene.Call("submitInterActiveTask", taskId);
        }

        public void FetchInterActiveTaskStatus(int taskId)
        {
            mDialogScene.Call("fetchInterActiveTaskStatus" ,taskId);
        }

        public void SetActivityMissionListener(ActivityMissionListener listener) {
            mAndroidActivityMissionListener.mActivityMissionListener = listener;
        }

        public void FetchActivityMissionStatus(int taskId, int count) {
            mDialogScene.Call("fetchActivityMissionStatus", taskId, count);
        }

        public void Destroy() {
            mDialogScene.Call("destroy");
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

            public void initialized(bool status, AndroidJavaObject interActiveInfo) {
                if(mInterActiveListener != null) {
                    InterActiveInfo info = new InterActiveInfo(new InterActiveInfoClient(interActiveInfo));
                    mInterActiveListener.Initialized(status, info);
                }
            }

            public void updateFromServer(int id, bool status, AndroidJavaObject interActiveInfo) {
                if(mInterActiveListener != null) {
                    InterActiveInfo info = new InterActiveInfo(new InterActiveInfoClient(interActiveInfo));
                    mInterActiveListener.UpdateFromServer(id, status, info);
                }
            }

            public void updateStatusFormH5(int id, bool status, int progress) {
                if (mInterActiveListener != null) {
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