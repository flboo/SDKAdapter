using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Qarth
{
    public class PrivacyMgr : TSingleton<PrivacyMgr>
    {
        private AndroidJavaObject m_HelperObject;
        public AndroidJavaObject helperObject
        {
            get
            {
                if (m_HelperObject == null)
                {
                    m_HelperObject = new AndroidJavaObject(Application.identifier + ".PrivacyHelper");
                }

                return m_HelperObject;
            }
        }

        private AndroidJavaObject m_PrivacyPolcyHelperObj;
        private PrivacyAndroidAdapter m_PrivacyAndroidAdapter;

        public void Init(Action onAgree, Action onDisagree)
        {
#if UNITY_EDITOR
            EventSystem.S.Send(SDKEventID.OnPrivacyAccept);
#elif UNITY_ANDROID 
            m_PrivacyAndroidAdapter = new PrivacyAndroidAdapter(onAgree, onDisagree);
            m_PrivacyAndroidAdapter.Init(helperObject);
            m_PrivacyPolcyHelperObj = helperObject.Call<AndroidJavaObject>(PrivacyDefine.METHOD_GET_PRIVACY_HELPER);
#endif
        }

        public void Init(Action onAgree, Action onDisagree, string packageName)
        {
#if UNITY_EDITOR
            EventSystem.S.Send(SDKEventID.OnPrivacyAccept);
#elif UNITY_ANDROID 
            m_PrivacyAndroidAdapter = new PrivacyAndroidAdapter(onAgree, onDisagree, packageName);
            m_PrivacyAndroidAdapter.Init(helperObject);
            m_PrivacyPolcyHelperObj = helperObject.Call<AndroidJavaObject>(PrivacyDefine.METHOD_GET_PRIVACY_HELPER);
#endif
        }

        /// <summary>
        /// 展示是否同意隐私协议弹窗
        /// </summary>
        public void ShowPrivacy()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (!PlayerPrefs.HasKey(PrivacyDefine.SAVE_KEY) && m_PrivacyPolcyHelperObj != null)
            {
                m_PrivacyPolcyHelperObj.Call(PrivacyDefine.METHOD_SHOW_PRIVACY);
            }
            else
            {
                EventSystem.S.Send(SDKEventID.OnPrivacyAccept);
            }
#endif
        }

        /// <summary>
        /// 打开用户协议界面
        /// </summary>
        public void JumpToUserAgreement()
        {
            if (m_PrivacyPolcyHelperObj != null)
            {
                m_PrivacyPolcyHelperObj.Call(PrivacyDefine.METHOD_JUMP_2_USER_AGREEMENT);
            }
        }

        /// <summary>
        /// 打开隐私政策界面
        /// </summary>
        public void JumpToPrivacyPolicy()
        {
            if (m_PrivacyPolcyHelperObj != null)
            {
                m_PrivacyPolcyHelperObj.Call(PrivacyDefine.METHOD_JUMP_2_PRIVACY_POLICY);
            }
        }

        /// <summary>
        /// 新增跳转至充值界面（接入佣乐宝打款渠道的才需要用（隐私协议版本升级至1.0.13以上））
        /// </summary>
        public void JumpToTopUpAgreement()
        {
            if (m_PrivacyPolcyHelperObj != null)
            {
                m_PrivacyPolcyHelperObj.Call(PrivacyDefine.METHOD_JUMP_2_TOP_UP_AGREEMENT);
            }
        }

        /// <summary>
        /// 新增跳转至服务协议界面（接入佣乐宝打款渠道的才需要用（隐私协议版本升级至1.0.13以上））
        /// </summary>
        public void JumpToServiceAgreement()
        {
            if (m_PrivacyPolcyHelperObj != null)
            {
                m_PrivacyPolcyHelperObj.Call(PrivacyDefine.METHOD_JUMP_2_SERVICE_AGREEMENT);
            }
        }

    }

    public class PrivacyDefine
    {
        public static string METHOD_GET_PRIVACY_HELPER = "getPrivacyHelper";
        public static string METHOD_SHOW_PRIVACY = "showDialog";
        public static string METHOD_SET_LISTENER = "setListener";
        public static string METHOD_JUMP_2_USER_AGREEMENT = "jumpToUserAgreement";
        public static string METHOD_JUMP_2_PRIVACY_POLICY = "jumpToPrivacyPolicy";
        public static string METHOD_JUMP_2_TOP_UP_AGREEMENT = "jumpToTopUpAgreement";
        public static string METHOD_JUMP_2_SERVICE_AGREEMENT = "jumpToServiceAgreement";
        public static string CLASS_LISTENER = ".ExActivityListener";
        public static string SAVE_KEY = "IAgreeWithPrivacyPolicy";

        public static string EVENT_PRIVCY_AGREE = "privacy_agree";
        public static string EVENT_PRIVCY_DISAGREE = "privacy_disagree";
    }

    public class PrivacyAndroidAdapter : AndroidJavaProxy
    {
        private Action m_OnAgree;
        private Action m_OnDisagree;

        public PrivacyAndroidAdapter(Action onAgree, Action onDisagree) : base(SDKConfig.S.bundleID + PrivacyDefine.CLASS_LISTENER)
        {
            m_OnAgree = onAgree;
            m_OnDisagree = onDisagree;
        }

        public PrivacyAndroidAdapter(Action onAgree, Action onDisagree, string packageName) : base(packageName + PrivacyDefine.CLASS_LISTENER)
        {
            m_OnAgree = onAgree;
            m_OnDisagree = onDisagree;
        }

        public void Init(AndroidJavaObject obj)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (obj != null)
                obj.Call(PrivacyDefine.METHOD_SET_LISTENER, this);
#endif
        }

        public void onAgree()
        {
            DataAnalysisMgr.S.CustomEvent(PrivacyDefine.EVENT_PRIVCY_AGREE);
            PlayerPrefs.SetInt(PrivacyDefine.SAVE_KEY, 1);

            if (m_OnAgree != null)
            {
                m_OnAgree.Invoke();
            }
        }

        public void onDisagree()
        {
            DataAnalysisMgr.S.CustomEvent(PrivacyDefine.EVENT_PRIVCY_DISAGREE);
            if (m_OnDisagree != null)
            {
                m_OnDisagree.Invoke();
            }
        }

    }
}
