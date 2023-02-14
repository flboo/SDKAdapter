using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace Qarth
{
    public class IOSAccountAdapter : AbstractSDKAdapter, IAccountAdapter
    {
        public string GetOpenUdid()
        {
            return "openudid";
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