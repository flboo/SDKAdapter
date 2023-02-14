using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


namespace Qarth.Plugin
{
    public class RateHandler_iOS : RateHandler
    {
        #region Import Native implementations
#if UNITY_IOS
        [DllImport("__Internal")]
#endif
        private static extern void Qarth_Rate_RequestReview();

#if UNITY_IOS
        [DllImport("__Internal")]
#endif
        private static extern bool Qarth_Rate_IsInSandbox();

        #endregion


        #region Features

        public override void RequestReview()
        {
            Qarth_Rate_RequestReview();
        }

        public override void OpenRatingPage()
        {
#if UNITY_IOS
            string iOS_7 = ""
                + "itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews"
                + "?id={0}" // App ID
                + "&type=Purple+Software"
                + "&mt=8";

            // string iOS_7 = "itms-apps://itunes.apple.com/app/id<APP_ID>";

            string iOS_8 = ""
                + "itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews"
                + "?id={0}" // App ID
                + "&type=Purple+Software"
                + "&mt=8"
                + "&onlyLatestVersion=true"
                + "&pageNumber=0"
                + "&sortOrdering=1";

            string iOS_11 = ""
                + "itms-apps://itunes.apple.com/us/app/id{0}" // App ID
                + "?action=write-review"
                + "&mt=8";

            string versionString = UnityEngine.iOS.Device.systemVersion;
            string[] versionTokens = versionString.Split("."[0]);
            int version = int.Parse(versionTokens[0]);

            string URL;
            if (version <= 7)
            {
                URL = iOS_7;
            }
            else if (version <= 8)
            {
                URL = iOS_8;
            }
            else
            {
                URL = iOS_11;
            }

            URL = string.Format(URL, RateMgr.S.iOS_App_ID());
            Application.OpenURL(URL);
#endif
        }

        public override bool IsInSandbox()
        {
            return Qarth_Rate_IsInSandbox();
        }

        #endregion


    }
}
