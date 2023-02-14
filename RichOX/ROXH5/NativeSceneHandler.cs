using System;
using RichOX.Api;

namespace Qarth
{
    public class NativeSceneHandler : IOXSceneHandler
    {
        public event EventHandler<EventArgs> OnLoaded;
        public event EventHandler<EventArgs> OnShown;
        public event EventHandler<EventArgs> OnClicked;
        public event EventHandler<EventArgs> OnClosed;
        public event EventHandler<FailedToLoadEventArgs> OnFailedToLoad;
        public event EventHandler<EventArgs> OnRenderSuccess;
        public event EventHandler<FailedToRenderEventArgs> OnFailedToRender;
        public event EventHandler<EventArgs> OnUpdate;

        private NativeScene m_NativeScene;

        public bool nativeSceneLoaded;
        public string sceneName;

        public bool isReady()
        {
            var isready = m_NativeScene?.IsReady();
            if (isready.HasValue && isready.Value)
            {
                return true;
            }
            return false;
        }

        public NativeSceneHandler(string key,string id)
        {
            sceneName = key;

            m_NativeScene = new NativeScene(id);

            m_NativeScene.OnLoaded += (sender, args) =>
            {
                nativeSceneLoaded = true;

                Log.i("ox_NativeScene_Loaded_{0}, NativeInfo: " + m_NativeScene.GetNativeInfo(), sceneName);
                m_NativeScene.ReportShown();

                OnLoaded?.Invoke(sender, args);
            };
            m_NativeScene.OnShown += (sender, args) =>
            {
                Log.i("ox_NativeScene_Shown_" + sceneName);

                OnShown?.Invoke(sender, args);
            };
            m_NativeScene.OnClicked += (sender, args) =>
            {
                Log.i("ox_NativeScene_Clicked_" + sceneName);

                OnClicked?.Invoke(sender, args);
            };
            m_NativeScene.OnClosed += (sender, args) =>
            {
                Log.i("ox_NativeScene_Closed_" + sceneName);

                OnClosed?.Invoke(sender, args);
            };
            m_NativeScene.OnFailedToLoad += (sender, args) =>
            {
                Log.i("ox_NativeScene_FailedToLoad_{0}"+ args.RichOXError,sceneName);

                OnFailedToLoad?.Invoke(sender, args);
            };
            m_NativeScene.OnRenderSuccess += (sender, args) =>
            {
                Log.i("ox_NativeScene_OnRenderSuccess_" + sceneName);

                OnRenderSuccess?.Invoke(sender, args);
            };
            m_NativeScene.OnFailedToRender += (sender, args) =>
            {
                Log.i("ox_NativeScene_OnFailedToRender_{0}"+ args.RichOXError,sceneName);

                OnFailedToRender?.Invoke(sender, args);
            };
            m_NativeScene.OnUpdate += (sender, args) =>
            {
                Log.i("ox_NativeScene_Update, NativeInfo:{0} " + m_NativeScene.GetNativeInfo(),sceneName);

                OnUpdate?.Invoke(sender, args);
            };

            Load();
        }

        public NativeScene GetNativeScene()
        {
            return m_NativeScene;
        }

        public void Load()
        {
            if(!nativeSceneLoaded)
                m_NativeScene?.Load();
        }

        public void Show()
        {
            if (nativeSceneLoaded)
            {
                m_NativeScene?.HandleClick();
            }
        }

        public void Hide()
        {

        }
    }

}