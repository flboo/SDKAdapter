using System;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using UnityEngine;

public class WeShareAndroidAdapter : AndroidJavaProxy, IShareCallback
{
    public WeShareAndroidAdapter() : base(WeShareDefine.ModooShareCallbackInteface)
    {
    }

    public event EventHandler<EventArgs> OnShareSuccess;
    public event EventHandler<EventArgs> OnShareCancle;
    public event EventHandler<EventArgs> OnShareFailed;



    public void shareCancel()
    {
        if (OnShareCancle != null)
        {
            OnShareCancle(this, EventArgs.Empty);
        }
    }

    public void shareFailed()
    {
        if (OnShareFailed != null)
        {
            OnShareFailed(this, EventArgs.Empty);
        }
    }

    public void shareSuccess()
    {
        if (OnShareSuccess != null)
        {
            OnShareSuccess.Invoke(this, EventArgs.Empty);
        }
    }


}

public class WeLoginAndroidAdapter : AndroidJavaProxy, ILoginCallBack
{
    public WeLoginAndroidAdapter() : base(WeShareDefine.ModooLoginCallbackInteface)
    {
    }



    public event Action<string> OnLoginSuccess;
    public event Action<string> OnLoginCancle;
    public event Action<string> OnLoginFailed;

    public void loginSuccess(string info)
    {
        if (OnLoginSuccess != null)
        {
            OnLoginSuccess.Invoke(info);
        }
    }

    public void loginCancel(string result)
    {
        if (OnLoginCancle != null)
        {
            OnLoginCancle.Invoke(result);
        }
    }

    public void loginFailed(string result)
    {
        if (OnLoginFailed != null)
        {
            OnLoginFailed.Invoke(result);
        }
    }
}