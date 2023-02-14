//
//  TGCPrivacyInterface.m
//
//  用于 Unity DllImport 的 Object-C 接口。
//  Unity 中都是通过这里面的接口来创建、加载、展示广告的。

#import <Foundation/Foundation.h>
#import <TGCPrivacy/TGCPrivacy.h>
#import <privacypolicy/privacypolicy.h>

// 注意：不要打包到 Framework 中
static NSString *TGCPrivacyStringFromUTF8String(const char *bytes) { return bytes ? @(bytes) : nil; }

void TGCPrivacyInit(const char *appId) {
    [PrivacyPolicyManager init:TGCPrivacyStringFromUTF8String(appId)];
}

void TGCPrivacyShowDialog(TGCTypePrivacyPolicyHelperClientRef clientRef,
                          TGCPrivacyAgreeCallback agreeCallback,
                          TGCPrivacyDisagreeCallback disagreeCallback) {
    TGCPrivacyHelper.sharedInstance.privacyPolicyHelperClientRef = clientRef;
    [TGCPrivacyHelper.sharedInstance showPrivacyDialogWithAgreeCallback:agreeCallback
                                                       disagreeCallback:disagreeCallback];
}

void TGCPrivacyJumpToUserAgreement() {
    [TGCPrivacyHelper.sharedInstance jumpToUserAgreement];
}

void TGCPrivacyJumpToPrivacyPolicy() {
    [TGCPrivacyHelper.sharedInstance jumpToPrivacyPolicy];
}
