using System;
using RichOX.Api;

namespace RichOX.Common
{
    public class DummyNativeSceneClient : INativeSceneClient
    {
        public event EventHandler<EventArgs> OnLoaded;
        public event EventHandler<EventArgs> OnShown;
        public event EventHandler<EventArgs> OnClicked;
        public event EventHandler<EventArgs> OnClosed;
        public event EventHandler<FailedToLoadEventArgs> OnFailedToLoad;
        public event EventHandler<EventArgs> OnRenderSuccess;
        public event EventHandler<FailedToRenderEventArgs> OnFailedToRender;
        public event EventHandler<EventArgs> OnUpdate;

        #region INativeSceneClient

        public void Load() { }

        public bool IsReady() {
            return false;
        }

        public NativeInfo GetNativeInfo() {
            return null;
        }

        public void ReportShown() { }

        public void HandleClick() { }

        public bool IsInterActive(string name)
        {
            return false;
        }
        public void SetInterActiveListener(InterActiveListener listener) { }
        public void LoadInterActiveInfo() { }
        public void SubmitInterActiveTask(int taskId) { }
        public void FetchInterActiveTaskStatus(int taskId) { }

        public void SetActivityMissionListener(ActivityMissionListener listener) {}
        public void FetchActivityMissionStatus(int taskId, int count) {}
        public void Destroy() { }

        #endregion
    }
}