#import "JPushUnityManager.h"
#import "JPUSHService.h"
#import "JPushEventCache.h"

#pragma mark - Utility Function

#if defined(__cplusplus)
extern "C" {
#endif
    extern void       UnitySendMessage(const char* obj, const char* method, const char* msg);
    extern NSString*  CreateNSString (const char* string);
    extern id         APNativeJSONObject(NSData *data);
    extern NSData *   APNativeJSONData(id obj);
#if defined(__cplusplus)
}
#endif

static NSString *gameObjectName = @"";

@interface JPushUnityInstnce : NSObject {
@private
}
+(JPushUnityInstnce*)sharedInstance;
@end


#if defined(__cplusplus)
extern "C" {
#endif
    const char *tagCallbackName_ = "OnJPushTagOperateResult";
    const char *aliasCallbackName_ = "OnJPushAliasOperateResult";
    
    static char *MakeHeapString(const char *string) {
        if (!string){
            return NULL;
        }
        char *mem = static_cast<char*>(malloc(strlen(string) + 1));
        if (mem) {
            strcpy(mem, string);
        }
        return mem;
    }
    
    NSString *CreateNSString (const char *string) {
        return [NSString stringWithUTF8String:(string ? string : "")];
    }
    
    id APNativeJSONObject(NSData *data) {
        if (!data) {
            return nil;
        }
        
        NSError *error = nil;
        id retId = [NSJSONSerialization JSONObjectWithData:data options:0 error:&error];
        
        if (error) {
            NSLog(@"%s trans data to obj with error: %@", __func__, error);
            return nil;
        }
        
        return retId;
    }
    
    NSData *APNativeJSONData(id obj) {
        NSError *error = nil;
        NSData *data = [NSJSONSerialization dataWithJSONObject:obj options:0 error:&error];
        if (error) {
            NSLog(@"%s trans obj to data with error: %@", __func__, error);
            return nil;
        }
        return data;
    }
    
    NSString *messageAsDictionary(NSDictionary * dic) {
        NSData *data = APNativeJSONData(dic);
        return [[NSString alloc]initWithData:data encoding:NSUTF8StringEncoding];
    }
    
    JPUSHTagsOperationCompletion tagsOperationCompletion = ^(NSInteger iResCode, NSSet *iTags, NSInteger seq) {
        NSMutableDictionary *dic = [[NSMutableDictionary alloc] init];
        [dic setObject:[NSNumber numberWithInteger:seq] forKey:@"sequence"];
        [dic setValue:[NSNumber numberWithUnsignedInteger:iResCode] forKey:@"code"];
        
        if (iResCode == 0) {
            dic[@"tags"] = [iTags allObjects];
        }
        
        UnitySendMessage([gameObjectName UTF8String], tagCallbackName_, messageAsDictionary(dic).UTF8String);
        
    };
    
    JPUSHAliasOperationCompletion aliasOperationCompletion = ^(NSInteger iResCode, NSString *iAlias, NSInteger seq) {
        NSMutableDictionary* dic = [[NSMutableDictionary alloc] init];
        [dic setObject:[NSNumber numberWithInteger:seq] forKey:@"sequence"];
        [dic setValue:[NSNumber numberWithUnsignedInteger:iResCode] forKey:@"code"];
        
        if (iResCode == 0) {
            [dic setObject:iAlias forKey:@"alias"];
        }
        
        UnitySendMessage([gameObjectName UTF8String], aliasCallbackName_, messageAsDictionary(dic).UTF8String);
    };
    
    NSInteger integerValue(int intValue) {
        NSNumber *n = [NSNumber numberWithInt:intValue];
        return [n integerValue];
    }
    
    int intValue(NSInteger integerValue) {
        NSNumber *n = [NSNumber numberWithInteger:integerValue];
        return [n intValue];
    }
    // private - end
    
    void _initJpush(char *gameObject) {
        gameObjectName = [NSString stringWithUTF8String:gameObject];
        NSNotificationCenter *msgCenter = [NSNotificationCenter defaultCenter];
        [[NSNotificationCenter defaultCenter] addObserver:[JPushUnityInstnce sharedInstance]
                                                 selector:@selector(networkDidRecieveMessage:)
                                                     name:kJPFNetworkDidReceiveMessageNotification
                                                   object:nil];
        
        [[NSNotificationCenter defaultCenter] addObserver:[JPushUnityInstnce sharedInstance]
                                                 selector:@selector(networkDidRecievePushNotification:)
                                                     name:@"JPushPluginReceiveNotification"
                                                   object:nil];
        
        [[NSNotificationCenter defaultCenter] addObserver:[JPushUnityInstnce sharedInstance]
                                                 selector:@selector(networkOpenPushNotification:)
                                                     name:@"JPushPluginOpenNotification"
                                                   object:nil];
        [[JPushEventCache sharedInstance] scheduleNotificationQueue];
    }
    
    void _setDebugJpush(bool enable) {
        if (enable) {
            [JPUSHService setDebugMode];
        } else {
            [JPUSHService setLogOFF];
        }
    }
    
    const char *_getRegistrationIdJpush() {
        NSString *registrationID = [JPUSHService registrationID];
        return MakeHeapString([registrationID UTF8String]);
    }
    
    // Tag & Alias - start
    
    void _setTagsJpush(int sequence, const char *tags) {
        NSString *nsTags = CreateNSString(tags);
        if (![nsTags length]) {
            return;
        }
        
        NSData *data = [nsTags dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *dict = APNativeJSONObject(data);
        NSArray *array = dict[@"Items"];
        NSSet *set = [[NSSet alloc] initWithArray:array];
        
        [JPUSHService setTags:set completion:tagsOperationCompletion seq:(NSInteger)sequence];
    }
    
    void _addTagsJpush(int sequence, char *tags) {
        NSString* tagsJsonStr = CreateNSString(tags);
        
        NSData *data = [tagsJsonStr dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *dict = APNativeJSONObject(data);
        NSArray *tagArr = dict[@"Items"];
        NSSet *tagSet = [[NSSet alloc] initWithArray:tagArr];
        
        [JPUSHService addTags:tagSet completion:tagsOperationCompletion seq:(NSInteger)sequence];
    }
    
    void _deleteTagsJpush(int sequence, char *tags) {
        NSString *tagsJsonStr = CreateNSString(tags);
        
        NSData *data = [tagsJsonStr dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *dict = APNativeJSONObject(data);
        NSArray *tagArr = dict[@"Items"];
        NSSet *tagSet = [[NSSet alloc] initWithArray:tagArr];
        
        [JPUSHService deleteTags:tagSet completion:tagsOperationCompletion seq:(NSInteger)sequence];
    }
    
    void _cleanTagsJpush(int sequence) {
        [JPUSHService cleanTags:tagsOperationCompletion seq:(NSInteger)sequence];
    }
    
    void _getAllTagsJpush(int sequence) {
        [JPUSHService getAllTags:tagsOperationCompletion seq:(NSInteger)sequence];
    }
    
    void _checkTagBindStateJpush(int sequence, char *tag) {
        NSString *nsTag = CreateNSString(tag);
        [JPUSHService validTag:nsTag completion:^(NSInteger iResCode, NSSet *iTags, NSInteger seq, BOOL isBind) {
            NSMutableDictionary *dic = [[NSMutableDictionary alloc] init];
            [dic setObject:[NSNumber numberWithInteger:seq] forKey:@"sequence"];
            [dic setValue:[NSNumber numberWithUnsignedInteger:iResCode] forKey:@"code"];
            
            if (iResCode == 0) {
                [dic setObject:[iTags allObjects] forKey:@"tags"];
                [dic setObject:[NSNumber numberWithBool:isBind] forKey:@"isBind"];
            }
            
            UnitySendMessage([gameObjectName UTF8String], tagCallbackName_, messageAsDictionary(dic).UTF8String);
        } seq:(NSInteger)sequence];
    }
    
    const char * _filterValidTagsJpush(char *tags){
        
        NSString *nsTags = CreateNSString(tags);
        if (![nsTags length]) {
            return nil;
        }
        
        NSData *data = [nsTags dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *dict = APNativeJSONObject(data);
        NSArray *array = dict[@"Items"];
        NSSet *set = [[NSSet alloc] initWithArray:array];
        
        NSSet *rSet =  [JPUSHService filterValidTags:set];
        
        NSMutableDictionary *dic = [[NSMutableDictionary alloc] init];
        
        if ([rSet count]) {
            dic[@"Items"] = [rSet allObjects];
        } else {
            return  nil;
        }
        
        return MakeHeapString([messageAsDictionary(dic) UTF8String]);
        
    }
    
    void _setAliasJpush(int sequence, const char * alias){
        NSString *nsAlias = CreateNSString(alias);
        if (![nsAlias length]) {
            return ;
        }
        
        [JPUSHService setAlias:nsAlias completion:aliasOperationCompletion seq:(NSInteger)sequence];
    }
    
    void _getAliasJpush(int sequence) {
        [JPUSHService getAlias:aliasOperationCompletion seq:(NSInteger)sequence];
    }
    
    void _deleteAliasJpush(int sequence) {
        [JPUSHService deleteAlias:aliasOperationCompletion seq:(NSInteger)sequence];
    }
    
    // Tag & Alias - end
    
    // ???????????? - start
    
    void _setBadgeJpush(const int badge){
        [JPUSHService setBadge:integerValue(badge)];
    }
    
    void _resetBadgeJpush(){
        [JPUSHService resetBadge];
    }
    
    void _setApplicationIconBadgeNumberJpush(const int badge){
        [UIApplication sharedApplication].applicationIconBadgeNumber = integerValue(badge);
    }
    
    int _getApplicationIconBadgeNumberJpush(){
        return intValue([UIApplication sharedApplication].applicationIconBadgeNumber);
    }
    
    // ???????????? - end
    
    // ???????????? - start
    
    void _startLogPageViewJpush(const char *pageName) {
        NSString *nsPageName = CreateNSString(pageName);
        if (![nsPageName length]) {
            return;
        }
        
        NSData *data =[nsPageName dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *dict = APNativeJSONObject(data);
        NSString *sendPageName = dict[@"pageName"];
        [JPUSHService startLogPageView:sendPageName];
    }
    
    void _stopLogPageViewJpush(const char *pageName) {
        NSString *nsPageName = CreateNSString(pageName);
        if (![nsPageName length]) {
            return;
        }
        
        NSData *data =[nsPageName dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *dict = APNativeJSONObject(data);
        NSString *sendPageName = dict[@"pageName"];
        [JPUSHService stopLogPageView:sendPageName];
    }
    
    void _beginLogPageViewJpush(const char *pageName, const int duration) {
        NSString *nsPageName = CreateNSString(pageName);
        if (![nsPageName length]) {
            return;
        }
        
        NSData *data =[nsPageName dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *dict = APNativeJSONObject(data);
        NSString *sendPageName = dict[@"pageName"];
        [JPUSHService beginLogPageView:sendPageName duration:duration];
    }
    
    // ???????????? - end
    
    // ????????????????????? - start
    
    void _setLocalNotificationJpush(int delay, int badge, char *alertBodyAndIdKey){
        NSDate *date = [NSDate dateWithTimeIntervalSinceNow:integerValue(delay)];
        
        NSString *nsalertBodyAndIdKey = CreateNSString(alertBodyAndIdKey);
        if (![nsalertBodyAndIdKey length]) {
            return ;
        }
        NSData       *data =[nsalertBodyAndIdKey dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *dict = APNativeJSONObject(data);
        NSString     *sendAlertBody = dict[@"alertBody"];
        NSString     *sendIdkey = dict[@"idKey"];
        
        [JPUSHService setLocalNotification:date alertBody:sendAlertBody badge:badge alertAction:nil identifierKey:sendIdkey userInfo:nil soundName:nil];
    }
    
    void _sendLocalNotificationJpush(char *params) {
        NSString *nsalertBodyAndIdKey = CreateNSString(params);
        if (![nsalertBodyAndIdKey length]) {
            return ;
        }
        NSData       *data =[nsalertBodyAndIdKey dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *dict = APNativeJSONObject(data);
        
        JPushNotificationContent *content = [[JPushNotificationContent alloc] init];
        if (dict[@"title"]) {
            content.title = dict[@"title"];
        }
        
        if (dict[@"subtitle"]) {
            content.subtitle = dict[@"subtitle"];
        }
        
        if (dict[@"content"]) {
            content.body = dict[@"content"];
        }
        
        if (dict[@"badge"]) {
            content.badge = dict[@"badge"];
        }
        
        if (dict[@"action"]) {
            content.action = dict[@"action"];
        }
        
        if (dict[@"extra"]) {
            content.userInfo = dict[@"extra"];
        }
        
        if (dict[@"sound"]) {
            content.sound = dict[@"sound"];
        }
        
        JPushNotificationTrigger *trigger = [[JPushNotificationTrigger alloc] init];
        if ([[[UIDevice currentDevice] systemVersion] floatValue] >= 10.0) {
            if (dict[@"fireTime"]) {
                NSNumber *date = dict[@"fireTime"];
                NSTimeInterval currentInterval = [[NSDate date] timeIntervalSince1970];
                NSTimeInterval interval = [date doubleValue] - currentInterval;
                interval = interval>0?interval:0;
                trigger.timeInterval = interval;
                
            }
        }
        
        else {
            if (dict[@"fireTime"]) {
                NSNumber *date = dict[@"fireTime"];
                trigger.fireDate = [NSDate dateWithTimeIntervalSince1970: [date doubleValue]];
            }
        }
        JPushNotificationRequest *request = [[JPushNotificationRequest alloc] init];
        request.content = content;
        request.trigger = trigger;
        
        if (dict[@"id"]) {
            if ([dict[@"id"] isKindOfClass:[NSString class]])
            {
                request.requestIdentifier = dict[@"id"];
                
            }else{
                NSNumber *identify = dict[@"id"];
                request.requestIdentifier = [identify stringValue];
            }
        }
        request.completionHandler = ^(id result) {
            NSLog(@"result");
        };
        
        [JPUSHService addNotification:request];
    }
    
    void _deleteLocalNotificationWithIdentifierKeyJpush(char *idKey){
        NSString *nsIdKey = CreateNSString(idKey);
        if (![nsIdKey length]) {
            return ;
        }
        NSData       *data =[nsIdKey dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *dict = APNativeJSONObject(data);
        NSString     *sendIdkey = dict[@"idKey"];
        
        [JPUSHService deleteLocalNotificationWithIdentifierKey:sendIdkey];
    }
    
    void _clearAllLocalNotificationsJpush(){
        [JPUSHService clearAllLocalNotifications];
    }
    
    
    
    
    void _removeNotificationJpush(char *idKey,bool delivered){
        JPushNotificationIdentifier *identifier = [[JPushNotificationIdentifier alloc] init];
        NSString *nsIdKey = CreateNSString(idKey);
        if (![nsIdKey length]) {
            NSLog(@"![nsIdKey length]");
            identifier.identifiers = nil;
        } else {
            NSData *data = [nsIdKey dataUsingEncoding:NSUTF8StringEncoding];
            NSDictionary *dict = APNativeJSONObject(data);
            NSArray *idKeyArr = dict[@"Items"];
            identifier.identifiers = idKeyArr;
        }
        identifier.delivered = delivered;
        [JPUSHService removeNotification:identifier];
    }
    
    void _removeNotificationAllJpush(){
        [JPUSHService removeNotification:nil];
    }
    
    void _findNotification(char *idKey,bool delivered){
        JPushNotificationIdentifier *identifier = [[JPushNotificationIdentifier alloc] init];
        NSString *nsIdKey = CreateNSString(idKey);
        if (![nsIdKey length]) {
            NSLog(@"![nsIdKey length]");
            identifier.identifiers = nil;
        } else {
            NSData *data = [nsIdKey dataUsingEncoding:NSUTF8StringEncoding];
            NSDictionary *dict = APNativeJSONObject(data);
            NSArray *idKeyArr = dict[@"Items"];
            identifier.identifiers = idKeyArr;
        }
        identifier.delivered = delivered;
        
        identifier.findCompletionHandler = ^(NSArray *results) {
            //results iOS10????????????UILocalNotification????????????
            //iOS10?????? ??????delivered???????????????UNNotification???UNNotificationRequest????????????
            NSLog(@"?????????????????? - ??????????????????%@",results);
            //            UnitySendMessage([gameObjectName UTF8String], "OnMobileNumberOperatorResult", messageAsDictionary(dic).UTF8String);
        };
        
        [JPUSHService findNotification:identifier];
    }
    
    
    // ????????????????????? - end
    
    //???????????? - start
    
    /**
     //    ????????? API ?????????????????????????????????????????????????????????10
     //    ????????????
     //    count
     //    ???????????????NSInteger ??????
     //    ????????????10
     //    iOS??????????????????????????????20????????????????????????
     */
    void _setGeofenecMaxCountJpush(const int count){
        [JPUSHService setGeofenecMaxCount:integerValue(count)];
    }
    
    //
    void _removeGeofenceWithIdentifierJpush(char *geofenceId){
        NSString *nsGeofenceId = CreateNSString(geofenceId);
        [JPUSHService removeGeofenceWithIdentifier:nsGeofenceId];
    }
    
    
    //???????????? - end
    
    //other - start
    /**
     ????????????
     API ????????????????????????????????????
     ????????????
     ?????????????????? Log ???????????????????????????????????????????????????????????????????????????????????????????????????
     */
    void _crashLogONJpush(){
        [JPUSHService crashLogON];
    }
    
    /**
     ????????????
     
     ?????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????
     ????????????
     mobileNumber ???????????????????????? ???+??? ???????????????????????????????????????????????? ???-??? ???????????????????????????????????? 20???????????? nil ???????????????????????????????????????
     completion ???????????????????????? error ?????????????????? error ?????????????????????????????????????????????????????????????????????
     ????????????
     ?????????????????????????????????10s ???????????? 3 ??????????????????????????????????????????????????????????????????????????? completion ????????????????????????completion ????????? nil ????????????????????????
     */
    void _setMobileNumberJpush(int sequence,char *mobileNumber){
        NSString *nsMobileNumber = CreateNSString(mobileNumber);
        if (![nsMobileNumber length]) {
            return;
        }
        [JPUSHService setMobileNumber:nsMobileNumber completion:^(NSError *error) {
            NSMutableDictionary *dic = [[NSMutableDictionary alloc] init];
            [dic setValue:[NSNumber numberWithUnsignedInteger:sequence] forKey:@"sequence"];
            if (!error) {
                [dic setValue:[NSNumber numberWithUnsignedInteger:0] forKey:@"code"];
                UnitySendMessage([gameObjectName UTF8String], "OnMobileNumberOperatorResult", messageAsDictionary(dic).UTF8String);
            }else{
                [dic setValue:[NSNumber numberWithUnsignedInteger:[error code]] forKey:@"code"];
            }
            UnitySendMessage([gameObjectName UTF8String], "OnMobileNumberOperatorResult", messageAsDictionary(dic).UTF8String);
        }];
    }
    
    
    void _setLatitudeJpush(double latitude, double longitude){
        [JPUSHService setLatitude:latitude longitude:longitude];
    }
    
    //other - end
    
#if defined(__cplusplus)
}
#endif

#pragma mark - Unity interface

@implementation JPushUnityManager : NSObject
@end

#pragma mark - Unity instance

@implementation JPushUnityInstnce

static JPushUnityInstnce * _sharedService = nil;

+ (JPushUnityInstnce*)sharedInstance {
    static dispatch_once_t onceAPService;
    dispatch_once(&onceAPService, ^{
        _sharedService = [[JPushUnityInstnce alloc] init];
    });
    return _sharedService;
}

- (void)networkDidRecieveMessage:(NSNotification *)notification {
    if (notification.name == kJPFNetworkDidReceiveMessageNotification && notification.userInfo){
        NSData *data = APNativeJSONData(notification.userInfo);
        NSString *jsonStr = [[NSString alloc]initWithData:data encoding:NSUTF8StringEncoding];
        UnitySendMessage([gameObjectName UTF8String], "OnReceiveMessage", jsonStr.UTF8String);
    }
}

- (void)networkDidRecievePushNotification:(NSNotification *)notification {
    if ([notification.name isEqual:@"JPushPluginReceiveNotification"] && notification.object){
        NSData *data = APNativeJSONData(notification.object);
        NSString *jsonStr = [[NSString alloc]initWithData:data encoding:NSUTF8StringEncoding];
        UnitySendMessage([gameObjectName UTF8String], "OnReceiveNotification", jsonStr.UTF8String);
    }
}

- (void)networkOpenPushNotification:(NSNotification *)notification {
    if ([notification.name isEqual:@"JPushPluginOpenNotification"] && notification.object){
        NSData *data = APNativeJSONData(notification.object);
        NSString *jsonStr = [[NSString alloc]initWithData:data encoding:NSUTF8StringEncoding];
        UnitySendMessage([gameObjectName UTF8String], "OnOpenNotification", jsonStr.UTF8String);
    }
}
@end
