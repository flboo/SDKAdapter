
namespace TaurusXAdSdk.Api
{
    public static class ATTrackingManager
    {
        public interface ATTrackingAuthorizationListener {
            void OnCompletion(ATTrackingAuthorizationStatus status);
        }

        public static ATTrackingAuthorizationStatus TrackingAuthorizationStatus() {
            #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return ATTrackingManagerClient.Instance.TrackingAuthorizationStatus();
            #else
                return ATTrackingAuthorizationStatus.NotDetermined;
            #endif
        }

        public static void RequestTrackingAuthorization(ATTrackingAuthorizationListener listener) {
            #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                ATTrackingManagerClient.Instance.RequestTrackingAuthorization(listener);
            #else
                if(listener != null) {
                    listener.OnCompletion(ATTrackingAuthorizationStatus.NotDetermined);
                }
            #endif
        }
    }
}
