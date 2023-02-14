using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace Qarth
{
    public interface IAccountAdapter : ISDKAdapter
    {
        string GetOpenUdid();
        byte[] Encrypt(string _json);
        byte[] Decrypt(byte[] _data);
    }
}
