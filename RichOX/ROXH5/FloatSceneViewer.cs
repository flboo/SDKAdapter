using System.Collections;
using System.Collections.Generic;
using GameWish.Game;
using RichOX.Api;
using UnityEngine;
using UnityEngine.UI;


namespace Qarth
{
    public class FloatSceneViewer : MonoBehaviour
    {
        private Image m_IconImg;

        private FloatSceneHandler m_Handler;


        void Start()
        {
            m_IconImg = GetComponent<Image>();
            m_IconImg.enabled = false;
        }

        public void SetFloatSceenHandler(FloatSceneHandler handler)
        {
            if (m_Handler != null)
                return;
            m_Handler = handler;
        }

        public FloatSceneHandler GetSceneHandler
        {
            get
            {
                return m_Handler;
            }
        }

        public void SetPositionRelative(Position rel,int x,int y)
        {
            if (m_Handler == null)
                return;

            m_Handler.SetPositionRelative(rel, x
                , y);
        }

        public void Load()
        {
            if (m_Handler == null)
                return;

            m_Handler.Load();
        }

        public void Show()
        {
            if (m_Handler == null)
                return;

#if UNITY_EDITOR
            m_IconImg.enabled = true;
#endif

            m_Handler.Show();
        }

        public void Hide()
        {
            if (m_Handler == null)
                return;

#if UNITY_EDITOR
            m_IconImg.enabled = false;
#endif
            m_Handler.Hide();
        }
    }
}