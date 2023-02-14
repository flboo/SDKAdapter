namespace AntiAddictionSdk.Common
{
    public interface ILimitTipClient {
        string GetTitle();

        string GetDesc();

        string GetButton();

        bool CanRealName();
    }
}