using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeGameSdk.Api;

namespace Qarth
{
    public class UserInfo
    {

        public UserInfo(string userID, string name, string ext, string token)
        {
            this.userID = userID;
            userName = name;
            userToken = token;
            userExt = ext;
        }

        public string userID;
        public string userName;
        public string userToken;
        public string userExt;
    }



    public class WeGameSdkAdapter : TMonoSingleton<WeGameSdkAdapter>
    {

        private Action<bool> OnLoginEvent;
        private Action<bool> OnExchangeEvent;
        private Action<bool> OnLogOutEvent;
        private Action<bool> OnExitEvent;

        private UserInfo m_LoginUserInfo = null;

        public string getUserID
        {
            get
            {
                if (m_LoginUserInfo == null)
                {
                    return "";
                }

                return m_LoginUserInfo.userID;
            }
        }

        public string getUserName
        {
            get
            {
                if (m_LoginUserInfo == null)
                {
                    return "";
                }

                return m_LoginUserInfo.userName;

            }

        }

        public string getUserToken
        {
            get
            {
                if (m_LoginUserInfo == null)
                {
                    return "";
                }

                return m_LoginUserInfo.userToken;

            }

        }

        public string getUserExt
        {
            get
            {
                if (m_LoginUserInfo == null)
                {
                    return "";
                }

                return m_LoginUserInfo.userExt;

            }
        }


        private bool m_IsInit = false;
        public override void OnSingletonInit()
        {
            Log.i("WeGameSdkAdapter Init Success");
        }

        /// <summary>
        /// 初始化的接口 
        /// </summary>
        /// <param name="bind">当前游戏是否绑定渠道账号系统</param>
        public void Init(bool bind)
        {
            WeGameSDK.Instance.Init(bind);
            if (!m_IsInit)
            {
                WeGameSDK.Instance.OnLogIn += OnLogin;
                WeGameSDK.Instance.OnExchange += OnExchange;
                WeGameSDK.Instance.OnLogout += OnLogout;
                WeGameSDK.Instance.OnExit += OnExit;
            }
        }

        private void OnExit(object sender, ExitEventArgs e)
        {

            if (e.Type == ExitEventArgs.EXIT_TYPE_CHANNEL)
            {
                if (OnExitEvent != null)
                {
                    OnExitEvent(true);
                }
                //Application.Quit();
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            else
            {
                PlayerPrefs.SetInt("channel_exit_key", 1);
            }

            SubmitUserInfo();
        }

        private void OnLogout(object sender, EventArgs e)
        {
            m_LoginUserInfo = null;
            if (OnLogOutEvent != null)
            {
                OnLogOutEvent(true);
            }
        }

        private void OnExchange(object sender, ExchangeEventArgs e)
        {
            if (e.Success)
            {
                if (e.LoginResult != null)
                {
                    m_LoginUserInfo = new UserInfo(e.LoginResult.GetUserId(), e.LoginResult.GetUserName(), e.LoginResult.GetExtension(), e.LoginResult.GetToken());
                }

                if (OnExchangeEvent != null)
                {
                    OnExchangeEvent(true);
                }
            }
            else
            {
                if (OnExchangeEvent != null)
                {
                    OnExchangeEvent(false);
                }
            }


        }

        private void OnLogin(object sender, LoginEventArgs e)
        {
            Log.i("logevent code " + e.Code + "result " + e.LoginResult);
            if (e.Code == 0)
            {
                if (e.LoginResult != null)
                {
                    m_LoginUserInfo = new UserInfo(e.LoginResult.GetUserId(), e.LoginResult.GetUserName(), e.LoginResult.GetExtension(), e.LoginResult.GetToken());
                }

                if (OnLoginEvent != null)
                {
                    OnLoginEvent(true);
                }
                // SubmitUserInfo();
            }
            else
            {
                if (OnLoginEvent != null)
                {
                    OnLoginEvent(false);
                }
            }
        }

        public void Login(Action<bool> loginEvent)
        {
            OnLoginEvent = loginEvent;
            WeGameSDK.Instance.Login();
        }

        public void Logout(Action<bool> logoutEvent)
        {
            OnLogOutEvent = logoutEvent;
            WeGameSDK.Instance.Logout();
        }

        public void ExitGame(Action<bool> exitAction)
        {
            OnExitEvent = exitAction;
            WeGameSDK.Instance.ExitGame();
        }
        /// <summary>
        /// 上传玩家数据 
        /// </summary>
        /// <param name="userInfo">具体可参见InitUserInfo中的设置</param>
        public void SubmitUserInfo(JSONObject userInfo = null)
        {
            if (userInfo == null)
            {
                InitUserInfo(out userInfo);
            }

            WeGameSDK.Instance.SubmitUserInfo(userInfo);
        }

        private void InitUserInfo(out JSONObject userInfo)
        {
            userInfo = new JSONObject();
            userInfo[SubmitInfo.SUBMIT_TYPE] = SubmitInfo.SUBMIT_TYPE_USER_CREATE;
            userInfo[SubmitInfo.GAME_ROLE_ID] = 123456; //（必填）玩家角色ID
            userInfo[SubmitInfo.GAME_ROLE_NAME] = "冷雨夜风"; //（必填）玩家角色名
            userInfo[SubmitInfo.GAME_PROFESSION_ID] = 1; //（必填）职业ID
            userInfo[SubmitInfo.GAME_PROFESSION] = "战士"; //（必填）职业名称
            userInfo[SubmitInfo.GAME_GENDER] = SubmitInfo.GANDER_TYPE_MALE;
            userInfo[SubmitInfo.GAME_POWER] = 120000; //（必填）战力数值
            userInfo[SubmitInfo.GAME_VIP] = 5; //（必填）当前用户VIP等级
            userInfo[SubmitInfo.GAME_GRADE] = 30; //（必填）玩家角色等级
            userInfo[SubmitInfo.GAME_SERVICE_ID] = 2; //（必填）游戏区服ID
            userInfo[SubmitInfo.GAME_SERVICE_NAME] = "测试服"; //（必填）游戏区服名称
            userInfo[SubmitInfo.GAME_SOCIATY] = "王者依旧"; //（必填）所属帮派名称
            userInfo[SubmitInfo.GAME_SOCIATY_ID] = 100; //（必填）所属帮派帮派ID


        }

    }

}


