using System;
using System.Collections;
using System.Collections.Generic;
using GameWish.Game;
using Qarth;
using RichOX.Api;
using UnityEngine;


namespace Qarth
{
    public class DialogSceneHandler : IOXSceneHandler
    {
        public event EventHandler<EventArgs> OnLoaded;
        public event EventHandler<EventArgs> OnShown;
        public event EventHandler<EventArgs> OnClicked;
        public event EventHandler<EventArgs> OnClosed;
        public event EventHandler<FailedToLoadEventArgs> OnFailedToLoad;
        public event EventHandler<EventArgs> OnRenderSuccess;
        public event EventHandler<FailedToRenderEventArgs> OnFailedToRender;

        private DialogScene m_DialogScene;

        public bool dialogSceneLoaded;
        public string sceneName;

        public bool isReady()
        {
            var isready = m_DialogScene?.IsReady();
            if (isready.HasValue && isready.Value)
            {
                return true;
            }
            return false;
        }

        public DialogSceneHandler(string key,string id)
        {
            sceneName = key;

            m_DialogScene = new DialogScene(id);

            // 监听事件
            m_DialogScene.OnLoaded += (sender, args) =>
            {
                dialogSceneLoaded = true;
                Log.i("ox_DialogScene_Loaded_" + sceneName);

                OnLoaded?.Invoke(sender, args);
            };
            m_DialogScene.OnShown += (sender, args) =>
            {
                Log.i("ox_DialogScene_Shown_" + sceneName);

                OnShown?.Invoke(sender, args);
            };
            m_DialogScene.OnClicked += (sender, args) =>
            {
                Log.i("ox_DialogScene_Clicked_" + sceneName);

                OnClicked?.Invoke(sender, args);
            };
            m_DialogScene.OnClosed += (sender, args) =>
            {
                Log.i("ox_DialogScene_Closed_" + sceneName);

                OnClosed?.Invoke(sender, args);
            };
            m_DialogScene.OnFailedToLoad += (sender, args) =>
            {
                Log.i("ox_DialogScene_FailedToLoad_{0}" + args.RichOXError, sceneName);

                OnFailedToLoad?.Invoke(sender, args);
            };

           Load();
        }

        public DialogScene GetDialogScene()
        {
            return m_DialogScene;
        }

        public void Load()
        {
            if (!dialogSceneLoaded)
                m_DialogScene?.Load();
        }

        public void Show()
        {
            if (dialogSceneLoaded)
                m_DialogScene?.Show();
        }

        public void Hide()
        {

        }

    }

}