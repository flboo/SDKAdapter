using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace ROXStrategy.Platforms.Android
{
    public static class ClassUtils
    {
        #region RichOX class names
        public const string ROXNormalStrategy = "com.richox.strategy.normal.ROXNormalStrategy";
        public const string NormalStrategyConfig = "com.richox.strategy.normal.bean.NormalStrategyConfig";
        public const string NormalStrategyWithdrawTask = "com.richox.strategy.normal.bean.NormalStrategyWithdrawTask";
        public const string NormalMissionResult = "com.richox.strategy.normal.bean.NormalMissionResult";
        public const string NormalAssetStock = "com.richox.strategy.normal.bean.NormalAssetStock";
        public const string NormalAssetsInfo = "com.richox.strategy.normal.bean.NormalAssetsInfo";
        public const string StrategyWithdrawRecord = "com.richox.strategy.base.bean.StrategyWithdrawRecord";
        public const string NormalTransformResult = "com.richox.strategy.normal.bean.NormalTransformResult";
        public const string TransformAssetInfo = "com.richox.strategy.normal.bean.NormalTransformResult$TransformAssetInfo";

        public const string WithdrawInfoBuilder = "com.richox.strategy.normal.bean.GlobalWithdrawInfo$Builder";

        public const string Object = "java.lang.Object";

        public const string ArrayList = "java.util.ArrayList";
        public const string HashMap = "java.util.HashMap";
        #endregion

        #region Unity class names
        public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";
        #endregion
    }
}