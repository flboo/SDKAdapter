using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROXBase.Api;
using System;
using System.Runtime.InteropServices;

namespace ROXBase.Platforms.Android
{
    public static class ClassUtils
    {
        #region RichOX class names
        public const string RichOXClassName = "com.richox.base.RichOX";

        public const string ROXInitBuilder = "com.richox.base.CommonBuilder$Builder";

        public const string ROXEventCallback = "com.richox.base.EventCallback";

        public const string ROXInitCallback = "com.richox.base.InitCallback";

        public const string Object = "java.lang.Object";

        public const string RichOXUser = "com.richox.base.RichOXUser";

        public const string CommonCallback = "com.richox.base.CommonCallback";

        public const string UserBean = "com.richox.base.bean.user.UserBean";

        public const string SpecificUserInfo = "com.richox.base.bean.user.SpecificUserInfo";

        public const string UserTokenBean = "com.richox.base.bean.user.UserTokenBean";

        public const string ArrayList = "java.util.ArrayList";

        public const string Runnable = "java.lang.Runnable";

        public const string ROXUser = "com.richox.base.ROXUser";
       
        #endregion

        #region Unity class names
        public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";
        #endregion
    }
}