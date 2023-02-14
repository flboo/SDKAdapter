
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace Qarth
{
    public class LebianMgr : TSingleton<LebianMgr>
    {
        AndroidJavaClass m_LebianClass;
        AndroidJavaObject m_Activity;
        AndroidJavaObject m_Application;
        AndroidJavaObject m_AppContext;

        private AndroidJavaObject unityActivity
        {
            get
            {
                if (m_Activity == null)
                {
                    AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    m_Activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
                }
                return m_Activity;
            }
        }
        private AndroidJavaObject unityApplication
        {
            get
            {
                if (m_Application == null)
                {
                    m_Application = unityActivity.Call<AndroidJavaObject>("getApplication");
                }
                return m_Application;
            }
        }
        private AndroidJavaObject unityAppContext
        {
            get
            {
                if (m_AppContext == null)
                {
                    m_AppContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");
                }
                return m_AppContext;
            }
        }
        private AndroidJavaClass lebianClass
        {
            get
            {
                if (m_LebianClass == null)
                {
                    m_LebianClass = new AndroidJavaClass("com.excelliance.lbsdk.LebianSdk");
                }
                return m_LebianClass;
            }
        }

        public void Init()
        {
            if (SDKConfig.S.lebianConfig.isEnable == false)
            {
                Log.i(">>>>>>>>>>>> lebian sdk is not enable");
                return;
            }

            StartQueryUpdate();
            SetPrivacyChecked();
            Log.i(">>>>>>>>>>>> lebian sdk inited");
        }


        // 默认情况下初始化不需要调用该接口，但是出于保险，先加上，如果有需要在后台回来调用，请及时通知SDK代码维护人员，需要额外处理
        private void StartQueryUpdate()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                lebianClass.CallStatic("queryUpdate", unityAppContext, null, null);

            }
            catch (Exception ex)
            {
                Log.e("call lebian sdk method error:" + ex.ToString());
            }
#endif
        }

        public void SetPrivacyChecked()
        {

#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                lebianClass.CallStatic<Boolean>("setPrivacyChecked", unityApplication, unityAppContext);

            }
            catch (Exception ex)
            {
                Debug.LogError("call lebian sdk method error:" + ex.ToString());
            }
#endif
        }
    }
}

