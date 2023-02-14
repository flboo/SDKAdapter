//
//  ROXInterface.m
//  ROX
//
//  Created by RichOX on 2020/7/16.
//  Copyright © 2020 RichOX. All rights reserved.
//
// 注意：不要打包到 Framework 中。

#import <Foundation/Foundation.h>
#import <RichOX/RichOX.h>

#pragma mark - Common methods
// Remove an object from cache.
void ROXRelease(ROXTypeRef ref) {
    if (ref) {
        ROXObjectCache *cache = [ROXObjectCache sharedInstance];
        [cache.references removeObjectForKey:[(__bridge NSObject*)ref rox_referenceKey]];
    }
}

static NSString *ROXStringFromUTF8String(const char *bytes) { return bytes ? @(bytes) : nil; }

/// Returns a C string from a C array of UTF8-encoded bytes.
static const char *cStringCopy(const char *string) {
    if (!string) {
        return NULL;
    }
    char *res = (char *)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

const char *ROXStringToChar(NSString *string) {
    if (string == nil) {
        string = @"";
    }
    return cStringCopy(string.UTF8String);
}

#pragma mark - Init
void ROXSetAppVerCode(int appVerCode) {
    [ROXManager setAppVerCode:appVerCode];
}

void ROXSetFissionPlatform(const char *platform) {
    [ROXManager setFissionPlatform:ROXStringFromUTF8String(platform)];
}

void ROXInit(const char *appId, const char *userId, const char *deviceId) {
    NSString* appIdString = ROXStringFromUTF8String(appId);
    NSString* userIdString = ROXStringFromUTF8String(userId);
    NSString* deviceIdString = ROXStringFromUTF8String(deviceId);
    [ROXManager initWithAppId:appIdString userId:userIdString deviceId:deviceIdString];
}

#pragma mark - ROXUnityManager
ROXTypeManagerRef ROXCreateManager(ROXTypeManagerClientRef *managerClient) {
    ROXUnityManager *manager = ROXUnityManager.sharedInstance;
    manager.managerClient = managerClient;
    return (__bridge ROXTypeManagerRef)manager;
}

// Event
void ROXSetEventCallback(ROXTypeManagerRef manager, ROXEventCallback eventCallback) {
    ROXUnityManager *internalManager = (__bridge ROXUnityManager *)manager;
    internalManager.eventCallback = eventCallback;
}

// Bind WeChat
void ROXSetBindWeChatCallback(ROXTypeManagerRef manager, ROXUnityBindWeChatCallback bindWeChatCallback) {
    ROXUnityManager *internalManager = (__bridge ROXUnityManager *)manager;
    internalManager.unityBindWeChatCallback = bindWeChatCallback;
}

void ROXNotifyBindWeChatResult(ROXTypeManagerRef manager, BOOL status, const char *result) {
    ROXUnityManager *internalManager = (__bridge ROXUnityManager *)manager;
    NSString *resultString = ROXStringFromUTF8String(result);
    [internalManager notifyBindWeChatStatus:status withResult:resultString];
}

// Update Gift Info
void ROXSetGiftUpdateCallback(ROXTypeManagerRef manager, ROXGiftUpdateCallback giftUpdateCallback) {
    ROXUnityManager *internalManager = (__bridge ROXUnityManager *)manager;
    internalManager.giftUpdateCallback = giftUpdateCallback;
}


#pragma mark - FloatScene
ROXTypeFloatSceneRef ROXCreateFloatScene(ROXTypeFloatSceneClientRef *floatSceneClient, const char *sceneId) {
    ROXUnityFloatView *floatView = [[ROXUnityFloatView alloc] initWithFloatSceneClientRef:floatSceneClient 
                                                                   sceneEntryId:ROXStringFromUTF8String(sceneId) 
                                                                 viewController:[ROXPluginUtil unityGLViewController]];
    ROXObjectCache *cache = [ROXObjectCache sharedInstance];
    [cache.references setObject:floatView forKey:[floatView rox_referenceKey]];
    return (__bridge ROXTypeFloatSceneRef)floatView;
}

void ROXSetFloatSceneCallbacks(ROXTypeFloatSceneRef floatScene,
                               ROXFloatSceneDidReceiveCallback receivedCallback,
                               ROXFloatSceneDidFailToReceiveWithErrorCallback failedCallback,
                               ROXFloatSceneWillPresentScreenCallback willPresentCallback,
                               ROXFloatSceneDidDismissScreenCallback didDismissCallback,
                               ROXFloatSceneWillLeaveApplicationCallback willLeaveCallback,
                               ROXFloatSceneRenderSuccessCallback renderSuccessCallback,
                               ROXFloatSceneRenderFailedCallback renderFailedCallback) {
    ROXUnityFloatView *internalFloatView = (__bridge ROXUnityFloatView *)floatScene;
    internalFloatView.receivedCallback = receivedCallback;
    internalFloatView.failedCallback = failedCallback;
    internalFloatView.willPresentCallback = willPresentCallback;
    internalFloatView.didDismissCallback = didDismissCallback;
    internalFloatView.willLeaveCallback = willLeaveCallback;
    internalFloatView.renderSuccessCallback = renderSuccessCallback;
    internalFloatView.renderFailedCallback = renderFailedCallback;
}

void ROXSetFloatScenePosition(ROXTypeFloatSceneRef floatScene, ROXUnityAdPosition position) {
    ROXUnityFloatView *internalFloatView = (__bridge ROXUnityFloatView *)floatScene;
    [internalFloatView setUnityPosition:position];
}

void ROXSetFloatScenePositionWithPos(ROXTypeFloatSceneRef floatScene, int x, int y) {
    ROXUnityFloatView *internalFloatView = (__bridge ROXUnityFloatView *)floatScene;
    [internalFloatView setUnityPositionWithX:x andY:y];
}

void ROXSetFloatScenePositionRelative(ROXTypeFloatSceneRef floatScene, 
                                      ROXUnityAdPosition position, 
                                      int offsetX, 
                                      int offsetY) {
    ROXUnityFloatView *internalFloatView = (__bridge ROXUnityFloatView *)floatScene;
    [internalFloatView setUnityPositionRelative:position offsetX:offsetX offsetY:offsetY];
}

void ROXSetFloatSceneSize(ROXTypeFloatSceneRef floatScene, int width, int height) {
    ROXUnityFloatView *internalFloatView = (__bridge ROXUnityFloatView *)floatScene;
    [internalFloatView setUnityWidth:width height:height];
}

void ROXLoadFloatScene(ROXTypeFloatSceneRef floatScene) {
    ROXUnityFloatView *internalFloatView = (__bridge ROXUnityFloatView *)floatScene;
    [internalFloatView load];
}

BOOL ROXFloatSceneIsReady(ROXTypeFloatSceneRef floatScene) {
    ROXUnityFloatView *internalFloatView = (__bridge ROXUnityFloatView *)floatScene;
    if([internalFloatView sceneRenderReady]) {
        NSLog(@"ROXUnityFloatView isReady");
        return YES;
    } else {
        NSLog(@"ROXUnityFloatView isn't Ready");
        return NO;
    }
}

void ROXShowFloatScene(ROXTypeFloatSceneRef floatScene) {
    ROXUnityFloatView *internalFloatView = (__bridge ROXUnityFloatView *)floatScene;
    [internalFloatView showUnity];
}

void ROXHideFloatScene(ROXTypeFloatSceneRef floatScene) {
    ROXUnityFloatView *internalFloatView = (__bridge ROXUnityFloatView *)floatScene;
    [internalFloatView hideUnity];
}

void ROXDestroyFloatScene(ROXTypeFloatSceneRef floatScene) {
    ROXUnityFloatView *internalFloatView = (__bridge ROXUnityFloatView *)floatScene;
    [internalFloatView removeUnity];
}


#pragma mark - DialogScene
ROXTypeDialogSceneRef ROXCreateDialogScene(ROXTypeDialogSceneClientRef *dialogSceneClient, 
                                           const char *sceneId) {
    ROXUnityDialog *dialog = [[ROXUnityDialog alloc] initWithDialogSceneClient:dialogSceneClient 
                                                        sceneEntryId:ROXStringFromUTF8String(sceneId)];
    ROXObjectCache *cache = [ROXObjectCache sharedInstance];
    [cache.references setObject:dialog forKey:[dialog rox_referenceKey]];
    return (__bridge ROXTypeDialogSceneRef)dialog;
}

void ROXSetDialogSceneCallbacks(ROXTypeDialogSceneRef dialogScene,
                                ROXDialogSceneDidReceiveCallback receivedCallback,
                                ROXDialogSceneDidFailToReceiveWithErrorCallback failedCallback,
                                ROXDialogSceneWillPresentScreenCallback willPresentCallback,
                                ROXDialogSceneDidDismissScreenCallback didDismissCallback,
                                ROXDialogSceneWillLeaveApplicationCallback willLeaveCallback,
                                ROXDialogSceneRenderSuccessCallback renderSuccessCallback,
                                ROXDialogSceneRenderFailedCallback renderFailedCallback) {
    ROXUnityDialog *internalDialog = (__bridge ROXUnityDialog *)dialogScene;
    internalDialog.receivedCallback = receivedCallback;
    internalDialog.failedCallback = failedCallback;
    internalDialog.willPresentCallback = willPresentCallback;
    internalDialog.didDismissCallback = didDismissCallback;
    internalDialog.willLeaveCallback = willLeaveCallback;
    internalDialog.renderSuccessCallback = renderSuccessCallback;
    internalDialog.renderFailedCallback = renderFailedCallback;
}

void ROXLoadDialogScene(ROXTypeDialogSceneRef dialogScene) {
    ROXUnityDialog *internalDialog = (__bridge ROXUnityDialog *)dialogScene;
    [internalDialog load];
}

BOOL ROXDialogSceneIsReady(ROXTypeDialogSceneRef dialogScene) {
    ROXUnityDialog *internalDialog = (__bridge ROXUnityDialog *)dialogScene;
    if([internalDialog sceneRenderReady]) {
        NSLog(@"ROXUnityDialog isReady");
        return YES;
    } else {
        NSLog(@"ROXUnityDialog isn't Ready");
        return NO;
    }
}

void ROXShowDialogScene(ROXTypeDialogSceneRef dialogScene) {
    ROXUnityDialog *internalDialog = (__bridge ROXUnityDialog *)dialogScene;
    [internalDialog showFromViewController:[ROXPluginUtil unityGLViewController]];
}

void ROXDestroyDialogScene(ROXTypeDialogSceneRef dialogScene) {
}


#pragma mark - NativeScene
ROXTypeNativeSceneRef ROXCreateNativeScene(ROXTypeNativeSceneClientRef *nativeSceneClient, 
                                           const char *sceneId) {
    ROXUnityNative *native = [[ROXUnityNative alloc] initWithNativeSceneClient:nativeSceneClient 
                                                                  sceneEntryId:ROXStringFromUTF8String(sceneId)
                                                                viewController:[ROXPluginUtil unityGLViewController]];
    ROXObjectCache *cache = [ROXObjectCache sharedInstance];
    [cache.references setObject:native forKey:[native rox_referenceKey]];
    return (__bridge ROXTypeNativeSceneRef)native;
}

void ROXSetNativeSceneCallbacks(ROXTypeNativeSceneRef nativeScene,
                                ROXNativeSceneDidReceiveCallback receivedCallback,
                                ROXNativeSceneDidFailToReceiveWithErrorCallback failedCallback,
                                ROXNativeSceneWillPresentScreenCallback willPresentCallback,
                                ROXNativeSceneDidDismissScreenCallback didDismissCallback,
                                ROXNativeSceneWillLeaveApplicationCallback willLeaveCallback,
                                ROXNativeSceneRenderSuccessCallback renderSuccessCallback,
                                ROXNativeSceneRenderFailedCallback renderFailedCallback,
                                ROXNativeSceneUpdateCallback updateCallback) {
    ROXUnityNative *internalNative = (__bridge ROXUnityNative *)nativeScene;
    internalNative.receivedCallback = receivedCallback;
    internalNative.failedCallback = failedCallback;
    internalNative.willPresentCallback = willPresentCallback;
    internalNative.didDismissCallback = didDismissCallback;
    internalNative.willLeaveCallback = willLeaveCallback;
    internalNative.renderSuccessCallback = renderSuccessCallback;
    internalNative.renderFailedCallback = renderFailedCallback;
    internalNative.updateCallback = updateCallback;
}

void ROXLoadNativeScene(ROXTypeNativeSceneRef nativeScene) {
    ROXUnityNative *internalNative = (__bridge ROXUnityNative *)nativeScene;
    [internalNative load];
}

BOOL ROXNativeSceneIsReady(ROXTypeNativeSceneRef nativeScene) {
    ROXUnityNative *internalNative = (__bridge ROXUnityNative *)nativeScene;
    if([internalNative sceneRenderReady]) {
        NSLog(@"ROXUnityNative isReady");
        return YES;
    } else {
        NSLog(@"ROXUnityNative isn't Ready");
        return NO;
    }
}

ROXTypeNativeDataRef ROXNativeSceneGetNativeInfo(ROXTypeNativeSceneRef nativeScene) {
    ROXUnityNative *internalNative = (__bridge ROXUnityNative *)nativeScene;
    ROXNativeData *nativeData = [internalNative getNativeInfo];
    
    ROXObjectCache *cache = [ROXObjectCache sharedInstance];
    [cache.references setObject:nativeData forKey:[nativeData rox_referenceKey]];
    return (__bridge ROXTypeNativeDataRef)nativeData;
}

void ROXNativeSceneReportShown(ROXTypeNativeSceneRef nativeScene) {
    ROXUnityNative *internalNative = (__bridge ROXUnityNative *)nativeScene;
    return [internalNative reportShown];
}

void ROXNativeSceneHandleClick(ROXTypeNativeSceneRef nativeScene) {
    ROXUnityNative *internalNative = (__bridge ROXUnityNative *)nativeScene;
    return [internalNative handleClick];
}

void ROXDestroyNativeScene(ROXTypeNativeSceneRef nativeScene) {
}


#pragma mark - ROXNativeData methods
const char *ROXGetNativeInfoTitle(ROXTypeNativeDataRef nativeData) {
    ROXNativeData *internalNativeData = (__bridge ROXNativeData *)nativeData;
    return ROXStringToChar(internalNativeData.title);
}

const char *ROXGetNativeInfoIconUrl(ROXTypeNativeDataRef nativeData) {
    ROXNativeData *internalNativeData = (__bridge ROXNativeData *)nativeData;
    return ROXStringToChar(internalNativeData.iconUrl);
}

const char *ROXGetNativeInfoDesc(ROXTypeNativeDataRef nativeData) {
    ROXNativeData *internalNativeData = (__bridge ROXNativeData *)nativeData;
    return ROXStringToChar(internalNativeData.desc);
}

const char *ROXGetNativeInfoCTA(ROXTypeNativeDataRef nativeData) {
    ROXNativeData *internalNativeData = (__bridge ROXNativeData *)nativeData;
    return ROXStringToChar(internalNativeData.callToAction);
}

const char *ROXGetNativeInfoMediaUrl(ROXTypeNativeDataRef nativeData) {
    ROXNativeData *internalNativeData = (__bridge ROXNativeData *)nativeData;
    return ROXStringToChar(internalNativeData.mediaUrl);
}
