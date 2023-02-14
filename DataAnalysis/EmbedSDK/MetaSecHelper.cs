using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;


namespace GameWish.Game
{
	public class MetaSecHelper : TSingleton<MetaSecHelper>
	{
		private AndroidJavaObject m_UnityActivityObject;
        private AndroidJavaClass m_ModooHelperJC;
		private AndroidJavaObject m_ActivityObject
        {
            get
            {
                if (m_UnityActivityObject == null)
                {
                    AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    m_UnityActivityObject = jc.GetStatic<AndroidJavaObject>("currentActivity");
                }

                return m_UnityActivityObject;
            }
        }

		private AndroidJavaClass MetaSceHelper
        {
            get
            {
                if (m_ModooHelperJC == null)
                {
                    m_ModooHelperJC = new AndroidJavaClass(SDKConfig.S.bundleIDAndroid + ".MetaHelper");
                }

                return m_ModooHelperJC;
            }
        }

		public void Init(string license)
		{
#if UNITY_ANDROID&&!UNITY_EDITOR
            MetaSceHelper.CallStatic("CommitLicense", m_ActivityObject, license);
#endif
        }
	}
	
}