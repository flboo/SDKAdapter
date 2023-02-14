using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace ROXShare.Platforms.Android
{
    public static class ClassUtils
    {
        #region RichOX class names
        public const string ROXShare = "com.richox.share.RichOXShare";
        public const string ROXShareCallback = "com.richox.share.ShareCallback";
        public const string ROXShareConstant = "com.richox.share.ShareConstant";

        public const string Object = "java.lang.Object";

        public const string HashMap = "java.util.HashMap";

        public const string Map = "java.util.Map";
        #endregion

        #region Unity class names
        public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";
        #endregion
    }
}