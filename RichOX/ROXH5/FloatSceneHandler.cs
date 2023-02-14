using System;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using RichOX.Api;
using UnityEngine;


namespace Qarth
{
    public class FloatSceneHandler : IOXSceneHandler
    {
        public event EventHandler<EventArgs> OnLoaded;
        public event EventHandler<EventArgs> OnShown;
        public event EventHandler<EventArgs> OnClicked;
        public event EventHandler<EventArgs> OnClosed;
        public event EventHandler<FailedToLoadEventArgs> OnFailedToLoad;
        public event EventHandler<EventArgs> OnRenderSuccess;
        public event EventHandler<FailedToRenderEventArgs> OnFailedToRender;

        private FloatScene m_FloatScene;

        public bool floatSceneLoaded;
        public string sceneName;

        public bool isReady()
        {
            var isready = m_FloatScene?.IsReady();
            if (isready.HasValue && isready.Value)
            {
                return true;
            }
            return false;
        }

        public FloatSceneHandler(string key,string id, int width, int height)
        {
            sceneName = key;

            m_FloatScene = new FloatScene(id);

            // 设置浮标尺寸，单位 dp（Android）pt(IOS)
            m_FloatScene.SetSize(width, height);

            // 监听事件
            m_FloatScene.OnLoaded += (sender, args) =>
            {
                floatSceneLoaded = true;
                Log.i("ox_FloatScene_Loaded_" + sceneName);

                OnLoaded?.Invoke(sender, args);
            };
            m_FloatScene.OnShown += (sender, args) =>
            {
                Log.i("ox_FloatScene_Shown_" + sceneName);

                OnShown?.Invoke(sender, args);
            };
            m_FloatScene.OnClicked += (sender, args) =>
            {
                Log.i("ox_FloatScene_Clicked_" + sceneName);

                OnClicked?.Invoke(sender, args);
            };
            m_FloatScene.OnClosed += (sender, args) =>
            {
                Log.i("ox_FloatScene_Closed_" + sceneName);

                OnClosed?.Invoke(sender, args);
            };
            m_FloatScene.OnFailedToLoad += (sender, args) =>
            {
                Log.i("ox_FloatScene_FailedToLoad_id_{0}" + args.RichOXError, sceneName);

                OnFailedToLoad?.Invoke(sender, args);
            };
        }

        public FloatScene GetFloatScene()
        {
            return m_FloatScene;
        }

        public void SetPositionRelative(Position rel, int px_x, int px_y)
        {
            int dp_x = Mathf.CeilToInt(CustomExtensions.Pixel2DP(px_x));
            int dp_y = Mathf.CeilToInt(CustomExtensions.Pixel2DP(px_y));

            //todo ios pt

            m_FloatScene?.SetPositionRelative(rel, dp_x, dp_y);
        }

        public void Load()
        {
            if (!floatSceneLoaded)
                m_FloatScene?.Load();
        }

        public void Show()
        {
            if (floatSceneLoaded)
                m_FloatScene?.Show();
        }

        public void Hide()
        {
            if (floatSceneLoaded)
                m_FloatScene?.Hide();
        }
    }

}