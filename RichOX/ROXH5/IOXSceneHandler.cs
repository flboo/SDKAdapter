using System;
using System.Collections;
using System.Collections.Generic;
using RichOX.Api;
using UnityEngine;


namespace Qarth
{
    public interface IOXSceneHandler
    {
        event EventHandler<EventArgs> OnLoaded;
        event EventHandler<EventArgs> OnShown;
        event EventHandler<EventArgs> OnClicked;
        event EventHandler<EventArgs> OnClosed;
        event EventHandler<FailedToLoadEventArgs> OnFailedToLoad;
        event EventHandler<EventArgs> OnRenderSuccess;
        event EventHandler<FailedToRenderEventArgs> OnFailedToRender;

        void Load();
        void Show();
        void Hide();

        bool isReady();
    }

}