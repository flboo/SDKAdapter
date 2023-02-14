using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace Qarth
{
    public class AdMixHandler : AdHandler
    {
        protected bool m_IsShowing = false;

        public override bool ShowAd()
        {
            if (!isAdReady)
            {
                return false;
            }

            if (DoShowAd())
            {
                //DataAnalysisMgr.S.CustomValueEvent("AD-Value", m_Config.ecpm);
                m_AdState = AdState.Showing;
            }

            return m_AdState == AdState.Showing;
        }

        public override void RefreshAd()
        {
            if (!m_IsShowing)
            {
                return;
            }
            //Log.i("AD->RefreshBanner:" + m_Config.id);
            DoRefreshAd();
        }


        public override void HideAd()
        {
            // if (!m_IsShowing)
            // {
            //     return;
            // }

            DoHideAd();
        }


        public override bool PreLoadAd()
        {
            if (m_AdState != AdState.NONE || string.IsNullOrEmpty(m_Config.unitID))
            {
                return false;
            }

            if (DoPreLoadAd())
            {
                m_AdState = AdState.Loading;
                m_AdInterface.OnAdRequest(m_Config);
            }
            else
            {
                Log.i(string.Format("AD-PreLoadAd:{0} - Failed", m_Config.id));
            }

            return m_AdState == AdState.Loading;
        }


        protected virtual bool DoShowAd()
        {
            return false;
        }

        protected virtual void DoHideAd()
        {

        }


        protected virtual bool DoPreLoadAd()
        {
            return false;
        }

        protected virtual bool DoRefreshAd()
        {
            return false;
        }
    }
}
