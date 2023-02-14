using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameWish.Game;
using TaurusXAdSdk.Api;
using TaurusXAdSdk.Common;
//using Advertisers.Api;

namespace Qarth
{
    public class TaurusXAdsAdapter : AbstractAdsAdapter
    {
        public static TaurusXAdsConfig m_Config;
        public delegate void OnAdsRewardInfo(string type, string network, string unitId, float ecpm);
        public static OnAdsRewardInfo OnRewardInfoCallBack;
        public delegate void OnAdsShowInfo(string type, string network, string unitId, float ecpm);
        public static OnAdsShowInfo OnShowInfoCallBack;
        public static string AdTag { get; set; }
        protected override bool DoAdapterInit(SDKConfig config, SDKAdapterConfig adapterConfig)
        {
            m_Config = adapterConfig as TaurusXAdsConfig;
            Debug.Log("TaurusX Ads config  start");
#if UNITY_ANDROID && !UNITY_EDITOR
	        //string appId = m_Config.appIDAndroid;
            string appId = TaurusXConfigUtil.GetAppId().ToString();
#elif UNITY_IPHONE
            string appId = m_Config.appIDIos;
#else
            string appId = "unexpected_platform";
#endif
            //TaurusXAds.SetLogEnable(true);
            RegisterTracker();
            // 设置 GDPR
            TaurusXAds.SetGdprConsent(true);
            // 初始化，[APP_ID] 是在 WeSdk 平台创建应用时分配的 ID
            Debug.Log("TaurusXAds apid : " + appId);
            TaurusXAds.Init(appId);

            // TaurusXProbeMgr.S.Init();
            return true;
        }

        public override string adPlatform
        {
            get { return "wesdk"; }
        }

        public override AdHandler CreateBannerHandler()
        {
            AdHandler handler = new TaurusXAdBannerHandler();
            return handler;
        }

        public override AdHandler CreateInterstitialHandler()
        {
            AdHandler handler = new TaurusXAdInterstitialHandler();
            return handler;
        }

        public override AdHandler CreateRewardVideoHandler()
        {
            AdHandler handler = new TaurusXAdRewardedVideoHandler();
            return handler;
        }

        public override AdHandler CreateMixViewHandler()
        {
            AdHandler handler = new TaurusXAdMixViewHandler();
            return handler;
        }
        public override AdHandler CreateMixViewLazyHandler()
        {
            AdHandler handler = new TaurusXAdMixViewLazyHandler();
            return handler;
        }

        public override AdHandler CreateMixOnlyViewHandler()
        {
            AdHandler handler = new TaurusxAdMixOnlyViewHandler();
            return handler;
        }

        public override AdHandler CreateMixFullScreenHandler()
        {
            AdHandler handler = new TaurusXAdMixFullScreenHandler();
            return handler;
        }


        //public override AdHandler CreateSplashAdHandler()
        //{
        //    AdHandler handler = new WeSdkSplashAdHandler();
        //    return handler;
        //}

        private void RegisterTracker()
        {
            // Dictionary<string, string> dict = new Dictionary<string, string>()
            // {
            //     {DataAnalysisDefine.AF_SDK_ECPM,""},
            //     {DataAnalysisDefine.AF_SDK_NAME,""},
            //     {DataAnalysisDefine.AF_PID,""},
            //     {DataAnalysisDefine.AF_AD_SOURCE,""},
            //     {DataAnalysisDefine.AF_AD_CLICK_TYPE,""}
            // };

            TaurusXTracker.Instance.OnAdRequest += (object sender, TrackerEventArgs args) =>
            {
                //SendAdEvent(DataAnalysisDefine.W_AD_REQUEST, dict, args.TrackerInfo);

            };
            // 加载成功
            TaurusXTracker.Instance.OnAdLoaded += (object sender, TrackerEventArgs args) =>
            {
                //SendAdEvent(DataAnalysisDefine.W_AD_FILL, dict, args.TrackerInfo);
            };
            // 广告展示
            TaurusXTracker.Instance.OnAdShown += (object sender, TrackerEventArgs args) =>
            {
                // SendAdEvent(DataAnalysisDefine.W_AD_IMP, dict, args.TrackerInfo);

                if (args.TrackerInfo.GetLineItem().GetAdType() == TaurusXAdSdk.Api.AdType.Banner)
                {
                    return;
                }
                //快手ipu上报
                DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.AF_WATCH_AD);

                LineItem lineItem = args.TrackerInfo.GetLineItem();
                SecondaryLineItem secondaryLineItem = args.TrackerInfo.GetLineItem().GetSecondaryLineItem();

                if (lineItem != null && secondaryLineItem != null)
                {
                    if (OnShowInfoCallBack != null)
                    {
                        OnShowInfoCallBack.Invoke(lineItem.GetAdType().ToString(), secondaryLineItem.GetNetwork().ToString(), secondaryLineItem.GetAdUnitId(), secondaryLineItem.GetEcpm());
                    }
                }
            };

            TaurusXTracker.Instance.OnAdCallShow += (object sender, TrackerEventArgs args) =>
            {
                // SendAdEvent(DataAnalysisDefine.W_AD_SHOW, dict, args.TrackerInfo);
            };
            TaurusXTracker.Instance.OnAdUnitCallShow += (object sender, TrackerAdUnitEventArgs args) =>
            {

            };

            // 点击广告
            TaurusXTracker.Instance.OnAdClicked += (object sender, TrackerEventArgs args) =>
            {
                // SendAdEvent(DataAnalysisDefine.W_AD_CLICK, dict, args.TrackerInfo);
            };
            // 广告关闭
            TaurusXTracker.Instance.OnAdClosed += (object sender, TrackerEventArgs args) =>
            {
                // SendAdEvent(DataAnalysisDefine.W_AD_CLOSE, dict, args.TrackerInfo);

            };
            // 加载失败
            TaurusXTracker.Instance.OnAdFailedToLoad += (object sender, TrackerEventArgs args) =>
            {
                //SendAdEvent(DataAnalysisDefine.W, dict, args.TrackerInfo);
            };
            // 激励视频开始播放
            TaurusXTracker.Instance.OnVideoStarted += (object sender, TrackerEventArgs args) =>
            {
                //SendAdEvent(DataAnalysisDefine.W_AD_IMP, dict, args.TrackerInfo);


            };
            // 激励视频播放结束
            TaurusXTracker.Instance.OnVideoCompleted += (object sender, TrackerEventArgs args) =>
            {

            };

            // 激励成功
            TaurusXTracker.Instance.OnRewarded += (object sender, TrackerEventArgs args) =>
            {
                // SendAdEvent(DataAnalysisDefine.W_AD_REWARD, dict, args.TrackerInfo);
                //EventSystem.S.Send(SDKEventID.OnAdTrackerInfo, (int)AdType.RewardedVideo, args.TrackerInfo);
                LineItem lineItem = args.TrackerInfo.GetLineItem();
                
                SecondaryLineItem secondaryLineItem = args.TrackerInfo.GetLineItem().GetSecondaryLineItem();

                if (lineItem != null && secondaryLineItem != null)
                {
                    if (OnRewardInfoCallBack != null)
                    {
                        OnRewardInfoCallBack.Invoke(lineItem.GetAdType().ToString(), secondaryLineItem.GetNetwork().ToString(), secondaryLineItem.GetAdUnitId(), secondaryLineItem.GetEcpm());
                    }
                }
            };
            // 激励失败
            TaurusXTracker.Instance.OnRewardFailed += (object sender, TrackerEventArgs args) =>
            {
                //SendAdEvent(DataAnalysisDefine., args.TrackerInfo);
            };
        }

        // private void SendAdEvent(string eventID, Dictionary<string, string> dict, TrackerInfo info)
        // {
        //     dict[DataAnalysisDefine.AF_SDK_ECPM] = info.GeteCPM().ToString();
        //     dict[DataAnalysisDefine.AF_PID] = info.GetNetworkAdUnitId();
        //     dict[DataAnalysisDefine.AF_AD_SOURCE] = info.GetAdUnitId();
        //     dict[DataAnalysisDefine.AF_AD_CLICK_TYPE] = typeDictionary[(int)info.GetAdType()];
        //     dict[DataAnalysisDefine.AF_SDK_NAME] = DataAnalysisDefine.GetPlatformIndexByWesdkIndex((int)info.GetNetworkId()).ToString();

        //     DataAnalysisMgr.S.CustomEventDic(eventID, dict);
        // }

        private Dictionary<int, string> typeDictionary = new Dictionary<int, string>()
        {
            {0,"4"},
            {1,"0"},
            {2,"1"},
            {3,"3"},
            {4,"2"},
            {5,"unknown"},
            {6,"unknown"},
            {7,"5"},
            {8,"6"}

        };
    }
}


