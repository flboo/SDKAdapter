using System;
using RichOX.Api;
using RichOX.Common;
using System.Runtime.InteropServices;
using AOT;

namespace RichOX.Platforms.iOS
{
    public class DialogSceneClient : IDialogSceneClient, IDisposable
    {
        private IntPtr mDialogScenePtr;
        private IntPtr mDialogSceneClientPtr;


        #region DialogScene callback types

        internal delegate void RichOXDialogSceneDidReceiveCallback(IntPtr dialogSceneClient);
        internal delegate void RichOXDialogSceneDidFailToReceiveWithErrorCallback(IntPtr dialogSceneClient, int errorCode, string errorMessage);
        internal delegate void RichOXDialogSceneWillPresentScreenCallback(IntPtr dialogSceneClient);
        internal delegate void RichOXDialogSceneDidDismissScreenCallback(IntPtr dialogSceneClient);
        internal delegate void RichOXDialogSceneWillLeaveApplicationCallback(IntPtr dialogSceneClient);
        internal delegate void RichOXDialogSceneRenderSuccessCallback(IntPtr dialogSceneClient);
        internal delegate void RichOXDialogSceneFailedToRenderCallback(IntPtr dialogSceneClient, int errorCode, string errorMessage);

        #endregion


        // This property should be used when setting the mDialogScenePtr.
        private IntPtr DialogScenePtr
        {
            get { return mDialogScenePtr; }

            set
            {
                Externs.ROXRelease(mDialogScenePtr);
                mDialogScenePtr = value;
            }
        }

        public DialogSceneClient(string sceneId)
        {
            mDialogSceneClientPtr = (IntPtr)GCHandle.Alloc(this);
            DialogScenePtr = Externs.ROXCreateDialogScene(mDialogSceneClientPtr, sceneId);

            Externs.ROXSetDialogSceneCallbacks(
                    DialogScenePtr,
                    DialogSceneDidReceiveCallback,
                    DialogSceneDidFailToReceiveWithErrorCallback,
                    DialogSceneWillPresentScreenCallback,
                    DialogSceneDidDismissScreenCallback,
                    DialogSceneWillLeaveApplicationCallback,
                    DialogSceneRenderSuccessCallback,
                    DialogSceneFailedToRenderCallback);
        }


        #region IDialogSceneClient

        public event EventHandler<EventArgs> OnLoaded;
        public event EventHandler<EventArgs> OnShown;
        public event EventHandler<EventArgs> OnClicked;
        public event EventHandler<EventArgs> OnClosed;
        public event EventHandler<FailedToLoadEventArgs> OnFailedToLoad;
        public event EventHandler<EventArgs> OnRenderSuccess;
        public event EventHandler<FailedToRenderEventArgs> OnFailedToRender;

        public void Load()
        {
            Externs.ROXLoadDialogScene(DialogScenePtr);
        }

        public bool IsReady() {
            return Externs.ROXDialogSceneIsReady(DialogScenePtr);
        }

        public void Show()
        {
            Externs.ROXShowDialogScene(DialogScenePtr);
        }

        public bool IsInterActive(string name)
        {
            return false;
        }

        public void SetInterActiveListener(InterActiveListener listener)
        {
        }

        public void LoadInterActiveInfo()
        {
        }

        public void SubmitInterActiveTask(int taskId)
        {
        }

        public void FetchInterActiveTaskStatus(int taskId)
        {
        }

        public void SetActivityMissionListener(ActivityMissionListener listener) {

        }

        public void FetchActivityMissionStatus(int taskId, int count) {

        }

        public void Destroy()
        {
            Externs.ROXDestroyDialogScene(DialogScenePtr);
            DialogScenePtr = IntPtr.Zero;
        }

        public void Dispose()
        {
            Destroy();
            ((GCHandle)mDialogSceneClientPtr).Free();
        }

        ~DialogSceneClient()
        {
            Dispose();
        }

        #endregion


        #region DialogScene callback methods

        [MonoPInvokeCallback(typeof(RichOXDialogSceneDidReceiveCallback))]
        private static void DialogSceneDidReceiveCallback(IntPtr dialogSceneClient)
        {
            DialogSceneClient client = IntPtrToDialogSceneClient(dialogSceneClient);
            if (client.OnLoaded != null) {
                client.OnLoaded(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXDialogSceneDidFailToReceiveWithErrorCallback))]
        private static void DialogSceneDidFailToReceiveWithErrorCallback(
            IntPtr dialogSceneClient, int errorCode, string errorMessage)
        {
            DialogSceneClient client = IntPtrToDialogSceneClient(dialogSceneClient);
            if (client.OnFailedToLoad != null)
            {
                FailedToLoadEventArgs args = new FailedToLoadEventArgs()
                {
                    RichOXError = new RichOXError(new RichOXErrorClient(errorCode, errorMessage))
                };
                client.OnFailedToLoad(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXDialogSceneWillPresentScreenCallback))]
        private static void DialogSceneWillPresentScreenCallback(IntPtr dialogSceneClient)
        {
            DialogSceneClient client = IntPtrToDialogSceneClient(dialogSceneClient);
            if (client.OnShown != null)
            {
                client.OnShown(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXDialogSceneDidDismissScreenCallback))]
        private static void DialogSceneDidDismissScreenCallback(IntPtr dialogSceneClient)
        {
            DialogSceneClient client = IntPtrToDialogSceneClient(dialogSceneClient);
            if (client.OnClosed != null)
            {
                client.OnClosed(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXDialogSceneWillLeaveApplicationCallback))]
        private static void DialogSceneWillLeaveApplicationCallback(IntPtr dialogSceneClient)
        {
            DialogSceneClient client = IntPtrToDialogSceneClient(dialogSceneClient);
            if (client.OnClicked != null)
            {
                client.OnClicked(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXDialogSceneRenderSuccessCallback))]
        private static void DialogSceneRenderSuccessCallback(IntPtr dialogSceneClient)
        {
            DialogSceneClient client = IntPtrToDialogSceneClient(dialogSceneClient);
            if (client.OnRenderSuccess != null)
            {
                client.OnRenderSuccess(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXDialogSceneFailedToRenderCallback))]
        private static void DialogSceneFailedToRenderCallback(
            IntPtr dialogSceneClient, int errorCode, string errorMessage)
        {
            DialogSceneClient client = IntPtrToDialogSceneClient(dialogSceneClient);
            if (client.OnFailedToRender != null)
            {
                FailedToRenderEventArgs args = new FailedToRenderEventArgs()
                {
                    RichOXError = new RichOXError(new RichOXErrorClient(errorCode, errorMessage))
                };
                client.OnFailedToRender(client, args);
            }
        }

        private static DialogSceneClient IntPtrToDialogSceneClient(IntPtr dialogSceneClient)
        {
            GCHandle handle = (GCHandle)dialogSceneClient;
            return handle.Target as DialogSceneClient;
        }

        #endregion
    }
}
