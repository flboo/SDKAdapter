using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppsFlyerSDK;

// This class is intended to be used the the AppsFlyerObject.prefab
namespace Qarth
{
    public class AppsFlyerInstance : TMonoSingleton<AppsFlyerInstance>, IAppsFlyerConversionData, IAppsFlyerUserInvite, IAppsFlyerValidateReceipt
    {
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        // Mark AppsFlyer CallBacks
        /// <summary>
        ///  `conversionData` contains information about install. Organic/non-organic, etc.
        /// <see>https://support.appsflyer.com/hc/en-us/articles/360000726098-Conversion-Data-Scenarios#Introduction</see>
        /// </summary>
        /// <param name="conversionData">JSON string of the returned conversion data.</param>
        public void onConversionDataSuccess(string conversionData)
        {
            AppsFlyer.AFLog("didReceiveConversionData", conversionData);
            Dictionary<string, object> conversionDataDictionary = AppsFlyer.CallbackStringToDictionary(conversionData);
            //Debug.LogError(">>>>>>>>>>>>>>>>>>>>>didReceiveConversionData:" + conversionData);
            if (conversionDataDictionary != null && conversionDataDictionary.ContainsKey("af_status"))
            {
                var af_status = conversionDataDictionary["af_status"].ToString().Trim().ToLower();
                try
                {
                    string appsFlyerAttr = PlayerPrefs.GetString("AppsFlyerAttr", "");
                    if (string.IsNullOrEmpty(appsFlyerAttr))
                    {
                        PlayerPrefs.SetString("AppsFlyerAttr", af_status);
                        DataAnalysisMgr.S.CustomEvent("AppsFlyerAttribute", af_status);
                    }
                    else
                    {
                        if (af_status != appsFlyerAttr)
                        {
                            Log.e("AppsFlyerTrackerCallbacks SaveAttr:" + appsFlyerAttr + "   CurrentAttr:" + af_status);
                            DataAnalysisMgr.S.CustomEvent("AppsFlyerTrackerExpection", af_status);
                        }
                    }
                }
                catch (System.Exception e)
                {
                    Log.e("AppsFlyerTrackerCallbacks:" + e.Message + e.StackTrace);
                }
            }
        }

        public void onConversionDataFail(string error)
        {
            AppsFlyer.AFLog("didReceiveConversionDataWithError", error);
        }

        /// <summary>
        /// `attributionData` contains information about OneLink, deeplink.
        /// <see>https://support.appsflyer.com/hc/en-us/articles/208874366-OneLink-Deep-Linking-Guide#Intro</see>
        /// </summary>
        /// <param name="attributionData">JSON string of the returned deeplink data.</param>
        public void onAppOpenAttribution(string attributionData)
        {
            AppsFlyer.AFLog("onAppOpenAttribution", attributionData);
            Dictionary<string, object> attributionDataDictionary = AppsFlyer.CallbackStringToDictionary(attributionData);
            // add direct deeplink logic here
        }

        public void onAppOpenAttributionFailure(string error)
        {
            AppsFlyer.AFLog("onAppOpenAttributionFailure", error);
        }

        /// <summary>
        /// The success callback for generating OneLink URLs. 
        /// </summary>
        /// <param name="link">A string of the newly created url.</param>
        public void onInviteLinkGenerated(string link)
        {

        }

        /// <summary>
        /// The error callback for generating OneLink URLs
        /// </summary>
        /// <param name="error">A string describing the error.</param>
        public void onInviteLinkGeneratedFailure(string error)
        {

        }

        /// <summary>
        /// (ios only) iOS allows you to utilize the StoreKit component to open
        /// the App Store while remaining in the context of your app.
        /// More details at <see>https://support.appsflyer.com/hc/en-us/articles/115004481946-Cross-Promotion-Tracking#tracking-cross-promotion-impressions</see>
        /// </summary>
        /// <param name="link">openStore callback Contains promoted `clickURL`</param>
        public void onOpenStoreLinkGenerated(string link)
        {
            Application.OpenURL(link);
        }


        /// <summary>
        /// The success callback for validateAndSendInAppPurchase API.
        /// For Android : the callback will return "Validate success".
        /// For iOS : the callback will return a JSON string from apples verifyReceipt API.
        /// </summary>
        /// <param name="result"></param>
        public void didFinishValidateReceipt(string result)
        {

        }

        /// <summary>
        /// The error callback for validateAndSendInAppPurchase API.
        /// </summary>
        /// <param name="error">A string describing the error.</param>
        public void didFinishValidateReceiptWithError(string error)
        {

        }


        public bool IsNonOrganicUser()
        {
            string attr = PlayerPrefs.GetString("AppsFlyerAttr", "");
            bool isNonOrganic = attr.Trim().ToLower().Equals("non-organic");
            return isNonOrganic;
        }
    }
}