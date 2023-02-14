using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace Qarth
{
    public class AccountMgr : TSingleton<AccountMgr>, IAccountAdapter
    {
        private static IAccountAdapter m_Adapter;
        public void Init()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            m_Adapter = new AndroidAccountAdapter();
#elif UNITY_IOS
			m_Adapter = new IOSAccountAdapter();
#else
            m_Adapter = new DefauleAccountAdapter();
#endif

        }
        public string GetOpenUdid()
        {
            return m_Adapter.GetOpenUdid();
        }
        public int GetPriorityScore()
        {
            throw new NotImplementedException();
        }
        public byte[] Encrypt(string _json)
        {
            return m_Adapter.Encrypt(_json);
        }
        public byte[] Decrypt(byte[] _data)
        {
            return m_Adapter.Decrypt(_data);
        }
        public bool InitWithConfig(SDKConfig config, SDKAdapterConfig adapterConfig)
        {
            throw new NotImplementedException();
        }
    }
}