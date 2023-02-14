using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using Tencent.GDT;
namespace GameWish.Game
{
    public class GDTActionDefine
    {
        public const string GDT_START_APP = "START_APP";
        public const string GDT_REGISTER = "REGISTER";
        public const string GDT_PURCHASE = "PURCHASE";
        public const string GDT_SHARE = "SHARE";

        //custom
        public const string GDT_GAME_INIT = "game_init";
        public const string GDT_REALNAME_START = "game_certification_start";
        public const string GDT_REALNAME_SUCCESS = "game_certification_success";
        public const string GDT_ENTER_GAME = "game_start_app";

    }

    public class GDTDataMgr : TSingleton<GDTDataMgr>
    {

        public void InitSDK(string appId, string actionSetId, string secretKey)
        {
#if !UNITY_EDITOR
            if (SDKConfig.S.dataAnalysisConfig.GDTActionConfig.isEnable)
            {
                GDTSDKManager.Init(appId);
                // 如果需要上报归因，或上报数据联运，请按如下方式初始化转化归因 SDK
                // Android 注意：归因 SDK 需要的权限需要齐全，具体参考归因 SDK 要求
                ///转化归因 action set id    //转化归因 secret key"
                GDTAction.Init(actionSetId, secretKey);
            }
#endif
        }

        /// <summary>
        ///   上报转化归因数据，请严格按照归因的要求上报
        /// </summary>
        /// <param name="action"></param>
        public void LogAction(string action)
        {
#if !UNITY_EDITOR
            if (SDKConfig.S.dataAnalysisConfig.GDTActionConfig.isEnable)
            {
                GDTAction.LogAction(action);
            }
#endif
        }

        /// <summary>
        /// 上报行为数据，请严格按照数据联运的要求上报
        /// </summary>
        public void ReportBehavior(string eventName, string itemid = "0")
        {
#if !UNITY_EDITOR
            if (SDKConfig.S.dataAnalysisConfig.GDTActionConfig.isEnable)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("item_id", itemid);
                new DataDetector().Report(eventName, param);
            }
#endif
        }



    }

}