using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameWish.Game;
using Proyecto26;
using System;

namespace Qarth
{
    public class DataAnalysisRemoteConfMgr : TSingleton<DataAnalysisRemoteConfMgr>
    {
        private Dictionary<string, string> m_Headers = new Dictionary<string, string>();

        //只能用字符串格式
        public class ParamsResponse
        {
            public string af_evt_expire_day;
        }
        private ParamsResponse m_Data;
        public ParamsResponse data
        {
            get { return m_Data; }
        }

        public void Init()
        {
            if (!m_Headers.ContainsKey("Content-Encoding"))
                m_Headers.Add("Content-Encoding", "gzip");
            else
                m_Headers["Content-Encoding"] = "gzip";

            if (!string.IsNullOrEmpty(SDKConfig.S.remoteConfAppName)
                && !string.IsNullOrEmpty(SDKConfig.S.remoteConfUrl))
            {
                CustomExtensions.FetchRemoteConfParams(
                    SDKConfig.S.remoteConfAppName,
                    "data_analysis_params",
                    OnRemoteValueFetched,
                    null,
                    "all",
                    SDKConfig.S.remoteConfUrl);
            }
            else
            {
                Log.e(">>> DataAnalysis云配地址为空,详见SDKConfig文件里的DataAnalysisConfig");
            }
        }

        void OnRemoteValueFetched(string value)
        {
            m_Data = LitJson.JsonMapper.ToObject<ParamsResponse>(value);
            EventSystem.S.Send(SDKEventID.OnDataAnalysisRemoteConfFetched);
        }
    }
}