namespace RichOX.Common
{
    public interface IInterActiveInfoClient
    {
        bool HasTriggered();

        int GetRewardedNumber();

        int GetMaxNumber();

        int GetCurrentNumber();

        string GetExtra();
    }
}
