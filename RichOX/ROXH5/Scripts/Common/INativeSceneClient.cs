using System;
using RichOX.Api;

namespace RichOX.Common
{
    public interface INativeSceneClient
    {
        event EventHandler<EventArgs> OnLoaded;
        event EventHandler<EventArgs> OnShown;
        event EventHandler<EventArgs> OnClicked;
        event EventHandler<EventArgs> OnClosed;
        event EventHandler<FailedToLoadEventArgs> OnFailedToLoad;
        event EventHandler<EventArgs> OnRenderSuccess;
        event EventHandler<FailedToRenderEventArgs> OnFailedToRender;
        event EventHandler<EventArgs> OnUpdate;

        void Load();

        bool IsReady();

        NativeInfo GetNativeInfo();

        void ReportShown();

        void HandleClick();

        bool IsInterActive(string name);
        void SetInterActiveListener(InterActiveListener listener);
        void LoadInterActiveInfo();
        void SubmitInterActiveTask(int taskId);
        void FetchInterActiveTaskStatus(int taskId);

        void SetActivityMissionListener(ActivityMissionListener listener);
        void FetchActivityMissionStatus(int taskId, int count);

        void Destroy();
    }
}
