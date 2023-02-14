using UnityEngine;
using System.Collections;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

namespace Qarth
{
    public static class DisplayMetricsUtil
    {
        private const float DEFAULT_DPI = 160.0f;
        private static ScreenSize screenSize;
        private static bool screenSizeInitialised = false;

        public enum ResolutionType
        {
            ldpi,
            mdpi,
            hdpi,
            xhdpi
        }

        /**
         * 1) Basically everything is designed for "smartphone"
         * 2) In case of "tablet" some things are bigger.(Like text)
         * 3) And for the small smartphones (and there are not many and support is not so important) some things are smaller.
         */
        public enum ScreenSize
        {
            smartphoneSmall,
            smartphone,
            tablet
        }


        public static int DpToPixel(this int dp)
        {
            // Convert the dps to pixels
            return (int)(dp * GetScale() + 0.5f);
        }

        public static int DpToPixel(this float dp)
        {
            // Convert the dps to pixels
            return (int)(dp * GetScale() + 0.5f);
        }

        public static int PixelToDp(this int px)
        {
            // Convert the pxs to dps
            return (int)(px / GetScale() - 0.5f);
        }

        public static int PixelToDp(this float px)
        {
            // Convert the pxs to dps
            return (int)(px / GetScale() - 0.5f);
        }

        public static ResolutionType GetResolutionType()
        {
            float scale = GetDPI() / DEFAULT_DPI;

            ResolutionType res;
            //http://developer.android.com/guide/practices/screens_support.html
            if (scale > 1.5f)
            {
                res = DisplayMetricsUtil.ResolutionType.xhdpi;
            }
            else if (scale > 1f)
            {
                res = DisplayMetricsUtil.ResolutionType.hdpi;
            }
            else if (scale > 0.75f)
            {
                res = DisplayMetricsUtil.ResolutionType.mdpi;
            }
            else
            {
                res = DisplayMetricsUtil.ResolutionType.ldpi;
            }

            return res;
        }

        private static ScreenSize GetScreenSize()
        {
            if (screenSizeInitialised)
            {
                return screenSize;
            }

            float shortSideDP = PixelToDp(GetShortSide());

            ScreenSize size;

            if (shortSideDP > 500)
            {
                size = DisplayMetricsUtil.ScreenSize.tablet;
            }
            else if (shortSideDP < 300)
            {
                size = DisplayMetricsUtil.ScreenSize.smartphoneSmall;
            }
            else
            {
                size = DisplayMetricsUtil.ScreenSize.smartphone;
            }

            screenSize = size;
            screenSizeInitialised = true;
            return size;
        }

        public static bool IsScreenSizeSmartphoneSmall()
        {
            return GetScreenSize() == ScreenSize.smartphoneSmall;
        }

        public static bool IsScreenSizeSmartphone()
        {
            return GetScreenSize() == ScreenSize.smartphone;
        }

        public static bool IsScreenSizeTablet()
        {
            return GetScreenSize() == ScreenSize.tablet;
        }

        public static float GetDPI_Debug()
        {
            return GetDPI();
        }

        private static float GetDPI()
        {
            float dpi = Screen.dpi <= 0 ? DEFAULT_DPI : Screen.dpi;

            if (DisplayMetricsAndroid.IsAndroid)
            {
                if (DisplayMetricsAndroid.DensityDPI > 0)
                {
                    //Log.e(">>>>>>>>>>dpiiii:" + DisplayMetricsAndroid.DensityDPI);
                    dpi = DisplayMetricsAndroid.DensityDPI;
                }
            }

            //Log.e(">>>>>>>>>>dpi:" + dpi);
            return dpi;
        }

        private static float GetScale()
        {
            //Log.e(">>>>>>>>>>den:" + DisplayMetricsAndroid.Density);
            return GetDPI() / DEFAULT_DPI;
        }

        public static ScreenSize GetScreenSize_DEBUG()
        {
            return screenSize;
        }

        public static float GetScale_DEBUG()
        {
            return GetScale();
        }

        public static int GetLongSide()
        {
            if (Screen.width >= Screen.height)
            {
                return Screen.width;
            }
            return Screen.height;
        }

        public static int GetShortSide()
        {
            if (Screen.width < Screen.height)
            {
                return Screen.width;
            }
            return Screen.height;
        }

        public static bool isScreenPortrait()
        {
            return Screen.height > Screen.width;
        }

        public static float GetShortSideInInch()
        {
            return (float)System.Math.Round(GetShortSide() / GetDPI(), 1);
        }

        public static float GetShortSideInCentimeters()
        {
            return (float)System.Math.Round(GetShortSide() / GetDPI() * 2.54f, 1);
        }

        public static int GetShortSideInDP()
        {
            return DisplayMetricsUtil.PixelToDp(DisplayMetricsUtil.GetShortSide());
        }

        public static float GetLongSideInInch()
        {
            return (float)System.Math.Round(GetLongSide() / GetDPI(), 1);
        }

        public static float GetLongSideInCentimeters()
        {
            return (float)System.Math.Round(GetLongSide() / GetDPI() * 2.54f, 1);
        }

        public static int GetLongSideInDP()
        {
            return DisplayMetricsUtil.PixelToDp(DisplayMetricsUtil.GetLongSide());
        }

#if UNITY_IOS
        public static int PxToPt(int px)
        {
            int ratio = 2;
            switch (Device.generation)
            {
                case DeviceGeneration.iPhoneX:
                case DeviceGeneration.iPhoneXS:
                case DeviceGeneration.iPhoneXSMax:
                case DeviceGeneration.iPhone8Plus:
                case DeviceGeneration.iPhone7Plus:
                case DeviceGeneration.iPhone6Plus:
                case DeviceGeneration.iPhone6SPlus:
                case DeviceGeneration.iPhoneUnknown:
                case DeviceGeneration.Unknown:
                    {
                        ratio = 3;
                    }
                    break;
            }

            return (int)(px * 1.0f / ratio);
        }

        public static int CalcSafeArea(int yPos, TaurusXAdSdk.Api.AdPosition pos)
        {
            bool needFix = false;
            switch (Device.generation)
            {
                case DeviceGeneration.iPhoneX:
                case DeviceGeneration.iPhoneXS:
                case DeviceGeneration.iPhoneXSMax:
                case DeviceGeneration.iPhoneXR:
                    {
                        needFix = true;

                    }
                    break;
                case DeviceGeneration.iPhoneUnknown:
                case DeviceGeneration.Unknown:
                    {
                        if (Screen.height * 1.0f / Screen.width > 2)
                        {
                            needFix = true;
                        }
                    }
                    break;
            }

            if (needFix)
            {
                if (pos == TaurusXAdSdk.Api.AdPosition.Top)
                    yPos -= 44;
                else if (pos == TaurusXAdSdk.Api.AdPosition.Bottom)
                    yPos += 34;
            }

            return yPos;
        }
#endif
    }


    public class DisplayMetricsAndroid
    {
        public static bool IsAndroid { get; protected set; }

        // The logical density of the display
        public static float Density { get; protected set; }

        // The screen density expressed as dots-per-inch
        public static float DensityDPI { get; protected set; }

        // The absolute height of the display in pixels
        public static int HeightPixels { get; protected set; }

        // The absolute width of the display in pixels
        public static int WidthPixels { get; protected set; }

        // The absolute height of the display in pixels
        public static int ShortSidePixels { get; protected set; }

        // The absolute width of the display in pixels
        public static int LongSidePixels { get; protected set; }

        // A scaling factor for fonts displayed on the display
        public static float ScaledDensity { get; protected set; }

        // The exact physical pixels per inch of the screen in the X dimension
        public static float XDPI { get; protected set; }

        // The exact physical pixels per inch of the screen in the Y dimension
        public static float YDPI { get; protected set; }

        static DisplayMetricsAndroid()
        {
            IsAndroid = false;



            // Early out if we're not on an Android device
            if (Application.platform != RuntimePlatform.Android)
            {
                IsAndroid = false;
                return;
            }
            IsAndroid = true;


            // The following is equivalent to this Java code:
            //
            // metricsInstance = new DisplayMetrics();
            // UnityPlayer.currentActivity.getWindowManager().getDefaultDisplay().getMetrics(metricsInstance);
            //
            // ... which is pretty much equivalent to the code on this page:
            // http://developer.android.com/reference/android/util/DisplayMetrics.html

            //FOR IOS
            //IsAndroid = false;


            using (

                AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"),
                metricsClass = new AndroidJavaClass("android.util.DisplayMetrics")
                )
            {
                using (
                    AndroidJavaObject metricsInstance = new AndroidJavaObject("android.util.DisplayMetrics"),
                    activityInstance = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity"),
                    windowManagerInstance = activityInstance.Call<AndroidJavaObject>("getWindowManager"),
                    displayInstance = windowManagerInstance.Call<AndroidJavaObject>("getDefaultDisplay")
                    )
                {
                    displayInstance.Call("getMetrics", metricsInstance);
                    Density = metricsInstance.Get<float>("density");
                    DensityDPI = metricsInstance.Get<int>("densityDpi");
                    HeightPixels = metricsInstance.Get<int>("heightPixels");
                    WidthPixels = metricsInstance.Get<int>("widthPixels");
                    ScaledDensity = metricsInstance.Get<float>("scaledDensity");
                    XDPI = metricsInstance.Get<float>("xdpi");
                    YDPI = metricsInstance.Get<float>("ydpi");
                    if (HeightPixels > WidthPixels)
                    {
                        LongSidePixels = HeightPixels;
                        ShortSidePixels = WidthPixels;
                    }
                    else
                    {
                        LongSidePixels = WidthPixels;
                        ShortSidePixels = HeightPixels;
                    }
                }
            }

        }
    }
}