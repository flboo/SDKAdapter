using RichOX.Common;

namespace RichOX.Platforms
{
    public static class ClientFactory
    {
        public static IRichOXClient RichOXClientInstance()
        {
            #if UNITY_EDITOR
                return new DummyRichOXClient();
	        #elif UNITY_ANDROID
                return RichOX.Platforms.Android.RichOXClient.Instance;
	        #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return RichOX.Platforms.iOS.RichOXClient.Instance;
            #else
                return new DummyRichOXClient();
            #endif
        }

        public static IFloatSceneClient BuildFloatSceneClient(string sceneId)
        {
            #if UNITY_EDITOR
                return new DummyFloatSceneClient();
	        #elif UNITY_ANDROID
                return new RichOX.Platforms.Android.FloatSceneClient(sceneId);
	        #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new RichOX.Platforms.iOS.FloatSceneClient(sceneId);
            #else
                return new DummyFloatSceneClient();
            #endif
        }

        public static IDialogSceneClient BuildDialogSceneClient(string sceneId)
        {
            #if UNITY_EDITOR
                return new DummyDialogSceneClient();
            #elif UNITY_ANDROID
                return new RichOX.Platforms.Android.DialogSceneClient(sceneId);
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new RichOX.Platforms.iOS.DialogSceneClient(sceneId);
            #else
                return new DummyDialogSceneClient();
            #endif
        }
        
        public static INativeSceneClient BuildNativeSceneClient(string sceneId)
        {
            #if UNITY_EDITOR
                return new DummyNativeSceneClient();
            #elif UNITY_ANDROID
                return new RichOX.Platforms.Android.NativeSceneClient(sceneId);
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new RichOX.Platforms.iOS.NativeSceneClient(sceneId);
            #else
                return new DummyDialogSceneClient();
            #endif
        }
    }
}