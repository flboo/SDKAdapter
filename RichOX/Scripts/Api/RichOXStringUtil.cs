using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace  ROXBase.Api
{
    public class RichOXStringUtil
    {
        public static string ValueOf(string info) 
        {
            if (string.IsNullOrEmpty(info)) 
            {
                return "";
            } 
            else
            {
                if (info == "null")
                {
                    return "";
                }
                else
                {
                    return info;
                }
            }
        }
    }
}