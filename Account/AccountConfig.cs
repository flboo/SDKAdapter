using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qarth
{
    [Serializable]
    public class AccountConfig
    {
        public bool isEnable = true;
        public PayAccountConfig payConfig;
    }
    [Serializable]
    public class PayAccountConfig
    {
        public bool isEnable = true;
        public string app_id = "";
    }
}