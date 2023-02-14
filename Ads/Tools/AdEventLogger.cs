using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace Qarth
{
    public class AdEventLogger
    {
        public bool isEnable
        {
            get;
            set;
        }

        public string adPlacement
        {
            get;
            set;
        }

        public string adTag
        {
            get;
            set;
        }

        public string adInterface
        {
            get;
            set;
        }

        private int m_AdType;
        private string m_StateEventKey;
        private string m_IPUEventKey;
        private string m_ImpressionKey;



        public AdEventLogger(int adType)
        {
            m_AdType = adType;
            // switch (m_AdType)
            // {
            //     case AdType.Interstitial:
            //         m_StateEventKey = DataAnalysisDefine.INTERSTITIAL_STATE;
            //         m_IPUEventKey = DataAnalysisDefine.INTERSTITIAL_IPU;
            //         m_ImpressionKey = DataAnalysisDefine.IMPRESSION_INTERSTITIAL;
            //         break;
            //     case AdType.RewardedVideo:
            //         m_StateEventKey = DataAnalysisDefine.REWARD_VIDEO_STATE;
            //         m_IPUEventKey = DataAnalysisDefine.REWARD_VIDEO_IPU;
            //         m_ImpressionKey = DataAnalysisDefine.IMPRESSION_VIDEO;
            //         break;
            //     case AdType.MixFullScreen:
            //     case AdType.MixView:
            //     case AdType.MixViewLazy:
            //         m_StateEventKey = DataAnalysisDefine.MIXVIEW_STATE;
            //         m_IPUEventKey = DataAnalysisDefine.MIXVIEW_VIDEO_IPU;
            //         m_ImpressionKey = DataAnalysisDefine.IMPRESSION_MIXVIEW;
            //         break;
            //     default:
            //         break;
            // }
        }

        public void Log(string label)
        {
            if (!isEnable)
            {
                return;
            }

            //DataAnalysisMgr.S.CustomEvent(m_StateEventKey, label);

            /* 
            if (!string.IsNullOrEmpty(adPlacement))
            {
                DataAnalysisMgr.S.CustomEvent(adPlacement, label);
            }
            */
        }

        public void LogView_Complex(string label)
        {
            if (!isEnable)
            {
                return;
            }
            var dict = new Dictionary<string, string>();
            dict.Add(DataAnalysisDefine.TT_AD_COMPLETE, label);
            dict.Add(DataAnalysisDefine.TT_AD_POSITION, adTag);
            DataAnalysisMgr.S.CustomEventDic(DataAnalysisDefine.TT_AD_VIEW, dict);
        }

        public void LogFill(TDAdConfig config)
        {
            if (!isEnable)
            {
                return;
            }

            //DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.AD_FILL_PLATFORM_COUNT, config.adPlatform);
            // DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.AD_FILL_UNITID_COUNT, config.id);
        }

        public void LogRequest(TDAdConfig config)
        {
            if (!isEnable)
            {
                return;
            }

            //DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.AD_REQUEST_PLATFORM_COUNT, config.adPlatform);
            //DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.AD_REQUEST_UNITID_COUNT, config.id);
        }

        public void LogIPU(TDAdConfig config = null)
        {
            if (!isEnable)
            {
                return;
            }

            DataAnalysisMgr.S.CustomEventWithDate(m_IPUEventKey, "IPU");
            AdAnalysisMgr.S.RecordAdReward(adInterface);
            if (config != null)
                DataAnalysisMgr.S.CustomEvent(m_ImpressionKey, config.adPlatform);
        }

        public void LogIPUInit()
        {
            if (!isEnable)
            {
                return;
            }

            DataAnalysisMgr.S.CustomEventDailySingle(m_IPUEventKey, "INIT");
        }
    }
}
