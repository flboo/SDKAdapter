using System;
using System.Collections;
using TaurusXAdSdk.Api;
using TaurusXAdSdk.Platforms.Android;
using Advertisers.Api;
using Advertisers.Common;
using UnityEngine;

namespace Advertisers.Platforms.Android
{
    public class ProbeManagerClient : IProbeManager
    {
        AndroidJavaObject mProbeManager;
        AndroidJavaObject mContext;
        AndroidJavaClass mProbeManagerClass;


        public ProbeManagerClient()
        {
            mProbeManagerClass = new AndroidJavaClass(Utils.ProbeManagerClassName);
            mProbeManager = mProbeManagerClass.CallStatic<AndroidJavaObject>("getInstance");
        }


        public void init()
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            mContext = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            mProbeManager.Call("init", mContext);
        }
        public bool getReportStatus()
        {
            return mProbeManager.Call<bool>("getReportStatus");
        }
        public void setReportStatus(bool status)
        {
            mProbeManager.Call("setReportStatus", status);
        }
        public void registerTrackListener(TrackListener listener)
        {
            
        }
        public void unRegisterTrackListener(TrackListener listener)
        {
            
        }
    }
}
