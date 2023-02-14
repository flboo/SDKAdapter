using System;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Qarth
{

    public enum ShareType
    {
        WeChat, QQ, Weibo

    }

    public enum LoginType
    {
        Wechat,
        QQ,
        Mobile,
        Vistor,
    }

    public enum SharePlace
    {
        Session = 0,
        Timeline = 1,
        Favorite = 2

    }

    public interface IShareCallback
    {
        void shareSuccess();
        void shareCancel();
        void shareFailed();
    }

    public interface ILoginCallBack
    {

        void loginSuccess(string info);


        void loginCancel(string result);


        void loginFailed(string result);

    }


    public class WeShareMgr : TSingleton<WeShareMgr>
    {

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
        private AndroidJavaClass m_ModooHelper
        {
            get
            {
                if (m_ModooHelperJC == null)
                {
                    m_ModooHelperJC = new AndroidJavaClass(WeShareDefine.ModooHelperClaseeName);
                }

                return m_ModooHelperJC;
            }
        }
        private AndroidJavaClass m_WeshareShreTypeJC
        {
            get
            {
                if (m_ModooShareTypeJC == null)
                {
                    m_ModooShareTypeJC = new AndroidJavaClass(WeShareDefine.ModooShareTypeEnum);
                }
                return m_ModooShareTypeJC;
            }
        }
        private AndroidJavaClass m_WeshareLoginTypeJC
        {
            get
            {
                if (m_ModooLoginTypeJC == null)
                {
                    m_ModooLoginTypeJC = new AndroidJavaClass(WeShareDefine.ModooLoginTypeEnum);
                }
                return m_ModooLoginTypeJC;
            }
        }
        private WeShareAndroidAdapter m_AndroidShareAdapter;
        private WeLoginAndroidAdapter m_AndroidLoginAdapter;
        private AndroidJavaObject m_UnityActivityObject;
        private AndroidJavaClass m_ModooHelperJC;
        private AndroidJavaClass m_ModooShareTypeJC;
        private AndroidJavaClass m_ModooLoginTypeJC;

        private AndroidJavaObject m_WeChatShareType;

        public void Init()
        {
            ConfigureCallBack();
        }

        public override void OnSingletonInit()
        {
            Log.i("WeshareMgr init");
        }

        private AndroidJavaObject GetShareType(ShareType type)
        {
            switch (type)
            {
                case ShareType.WeChat:
                    return m_WeshareShreTypeJC.GetStatic<AndroidJavaObject>("WeChat");
                case ShareType.QQ:
                    return m_WeshareShreTypeJC.GetStatic<AndroidJavaObject>("QQ");
                case ShareType.Weibo:
                    return m_WeshareShreTypeJC.GetStatic<AndroidJavaObject>("Weibo");
                default:
                    return null;
            }

        }

        private AndroidJavaObject GetLoginType(LoginType type)
        {
            switch (type)
            {
                case LoginType.Wechat:
                    return m_WeshareLoginTypeJC.GetStatic<AndroidJavaObject>("Wechat");
                case LoginType.QQ:
                    return m_WeshareLoginTypeJC.GetStatic<AndroidJavaObject>("QQ");
                case LoginType.Mobile:
                    return m_WeshareLoginTypeJC.GetStatic<AndroidJavaObject>("Mobile");
                case LoginType.Vistor:
                    return m_WeshareLoginTypeJC.GetStatic<AndroidJavaObject>("Vistor");
                default:
                    return null;
            }

        }

        private void ConfigureCallBack()
        {
#if UNITY_ANDROID
            m_ModooHelper.CallStatic("init", m_ActivityObject);
            m_AndroidLoginAdapter = new WeLoginAndroidAdapter();
            m_AndroidShareAdapter = new WeShareAndroidAdapter();
            m_ModooHelper.CallStatic("setLoginCallback", m_AndroidLoginAdapter);
            m_ModooHelper.CallStatic("registerShareCallback", m_AndroidShareAdapter);

            m_AndroidShareAdapter.OnShareSuccess += (sender, args) =>
            {
                if (m_ShareCallback != null)
                {
                    ThreadMgr.S.mainThread.PostAction(() =>
                    {
                        try
                        {
                            m_ShareCallback.Invoke(1);
                            m_ShareCallback = null;
                        }
                        catch (Exception e)
                        {
                            Debug.LogError("wechat share error:"+e);
                        }
                    });
                }

                if (m_NotUnityShareCallback != null)
                {
                    try
                    {
                        m_NotUnityShareCallback.Invoke(1);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("not unity wechat share error:" + e);
                    }
                }
            };

            m_AndroidShareAdapter.OnShareCancle += (sender, args) =>
            {
                if (m_ShareCallback != null)
                {
                    ThreadMgr.S.mainThread.PostAction(() =>
                    {
                        try
                        {
                            m_ShareCallback.Invoke(-1);
                            m_ShareCallback = null;
                        }
                        catch (Exception e)
                        {
                            Debug.LogError("wechat share error:" + e);
                        }
                    });
                }

                if (m_NotUnityShareCallback != null)
                {
                    try
                    {
                        m_NotUnityShareCallback.Invoke(-1);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("not unity wechat share error:" + e);
                    }
                }
            };

            m_AndroidShareAdapter.OnShareFailed += (sender, args) =>
            {
                if (m_ShareCallback != null)
                {
                    ThreadMgr.S.mainThread.PostAction(() =>
                    {
                        try
                        {
                            m_ShareCallback.Invoke(0);
                            m_ShareCallback = null;
                        }
                        catch (Exception e)
                        {
                            Debug.LogError("wechat share error:" + e);
                        }
                    });
                }

                if (m_NotUnityShareCallback != null)
                {
                    try
                    {
                        m_NotUnityShareCallback.Invoke(0);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("not unity wechat share error:" + e);
                    }
                }
            };

            m_AndroidLoginAdapter.OnLoginSuccess += (info) =>
            {
                if (m_LoginCallback != null)
                {
                    ThreadMgr.S.mainThread.PostAction(() =>
                    {
                        try
                        {
                            m_LoginCallback.Invoke(1,info);
                            m_LoginCallback = null;
                        }
                        catch (Exception e)
                        {
                            Debug.LogError("wechat login error:" + e);
                        }
                    });
                }
            };

            m_AndroidLoginAdapter.OnLoginCancle += (result) =>
            {
                if (m_LoginCallback != null)
                {
                    ThreadMgr.S.mainThread.PostAction(() =>
                    {
                        try
                        {
                            m_LoginCallback.Invoke(-1,result);
                            m_LoginCallback = null;
                        }
                        catch (Exception e)
                        {
                            Debug.LogError("wechat login error:" + e);
                        }
                    });
                }
            };

            m_AndroidLoginAdapter.OnLoginFailed += (result) =>
            {
                if (m_LoginCallback != null)
                {
                    ThreadMgr.S.mainThread.PostAction(() =>
                    {
                        try
                        {
                            m_LoginCallback.Invoke(0,result);
                            m_LoginCallback = null;
                        }
                        catch (Exception e)
                        {
                            Debug.LogError("wechat login error:" + e);
                        }
                    });
                }
            };
#endif
        }

        /// <summary>
        /// 根据名称从RES中获取对应资源ID
        /// @param name 资源名
        /// @return 资源对应的ID
        /// </summary>
        public int GetResIdFromName(string name)
        {
#if UNITY_ANDROID
            return m_ModooHelper.CallStatic<int>("getDefaultIconRes", name);
#endif
            return -1;
        }

        public void ShareImageByPath(ShareType platform, SharePlace type, string path,Action<int> shareCallback)
        {
#if UNITY_ANDROID
            m_ShareCallback = shareCallback;
            m_ModooHelper.CallStatic("shareImageByPath", GetShareType(platform), (int)type, path);
#endif
        }

        public void ShareImageBytes(ShareType platform, SharePlace type, byte[] bytes, Action<int> shareCallback)
        {
#if UNITY_ANDROID
            m_ShareCallback = shareCallback;
            m_ModooHelper.CallStatic("shareImageBytes", GetShareType(platform), (int)type, bytes);
#endif
        }

        public void ShareImageBytesNotUnity(ShareType platform, SharePlace type, byte[] bytes, Action<int> notUnityShareCallback)
        {
#if UNITY_ANDROID
            m_NotUnityShareCallback = notUnityShareCallback;
            m_ModooHelper.CallStatic("shareImageBytes", GetShareType(platform), (int)type, bytes);
#endif
        }

        public void ShareImageByResId(ShareType platform, SharePlace type, int mipmapId)
        {
#if UNITY_ANDROID
            m_ModooHelper.CallStatic("shareImageResId", GetShareType(platform), (int)type, mipmapId);
#endif
        }

        public void ShareWebPage(ShareType platform, SharePlace place, string url, string title, string description,
            int mipmapId)
        {
#if UNITY_ANDROID
            m_ModooHelper.CallStatic("shareWebpage", GetShareType(platform), (int)place, url, title, description, mipmapId);
#endif
        }

        public void ShareMusic(ShareType platform, SharePlace place, string url, string title, string description, string mipmapId)
        {
#if UNITY_ANDROID
            m_ModooHelper.CallStatic("shareMusic", GetShareType(platform), (int)place, url, title, description, mipmapId);
#endif
        }

        public void ShareVideo(ShareType platform, SharePlace place, string url, string title, string description, string mipmapId)
        {
#if UNITY_ANDROID
            m_ModooHelper.CallStatic("shareVideo", GetShareType(platform), (int)place, url, title, description, mipmapId);
#endif
        }

        public void ShareText(ShareType platform, SharePlace place, string text)
        {
#if UNITY_ANDROID
            m_ModooHelper.CallStatic("shareText", GetShareType(platform), (int)place, text);
#endif
        }

        public void Login(LoginType type ,Action<int,string> loginCallback)
        {
#if UNITY_ANDROID
            m_LoginCallback = loginCallback;
            m_ModooHelper.CallStatic("login", GetLoginType(type));
#endif
        }


        //code 1=success 0=failed -1=cancel

        private event Action<int> m_ShareCallback;

        private event Action<int,string> m_LoginCallback;

        private event Action<int> m_NotUnityShareCallback;
    }
}