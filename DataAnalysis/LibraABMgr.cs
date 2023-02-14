using System;
using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSdk.Api;
using TaurusXAdSdk.Api;
using UnityEngine;


namespace Qarth
{
    public class LibraABMgr : TSingleton<LibraABMgr>
    {
        private GameAnalyticsSdk.Api.JSONObject m_ABJson;
        public override void OnSingletonInit()
        {
            Log.i("LibraABMgr Init");
        }


        public void RegisterABFetchCallback(OnEvent evtCallback)
        {
            EventSystem.S.Register(SDKEventID.OnLibraABAllFetch, evtCallback);
        }

        public void FetchABConfs(OnEvent evtCallback)
        {
            EventSystem.S.Register(SDKEventID.OnLibraABAllFetch, evtCallback);
            FetchABConfs();
        }

        public void FetchABConfs()
        {
            try
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (TaurusXConfigUtil.GetChannel() == "bytedance")
                {
				    m_ABJson = GASdk.GetPreProperties();						
                    if (m_ABJson != null)
                    {
                        Debug.Log(" Libra Confs: " + m_ABJson.ToString());
                        EventSystem.S.Send(SDKEventID.OnLibraABAllFetch);
                    }
                }
#elif UNITY_IOS && !UNITY_EDITOR
                TTEventSender.GetABTestConfs();
#endif
            }
            catch (Exception e)
            {
                Log.e("Libra AB test " + e.Message);
            }
        }

        public string GetLibraConf(string keyName, string defaultVal)
        {
            try
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (m_ABJson != null && m_ABJson.Count > 0)
                {
                    string value = m_ABJson[keyName].Value;

                    string lastValue = PlayerPrefs.GetString("LibraABConfig_" + keyName, defaultVal);
                    if (string.IsNullOrEmpty(value))
                    {
                        value = defaultVal;
                    }
                    if (value != lastValue)
                    {
                        PlayerPrefs.SetString("LibraABConfig_" + keyName, value);
                    }
                    return value;
                }
#elif UNITY_IOS && !UNITY_EDITOR
                return TTEventSender.GetABTestConf(keyName, defaultVal));
#endif
            }
            catch (Exception e)
            {
                Log.e("Libra AB Test " + e.Message);
            }

            return defaultVal;

        }
    }

}

