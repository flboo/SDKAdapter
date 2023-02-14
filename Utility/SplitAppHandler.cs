using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class SplitAppHandler
    {
        private static AndroidJavaObject m_AndroidJavaObject;
        private static AndroidJavaObject m_UnityActivity;

        public static string GetChannelId()
        {
            if (m_UnityActivity == null)
            {
                AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                m_UnityActivity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            }

            if (m_AndroidJavaObject == null)
            {
                m_AndroidJavaObject = new AndroidJavaObject("com.bytedance.hume.readapk.HumeSDK");
            }
            return m_AndroidJavaObject?.CallStatic<string>("getChannel",m_UnityActivity);
        }
	}
	
}