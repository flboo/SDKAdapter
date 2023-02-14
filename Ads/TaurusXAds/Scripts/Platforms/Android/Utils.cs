using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Platforms.Android
{
    public static class Utils
    {
        #region Ads Unity Plugin class names
        public const string TaurusXClassName = "com.taurusx.ads.core.api.TaurusXAds";
        public const string NetworkClassName = "com.taurusx.ads.core.api.model.Network";
        public const string NetworkConfigsClassName = "com.taurusx.ads.core.api.ad.networkconfig.NetworkConfigs";
        public const string SegmentClassName = "com.taurusx.ads.core.api.segment.Segment";
        public const string AdSizeClassName = "com.taurusx.ads.core.api.ad.config.AdSize";
        public const string LineItemFilterClassName = "com.taurusx.ads.core.api.requestfilter.LineItemFilter";

        public const string BannerAdClassName = "com.taurusx.ads.core.api.ad.BannerAdView";
        public const string InterstitialAdClassName = "com.taurusx.ads.core.api.ad.InterstitialAd";
        public const string RewardedVideoAdClassName = "com.taurusx.ads.core.api.ad.RewardedVideoAd";
        public const string NativeAdClassName = "com.taurusx.ads.core.api.ad.nativead.NativeAd";
        public const string SplashAdClassName = "com.taurusx.ads.core.api.ad.SplashAd";
        public const string MixViewAdClassName = "com.taurusx.ads.core.api.ad.MixViewAd";
        public const string MixFullScreenAdClassName = "com.taurusx.ads.core.api.ad.mixfull.MixFullScreenAd";

        public const string AdListenerClassName = "com.taurusx.ads.core.api.listener.newapi.base.BaseAdListener";
        public const string InterstitialAdListenerClassName = "com.taurusx.ads.core.api.listener.newapi.base.BaseInterstitialAdListener";
        public const string RewardedVideoAdListenerClassName = "com.taurusx.ads.core.api.listener.newapi.base.BaseRewardedVideoAdListener";
        public const string SplashAdListenerClassName = "com.taurusx.ads.core.api.listener.newapi.base.BaseSplashAdListener";

        public const string BannerAdSizeClassName = "com.taurusx.ads.core.api.model.BannerAdSize";

        public const string UnityNativeAdLayoutClassName = "com.taurusx.ads.core.api.ad.nativead.layout.UnityNativeAdLayout";
        public const string InteractiveAreaClassName = "com.taurusx.ads.core.api.ad.nativead.layout.InteractiveArea";

        public const string TrackerClassName = "com.taurusx.ads.core.api.tracker.TaurusXAdsTracker";
        public const string TrackerInfoClassName = "com.taurusx.ads.core.api.tracker.TrackerInfo";
        public const string TrackerListenerClassName = "com.taurusx.ads.core.api.tracker.TrackerListener";

        public const string GameConfigClassName = "com.taurusx.ads.core.api.GameConfigUtil";

        public const string AutoLoadConfigClassName = "com.taurusx.ads.core.api.ad.AutoLoadConfig";

        public const string ScreenUtilClassName = "com.taurusx.ads.core.api.utils.ScreenUtil";
        #endregion

        #region Unity class names
        public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";
        #endregion

        #region Android SDK class names
        public const string BundleClassName = "android.os.Bundle";
        public const string DateClassName = "java.util.Date";
        public const string ArrayClassName = "java.lang.reflect.Array";
        public const string IntegerClassName = "java.lang.Integer";
        #endregion

        #region util function
        public static AndroidJavaObject GetActivity()
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            return playerClass.GetStatic<AndroidJavaObject>("currentActivity");
        }

        private static int GetJavaBannerAdSizeCode(BannerAdSize adSize)
        {
            switch (adSize)
            {
                case BannerAdSize.BANNER_320_50:
                    return 1;
                case BannerAdSize.BANNER_300_250:
                    return 2;
                case BannerAdSize.BANNER_320_100:
                    return 3;
                case BannerAdSize.BANNER_468_60:
                    return 34;
                case BannerAdSize.BANNER_728_90:
                    return 11;
            }
            return -1;
        }

        public static AndroidJavaObject GetJavaBannerAdSize(BannerAdSize adSize)
        {
            AndroidJavaClass bannerAdSizeClass = new AndroidJavaClass(BannerAdSizeClassName);
            return bannerAdSizeClass.CallStatic<AndroidJavaObject>("getSize", GetJavaBannerAdSizeCode(adSize));
        }

        public static AndroidJavaObject GetJavaAdSize(float width, float height)
        {
            AndroidJavaObject adSize = new AndroidJavaObject(AdSizeClassName, width, height);
            return adSize;
        }

        public static BannerAdSize FromJavaBannerAdSize(AndroidJavaObject adSize)
        {
            int id = adSize.Call<int>("getId");
            switch (id)
            {
                case 2:
                    return BannerAdSize.BANNER_300_250;
                case 3:
                    return BannerAdSize.BANNER_320_100;
                case 34:
                    return BannerAdSize.BANNER_468_60;
                case 11:
                    return BannerAdSize.BANNER_728_90;
                case 1:
                default:
                    return BannerAdSize.BANNER_320_50;
            }
        }

        public static AdEventArgs GenerateAdEventArgs(AndroidJavaObject lineItem) {
            return new AdEventArgs() {
                LineItem = new LineItem(new LineItemClient(lineItem))
            };
        }

        public static AdFailedToLoadEventArgs GenerateAdFailedToLoadEventArgs(AndroidJavaObject adError) {
            return new AdFailedToLoadEventArgs() {
                AdError = new AdError(new AdErrorClient(adError))
            };
        }

        public static Dictionary<string, string> ToCSharpDictionary(AndroidJavaObject map)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            if (map != null)
            {
                AndroidJavaObject entrySet = map.Call<AndroidJavaObject>("entrySet");
                if (entrySet != null)
                {
                    AndroidJavaObject entryArray = entrySet.Call<AndroidJavaObject>("toArray");
                    AndroidJavaClass arrayClass = new AndroidJavaClass(ArrayClassName);
                    int length = arrayClass.CallStatic<int>("getLength", entryArray);
                    for (int i = 0; i < length; i++)
                    {
                        AndroidJavaObject entry = arrayClass.CallStatic<AndroidJavaObject>("get", entryArray, i);
                        if (entry != null)
                        {
                            string key = entry.Call<string>("getKey");
                            string value = entry.Call<string>("getValue");
                            dictionary.Add(key, value);
                        }
                    }
                }
            }

            return dictionary;
        }

        public static AndroidJavaObject ToJavaArray(int[] values)
        {
            AndroidJavaClass arrayClass = new AndroidJavaClass(ArrayClassName);
            AndroidJavaObject arrayObject = arrayClass.CallStatic<AndroidJavaObject>("newInstance", new AndroidJavaClass(IntegerClassName), values.Length);
            for (int i = 0; i < values.Length; i++)
            {
                arrayClass.CallStatic("set", arrayObject, i, new AndroidJavaObject(IntegerClassName, values[i]));
            }
            return arrayObject;
        }

        public static AndroidJavaObject ToJavaNetwork(Api.Network network)
        {
            AndroidJavaClass networkClass = new AndroidJavaClass(NetworkClassName);
            return networkClass.CallStatic<AndroidJavaObject>("fromId", (int)network);
        }

        public static AndroidJavaObject ToJavaSegment(Segment segment)
        {
            AndroidJavaClass javaSegmentClass = new AndroidJavaClass(SegmentClassName);
            AndroidJavaObject javaSegmentBuilder = javaSegmentClass.CallStatic<AndroidJavaObject>("Builder");
            if (segment != null)
            {
                javaSegmentBuilder.Call<AndroidJavaObject>("setChannel", segment.GetChannel());
            }
            return javaSegmentBuilder.Call<AndroidJavaObject>("build");
        }

        public static AndroidJavaObject ToJavaNetworkConfigs(NetworkConfigs configs)
        {
            AndroidJavaClass networkConfigsClazz = new AndroidJavaClass(NetworkConfigsClassName);
            AndroidJavaObject networkConfigsBuilder = networkConfigsClazz.CallStatic<AndroidJavaObject>("Builder");

            if (configs != null)
            {
                ArrayList configList = configs.GetConfigList();
                for (int i = 0; i < configList.Count; i++)
                {
                    NetworkConfig config = (NetworkConfig)configList[i];
                    if (config != null)
                    {
                        networkConfigsBuilder.Call<AndroidJavaObject>("addConfig", config.ToAndroidJavaObject());
                    }
                }
            }

            return networkConfigsBuilder.Call<AndroidJavaObject>("build");
        }

        public static AndroidJavaObject ToJavaUnityNativeAdLayout(NativeAdLayout layout)
        {
            if (layout == null)
            {
                return null;
            }

            AndroidJavaObject unityNativeAdLayout = new AndroidJavaObject(UnityNativeAdLayoutClassName, layout.GetLayout());

            InteractiveArea area = layout.GetInteractiveArea();
            if (area != null)
            {
                AndroidJavaObject javaArea = ToJavaInteractiveArea(area);
                if (javaArea != null)
                {
                    unityNativeAdLayout.Call("setInteractiveArea", javaArea);
                }
            }

            return unityNativeAdLayout;
        }

        public static AndroidJavaObject ToJavaInteractiveArea(InteractiveArea area)
        {
            if (area == null)
            {
                return null;
            }

            AndroidJavaClass areaClass = new AndroidJavaClass(InteractiveAreaClassName);
            AndroidJavaObject areaBuilder = areaClass.CallStatic<AndroidJavaObject>("Builder");

            if (area.HasTitle())
            {
                areaBuilder.Call<AndroidJavaObject>("addTitle");
            }
            if (area.HasSubTitle())
            {
                areaBuilder.Call<AndroidJavaObject>("addSubTitle");
            }
            if (area.HasBody())
            {
                areaBuilder.Call<AndroidJavaObject>("addBody");
            }
            if (area.HasAdvertiser())
            {
                areaBuilder.Call<AndroidJavaObject>("addAdvertiser");
            }
            if (area.HasCallToAction())
            {
                areaBuilder.Call<AndroidJavaObject>("addCallToAction");
            }

            if (area.HasIconLayout())
            {
                areaBuilder.Call<AndroidJavaObject>("addIconLayout");
            }
            if (area.HasMediaViewLayout())
            {
                areaBuilder.Call<AndroidJavaObject>("addMediaViewLayout");
            }
            if (area.HasAdChoicesLayout())
            {
                areaBuilder.Call<AndroidJavaObject>("addAdChoicesLayout");
            }

            if (area.HasRatingBar())
            {
                areaBuilder.Call<AndroidJavaObject>("addRatingBar");
            }
            if (area.HasRatingTextView())
            {
                areaBuilder.Call<AndroidJavaObject>("addRatingTextView");
            }
            if (area.HasPrice())
            {
                areaBuilder.Call<AndroidJavaObject>("addPrice");
            }
            if (area.HasStore())
            {
                areaBuilder.Call<AndroidJavaObject>("addStore");
            }

            if (area.HasRootLayout())
            {
                areaBuilder.Call<AndroidJavaObject>("addRootLayout");
            }

            if (area.HasCustomView())
            {
                areaBuilder.Call<AndroidJavaObject>("addCustomView");
            }

            return areaBuilder.Call<AndroidJavaObject>("build");
        }
        #endregion
    }
}
