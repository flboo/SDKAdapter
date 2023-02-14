using ROXSect.Common;

namespace ROXSect.Platforms
{
    public static class ClientFactory
    {
        public static IROXSect ROXSectClientInstance()
        {
            #if UNITY_EDITOR
                return new DummyROXSect();
	        #elif UNITY_ANDROID
                return ROXSect.Platforms.Android.ROXSectClient.Instance;
	        #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return ROXSect.Platforms.iOS.ROXSectClient.Instance;
            #else
                return new DummyROXSect();
            #endif
        }

        public static IROXSectClient ROXSectClientInstanceNew()
        {
            #if UNITY_EDITOR
                return new DummyROXSectClient();
	        #elif UNITY_ANDROID
                return ROXSect.Platforms.Android.ROXSectClientManager.Instance;
	        #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return ROXSect.Platforms.iOS.ROXSectClientManager.Instance;
            #else
                return new DummyROXSectClient();
            #endif
        }
    }
}