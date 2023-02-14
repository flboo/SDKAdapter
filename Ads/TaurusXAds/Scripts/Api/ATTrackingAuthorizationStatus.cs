namespace TaurusXAdSdk.Api
{
    public enum ATTrackingAuthorizationStatus
    {
        NotDetermined = 0, // 不确定，目前可以获取到 idfa
        Restricted = 1, // 系统设置限制，无法获取 idfa
        Denied = 2, // 用户禁止，无法获取 idfa
        Authorized = 3 // 用户允许，可以获取 idfa
    }
}
