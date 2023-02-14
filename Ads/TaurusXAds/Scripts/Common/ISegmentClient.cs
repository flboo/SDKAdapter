namespace TaurusXAdSdk.Common
{
    public interface ISegmentClient
    {
        string GetId();

        int GetPriority();

        string GetChannel();

        string GetCondition();
    }
}