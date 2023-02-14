using System;
using RichOX.Common;
using RichOX.Api;
using System.Runtime.InteropServices;
using AOT;

namespace RichOX.Platforms.iOS
{
    public class FloatSceneClient : IFloatSceneClient, IDisposable
    {
        private IntPtr mFloatScenePtr;
        private IntPtr mFloatSceneClientPtr;


        #region Banner callback types

        internal delegate void RichOXFloatSceneDidReceiveCallback(IntPtr floatSceneClient);
        internal delegate void RichOXFloatSceneDidFailToReceiveWithErrorCallback(IntPtr floatSceneClient, int errorCode,string errorMessage);
        internal delegate void RichOXFloatSceneWillPresentScreenCallback(IntPtr floatSceneClient);
        internal delegate void RichOXFloatSceneDidDismissScreenCallback(IntPtr floatSceneClient);
        internal delegate void RichOXFloatSceneWillLeaveApplicationCallback(IntPtr floatSceneClient);
        internal delegate void RichOXFloatSceneRenderSuccessCallback(IntPtr floatSceneClient);
        internal delegate void RichOXFloatSceneFailedToRenderCallback(IntPtr floatSceneClient, int errorCode, string errorMessage);

        #endregion

        // This property should be used when setting the mFloatScenePtr.
        private IntPtr FloatScenePtr
        {
            get { return mFloatScenePtr; }

            set
            {
                Externs.ROXRelease(mFloatScenePtr);
                mFloatScenePtr = value;
            }
        }


        public FloatSceneClient(string sceneId)
        {
            mFloatSceneClientPtr = (IntPtr)GCHandle.Alloc(this);
            FloatScenePtr = Externs.ROXCreateFloatScene(mFloatSceneClientPtr, sceneId);

            Externs.ROXSetFloatSceneCallbacks(
                FloatScenePtr,
                FloatSceneDidReceiveCallback,
                FloatSceneDidFailToReceiveWithErrorCallback,
                FloatSceneWillPresentScreenCallback,
                FloatSceneDidDismissScreenCallback,
                FloatSceneWillLeaveApplicationCallback,
                FloatSceneRenderSuccessCallback,
                FloatSceneFailedToRenderCallback);
        }


        #region IBannerClient

        public event EventHandler<EventArgs> OnLoaded;
        public event EventHandler<EventArgs> OnShown;
        public event EventHandler<EventArgs> OnClicked;
        public event EventHandler<EventArgs> OnClosed;
        public event EventHandler<FailedToLoadEventArgs> OnFailedToLoad;
        public event EventHandler<EventArgs> OnRenderSuccess;
        public event EventHandler<FailedToRenderEventArgs> OnFailedToRender;

        public void SetPosition(Position position)
        {
            Externs.ROXSetFloatScenePosition(FloatScenePtr, (int)position);
        }

        public void SetPosition(int x, int y)
        {
            Externs.ROXSetFloatScenePositionWithPos(FloatScenePtr, x, y);
        }

        public void SetPositionRelative(Position position, int offsetX, int offsetY)
        {
            Externs.ROXSetFloatScenePositionRelative(FloatScenePtr, (int)position, offsetX, offsetY);
        }

        public void SetSize(int width, int height)
        {
            Externs.ROXSetFloatSceneSize(FloatScenePtr, width, height);
        }

        public void Load()
        {
            Externs.ROXLoadFloatScene(FloatScenePtr);
        }

        public bool IsReady()
        {
            return Externs.ROXFloatSceneIsReady(FloatScenePtr);
        }

        public void Show()
        {
            Externs.ROXShowFloatScene(FloatScenePtr);
        }

        public void Hide()
        {
            Externs.ROXHideFloatScene(FloatScenePtr);
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
            Externs.ROXDestroyFloatScene(FloatScenePtr);
            FloatScenePtr = IntPtr.Zero;
        }

        public void Dispose()
        {
            Destroy();
            ((GCHandle)mFloatSceneClientPtr).Free();
        }

        ~FloatSceneClient()
        {
            Dispose();
        }

        #endregion


        #region Banner callback methods

        [MonoPInvokeCallback(typeof(RichOXFloatSceneDidReceiveCallback))]
        private static void FloatSceneDidReceiveCallback(IntPtr floatSceneClient)
        {
            FloatSceneClient client = IntPtrToFloatSceneClient(floatSceneClient);
            if (client.OnLoaded != null)
            {
                client.OnLoaded(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXFloatSceneDidFailToReceiveWithErrorCallback))]
        private static void FloatSceneDidFailToReceiveWithErrorCallback(IntPtr floatSceneClient, int errorCode, string errorMessage)
        {
            FloatSceneClient client = IntPtrToFloatSceneClient(floatSceneClient);
            if (client.OnFailedToLoad != null)
            {
                FailedToLoadEventArgs args = new FailedToLoadEventArgs()
                {
                    RichOXError = new RichOXError(new RichOXErrorClient(errorCode, errorMessage))
                };
                client.OnFailedToLoad(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXFloatSceneWillPresentScreenCallback))]
        private static void FloatSceneWillPresentScreenCallback(IntPtr floatSceneClient)
        {
            FloatSceneClient client = IntPtrToFloatSceneClient(floatSceneClient);
            if (client.OnShown != null)
            {
                client.OnShown(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXFloatSceneDidDismissScreenCallback))]
        private static void FloatSceneDidDismissScreenCallback(IntPtr floatSceneClient)
        {
            FloatSceneClient client = IntPtrToFloatSceneClient(floatSceneClient);
            if (client.OnClosed != null)
            {
                client.OnClosed(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXFloatSceneWillLeaveApplicationCallback))]
        private static void FloatSceneWillLeaveApplicationCallback(IntPtr floatSceneClient)
        {
            FloatSceneClient client = IntPtrToFloatSceneClient(floatSceneClient);
            if (client.OnClicked != null)
            {
                client.OnClicked(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXFloatSceneRenderSuccessCallback))]
        private static void FloatSceneRenderSuccessCallback(IntPtr floatSceneClient)
        {
            FloatSceneClient client = IntPtrToFloatSceneClient(floatSceneClient);
            if (client.OnRenderSuccess != null)
            {
                client.OnRenderSuccess(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXFloatSceneFailedToRenderCallback))]
        private static void FloatSceneFailedToRenderCallback(IntPtr floatSceneClient, int errorCode, string errorMessage)
        {
            FloatSceneClient client = IntPtrToFloatSceneClient(floatSceneClient);
            if (client.OnFailedToRender != null)
            {
                FailedToRenderEventArgs args = new FailedToRenderEventArgs()
                {
                    RichOXError = new RichOXError(new RichOXErrorClient(errorCode, errorMessage))
                };
                client.OnFailedToRender(client, args);
            }
        }

        private static FloatSceneClient IntPtrToFloatSceneClient(IntPtr floatSceneClient)
        {
            GCHandle handle = (GCHandle)floatSceneClient;
            return handle.Target as FloatSceneClient;
        }

        #endregion

    }
}
