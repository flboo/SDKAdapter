using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameWish.Game;
using Qarth;

namespace Qarth
{
    [System.Serializable]
    public class SDKConfig : ScriptableObject
    {
        #region 初始化过程
        private static SDKConfig s_Instance;

        private static SDKConfig LoadInstance()
        {
            ResLoader loader = ResLoader.Allocate("AppConfig", null);

            UnityEngine.Object obj = loader.LoadSync("Resources/Config/SDKConfig");
            if (obj == null)
            {
                Log.e("Not Find SDK Config, Will Use Default App Config.");
                loader.Recycle2Cache();
                return null;
            }

            Log.i("Success Load SDK Config.");
            s_Instance = obj as SDKConfig;

            SDKConfig newAB = GameObject.Instantiate(s_Instance);

            s_Instance = newAB;

            loader.Recycle2Cache();

            return s_Instance;
        }

        public static SDKConfig S
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = LoadInstance();
                }

                return s_Instance;
            }
        }


        #endregion

        #region 数据
        public string bundleIDAndroid;
        public int signatures;
        public string iosAppID;
        public string remoteConfUrl;
        public string remoteConfAppName;

        public bool shopCheckCtrl;
        public BuglyConfig buglyConfig;
        public DataAnalysisConfig dataAnalysisConfig;
        public AdsConfig adsConfig;
        public TGCenterConfig tGCenterConfig;
        public RichOXConfig richOXConfig;
        public JPushConfig jpushConfig;

        public string bundleID
        {
            get
            {
#if UNITY_IOS
                return bundleIDIos;
#elif UNITY_ANDROID
                return bundleIDAndroid;
#endif
                return bundleIDAndroid;
            }
        }

        #endregion
    }
}
