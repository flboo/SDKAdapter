using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Qarth
{
    public enum OXSceneType
    {
        FloatScene,
        DialogScene,
        NativeScene,
    }

    public enum OXEventID
    {
        None = 60000,
        OnRichOXReady,
        OnRichOXH5Inited,
        OnUserInfoRefresh,
        OnNormalStrategyFetched,
        OnUserAssetInfoUpdate,
        OnWithdrawSuccess,
        OnROXShareRestoreParam,
        OnROXSectInfoRefresh,
        OnPiggyBankRefresh,
        OnPiggyBankDraw,
        OnCustomRuleMissionComplete,
        OnWechatLoginSuccess,
        OnWechatRegisterSuccess,
        OnWechatBindSuccess,
    }

    public struct OXSectDefine
    {
        public const string restoreIDKey = "uid";
        public const string restoreLevelKey = "fission_level";
    }

}