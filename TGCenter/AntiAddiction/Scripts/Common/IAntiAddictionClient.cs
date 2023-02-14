using System;
using UnityEngine;
using AntiAddictionSdk.Api;

namespace AntiAddictionSdk.Common
{
    public interface IAntiAddictionClient {
        // 实名认证
        User GetUser();
        void RealName(RealNameCallback callback);
        void RealName(string name, string idNumber, RealNameCallback callback);
        void SetAutoShowTimeLimitPage(bool show);
        void SetTimeLimitCallback(TimeLimitCallback callback);
        TimeLimit CheckTimeLimitStatus();
        void Logout();

        // 健康提示、适龄提示
        AgeTip GetAgeTip();
        void ShowAgeTipPage(AgeTipCallback callback);
        HealthGameTip GetHealthGameTip();
        void ShowHealthGameTipPage(HealthGameTipCallback callback);
    }
}