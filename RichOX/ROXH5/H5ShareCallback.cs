using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Qarth
{
	public class H5ShareCallback : RichOX.Api.H5ShareCallback
    {
        private Action<string, Hashtable> m_GenShareUrlAction;
        private Action<string, string, byte[]> m_ShareContentAction;

        public void RegisterGenShareUrlCallback(Action<string, Hashtable> callback)
        {
            m_GenShareUrlAction = callback;
        }

        public void RegisterShareContentAction(Action<string, string, byte[]> callback)
        {
            m_ShareContentAction = callback;
        }

        public void GenShareUrl(string host, Hashtable paramsMap)
        {
            m_GenShareUrlAction?.Invoke(host,paramsMap);
        }

        public void ShareContent(string title, string content, byte[] bitmap)
        {
            m_ShareContentAction?.Invoke(title,content,bitmap);
        }
    }
	
}