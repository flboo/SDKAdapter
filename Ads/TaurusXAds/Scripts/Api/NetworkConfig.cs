using UnityEngine;
using System;

namespace TaurusXAdSdk.Api
{
    public class NetworkConfig
    {
        protected NetworkConfig mBaseClient;

        protected AndroidJavaObject mAndroidObject;
        protected IntPtr miOSPtr;

        virtual
        public AndroidJavaObject ToAndroidJavaObject()
        {
            return mBaseClient != null ? mBaseClient.ToAndroidJavaObject() : mAndroidObject;
        }

        virtual
        public IntPtr ToiOSIntPrt()
        {
            return mBaseClient != null ? mBaseClient.ToiOSIntPrt() : miOSPtr;
        }
    }
}
