using System;
using System.Runtime.InteropServices;

namespace ROXShare.Platforms.iOS {
    internal class Externs {
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXFissionRelease(IntPtr client);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXFissionCreateManager(IntPtr client);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXFissionStart(IntPtr manager, ROXShareClient.RichOXFissionGetInstallParamsCallback callback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr RichOXFissionCreateDictionary();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXFissionDictionaryAddStringValue(IntPtr dicPtr, string key, string stringValue);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXFissionDictionaryAddIntValue(IntPtr dicPtr, string key, int intValue);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXFissionDictionaryAddLongValue(IntPtr dicPtr, string key, long longValue);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXFissionDictionaryAddBoolValue(IntPtr dicPtr, string key, bool boolValue);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void RichOXFissionGenShareLink(string host, IntPtr dicPtr, ROXShareClient.RichOXFissionGenShareLinkCallback successCallback, ROXShareClient.RichOXFissionFailureCallback failedCallback);
    
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern  byte[] RichOXFissionGetQRCodeData(float width, float height, string shareUrl);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern  void RichOXFissionReportFissionParam(bool overSea);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern  void RichOXFissionReportFissionCustomParam(bool overSea, IntPtr customParamPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern  void RichOXFissionShowShare();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern  void RichOXFissionOpenSharePage();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern  void RichOXFissionStartShare();

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern  void RichOXFissionShareSuccess();
    }
}