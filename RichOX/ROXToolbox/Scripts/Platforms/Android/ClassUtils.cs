using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace ROXToolbox.Platforms.Android
{
    public static class ClassUtils
    {
        #region Toolbox class names
        public const string RichOXToolbox = "com.richox.toolbox.RichOXToolbox";

        public const string Object = "java.lang.Object";

        public const string ArrayList = "java.util.ArrayList";
        #endregion

        #region Unity class names
        public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";
        #endregion
    }
}