using System;
using RichOX.Api;

namespace RichOX.Common
{
    public interface IDialogSceneClient
    {
        event EventHandler<EventArgs> OnLoaded;
        event EventHandler<EventArgs> OnShown;
        event EventHandler<EventArgs> OnClicked;
        event EventHandler<EventArgs> OnClosed;
        event EventHandler<FailedToLoadEventArgs> OnFailedToLoad;
        event EventHandler<EventArgs> OnRenderSuccess;
        event EventHandler<FailedToRenderEventArgs> OnFailedToRender;

        void Load();

        bool IsReady();

        void Show();

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
