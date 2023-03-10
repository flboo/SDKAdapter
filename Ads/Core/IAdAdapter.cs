using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using UnityEngine.UI;

namespace Qarth
{
    public enum AdPosition
    {
        Top = 0,
        Bottom = 1,
        TopLeft = 2,
        TopRight = 3,
        BottomLeft = 4,
        BottomRight = 5,
        Center = 6,

        CustomDefine = 100,
    }

    public class AdMixViewStyle
    {
        public const string SMALL = "taurusx_ads_nativead_small";
        public const string MEDIUM = "taurusx_ads_nativead_medium";
        public const string LARGE_1 = "taurusx_ads_nativead_large_1";
        public const string LARGE_2 = "taurusx_ads_nativead_large_2";
        public const string LARGE_3 = "taurusx_ads_nativead_large_3";
        public const string LARGE_4 = "taurusx_ads_nativead_large_4";
        public const string CUSTOM = "taurusx_native_layout";
    }

    [Serializable]
    public class AdSize
    {
        [SerializeField]
        private int m_Width;
        [SerializeField]
        private int m_Height;

        public static readonly AdSize Banner = new AdSize(320, 50);
        public static readonly AdSize MediumRectangle = new AdSize(300, 250);
        public static readonly AdSize IABBanner = new AdSize(468, 60);
        public static readonly AdSize Leaderboard = new AdSize(728, 90);
        public static readonly AdSize WideSkyscraper = new AdSize(120, 600);
        public static readonly AdSize SmartBanner = new AdSize(-1, -2);

        public AdSize(int width, int height)
        {
            this.m_Width = width;
            this.m_Height = height;
        }

        public int width
        {
            get
            {
                return m_Width;
            }
        }

        public int height
        {
            get
            {
                return m_Height;
            }
        }
    }

    public enum AdState
    {
        NONE,
        Loading,
        Loaded,
        Showing,
        Failed,
    }

    public class AdType
    {
        public const int Banner = 0;
        public const int Interstitial = 1;
        public const int RewardedVideo = 2;
        public const int NativeAD = 3;
        public const int SplashAD = 4;
        public const int MixView = 5;
        public const int MixFullScreen = 6;
        public const int MixViewLazy = 7;
        public const int MixOnlyView = 9;
    }

    public interface IBannerHandler
    {
        Vector2 GetBannerSizeInPixel();
    }

    public interface INativeAdHandler
    {
        string adTitle { get; }
        string socialContext { get; }
        string callToAction { get; }
        Sprite iconSprite { get; }
        Sprite coverImage { get; }
        AdState adState { get; }

        void RegisterGameObjectForImpression(GameObject gb, Button[] buttons);
        void UnRegisterGameObjectForImpression();
        void LoadAd();
        void Bind(Action<INativeAdHandler> textLoadL, Action<INativeAdHandler> iconLoadL, Action<INativeAdHandler> coverImageL);
        void UnBind(Action<INativeAdHandler> textLoadL, Action<INativeAdHandler> iconLoadL, Action<INativeAdHandler> coverImageL);
    }

    public interface IAdAdapter : ISDKAdapter
    {
        string adPlatform { get; }

        int adPlatformScore { get; }

        int platformIndex { get; set; }

        AdHandler CreateBannerHandler();
        AdHandler CreateInterstitialHandler();
        AdHandler CreateRewardVideoHandler();
        AdHandler CreateNativeAdHandler();
        AdHandler CreateSplashAdHandler();

        AdHandler CreateMixViewHandler();

        AdHandler CreateMixOnlyViewHandler();
        AdHandler CreateMixViewLazyHandler();
        AdHandler CreateMixFullScreenHandler();

        void InitWithData();
    }
}
