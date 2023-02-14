using System;
using System.Runtime.InteropServices;

namespace RichOX.Platforms.iOS
{
    internal class Externs
    {
        #region Common externs

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXRelease(IntPtr obj);

        #endregion


        #region ROXManager externs

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXSetAppVerCode(int appVerCode);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXSetFissionPlatform(string platform);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXInit(string appId, string userId, string deviceId);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr ROXCreateManager(IntPtr managerClient);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXSetEventCallback(
            IntPtr managerPtr, RichOXClient.RichOXEventCallback eventCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXSetBindWeChatCallback(
            IntPtr managerPtr, RichOXClient.RichOXBindWeChatCallback bindWeChatCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXNotifyBindWeChatResult(IntPtr managerPtr, 
                                                              bool status, 
                                                              string reason);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXSetGiftUpdateCallback(
            IntPtr managerPtr, RichOXClient.RichOXGiftUpdateCallback giftUpdateCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXSetLanguageCode(string languageCode);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXSetGetShareLinkCallback(
            IntPtr managerPtr, RichOXClient.ROXUnityGetShareLinkCallback getShareLinkCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXNotifyGetShareLinkResult(
            IntPtr managerPtr, string shareLink, int code, string message);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXSetShareCallback(
            IntPtr managerPtr, RichOXClient.ROXUnityShareCallback shareCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXNotifyShareResult(
            IntPtr managerPtr, int code, string message);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr ROXTypeNSDictionaryGetAllKeys(IntPtr dicPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int ROXTypeNSArrayGetCount(IntPtr arrayPtr);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string ROXTypeNSArrayGetItem(IntPtr arrayPtr, int index);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string ROXTypeNSDictionaryGetStringValue(IntPtr dicPtr, string key);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern int ROXTypeNSDictionaryGetIntValue(IntPtr dicPtr, string key);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern long ROXTypeNSDictionaryGetLongValue(IntPtr dicPtr, string key);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool ROXTypeNSDictionaryGetBoolValue(IntPtr dicPtr, string key);

        #endregion


        #region FloatScene externs

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr ROXCreateFloatScene(IntPtr floatSceneClient, string sceneId);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXSetFloatSceneCallbacks(
            IntPtr floatScene,
            FloatSceneClient.RichOXFloatSceneDidReceiveCallback receivedCallback,
            FloatSceneClient.RichOXFloatSceneDidFailToReceiveWithErrorCallback failedCallback,
            FloatSceneClient.RichOXFloatSceneWillPresentScreenCallback willPresentCallback,
            FloatSceneClient.RichOXFloatSceneDidDismissScreenCallback didDismissCallback,
            FloatSceneClient.RichOXFloatSceneWillLeaveApplicationCallback willLeaveCallback,
            FloatSceneClient.RichOXFloatSceneRenderSuccessCallback renderSuccessCallback,
            FloatSceneClient.RichOXFloatSceneFailedToRenderCallback failedToRenderCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXSetFloatScenePosition(IntPtr floatScene, int position);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXSetFloatScenePositionWithPos(IntPtr floatScene, int x, int y);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXSetFloatScenePositionRelative(IntPtr floatScene, int position, int offsetX, int offsetY);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXSetFloatSceneSize(IntPtr floatScene, int width, int height);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXLoadFloatScene(IntPtr floatScene);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool ROXFloatSceneIsReady(IntPtr floatScene);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXShowFloatScene(IntPtr floatScene);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXHideFloatScene(IntPtr floatScene);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXDestroyFloatScene(IntPtr floatScene);

        #endregion


        #region DialogSceneClient externs

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr ROXCreateDialogScene(IntPtr dialogScene, string sceneId);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXSetDialogSceneCallbacks(
            IntPtr dialogScene,
            DialogSceneClient.RichOXDialogSceneDidReceiveCallback receivedCallback,
            DialogSceneClient.RichOXDialogSceneDidFailToReceiveWithErrorCallback failedCallback,
            DialogSceneClient.RichOXDialogSceneWillPresentScreenCallback willPresentCallback,
            DialogSceneClient.RichOXDialogSceneDidDismissScreenCallback didDismissCallback,
            DialogSceneClient.RichOXDialogSceneWillLeaveApplicationCallback willLeaveCallback,
            DialogSceneClient.RichOXDialogSceneRenderSuccessCallback renderSuccessCallback,
            DialogSceneClient.RichOXDialogSceneFailedToRenderCallback failedToRenderCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXLoadDialogScene(IntPtr dialogScene);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool ROXDialogSceneIsReady(IntPtr dialogScene);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXShowDialogScene(IntPtr dialogScene);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXDestroyDialogScene(IntPtr dialogScene);

        #endregion


        #region NativeSceneClient externs

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr ROXCreateNativeScene(IntPtr nativeScene, string sceneId);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXSetNativeSceneCallbacks(
            IntPtr nativeScene,
            NativeSceneClient.RichOXNativeSceneDidReceiveCallback receivedCallback,
            NativeSceneClient.RichOXNativeSceneDidFailToReceiveWithErrorCallback failedCallback,
            NativeSceneClient.RichOXNativeSceneWillPresentScreenCallback willPresentCallback,
            NativeSceneClient.RichOXNativeSceneDidDismissScreenCallback didDismissCallback,
            NativeSceneClient.RichOXNativeSceneWillLeaveApplicationCallback willLeaveCallback,
            NativeSceneClient.RichOXNativeSceneRenderSuccessCallback renderSuccessCallback,
            NativeSceneClient.RichOXNativeSceneFailedToRenderCallback failedToRenderCallback,
            NativeSceneClient.RichOXNativeSceneUpdateCallback updateCallback);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXLoadNativeScene(IntPtr nativeScene);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern bool ROXNativeSceneIsReady(IntPtr nativeScene);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern IntPtr ROXNativeSceneGetNativeInfo(IntPtr nativeScene);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXNativeSceneReportShown(IntPtr nativeScene);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXNativeSceneHandleClick(IntPtr nativeScene);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern void ROXDestroyNativeScene(IntPtr nativeScene);

        #endregion


        #region NativeInfoClient externs

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string ROXGetNativeInfoTitle(IntPtr nativeInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string ROXGetNativeInfoIconUrl(IntPtr nativeInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string ROXGetNativeInfoDesc(IntPtr nativeInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string ROXGetNativeInfoCTA(IntPtr nativeInfo);

        #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
        [DllImport("__Internal")]
        #endif
        internal static extern string ROXGetNativeInfoMediaUrl(IntPtr nativeInfo);

        #endregion
    }
}