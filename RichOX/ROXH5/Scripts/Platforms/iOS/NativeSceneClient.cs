using System;
using RichOX.Api;
using RichOX.Common;
using System.Runtime.InteropServices;
using AOT;

namespace RichOX.Platforms.iOS
{
    public class NativeSceneClient : INativeSceneClient, IDisposable
    {
        private IntPtr mNativeScenePtr;
        private IntPtr mNativeSceneClientPtr;


        #region NativeScene callback types

        internal delegate void RichOXNativeSceneDidReceiveCallback(IntPtr nativeSceneClient);
        internal delegate void RichOXNativeSceneDidFailToReceiveWithErrorCallback(IntPtr nativeSceneClient, int errorCode, string errorMessage);
        internal delegate void RichOXNativeSceneWillPresentScreenCallback(IntPtr nativeSceneClient);
        internal delegate void RichOXNativeSceneDidDismissScreenCallback(IntPtr nativeSceneClient);
        internal delegate void RichOXNativeSceneWillLeaveApplicationCallback(IntPtr nativeSceneClient);
        internal delegate void RichOXNativeSceneRenderSuccessCallback(IntPtr nativeSceneClient);
        internal delegate void RichOXNativeSceneFailedToRenderCallback(IntPtr nativeSceneClient, int errorCode, string errorMessage);
        internal delegate void RichOXNativeSceneUpdateCallback(IntPtr nativeSceneClient);

        #endregion


        // This property should be used when setting the mNativeScenePtr.
        private IntPtr NativeScenePtr
        {
            get { return mNativeScenePtr; }

            set
            {
                Externs.ROXRelease(mNativeScenePtr);
                mNativeScenePtr = value;
            }
        }

        public NativeSceneClient(string sceneId)
        {
            mNativeSceneClientPtr = (IntPtr)GCHandle.Alloc(this);
            NativeScenePtr = Externs.ROXCreateNativeScene(mNativeSceneClientPtr, sceneId);

            Externs.ROXSetNativeSceneCallbacks(
                    NativeScenePtr,
                    NativeSceneDidReceiveCallback,
                    NativeSceneDidFailToReceiveWithErrorCallback,
                    NativeSceneWillPresentScreenCallback,
                    NativeSceneDidDismissScreenCallback,
                    NativeSceneWillLeaveApplicationCallback,
                    NativeSceneRenderSuccessCallback,
                    NativeSceneFailedToRenderCallback,
                    NativeSceneUpdateCallback);
        }


        #region INativeSceneClient

        public event EventHandler<EventArgs> OnLoaded;
        public event EventHandler<EventArgs> OnShown;
        public event EventHandler<EventArgs> OnClicked;
        public event EventHandler<EventArgs> OnClosed;
        public event EventHandler<FailedToLoadEventArgs> OnFailedToLoad;
        public event EventHandler<EventArgs> OnRenderSuccess;
        public event EventHandler<FailedToRenderEventArgs> OnFailedToRender;
        public event EventHandler<EventArgs> OnUpdate;

        public void Load()
        {
            Externs.ROXLoadNativeScene(NativeScenePtr);
        }

        public bool IsReady() {
            return Externs.ROXNativeSceneIsReady(NativeScenePtr);
        }

        public NativeInfo GetNativeInfo() {
            return new NativeInfo(new NativeInfoClient(Externs.ROXNativeSceneGetNativeInfo(NativeScenePtr)));
        }

        public void ReportShown() {
            Externs.ROXNativeSceneReportShown(NativeScenePtr);
        }

        public void HandleClick() {
            Externs.ROXNativeSceneHandleClick(NativeScenePtr);
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
            Externs.ROXDestroyNativeScene(NativeScenePtr);
            NativeScenePtr = IntPtr.Zero;
        }

        public void Dispose()
        {
            Destroy();
            ((GCHandle)mNativeSceneClientPtr).Free();
        }

        ~NativeSceneClient()
        {
            Dispose();
        }

        #endregion


        #region NativeScene callback methods

        [MonoPInvokeCallback(typeof(RichOXNativeSceneDidReceiveCallback))]
        private static void NativeSceneDidReceiveCallback(IntPtr nativeSceneClient)
        {
            NativeSceneClient client = IntPtrToNativeSceneClient(nativeSceneClient);
            if (client.OnLoaded != null) {
                client.OnLoaded(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXNativeSceneDidFailToReceiveWithErrorCallback))]
        private static void NativeSceneDidFailToReceiveWithErrorCallback(
            IntPtr nativeSceneClient, int errorCode, string errorMessage)
        {
            NativeSceneClient client = IntPtrToNativeSceneClient(nativeSceneClient);
            if (client.OnFailedToLoad != null)
            {
                FailedToLoadEventArgs args = new FailedToLoadEventArgs()
                {
                    RichOXError = new RichOXError(new RichOXErrorClient(errorCode, errorMessage))
                };
                client.OnFailedToLoad(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXNativeSceneWillPresentScreenCallback))]
        private static void NativeSceneWillPresentScreenCallback(IntPtr nativeSceneClient)
        {
            NativeSceneClient client = IntPtrToNativeSceneClient(nativeSceneClient);
            if (client.OnShown != null)
            {
                client.OnShown(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXNativeSceneDidDismissScreenCallback))]
        private static void NativeSceneDidDismissScreenCallback(IntPtr nativeSceneClient)
        {
            NativeSceneClient client = IntPtrToNativeSceneClient(nativeSceneClient);
            if (client.OnClosed != null)
            {
                client.OnClosed(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXNativeSceneWillLeaveApplicationCallback))]
        private static void NativeSceneWillLeaveApplicationCallback(IntPtr nativeSceneClient)
        {
            NativeSceneClient client = IntPtrToNativeSceneClient(nativeSceneClient);
            if (client.OnClicked != null)
            {
                client.OnClicked(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXNativeSceneRenderSuccessCallback))]
        private static void NativeSceneRenderSuccessCallback(IntPtr nativeSceneClient)
        {
            NativeSceneClient client = IntPtrToNativeSceneClient(nativeSceneClient);
            if (client.OnRenderSuccess != null)
            {
                client.OnRenderSuccess(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXNativeSceneFailedToRenderCallback))]
        private static void NativeSceneFailedToRenderCallback(
            IntPtr nativeSceneClient, int errorCode, string errorMessage)
        {
            NativeSceneClient client = IntPtrToNativeSceneClient(nativeSceneClient);
            if (client.OnFailedToRender != null)
            {
                FailedToRenderEventArgs args = new FailedToRenderEventArgs()
                {
                    RichOXError = new RichOXError(new RichOXErrorClient(errorCode, errorMessage))
                };
                client.OnFailedToRender(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXNativeSceneUpdateCallback))]
        private static void NativeSceneUpdateCallback(IntPtr nativeSceneClient)
        {
            NativeSceneClient client = IntPtrToNativeSceneClient(nativeSceneClient);
            if (client.OnUpdate != null)
            {
                client.OnUpdate(client, EventArgs.Empty);
            }
        }

        private static NativeSceneClient IntPtrToNativeSceneClient(IntPtr nativeSceneClient)
        {
            GCHandle handle = (GCHandle)nativeSceneClient;
            return handle.Target as NativeSceneClient;
        }

        #endregion
    }
}
