using UnityEngine;
using TaurusXAdSdk.Common;

namespace TaurusXAdSdk.Platforms.Android
{
    public class ScreenUtilClient : IScreenUtilClient
    {
        static ScreenUtilClient sInstance = new ScreenUtilClient();

        public static ScreenUtilClient Instance {
            get {
                return sInstance;
            }
        }

        readonly AndroidJavaClass mScreenUtil;

        ScreenUtilClient()
        {
            mScreenUtil = new AndroidJavaClass(Utils.ScreenUtilClassName);
        }

        #region IScreenUtilClient

        public bool IsPortrait()
        {
            return mScreenUtil.CallStatic<bool>("isScreenPortrait", Utils.GetActivity());
        }

        public bool IsTablet()
        {
            return mScreenUtil.CallStatic<bool>("isTablet", Utils.GetActivity());
        }

        #endregion
    }
}
