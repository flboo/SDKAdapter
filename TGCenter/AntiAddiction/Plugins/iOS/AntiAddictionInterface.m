//
//  AntiAddictionInterface.m
//  AntiAddiction
//
//  Created by TGCenter on 2021/4/7.
//  Copyright © 2021 tgcenter. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <AntiAddiction/AntiAddiction.h>

// 注意：不要打包到 Framework 中

#pragma mark - Common
// Remove an object from cache.
void AntiAddictionRelease(AntiAddictionTypeRef ref) {
    if (ref) {
        AntiAddictionObjectCache *cache = [AntiAddictionObjectCache sharedInstance];
        [cache.references removeObjectForKey:[(__bridge NSObject*)ref antiAddiction_referenceKey]];
    }
}

static NSString *AntiAddictionStringFromUTF8String(const char *bytes) { return bytes ? @(bytes) : nil; }

/// Returns a C string from a C array of UTF8-encoded bytes.
static const char *AntiAddictionCStringCopy(const char *string) {
    if (!string) {
        return NULL;
    }
    char *res = (char *)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

const char *AntiAddictionStringToChar(NSString *string) {
    if (string == nil) {
        string = @"";
    }
    return AntiAddictionCStringCopy(string.UTF8String);
}
#pragma mark - Common

#pragma mark - AntiAddictionSdk
void AntiAddictionSetAntiAddictionClient(AntiAddictionTypeAntiAddictionClientRef *client) {
    [AntiAddictionUnity.sharedInstance setAntiAddictionClient:client];
}

AntiAddictionTypeUserRef AntiAddictionGetUser() {
    AntiAddictionUser *user = [AntiAddictionSdk getUser];
    return [AntiAddictionUnity AntiAddictionCreateUser:user];
}

void AntiAddictionRealName(AntiAddictionRealNameCallback callback) {
    [AntiAddictionUnity.sharedInstance realName:callback];
}

void AntiAddictionRealNameUseCustomUI(const char *name, const char *idNumber, AntiAddictionRealNameCallback callback) {
    [AntiAddictionUnity.sharedInstance realName:AntiAddictionStringFromUTF8String(name)
                                       idNumber:AntiAddictionStringFromUTF8String(idNumber)
                                       callback:callback];
}

void AntiAddictionSetAutoShowTimeLimitPage(BOOL show) {
    [AntiAddictionSdk setAutoShowTimeLimitPage:show];
}

void AntiAddictionSetTimeLimitCallback(AntiAddictionTimeLimitCallback callback) {
    [AntiAddictionUnity.sharedInstance setTimeLimitCallback:callback];
}

AntiAddictionTypeTimeLimitRef AntiAddictionCheckTimeLimitStatus() {
    return nil;
}

void AntiAddictionLogout() {
    [AntiAddictionSdk logOut];
}

#pragma mark - AntiAddictionSdk


#pragma mark - User
AntiAddictionTypeRealNameResultRef AntiAddictionUserGetRealNameResult(AntiAddictionTypeUserRef user) {
    AntiAddictionUser *internalUser = (__bridge AntiAddictionUser *)user;
    
    AntiAddictionRealNameResult *result = internalUser.realNameResult;
    if (result == nil) {
        result = [[AntiAddictionRealNameResult alloc] init];
    }
    AntiAddictionObjectCache *cache = [AntiAddictionObjectCache sharedInstance];
    [cache.references setObject:result forKey:[result antiAddiction_referenceKey]];
    return (__bridge AntiAddictionTypeRealNameResultRef)result;
}

int AntiAddictionUserGetAge(AntiAddictionTypeUserRef user) {
    AntiAddictionUser *internalUser = (__bridge AntiAddictionUser *)user;
    return [internalUser getAge];
}

BOOL AntiAddictionUserIsTourist(AntiAddictionTypeUserRef user) {
    AntiAddictionUser *internalUser = (__bridge AntiAddictionUser *)user;
    BOOL isTourist = [internalUser isTourist];
    NSLog(@"User isTourist: %d", isTourist);
    return isTourist;
}

BOOL AntiAddictionUserIsChild(AntiAddictionTypeUserRef user) {
    AntiAddictionUser *internalUser = (__bridge AntiAddictionUser *)user;
    BOOL isChild = [internalUser isChild];
    NSLog(@"User isChild: %d", isChild);
    return isChild;
}

BOOL AntiAddictionUserIsAdult(AntiAddictionTypeUserRef user) {
    AntiAddictionUser *internalUser = (__bridge AntiAddictionUser *)user;
    BOOL isAdult = [internalUser isAdult];
    NSLog(@"User isAdult: %d", isAdult);
    return isAdult;
}
#pragma mark - User


#pragma mark - RealNameResult
BOOL AntiAddictionRealNameResultIsInitial(AntiAddictionTypeRealNameResultRef result) {
    AntiAddictionRealNameResult *internalResult = (__bridge AntiAddictionRealNameResult *)result;
    BOOL isInitial = [internalResult isInitial];
    NSLog(@"RealNameResult isInitial: %d", isInitial);
    return isInitial;
}

BOOL AntiAddictionRealNameResultIsSuccess(AntiAddictionTypeRealNameResultRef result) {
    AntiAddictionRealNameResult *internalResult = (__bridge AntiAddictionRealNameResult *)result;
    BOOL isSuccess = [internalResult isSuccess];
    NSLog(@"RealNameResult isSuccess: %d", isSuccess);
    return isSuccess;
}

BOOL AntiAddictionRealNameResultIsProcessing(AntiAddictionTypeRealNameResultRef result) {
    AntiAddictionRealNameResult *internalResult = (__bridge AntiAddictionRealNameResult *)result;
    BOOL isProcessing = [internalResult isProcessing];
    NSLog(@"RealNameResult isProcessing: %d", isProcessing);
    return isProcessing;
}

BOOL AntiAddictionRealNameResultIsFail(AntiAddictionTypeRealNameResultRef result) {
    AntiAddictionRealNameResult *internalResult = (__bridge AntiAddictionRealNameResult *)result;
    BOOL isFail = [internalResult isFail];
    NSLog(@"RealNameResult isFail: %d", isFail);
    return isFail;
}

int AntiAddictionRealNameResultGetResultCode(AntiAddictionTypeRealNameResultRef result) {
    AntiAddictionRealNameResult *internalResult = (__bridge AntiAddictionRealNameResult *)result;
    return [internalResult getResultCode];
}

const char *AntiAddictionRealNameResultGetResultMsg(AntiAddictionTypeRealNameResultRef result) {
    AntiAddictionRealNameResult *internalResult = (__bridge AntiAddictionRealNameResult *)result;
    return AntiAddictionStringToChar([internalResult getResultMsg]);
}

const char *AntiAddictionRealNameResultGetPlayerId(AntiAddictionTypeRealNameResultRef result) {
    AntiAddictionRealNameResult *internalResult = (__bridge AntiAddictionRealNameResult *)result;
    return AntiAddictionStringToChar([internalResult getPlayerId]);
}
#pragma mark - RealNameResult


#pragma mark - TimeLimit
int AntiAddictionTimeLimitGetReason(AntiAddictionTypeTimeLimitRef timeLimit) {
    AntiAddictionTimeLimit *internalTimeLimit = (__bridge AntiAddictionTimeLimit *)timeLimit;
    return internalTimeLimit.reason;
}

BOOL AntiAddictionTimeLimitIsLimit(AntiAddictionTypeTimeLimitRef timeLimit) {
    AntiAddictionTimeLimit *internalTimeLimit = (__bridge AntiAddictionTimeLimit *)timeLimit;
    BOOL isLimit = internalTimeLimit.isLimit;
    NSLog(@"TimeLimit isLimit: %d", isLimit);
    return isLimit;
}

long AntiAddictionTimeLimitGetTimeToLimit(AntiAddictionTypeTimeLimitRef timeLimit) {
    AntiAddictionTimeLimit *internalTimeLimit = (__bridge AntiAddictionTimeLimit *)timeLimit;
    return internalTimeLimit.timeToLimit;
}

AntiAddictionTypeLimitTipRef AntiAddictionTimeLimitGetLimitTip(AntiAddictionTypeTimeLimitRef timeLimit) {
    AntiAddictionTimeLimit *internalTimeLimit = (__bridge AntiAddictionTimeLimit *)timeLimit;
    AntiAddictionLimitTip *limitTip = internalTimeLimit.limitTip;
    if (limitTip == nil) {
        limitTip = [[AntiAddictionLimitTip alloc] init];
    }
    AntiAddictionObjectCache *cache = [AntiAddictionObjectCache sharedInstance];
    [cache.references setObject:limitTip forKey:[limitTip antiAddiction_referenceKey]];
    return (__bridge AntiAddictionTypeLimitTipRef)limitTip;
}
#pragma mark - TimeLimit


#pragma mark - LimitTip
const char *AntiAddictionLimitTipGetTitle(AntiAddictionTypeLimitTipRef limitTip) {
    AntiAddictionLimitTip *internalLimitTip = (__bridge AntiAddictionLimitTip *)limitTip;
    return AntiAddictionStringToChar(internalLimitTip.title);
}

const char *AntiAddictionLimitTipGetDesc(AntiAddictionTypeLimitTipRef limitTip) {
    AntiAddictionLimitTip *internalLimitTip = (__bridge AntiAddictionLimitTip *)limitTip;
    return AntiAddictionStringToChar(internalLimitTip.desc);
}

const char *AntiAddictionLimitTipGetButton(AntiAddictionTypeLimitTipRef limitTip) {
    AntiAddictionLimitTip *internalLimitTip = (__bridge AntiAddictionLimitTip *)limitTip;
    return AntiAddictionStringToChar(internalLimitTip.button);
}

BOOL AntiAddictionLimitTipCanRealName(AntiAddictionTypeLimitTipRef limitTip) {
    AntiAddictionLimitTip *internalLimitTip = (__bridge AntiAddictionLimitTip *)limitTip;
    BOOL canRealName = internalLimitTip.canRealName;
    NSLog(@"LimitTip canRealName: %d", canRealName);
    return canRealName;
}
#pragma mark - LimitTip


#pragma mark - AgeTip
AntiAddictionTypeAgeTipRef AntiAddictionGetAgeTip() {
    AntiAddictionAgeTipObject *ageTipObject = [AntiAddictionSdk getAgeTipContent];
    if (ageTipObject == nil) {
        ageTipObject = [[AntiAddictionAgeTipObject alloc] init];
    }
    
    AntiAddictionObjectCache *cache = [AntiAddictionObjectCache sharedInstance];
    [cache.references setObject:ageTipObject forKey:[ageTipObject antiAddiction_referenceKey]];
    return (__bridge AntiAddictionTypeAgeTipRef)ageTipObject;
}

const char *AntiAddictionAgeTipGetIconByteArrayString(AntiAddictionTypeAgeTipRef ageTip) {
    AntiAddictionAgeTipObject *internalAgeTip = (__bridge AntiAddictionAgeTipObject *)ageTip;
    NSData *icon = internalAgeTip.iconData;
    NSString *base64String = icon != nil ? [icon base64EncodedStringWithOptions:0] : @"";
    return AntiAddictionStringToChar(base64String);
}

const char *AntiAddictionAgeTipGetText(AntiAddictionTypeAgeTipRef ageTip) {
    AntiAddictionAgeTipObject *internalAgeTip = (__bridge AntiAddictionAgeTipObject *)ageTip;
    return AntiAddictionStringToChar(internalAgeTip.ageTipContent);
}

void AntiAddictionShowAgeTip(AntiAddictionAgeTipShowCallback showCallback,
                             AntiAddictionAgeTipCloseCallback closeCallback) {
    [AntiAddictionUnity.sharedInstance showAgeTip:showCallback closeCallback:closeCallback];
}
#pragma mark - AgeTip


#pragma mark - HealthGameTip
AntiAddictionTypeHealthGameTipRef AntiAddictionGetHealthGameTip() {
    AntiAddictionAgeTipObject *ageTipObject = [AntiAddictionSdk getAgeTipContent];
    if (ageTipObject == nil) {
        ageTipObject = [[AntiAddictionAgeTipObject alloc] init];
    }
    
    AntiAddictionObjectCache *cache = [AntiAddictionObjectCache sharedInstance];
    [cache.references setObject:ageTipObject forKey:[ageTipObject antiAddiction_referenceKey]];
    return (__bridge AntiAddictionTypeHealthGameTipRef)ageTipObject;
}

const char *AntiAddictionHealthGameTipGetTipText(AntiAddictionTypeHealthGameTipRef healthGameTip) {
    AntiAddictionAgeTipObject *internalAgeTip = (__bridge AntiAddictionAgeTipObject *)healthGameTip;
    return AntiAddictionStringToChar(internalAgeTip.healthGameAdviceContent);
}

const char *AntiAddictionHealthGameTipGetAppInfoText(AntiAddictionTypeHealthGameTipRef healthGameTip) {
    AntiAddictionAgeTipObject *internalAgeTip = (__bridge AntiAddictionAgeTipObject *)healthGameTip;
    return AntiAddictionStringToChar(internalAgeTip.healthGameAdviceAppInfo);
}

void AntiAddictionShowHealthGameTip(AntiAddictionHealthGameTipShowCallback showCallback,
                                    AntiAddictionHealthGameTipShowFailedCallback showFailedCallback,
                                    AntiAddictionHealthGameTipCloseCallback closeCallback) {
    [AntiAddictionUnity.sharedInstance showHealthGameTip:showCallback
                                      showFailedCallback:showFailedCallback
                                           closeCallback:closeCallback];
}
#pragma mark - HealthGameTip
