using System;
using System.Collections;
using System.Collections.Generic;
using ROXBase.Api;
using UnityEngine;
using ROXShare.Api;
namespace Qarth
{
    public class RichOXCallback<T> : ROXInterface<T>, ROXShareInterface<T>
    {
        public Respond respond;
        public string operation;
        public Action<Respond> callback;

        public class Respond
        {
            public T result;
            public bool success;
            public int code;
            public string msg;
        }

        public RichOXCallback()
        {
            respond = new Respond();
        }

        public void OnSuccess(T t)
        {
            respond.result = t;
            respond.success = true;
            ThreadMgr.S.mainThread.PostAction(() =>
            {
                try
                {
                    callback?.Invoke(respond);
                    Log.i("ox success " + respond.ToString());
                }
                catch (Exception e)
                {
                    Debug.LogError("ox_callback_error:" + operation + "/" + e);
                }
            });
        }

        public void OnFailed(int code, string msg)
        {
            respond.success = false;
            respond.code = code;
            respond.msg = msg;
            ThreadMgr.S.mainThread.PostAction(() =>
            {
                try
                {
                    callback?.Invoke(respond);
                }
                catch (Exception e)
                {
                    Debug.LogError("ox_callback_error:" + operation + "/" + e);
                }
            });

            Debug.LogError("ox_respond_failed:" + operation + "/code:" + code + "/msg:" + msg);
        }

    }

}