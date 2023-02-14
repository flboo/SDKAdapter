using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace Qarth
{
    public class DataAnalysisDefine
    {
        public const string FIRST_INSTALL_TIMESTAMP = "game_first_install_timestamp_2019";
        public const string FIRST_INSTALL_TIME = "game_first_install_time_9999";
        public const string REMAIN_EVENT_DAY = "remain_event_day_record_2020";
        public const string HAS_RETENTION = "retention_flag_day1";
        public const string DAY1_RETENTION = "day1_retention";

        public const string SHARE_POSTCARD = "Share2Social";
        public const string PURCHASE_PRODUCT_FAILED = "ProductRequestFailed";
        public const string CROSS_EXT = "CrossExt";

        #region ingame_ad
        // public static string SHOW_INTERSTITIAL = "ShowInterstitial";
        // public static string SHOW_REWARD_VIDEO = "ShowRewardedVideo";

        // public static string REWARD_VIDEO_STATE = "RewardVideoState";
        // public static string INTERSTITIAL_STATE = "InterstitialState";
        // public static string MIXVIEW_STATE = "MixViewState";

        // public static string REWARD_VIDEO_IPU = "IPU_RewardVideo";
        // public static string INTERSTITIAL_IPU = "IPU_Interstitial";
        // public static string MIXVIEW_VIDEO_IPU = "IPU_MixView";

        // public static string IMPRESSION_VIDEO = "Impression_Video";
        // public static string IMPRESSION_INTERSTITIAL = "Impression_Inter";
        // public static string IMPRESSION_MIXVIEW = "Impression_MixView";
        // public static string AD_DISPLAY = "ShowAD";
        // public static string AD_SHOW_COUNT = "Ad_Show_Count";

        // public static string AD_REQUEST_PLATFORM_COUNT = "ad_request_count_placement";
        // public static string AD_FILL_PLATFORM_COUNT = "ad_fill_count_placement";
        // public static string AD_REQUEST_UNITID_COUNT = "ad_request_count_unitid";
        // public static string AD_FILL_UNITID_COUNT = "ad_fill_count_unitid";
        // public static string AD_SHOW_UNITID_COUNT = "ad_show_count_unitid";
        #endregion

        #region satori_ad
        public static string W_AD_IMP = "w_ad_imp";
        public const string AF_SDK_VALUE = "af_revenue";
        public const string AF_WATCH_AD = "af_watch_ad";

        #endregion

        public const string PANEL_EVENT = "PanelEvent";
        public const string SINGLE_PANEL_EVENT = "SinglePanelEvent";
        public const string OPEN_MARKET = "OpenMarketRatePage";

        public const string FAKE_APP = "FakeApp";
        public const string DOWNLOAD_OFFICIAL_VERSION = "DownloadOfficial";
        public const string OPEN_OFFICIAL_AD_PANEL = "OpenOfficialAdPanel";

        public const string W_RATE_STAR = "w_rate_star";
        public const string W_OPEN_MARKET = "w_open_market_rate_page";

        // standard evt: https://webeye.feishu.cn/docs/doccnEkzcz6mNTtgAgWOXWRAXqd#zlHyOj
        public const string W_GUIDE = "w_guide";//引导
        public const string W_CHECK_IN = "w_check_in";//签到
        public const string W_SIGNUP = "w_sign_up";//注册

        public const string W_COIN_ACQUIRED = "w_coin_acquired";//获取资源
        public const string W_COIN_USED = "w_coin_used";//资源消耗
        public const string W_LEVEL_UP = "w_level_up";//升级
        public const string W_TOOL_ACQUIRED = "w_tool_acquired";//获取道具
        public const string W_TOOL_UPDATE = "w_tool_update";//道具升级 
        public const string W_TOOL_USED = "w_tool_used";//使用道具 
        public const string W_TOOL_SOLD = "w_tool_sold";//出售道具
        public const string W_GAME_MATCH = "w_game_match";//游戏对局
        public const string W_START_PLAY = "w_start_play";//开始玩法
        public const string W_END_PLAY = "w_end_play";//开始玩法
        public const string W_TASK = "w_task";//任务相关

        public const string W_ADSCENE_SHOW = "w_adscene_show";//广告场景展示
        public const string w_ADREWARD_CLICK = "w_adreward_click";//点击激励视频按钮
        public const string w_AD_REWARD = "w_ad_reward";//成功观看激励视频
        public static string W_PAGE_VIEW = "w_page_view";//页面访问
        public static string W_PAGE_CLICK = "w_click";//页面点击

        public const string W_SHARE_START = "w_start_share";//点击分享
        public const string W_SHARE_OPEN = "w_open_share";//打开分享界面

        public const string W_WITHDRAW = "w_withdraw";//提现成功
        public const string W_WITHDRAW_FAIL = "w_withdraw_fail";//提现成功

        public const string W_PURCHASE_REQUEST = "w_purchase_request";
        public const string W_PURCHASE_FAILED = "w_purchase_failed";
        public const string W_PURCHASE_SUCCESS = "w_purchase_success";
        public const string W_PURCHASE_CANCEL = "w_purchase_cancel";

        // public const string W_APP_INSTALL = "w_first_install";//用户安装后首次打开app进行上报
        // public const string W_ENGAGEMENT = "w_engagement";
        public const string W_LOG_IN = "w_log_in";
        public const string W_LOGOUT = "w_log_out";
        public const string W_FIRST_GET_COINS = "first_get_coin";


        public const string W_RETENTION = "w_retention";
        public const string W_KEY_BEHAVIOR_1 = "w_key_behavior1";
        public const string W_KEY_BEHAVIOR_2 = "w_key_behavior2";
        public const string W_KEY_BEHAVIOR_3 = "w_key_behavior3";
        public const string W_KEY_BEHAVIOR_4 = "w_key_behavior4";
        public const string W_KEY_BEHAVIOR_5 = "w_key_behavior5";
        public const string W_KEY_BEHAVIOR_6 = "w_key_behavior6";
        public const string W_KEY_BEHAVIOR_7 = "w_key_behavior7";
        public const string W_KEY_BEHAVIOR_8 = "w_key_behavior8";
        public const string W_KEY_BEHAVIOR_9 = "w_key_behavior9";
        public const string W_KEY_BEHAVIOR_10 = "w_key_behavior10";
        public const string W_KEY_BEHAVIOR_11 = "w_key_behavior11";

        public static List<string> PLATFORMLIST = new List<string>
        {
            "mopub", "dfp", "admob","facebook", "applovin" ,"max", "unity", "fyber", "ironsource", "adcolony",
            "chartboost", "vungle", "displayio", "alt", "senjoy", "appnext", "bat", "inmobi" ,"inneractive", "csj",
            "gdt","spread","marketplace","wemob","360","4399","baidu","mobvista","oppo","vivo",
            "xiaomi","creative","duadplatform","amazon","flurry","tapjoy","nend","unknown","adgeneration","maio" ,
            "aligames"
        };

        public static Dictionary<float, List<string>> countryRatioDic = new Dictionary<float, List<string>>()
        {
            { 1,new List<string>(){  "Dutch","English","German","Swedish","German","Dutch", "Japanese","Norwegian", "Danish"}},
            { 0.6f,new List<string>(){"ChineseTraditional","Icelandic","English","French","Finnish","Spanish"}},
            { 0.36f, new List<string>(){"Korean","Spanish","Portuguese","Estonian","Slovenian","Latvian","Italian","Greek","Bulgarian"}},
            { 0.1f,new List<string>(){"Romanian","Belarusian","Polish","Slovak","Czech","SerboCroatian","Indonesian","Hungarian","Russian","Ukrainian"}}
        };

        public static List<string> WesdkPlatformList = new List<string>
        {
            "unknown","adcolony","admob","applovin","chartboost","facebook","ironsource","mopub","unity","marketplace","fyber","inmobi","vungle","dfp",
            "creative","duadplatform","baidu","displayio","csj","gdt","amazon","flurry","tapjoy","360","xiaomi","4399",
            "oppo","vivo","mobvista","nend", "adgeneration", "maio", "aligames",
            "criteo","zhonghuiads","tms","five","kuaishou","imobile","pangle"
        };

        public static int GetPlatformIndexByWesdkIndex(int index)
        {
            int id = -1;

            if (index >= WesdkPlatformList.Count)
            {
                return 37;
            }
            if (PLATFORMLIST.Contains(WesdkPlatformList[index]))
            {
                id = PLATFORMLIST.IndexOf(WesdkPlatformList[index]);
                return id;
            }

            return 37;
        }

        #region Toutiao
        public const string TT_AD_SHOW = "ad_show";
        public const string TT_AD_POSITION = "ad_position";
        public const string TT_IS_AD_SUCCESS = "is_success";
        public const string TT_AD_CLICK = "ad_button_click";
        public const string TT_AD_VIEW = "ad_view";
        public const string TT_AD_COMPLETE = "is_completed";
        #endregion


        #region GDT
        public const string START_APP = "START_APP";
        public const string PAGE_VIEW = "PAGE_VIEW";
        public const string REGISTER = "REGISTER";
        public const string VIEW_CONTENT = "VIEW_CONTENT";
        public const string CONSULT = "CONSULT";
        public const string ADD_TO_CART = "ADD_TO_CART";
        public const string PURCHASE = "PURCHASE";
        public const string SEARCH = "SEARCH";
        public const string ADD_TO_WISHLIST = "ADD_TO_WISHLIST";
        public const string INITIATE_CHECKOUT = "INITIATE_CHECKOUT";
        public const string COMPLETE_ORDER = "COMPLETE_ORDER";
        public const string DOWNLOAD_APP = "DOWNLOAD_APP";
        public const string RATE = "RATE";
        public const string RESERVATION = "RESERVATION";
        public const string SHARE = "SHARE";
        public const string APPLY = "APPLY";
        public const string CLAIM_OFFER = "CLAIM_OFFER";
        public const string NAVIGATE = "NAVIGATE";
        public const string PRODUCT_RECOMMEND = "PRODUCT_RECOMMEND";
        #endregion


    }


}
