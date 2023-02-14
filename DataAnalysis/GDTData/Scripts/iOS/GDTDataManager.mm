#import <Foundation/Foundation.h>
#import "GDTAction.h"
#import "GDTDataDetector.h"

#if defined (__cplusplus)
extern "C" {
#endif

void GDT_UnionPlatform_ActionInit(const char *actionSetId, const char *secretKey) {
    if (actionSetId == NULL || actionSetId == NULL) {
        return;
    }
    NSString *actionId = [[NSString alloc] initWithUTF8String:actionSetId];
    NSString *secret = [[NSString alloc] initWithUTF8String:secretKey];
    [GDTAction init:actionId secretKey:secret];
}

void GDT_UnionPlatform_ActionLog(const char *eventName) {
    if (eventName == NULL) {
        return;
    }
    NSString *name = [[NSString alloc] initWithUTF8String:eventName];
    [GDTAction logAction:name actionParam:nil];
}




void GDT_UnionPlatform_DataDetectorReport(const char *eventName, void *paras) {
    if (eventName == NULL) {
        return;
    }
    NSString *name = [[NSString alloc] initWithUTF8String:eventName];
    NSDictionary *dict = (__bridge NSMutableDictionary *)paras;
    [GDTDataDetector sendEventWithName:name extParams:dict];
    (__bridge_transfer NSMutableDictionary *)paras;
}

void *GDT_UnionPlatform_DataDetectorReportDictionary() {
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    void *d = (__bridge_retained void *)dict;
    return d;
}

void GDT_UnionPlatform_DataDetectorReportAddParams(void *paras, const char *key, const char *value) {
    if (key == NULL || value == NULL || paras == NULL) {
        return;
    }
    NSString *k = [[NSString alloc] initWithUTF8String:key];
    NSString *v = [[NSString alloc] initWithUTF8String:value];
    NSMutableDictionary *dict = (__bridge NSMutableDictionary *)paras;
    [dict setObject:v forKey:k];
}


#if defined (__cplusplus)
}
#endif
