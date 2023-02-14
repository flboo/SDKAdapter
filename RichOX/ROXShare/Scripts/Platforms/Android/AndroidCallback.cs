using UnityEngine;
using System;
using ROXShare.Api;
using ROXShare.Common;

namespace ROXShare.Platforms.Android {
    class AndroidCallback : AndroidJavaProxy {
        public event EventHandler<ROXShareEventArgs> OnSuccess;
        public event EventHandler<ROXShareEventArgs> OnFailed;

        public AndroidCallback() : base(ClassUtils.ROXShareCallback)
        {
        }

        public void onSuccess(AndroidJavaObject response) 
        {
            Debug.Log("common response success");
            Debug.Log("the response is " + response);
            if (OnSuccess != null)
            {
                ROXShareEventArgs args = new ROXShareEventArgs() 
                {
                    SuccessResponse = new ROXShareSuccessResponse(response)
                };
                OnSuccess(this, args);
            }
        }

        public void onFailed(int code, string msg) 
        {
            if (OnFailed != null)
            {
                Debug.Log("common response failed");
                ROXShareEventArgs args = new ROXShareEventArgs() 
                {
                    ErrorResponse = new ROXShareErrorResponse(code, msg)
                };
                OnFailed(this, args);
            }
        }
    }  

    class AndroidStringCallback : AndroidJavaProxy {
        public event EventHandler<ROXShareEventArgs> OnSuccess;
        public event EventHandler<ROXShareEventArgs> OnFailed;

        public AndroidStringCallback() : base(ClassUtils.ROXShareCallback)
        {
        }

        public void onSuccess(string response) 
        {
            Debug.Log("common response success");
            Debug.Log("the response is " + response);
            if (OnSuccess != null)
            {
                ROXShareEventArgs args = new ROXShareEventArgs() 
                {
                    SuccessResponse = new ROXShareSuccessResponse(response)
                };
                OnSuccess(this, args);
            }
        }

        public void onFailed(int code, string msg) 
        {
            if (OnFailed != null)
            {
                Debug.Log("common response failed");
                ROXShareEventArgs args = new ROXShareEventArgs() 
                {
                    ErrorResponse = new ROXShareErrorResponse(code, msg)
                };
                OnFailed(this, args);
            }
        }
    }  

}