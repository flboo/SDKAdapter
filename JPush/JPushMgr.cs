using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JPush;
using System;

#if UNITY_IPHONE
using LitJson;
#endif

namespace Qarth
{
    public enum JpushEventID
    {
        OnReceiveMessage = 888888888,
        OnReceiveNotification,
        OnOpenNotification,
        OnJPushTagOperateResultGet,
        OnJPushAliasOperateResultGet,
        OnMobileNumberOperatorResultGet,
    }

    public class JPushMgr : TMonoSingleton<JPushMgr>
    {
        public override void OnSingletonInit()
        {
            Log.i("JPushMgr OnSingletonInit");
        }


        public void Init()
        {
            if (SDKConfig.S.jpushConfig.isEnable)
            {
                Log.i("JPushMgr init");
#if !UNITY_EDITOR
                gameObject.AddComponent<JPushBinding>();
                SetDebugMode(SDKConfig.S.jpushConfig.isDebugMode);
                JPushBinding.Init(gameObject.name);
                GetRegistrationId();
                if (SDKConfig.S.jpushConfig.isDebugMode)
                {
                    Log.e("jiguang id:{0}", JPushMgr.S.GetRegistrationId());
                }
#endif
            }
        }


        void OnApplicationPause(bool paused)
        {        //程序进入后台时
            if (!paused)
            {
                Debug.Log("JPushMgr pause back");
            }
        }

        #region public method

        /// <summary>
        /// 获取当前设备的Registration Id
        /// </summary>
        /// <returns></returns>
        public string GetRegistrationId()
        {
            return JPushBinding.GetRegistrationId();
        }

        /// <summary>
        /// 给当前设备设置标签。注意该操作是覆盖逻辑，即每次调用会覆盖之前已经设置的标签。
        /// tags: 标签列表。有效的标签组成：字母（区分大小写）、数字、下划线、汉字、特殊字符（@!#$&*+=.|）。
        /// 限制：每个 tag 命名长度限制为 40 字节，单个设备最多支持设置 1000 个 tag，且单次操作总长度不得超过 5000 字节（判断长度需采用 UTF-8 编码）。
        /// /// </summary>
        /// <param name="sequence">sequence: 作为一次操作的唯一标识，会在 `OnJPushTagOperateResult` 回调中一并返回。</param>
        /// <param name="tags">tags: 标签列表。</param>
        public void SetTags(int sequence, List<string> tags)
        {
            JPushBinding.SetTags(sequence, tags);
        }

        /// <summary>
        /// 给当前设备在已有的基础上新增标签
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="tags"></param>
        public void AddTags(int sequence, List<string> tags)
        {
            JPushBinding.AddTags(sequence, tags);
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="tags"></param>
        public void DeleteTags(int sequence, List<string> tags)
        {
            JPushBinding.DeleteTags(sequence, tags);
        }

        /// <summary>
        /// 清空标签
        /// </summary>
        /// <param name="sequence"></param>
        public void CleanTags(int sequence)
        {
            JPushBinding.CleanTags(sequence);
        }

        /// <summary>
        /// 获取当前设备的所有标签
        /// </summary>
        /// <param name="sequence"></param>
        public void GetAllTags(int sequence)
        {
            JPushBinding.GetAllTags(sequence);
        }

        /// <summary>
        /// 检查制定标签是否绑定
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="tag"></param>
        public void CheckTagBindState(int sequence, string tag)
        {
            JPushBinding.CheckTagBindState(sequence, tag);
        }

        /// <summary>
        /// 设置别名。
        /// <para>注意：这个接口是覆盖逻辑，而不是增量逻辑。即新的调用会覆盖之前的设置。</para>
        /// </summary>
        /// <param name="sequence">用户自定义的操作序列号。同操作结果一起返回，用来标识一次操作的唯一性。</param>
        /// <param name="alias">
        ///     别名。
        ///     <para>有效的别名组成：字母（区分大小写）、数字、下划线、汉字、特殊字符@!#$&*+=.|。</para>
        ///     <para>限制：alias 命名长度限制为 40 字节（判断长度需采用 UTF-8 编码）。</para>
        /// </param>
        public void SetAlias(int sequence, string alias)
        {
            JPushBinding.SetAlias(sequence, alias);
        }

        /// <summary>
        /// 删除别名。
        /// </summary>
        /// <param name="sequence">用户自定义的操作序列号。同操作结果一起返回，用来标识一次操作的唯一性。</param>
        public void DeleteAlias(int sequence)
        {
            JPushBinding.DeleteAlias(sequence);
        }

        /// <summary>
        /// 获取当前设备设置的别名。
        /// </summary>
        /// <param name="sequence">用户自定义的操作序列号。同操作结果一起返回，用来标识一次操作的唯一性。</param>
        public void GetAlias(int sequence)
        {
            JPushBinding.GetAlias(sequence);
        }

        ///接口返回
        ///有效的 tag 集合。
        public List<string> FilterValidTags(List<string> jsonTags)
        {
            return JPushBinding.FilterValidTags(jsonTags);
        }

#if UNITY_ANDROID

        /// <summary>
        /// (Android only)动态配置 channel，优先级比 AndroidManifest 里配置的高 channel 希望配置的 channel，传 null 表示依然使用 AndroidManifest 里配置的 channel
        /// </summary>
        /// <param name="channel"></param>
        public void SetChannel(string channel)
        {
            JPushBinding.SetChannel(channel);
        }

        //用于上报用户的通知栏被打开，或者用于上报用户自定义消息被展示等客户端需要统计的事件。
        //参数说明
        //msgId：推送每一条消息和通知对应的唯一 ID
        public void ReportNotificationOpened(string msgId)
        {
            JPushBinding.ReportNotificationOpened(msgId);
        }

        //调用此 API 设置手机号码。该接口会控制调用频率，频率为 10s 之内最多 3 次。
        //sequence
        //用户自定义的操作序列号，同操作结果一起返回，用来标识一次操作的唯一性。
        //mobileNumber
        //手机号码。如果传 null 或空串则为解除号码绑定操作。
        //限制：只能以 “+” 或者 数字开头；后面的内容只能包含 “-” 和数字。
        public void SetMobileNumberAndroid(int sequence, string mobileNumber)
        {
            JPushBinding.SetMobileNumberAndroid(sequence, mobileNumber);
        }

        //JPush SDK 开启和关闭省电模式，默认为关闭。
        //参数说明
        //S
        //enable 是否需要开启或关闭，true 为开启，false 为关闭
        public void SetPowerSaveMode(bool enable)
        {
            JPushBinding.SetPowerSaveMode(enable);
        }

        /// <summary>
        /// 停止 JPush 推送服务。 
        /// </summary>
        public void StopPush()
        {
            if (!SDKConfig.S.jpushConfig.isEnable)
                return;

            JPushBinding.StopPush();
        }

        /// <summary>
        /// 唤醒 JPush 推送服务，使用了 StopPush 必须调用此方法才能恢复。
        /// </summary>
        public void ResumePush()
        {
            if (!SDKConfig.S.jpushConfig.isEnable)
                return;

            if (IsPushStopped())
                JPushBinding.ResumePush();
        }

        /// <summary>
        /// 判断当前 JPush 服务是否停止。
        /// </summary>
        /// <returns>true: 已停止；false: 未停止。</returns>
        public bool IsPushStopped()
        {
            return JPushBinding.IsPushStopped();
        }

        /// <summary>
        /// 设置允许推送时间。
        /// </summary>
        /// <parm name="days">为 0~6 之间由","连接而成的字符串。</parm>
        /// <parm name="startHour">0~23</parm>
        /// <parm name="endHour">0~23</parm>
        public void SetPushTime(string days, int startHour, int endHour)
        {
            JPushBinding.SetPushTime(days, startHour, endHour);
        }

        /// <summary>
        /// 设置通知静默时间。
        /// </summary>
        /// <parm name="startHour">0~23</parm>
        /// <parm name="startMinute">0~59</parm>
        /// <parm name="endHour">0~23</parm>
        /// <parm name="endMinute">0~23</parm>
        public void SetSilenceTime(int startHour, int startMinute, int endHour, int endMinute)
        {
            JPushBinding.SetSilenceTime(startHour, startMinute, endHour, endMinute);
        }

        /// <summary>
        /// 设置保留最近通知条数。
        /// </summary>
        /// <param name="num">要保留的最近通知条数。</param>
        public void SetLatestNotificationNumber(int num)
        {
            JPushBinding.SetLatestNotificationNumber(num);
        }

        public void AddLocalNotification(int builderId, string content, string title, int nId,
                int broadcastTime, string extrasStr)
        {
            JPushBinding.AddLocalNotification(builderId, content, title, nId, broadcastTime, extrasStr);
        }

        public void AddLocalNotificationByDate(int builderId, string content, string title, int nId,
                int year, int month, int day, int hour, int minute, int second, string extrasStr)
        {
            JPushBinding.AddLocalNotificationByDate(builderId, content, title, nId, year, month, day, hour, minute, second, extrasStr);
        }

        public void RemoveLocalNotification(int notificationId)
        {
            JPushBinding.RemoveLocalNotification(notificationId);
        }

        public void ClearLocalNotifications()
        {
            JPushBinding.ClearLocalNotifications();
        }

        public void ClearAllNotifications()
        {
            JPushBinding.ClearAllNotifications();
        }

        public void ClearNotificationById(int notificationId)
        {
            JPushBinding.ClearNotificationById(notificationId);
        }

        /// <summary>
        /// 用于 Android 6.0 及以上系统申请权限。
        /// </summary>
        public void RequestPermission()
        {
            JPushBinding.RequestPermission();
        }

        public void SetBasicPushNotificationBuilder()
        {
            JPushBinding.SetBasicPushNotificationBuilder();
        }

        public void SetCustomPushNotificationBuilder()
        {
        }

        public void InitCrashHandler()
        {
            JPushBinding.InitCrashHandler();
        }

        public void StopCrashHandler()
        {
            JPushBinding.StopCrashHandler();
        }

        public bool GetConnectionState()
        {
            return JPushBinding.GetConnectionState();
        }
#endif

        #endregion

        #region 事件监听

        /* data format
       {
          "message": "hhh",
          "extras": {
              "f": "fff",
              "q": "qqq",
              "a": "aaa"
          }
       }
       */
        // 自己处理由 JPush 推送下来的消息。
        void OnReceiveMessage(string jsonStr)
        {
            EventSystem.S.Send(JpushEventID.OnReceiveMessage, jsonStr);
        }

        /**
         * {
         *	 "title": "notiTitle",
         *   "content": "content",
         *   "extras": {
         *		"key1": "value1",
         *       "key2": "value2"
         * 	}
         * }
         */
        // 获取的是 json 格式数据，开发者根据自己的需要进行处理。
        void OnReceiveNotification(string jsonStr)
        {
            EventSystem.S.Send(JpushEventID.OnReceiveNotification, jsonStr);
        }

        //自己处理点击通知栏中的通知
        void OnOpenNotification(string jsonStr)
        {
            EventSystem.S.Send(JpushEventID.OnOpenNotification, jsonStr);
        }

        /// <summary>
        /// JPush 的 tag 操作回调。
        /// </summary>
        /// <param name="result">操作结果，为 json 字符串。</param>
        void OnJPushTagOperateResult(string result)
        {
            EventSystem.S.Send(JpushEventID.OnJPushTagOperateResultGet, result);
        }

        /// <summary>
        /// JPush 的 alias 操作回调。
        /// </summary>
        /// <param name="result">操作结果，为 json 字符串。</param>
        void OnJPushAliasOperateResult(string result)
        {
            EventSystem.S.Send(JpushEventID.OnJPushAliasOperateResultGet, result);
        }

        void OnMobileNumberOperatorResult(string result)
        {
            EventSystem.S.Send(JpushEventID.OnMobileNumberOperatorResultGet, result);
        }
        #endregion

        private void SetDebugMode(bool enable)
        {
            JPushBinding.SetDebug(enable);
        }


    }
}
