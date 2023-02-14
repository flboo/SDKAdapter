using UnityEngine;
using System;
using ROXBase.Api;
using ROXBase.Common;

namespace ROXBase.Platforms.Android {
    class AndroidRunnable : AndroidJavaProxy
    {
        public event EventHandler<EventArgs> Run;     

        public AndroidRunnable() : base(ClassUtils.Runnable)
        {
        }

        public void run() 
        {
            Debug.Log("Run on UI thread !!!!!");
            if (Run != null)
            {
                EventArgs runnableArgs = new EventArgs() ;                               
                Run(this, runnableArgs);
            }
        }       
    }   
}