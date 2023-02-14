using AntiAddictionSdk.Common;

namespace AntiAddictionSdk.Api
{
    public class TimeLimit {

        public enum Reason {
            // 1、没有限制
            No_Limit = 0,

            // 2、游客限制原因
            Tourist_1_Hour = 1, // 不超过 1 小时体验模式

            // 3、未成年人限制原因
            Child_8h_22h = 2, // 每日 22 时到次日 8 时
            Child_Holiday_3_Hour = 3, // 法定节假日每日游戏时长超过 3 小时
            Child_Normal_90_Min = 4 // 其他时间每日游戏时长超过 1.5 小时
        }

        private ITimeLimitClient mClient;
        public TimeLimit(ITimeLimitClient client) {
            mClient = client;
        }

        // 限制的原因
        public int GetReason() {
            return mClient.GetReason();
        }

        // 是否达到时间限制
        public bool IsLimit() {
            return mClient.IsLimit();
        }

        // 距离限制的时间，单位 ms
        // 限制时间小于 10 分钟时，需要提示用户
        // 0 表示达到限制条件了，需要提示用户
        public long GetTimeToLimit() {
            return mClient.GetTimeToLimit();
        }

        // 获取提示内容
        public LimitTip GetLimitTip() {
            return mClient.GetLimitTip();
        }
    }
}