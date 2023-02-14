using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaurusXAdSdk.Api;

namespace Advertisers.Platforms.Android
{
    public static class Utils
    {

        public const string AdUnitTrackInfoClassName = "com.fodlab.probe.AdUnitTrackInfo";
        public const string LineItemTrackInfoClassName = "com.fodlab.probe.LineItemTrackInfo";

        public const string ProbeManagerClassName = "com.fodlab.probe.ProbeManager";
        public const string SimpleTrackerListenerClassName = "com.taurusx.ads.core.api.tracker.TrackerListener";

        public const string TrackInfoClassName = "com.taurusx.ads.core.api.tracker.TrackerInfo";
        public const string AdUnitInfoClassName = "com.taurusx.ads.core.api.tracker.AdUnitInfo";
        public const string AdContentInfoClassName = "com.taurusx.ads.core.api.tracker.contentinfo.AdContentInfo";
        



        #region Unity class names
        public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";
        #endregion

        public static AndroidJavaObject ToJavaTrackInfo(TrackerInfo info)
        {
            AndroidJavaObject trackInfo = new AndroidJavaObject(TrackInfoClassName);
            AndroidJavaObject adContentInfo = new AndroidJavaObject(AdContentInfoClassName);
            if(info != null) {
                adContentInfo.Call("setAdMode", info.GetAdContentInfo().GetAdMode());
                adContentInfo.Call("setAdvertiser", info.GetAdContentInfo().GetAdvertiser());
                adContentInfo.Call("setBody", info.GetAdContentInfo().GetBody());
                adContentInfo.Call("setCallToAction", info.GetAdContentInfo().GetCallToAction());
                adContentInfo.Call("setClickUrl", info.GetAdContentInfo().GetClickUrl());
                // adContentInfo.Call("setContentType", info.GetAdContentInfo().GetContentType());
                adContentInfo.Call("setIconUrl", info.GetAdContentInfo().GetIconUrl());
                adContentInfo.Call("setImageUrl", info.GetAdContentInfo().GetImageUrl());
                // adContentInfo.Call("setIsApp", info.GetAdContentInfo().GetIsApp());
                adContentInfo.Call("setPkgName", info.GetAdContentInfo().GetPkgName());
                adContentInfo.Call("setPrice", info.GetAdContentInfo().GetPrice());
                adContentInfo.Call("setRating", info.GetAdContentInfo().GetRatinig());
                // adContentInfo.Call("setRenderType", info.GetAdContentInfo().GetRenderType());
                adContentInfo.Call("setStore", info.GetAdContentInfo().GetStore());
                adContentInfo.Call("setSubTitle", info.GetAdContentInfo().GetSubTitle());
                adContentInfo.Call("setTitle", info.GetAdContentInfo().GetTitle());
                adContentInfo.Call("setVideoUrl", info.GetAdContentInfo().GetVideoUrl());

                trackInfo.Call("setAdContentInfo", adContentInfo);
                trackInfo.Call("setAdType", info.GetAdType());
                trackInfo.Call("setAdUnitId", info.GetAdUnitId());
                trackInfo.Call("setAdUnitName", info.GetAdUnitName());
                trackInfo.Call("setECPM", info.GeteCPM());
                trackInfo.Call("setNetworkAdUnitId", info.GetNetworkAdUnitId());
                trackInfo.Call("setNetworkId", info.GetNetworkId());
            }
            return trackInfo;
        }

        public static AndroidJavaObject ToJavaAdUnitInfo(TrackerAdUnitInfo info) 
        {
            AndroidJavaObject adUnitInfo = new AndroidJavaObject(AdUnitInfoClassName);
            AndroidJavaObject adContentInfo = new AndroidJavaObject(AdContentInfoClassName);
            if(info != null) {
                adContentInfo.Call("setAdMode", info.GetAdContentInfo().GetAdMode());
                adContentInfo.Call("setAdvertiser", info.GetAdContentInfo().GetAdvertiser());
                adContentInfo.Call("setBody", info.GetAdContentInfo().GetBody());
                adContentInfo.Call("setCallToAction", info.GetAdContentInfo().GetCallToAction());
                adContentInfo.Call("setClickUrl", info.GetAdContentInfo().GetClickUrl());
                // adContentInfo.Call("setContentType", info.GetAdContentInfo().GetContentType());
                adContentInfo.Call("setIconUrl", info.GetAdContentInfo().GetIconUrl());
                adContentInfo.Call("setImageUrl", info.GetAdContentInfo().GetImageUrl());
                // adContentInfo.Call("setIsApp", info.GetAdContentInfo().GetIsApp());
                adContentInfo.Call("setPkgName", info.GetAdContentInfo().GetPkgName());
                adContentInfo.Call("setPrice", info.GetAdContentInfo().GetPrice());
                adContentInfo.Call("setRating", info.GetAdContentInfo().GetRatinig());
                // adContentInfo.Call("setRenderType", info.GetAdContentInfo().GetRenderType());
                adContentInfo.Call("setStore", info.GetAdContentInfo().GetStore());
                adContentInfo.Call("setSubTitle", info.GetAdContentInfo().GetSubTitle());
                adContentInfo.Call("setTitle", info.GetAdContentInfo().GetTitle());
                adContentInfo.Call("setVideoUrl", info.GetAdContentInfo().GetVideoUrl());

                adUnitInfo.Call("setAdUnitId", info.GetAdUnitId());
                adUnitInfo.Call("setAdUnitName", info.GetAdUnitName());
                adUnitInfo.Call("setAdType", info.GetAdType());
                adUnitInfo.Call("setAdContentInfo", adContentInfo);
            }
            return adUnitInfo;
        }
    }
}