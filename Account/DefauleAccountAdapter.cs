using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace Qarth
{
    public class DefauleAccountAdapter : AbstractSDKAdapter, IAccountAdapter
    {
        public string GetOpenUdid()
        {
            return SystemInfo.deviceUniqueIdentifier;
        }
        public byte[] Encrypt(string _json)
        {
            return null;
        }
        public byte[] Decrypt(byte[] _data)
        {
            return null;
        }
    }
}