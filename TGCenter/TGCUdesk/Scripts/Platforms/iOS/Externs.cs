using System;
using System.Runtime.InteropServices;

namespace TGCUdesk.Platforms.iOS
{
    internal class Externs
    {

        #region UdeskHelper
        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void TGCUdeskEnter();
        #endregion
    }
}
