using System;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using UnityEngine;
using TaurusXAdSdk.Api;

namespace Qarth
{
    public class TaurusXAdSplashAdHandler : AdFullScreenHandler
    {
        private SplashAd m_SplashAd;

        public override bool isAdReady
        {
            get { return true; }
        }

        protected override bool DoPreLoadAd()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (m_SplashAd == null)
            {
                m_SplashAd = new SplashAd(TaurusXConfigUtil.GetAdUnitId(m_Config.unitID));
            }
#endif
            return true;
        }

        protected override bool DoShowAd()
        {
            var orient = SplashOrientation.Portrait;

            switch (Screen.orientation)
            {
                case ScreenOrientation.LandscapeLeft:
                case ScreenOrientation.LandscapeRight:
                    orient = SplashOrientation.Landscape;
                    break;
            }

            if (m_SplashAd == null)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (m_SplashAd == null)
            {
                //m_SplashAd = new SplashAd(WeSdkConfigUtil.GetAdUnitId(m_Config.unitID));
            }
#endif                
            }

            m_SplashAd.Show(orient);
            return true;
        }

    }
}

