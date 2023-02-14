
using System;
using System.Runtime.InteropServices;
using AOT;
using System.Collections;
using System.Collections.Generic;
using ROXBase.Api;
using ROXShare.Api;
using ROXShare.Common;

namespace ROXShare.Platforms.iOS
{
    public class ROXShareClient : IROXShare
    {
        static ROXShareClient sInstance = new ROXShareClient();

        public static ROXShareClient Instance {
            get {
                return sInstance;
            }
        }

        #region callback types

        internal delegate void RichOXFissionGetInstallParamsCallback(IntPtr client, string paramJson);

        internal delegate void RichOXFissionGenShareLinkCallback(string shareUrl);
        internal delegate void RichOXFissionFailureCallback(int code, string message);

        #endregion

        private ROXShareInterface<Hashtable> getInstallParamsCallback;
        private ROXShareInterface<string> getShareLinkCallback;

        public void Init() {

        }

        public void GetInstallParams(ROXShareInterface<Hashtable> callback) {
            Instance.getInstallParamsCallback = callback;
            IntPtr managerClient = Externs.RichOXFissionCreateManager((IntPtr)GCHandle.Alloc(this));
            Externs.RichOXFissionStart(managerClient, getFissionParamsCallback);
        }

        public void GenShareUrl(string shareUrl, Hashtable urlParams, ROXShareInterface<string> callback){
            IntPtr paramDic = Externs.RichOXFissionCreateDictionary();
            foreach (string item in urlParams.Keys) 
            {
                if (urlParams[item] is int) {
                    Externs.RichOXFissionDictionaryAddIntValue(paramDic, item, (int)urlParams[item]); 
                } else if (urlParams[item] is long) {
                    Externs.RichOXFissionDictionaryAddLongValue(paramDic, item, (long)urlParams[item]); 
                } else if (urlParams[item] is bool) {
                    Externs.RichOXFissionDictionaryAddBoolValue(paramDic, item, (bool)urlParams[item]); 
                } else if (urlParams[item] is string) {
                    Externs.RichOXFissionDictionaryAddStringValue(paramDic, item, (string)urlParams[item]); 
                }
            }
            getShareLinkCallback = callback;
            Externs.RichOXFissionGenShareLink(shareUrl, paramDic, getShareLinkSuccessCallback, getShareLinkFailedCallback);
        }

        public byte[] GetQRCodeBytes(string shareUrl, int width, int height){
            return Externs.RichOXFissionGetQRCodeData((float)width, (float)height, shareUrl);
        }

        // Bitmap GetQRCodeBitmap(string shareUrl, int width, int height);

        public void ReportRegister() {
            
        }

        public void ReportEvent(string lable, int value) {

        }

        public void ReportOpenShare() {
            Externs.RichOXFissionOpenSharePage();
        }

        public void ReportStartShare() {
            Externs.RichOXFissionStartShare();
        }

        public void ReportBindEvent(bool oversea) {
            Externs.RichOXFissionReportFissionParam(oversea);
        }

        public void ReportBindEvent(bool oversea, Hashtable bindParams)
        {
            IntPtr paramDic = Externs.RichOXFissionCreateDictionary();
            foreach (string item in bindParams.Keys) 
            {
                if (bindParams[item] is int) {
                    Externs.RichOXFissionDictionaryAddIntValue(paramDic, item, (int)bindParams[item]); 
                } else if (bindParams[item] is long) {
                    Externs.RichOXFissionDictionaryAddLongValue(paramDic, item, (long)bindParams[item]); 
                } else if (bindParams[item] is bool) {
                    Externs.RichOXFissionDictionaryAddBoolValue(paramDic, item, (bool)bindParams[item]); 
                } else if (bindParams[item] is string) {
                    Externs.RichOXFissionDictionaryAddStringValue(paramDic, item, (string)bindParams[item]); 
                }
            }
            Externs.RichOXFissionReportFissionCustomParam(oversea, paramDic);
        }

        public void ReportShowShare() {
            Externs.RichOXFissionShowShare();
        }

        public void ReportShareSuccess() {
            Externs.RichOXFissionShareSuccess();
        }

        [MonoPInvokeCallback(typeof(RichOXFissionGetInstallParamsCallback))]
        private static void getFissionParamsCallback(IntPtr client, string paramJson) {
            if (Instance.getInstallParamsCallback != null)
            {
                Hashtable tables = new Hashtable();
                
                if (paramJson.Length > 0)
                {
                    JSONObject serverExtrasObject = (JSONObject)JSONNode.Parse(paramJson);
                    foreach (KeyValuePair<string, JSONNode> kv in serverExtrasObject)
                    {
                        tables.Add(kv.Key, kv.Value);
                    }
                }
                
                Instance.getInstallParamsCallback.OnSuccess(tables);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXFissionGenShareLinkCallback))]
        private static void getShareLinkSuccessCallback(string shareUrl) {
            if (Instance.getShareLinkCallback != null)
            {
                Instance.getShareLinkCallback.OnSuccess(shareUrl);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXFissionFailureCallback))]
        private static void getShareLinkFailedCallback(int code, string message) {
            if (Instance.getShareLinkCallback != null)
            {
                Instance.getShareLinkCallback.OnFailed(code, message);
            }
        }
    }
}