using System;
using System.Collections;
using System.Collections.Generic;
using ROXShare.Api;
using UnityEngine;


namespace GameWish.Game
{
	public class ROXH5ShareCallback : ROXShareInterface<string>
    {
        public Action<int,string> callback;

        public void OnSuccess(string t)
        {
            callback?.Invoke(0,t);
        }

        public void OnFailed(int code, string msg)
        {
            callback?.Invoke(code,msg);
        }
    }
	
}