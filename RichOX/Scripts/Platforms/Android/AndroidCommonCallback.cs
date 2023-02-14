using UnityEngine;
using System;
using ROXBase.Api;
using ROXBase.Common;

namespace ROXBase.Platforms.Android {
    class AndroidCommonCallback : AndroidJavaProxy {
        public event EventHandler<RichOXEventArgs> OnSuccess;
        public event EventHandler<RichOXEventArgs> OnFailed;

        public AndroidCommonCallback() : base(ClassUtils.CommonCallback)
        {
        }

        public void onSuccess(AndroidJavaObject response) 
        {
            Debug.Log("common response success");
            Debug.Log("the response is " + response);
            if (OnSuccess != null)
            {
                RichOXEventArgs args = new RichOXEventArgs() 
                {
                    CommonResponse = new ROXCommonResponse(response)
                };
                OnSuccess(this, args);
            }
        }

        public void onFailed(int code, string msg) 
        {
            if (OnFailed != null)
            {
                Debug.Log("common response failed");
                RichOXEventArgs args = new RichOXEventArgs() 
                {
                    ErrorResponse = new ROXErrorResponse(code, msg)
                };
                OnFailed(this, args);
            }
        }
    }

    class AndroidBooleanCallback : AndroidJavaProxy {
        public event EventHandler<RichOXEventArgs> OnSuccess;
        public event EventHandler<RichOXEventArgs> OnFailed;

        public AndroidBooleanCallback() : base(ClassUtils.CommonCallback)
        {
        }

        public void onSuccess(bool response) 
        {
            Debug.Log("boolean response success");
            Debug.Log("the response is " + response);
            if (OnSuccess != null)
            {
                RichOXEventArgs args = new RichOXEventArgs() 
                {
                    CommonResponse = new ROXCommonResponse(response)
                };
                OnSuccess(this, args);
            }
        }

        public void onFailed(int code, string msg) 
        {
            if (OnFailed != null)
            {
                Debug.Log("common response failed");
                RichOXEventArgs args = new RichOXEventArgs() 
                {
                    ErrorResponse = new ROXErrorResponse(code, msg)
                };
                OnFailed(this, args);
            }
        }
    }

    class AndroidIntCallback : AndroidJavaProxy {
        public event EventHandler<RichOXEventArgs> OnSuccess;
        public event EventHandler<RichOXEventArgs> OnFailed;

        public AndroidIntCallback() : base(ClassUtils.CommonCallback)
        {
        }

        public void onSuccess(int response) 
        {
            Debug.Log("int response success");
            Debug.Log("the response is " + response);
            if (OnSuccess != null)
            {
                RichOXEventArgs args = new RichOXEventArgs() 
                {
                    CommonResponse = new ROXCommonResponse(response)
                };
                OnSuccess(this, args);
            }
        }

        public void onFailed(int code, string msg) 
        {
            if (OnFailed != null)
            {
                Debug.Log("common response failed");
                RichOXEventArgs args = new RichOXEventArgs() 
                {
                    ErrorResponse = new ROXErrorResponse(code, msg)
                };
                OnFailed(this, args);
            }
        }
    }

    class AndroidStringCallback : AndroidJavaProxy {
        public event EventHandler<RichOXEventArgs> OnSuccess;
        public event EventHandler<RichOXEventArgs> OnFailed;

        public AndroidStringCallback() : base(ClassUtils.CommonCallback)
        {
        }

        public void onSuccess(string response) 
        {
            Debug.Log("int response success");
            Debug.Log("the response is " + response);
            if (OnSuccess != null)
            {
                RichOXEventArgs args = new RichOXEventArgs() 
                {
                    CommonResponse = new ROXCommonResponse(response)
                };
                OnSuccess(this, args);
            }
        }

        public void onFailed(int code, string msg) 
        {
            if (OnFailed != null)
            {
                Debug.Log("common response failed");
                RichOXEventArgs args = new RichOXEventArgs() 
                {
                    ErrorResponse = new ROXErrorResponse(code, msg)
                };
                OnFailed(this, args);
            }
        }
    }

}