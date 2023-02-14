using UnityEngine;
using TaurusXAdSdk.Api;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class TaurusXClient : ITaurusXClient
    {
        static TaurusXClient sInstance = new TaurusXClient();

        public static TaurusXClient Instance {
            get {
                return sInstance;
            }
        }

        private readonly AndroidJavaObject mTaurusX;

        TaurusXClient() {
            AndroidJavaClass sdkClass = new AndroidJavaClass(Utils.TaurusXClassName);
            mTaurusX = sdkClass.CallStatic<AndroidJavaObject>("getDefault");
        }

        #region ITaurusXClient
        public void Init(string appId)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            mTaurusX.Call("init", activity, appId);
        }

        public void SetGdprConsent(bool consent)
        {
            mTaurusX.Call("setGdprConsent", consent);
        }

        public void SetLogEnable(bool enable)
        {
            mTaurusX.Call("setLogEnable", enable);
        }

        public void SetTestMode(bool testMode)
        {
            mTaurusX.Call("setTestMode", testMode);
        }

        public void SetTestServer(string testServer)
        {
            mTaurusX.Call("setTestServer", testServer);
        }

        public void SetNetworkDebugMode(bool debugMode)
        {
            mTaurusX.Call("setNetworkDebugMode", debugMode);
        }

        public void SetNetworkDebugMode(Api.Network network, bool debugMode) 
        {
            mTaurusX.Call("setNetworkDebugMode", Utils.ToJavaNetwork(network), debugMode);
        }

        public void SetNetworkTestMode(bool testMode)
        {
            mTaurusX.Call("setNetworkTestMode", testMode);
        }

        public void SetNetworkTestMode(Api.Network network, bool testMode)
        {
            mTaurusX.Call("setNetworkTestMode", Utils.ToJavaNetwork(network), testMode);
        }

        public void SetiOSAppPauseOnBackground(bool pause)
        {
            // Keep Empty
        }

        public void SetGlobalNetworkConfigs(NetworkConfigs networkConfigs)
        {
            mTaurusX.Call("setGlobalNetworkConfigs", Utils.ToJavaNetworkConfigs(networkConfigs));
        }

        public void SetSegment(Segment segment)
        {
            mTaurusX.Call("setSegment", Utils.ToJavaSegment(segment));
        }

        public void SetDownloadConfirmNetwork(DownloadNetwork network)
        {
            mTaurusX.Call("setDownloadConfirmNetwork", (int) network);
        }

        public void SetLineItemFilter(LineItemFilter filter)
        {
            if (filter != null)
            {
                mTaurusX.Call("setLineItemFilter", new AndroidLineItemFilter(filter));
            }
        }

        public string GetUid()
        {
            return mTaurusX.Call<string>("getUid");
        }

        #endregion
    }
}