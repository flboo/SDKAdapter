namespace TaurusXAdSdk.Common
{
    public interface ITaurusXConfigUtilClient
    {
        string GetAppId();

        string GetAdUnitId(string name);

        string GetChannel();

        string GetString(string name);
    }
}
