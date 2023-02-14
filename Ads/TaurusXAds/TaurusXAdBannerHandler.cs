using System;
using GameWish.Game;
using Qarth;
using UnityEngine;
using TaurusXAdSdk.Api;


namespace Qarth
{
    public class TaurusXAdBannerHandler : AdBannerHandler
    {
        private BannerAd m_BannerView;

        public override bool isAdReady
        {
            get
            {
                if (m_BannerView == null)
                    return false;
                return true;
            }
        }

        private float screenDensity
        {
            get
            {
                return Screen.dpi / 160;
            }
        }

        protected override bool DoPreLoadAd()
        {
            if (string.IsNullOrEmpty(m_Config.unitID))
            {
                return false;
            }
#if UNITY_ANDROID && !UNITY_EDITOR
            if (m_BannerView == null)
            {
                m_BannerView = new BannerAd(TaurusXConfigUtil.GetAdUnitId(m_Config.unitID));
                m_BannerView.OnAdLoaded += HandleOnAdLoaded;
                m_BannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
                m_BannerView.OnAdShown += HandleOnAdOpened;
                //m_BannerView.OnAdClicked += HandleOnAdOpened;
                m_BannerView.OnAdClosed += HandleOnAdClosed;
                m_BannerView.LoadAd();
                m_BannerView.Hide();
            }

            return true;
#endif
            return false;
        }

        protected override bool DoShowAd()
        {
            if (m_BannerView == null)
            {
                return false;
            }

            m_IsShowing = true;

            SyncAdPosition();

            m_BannerView.Show();

            return true;
        }

        protected override bool DoRefreshAd()
        {
            if (!m_IsShowing)
            {
                return false;
            }

            m_AdState = AdState.Loading;

            m_BannerView.LoadAd();

            return true;
        }

        public override void SyncAdPosition()
        {
            base.SyncAdPosition();
            if (m_AdInterface.adPosition == AdPosition.CustomDefine)
            {
                Vector2Int pos = m_AdInterface.adCustomGrid;
                m_BannerView.SetPosition(pos.x, pos.y);
            }
            else
            {
                m_BannerView.SetPosition(ConvertAdPosition(m_AdInterface.adPosition));
            }
        }

        public void DestryAD()
        {
            if (m_BannerView == null)
            {
                return;
            }

            m_IsShowing = false;
            m_BannerView.Destroy();
            m_BannerView = null;
        }

        protected override void DoHideAd()
        {
            if (m_BannerView == null)
            {
                return;
            }

            m_IsShowing = false;

            m_BannerView.Hide();
        }

        public override string ToString()
        {
            return "#WebeyeBanner:" + m_Config.id;
        }

        public Vector2 GetBannerSizeInPixel()
        {
            throw new NotImplementedException();
        }

        protected void HandleOnAdLoaded(object sender, EventArgs args)
        {
            HandleOnAdLoaded();
        }

        protected void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            HandleOnAdFailedToLoad(args.AdError.GetMessage());
        }
        protected void HandleOnAdClosed(object sender, EventArgs args)
        {
            HandleOnAdClosed();
        }

        protected void HandleOnAdOpened(object sender, EventArgs args)
        {
            HandleOnAdOpened();
        }

        protected static BannerAdPosition ConvertAdPosition(AdPosition position)
        {
            return (BannerAdPosition)((int)position);
        }

    }
}

