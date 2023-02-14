using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace EmbedSDK.Platforms.Android
{
    public class EmbedSDKAndroidClient : IEmbedSDKClient
    {
        AndroidJavaClass mEmbedMgrClass;
        AndroidJavaObject mEmbedMgrInstance;
        AndroidJavaObject mContext;

        public EmbedSDKAndroidClient()
        {
            mEmbedMgrClass = new AndroidJavaClass(Utils.EmbedMgrClassName);
            if (mEmbedMgrClass != null)
                mEmbedMgrInstance = mEmbedMgrClass.CallStatic<AndroidJavaObject>("getInstance");
        }

        public void Init(EmbedSDKConfig config)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            mContext = activity.Call<AndroidJavaObject>("getApplication");

            //默认关闭debug,只上报自建
            InitWithPlatforms(config.isDebugMode, config.enableAF);
        }

        void InitWithPlatforms(bool debug = false, bool af = true, bool umeng = false, bool facebook = false, bool firebase = false)
        {
            if (mEmbedMgrInstance == null)
                return;
            var initObject = mEmbedMgrInstance.Call<AndroidJavaObject>("enableDebugMode", debug);
            initObject = initObject.Call<AndroidJavaObject>("enableAppsFlyer", af);
            initObject = initObject.Call<AndroidJavaObject>("enableUmeng", umeng);
            initObject = initObject.Call<AndroidJavaObject>("enableFacebook", facebook);
            initObject = initObject.Call<AndroidJavaObject>("enableFireBase", firebase);

            // if (!string.IsNullOrEmpty(SDKConfig.S.dataAnalysisConfig.remoteConfUrl))
            // {
            //     initObject = initObject.Call<AndroidJavaObject>("setEventExpiredConfigUrl",
            //         string.Format("{0}/v1/kv/{1}/default/expired_date",
            //         SDKConfig.S.dataAnalysisConfig.remoteConfUrl,
            //         SDKConfig.S.dataAnalysisConfig.remoteConfAppName));
            // }

            initObject.Call("init", mContext);
        }

        //设置白名单事件
        public void SetWhitelist(List<string> evtNames)
        {
            if (mEmbedMgrClass == null)
                return;
            string name = "";
            for (int i = 0; i < evtNames.Count; i++)
            {
                if (i == 0)
                    name += evtNames[i];
                else
                    name += string.Concat(",", evtNames[i]);
            }
            mEmbedMgrClass.CallStatic("setWhitelist", name);
        }

        //设置黑名单事件
        public void SetBlacklist(List<string> evtNames)
        {
            if (mEmbedMgrClass == null)
                return;
            string name = "";
            for (int i = 0; i < evtNames.Count; i++)
            {
                if (i == 0)
                    name += evtNames[i];
                else
                    name += string.Concat(",", evtNames[i]);
            }
            mEmbedMgrClass.CallStatic("setBlacklist", name);
        }

        #region 通用属性
        //有自建用户体系的调用，目前只支持firebase和appsflyer
        /**
          *设置自定义属性(只支持firebase)
          *@param key 属性的名称
          *@param value 属性的值
        */
        public void SetUserProperty(string key, string value)
        {
            if (mEmbedMgrClass == null)
                return;
            mEmbedMgrClass.CallStatic("setUserProperty", mContext, key, value);
        }
        /**
          *绑定App自建用户体系账号
          *@param userId App自建用户体系的userid(只支持firebase和appsflyer)
        */
        public void SetUserAppId(string userAppId)
        {
            if (mEmbedMgrClass == null)
                return;
            mEmbedMgrClass.CallStatic("setUserAppId", mContext, userAppId);
        }
        #endregion

        #region 用户体系事件
        //注册、登录行为的调用
        /**
          *注册成功
          *@param type 渠道，一般为第三方平台
          */
        public void ReportSignUpSuccess(string type)
        {
            if (mEmbedMgrClass == null)
                return;
            mEmbedMgrClass.CallStatic("reportSignUpSuccess", mContext, type);
        }

        /**
          *注册失败
          *@param type 渠道，一般为第三方平台
          *@param description 失败原因
          */
        public void ReportSignUpFailed(string type, string description)
        {
            if (mEmbedMgrClass == null)
                return;
            mEmbedMgrClass.CallStatic("reportSignUpFailed", mContext, type, description);
        }

        /**
          *登录成功
          *@param type 渠道，一般为第三方平台
          */
        public void ReportLogInSuccess(string type)
        {
            if (mEmbedMgrClass == null)
                return;
            mEmbedMgrClass.CallStatic("reportLogInSuccess", mContext, type);
        }

        /**
          *登录失败
          *@param type 渠道，一般为第三方平台
          *@param description 失败原因
          */
        public void ReportLogInFailed(string type, string description)
        {
            if (mEmbedMgrClass == null)
                return;
            mEmbedMgrClass.CallStatic("reportLogInFailed", mContext, type, description);
        }

        /**
          *登出成功
          */
        public void ReportLogOutSuccess(string context)
        {
            if (mEmbedMgrClass == null)
                return;
            mEmbedMgrClass.CallStatic("reportLogOutSuccess", mContext);
        }

        /**
          *登出失败
          *@param description 失败原因
          */
        public void ReportLogOutFailed(string description)
        {
            if (mEmbedMgrClass == null)
                return;
            mEmbedMgrClass.CallStatic("reportLogOutFailed", mContext, description);
        }
        #endregion

        #region 变现事件 
        // 有内购事件的调用,目前国内无内购,不处理
        #endregion

        #region 其他事件
        /**
            *激励成功后给用户奖励
            *@param itemId 激励场景id
            *@param itemType 场景类型
            *@param description 场景描述
            *@param value 激励的奖励，如金币，内容等
        */
        public void ReportUserADReward(string itemId, string itemType, string description, string value)
        {
            if (mEmbedMgrClass == null)
                return;
            mEmbedMgrClass.CallStatic("reportUserADReward", mContext, itemId, itemType, description, value);
        }

        /**
          *自定义事件
          *@param eventName 事件名
          *@param eventValue 事件属性（map结构键值对），无参数时可置空
        */
        public void ReportCustomEvent(string eventName, Dictionary<string, string> eventValues)
        {
            if (mEmbedMgrClass == null)
                return;
            mEmbedMgrClass.CallStatic("reportCustomEvent", mContext, eventName, Utils.DictToMap(eventValues));
        }
        #endregion

        #region 事件属性
        // 指设置该属性之后，通过本sdk打点的事件都会带上设置的属性字段。
        /**
         *添加事件属性
         *@param key 属性名（避免使用已有属性字段,否则不生效）
         *@param value 属性值
         */
        public void SetEventProperty(string key, string value)
        {
            if (mEmbedMgrClass == null)
                return;
            mEmbedMgrClass.CallStatic("setEventProperty", mContext, key, value);
        }

        /**
         *移除事件属性
         *@param key 属性名
         */
        public void RemoveEventProperty(string key)
        {
            if (mEmbedMgrClass == null)
                return;
            mEmbedMgrClass.CallStatic("removeEventProperty", mContext, key);
        }
        #endregion
    }
}
