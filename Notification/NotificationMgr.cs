using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_IOS && UNITY_NOTIFY
using Unity.Notifications.iOS;
#endif
#if UNITY_ANDROID && !UNITY_EDITOR && UNITY_NOTIFY
using Unity.Notifications.Android;
#endif

namespace Qarth
{
    public class NotificationMgr : TMonoSingleton<NotificationMgr>
    {
        // key = tile + msg 
        public Dictionary<string, LocalNotifyHolder> m_DictCachedNotifies = new Dictionary<string, LocalNotifyHolder>();

        public override void OnSingletonInit()
        {
            Log.i("NotificationMgr OnSingletonInit");
        }

        public void Init()
        {
            Log.i("NotificationMgr init");
            CleanNotification();
            RegisterNoticationChannel();
        }


        void OnApplicationPause(bool paused)
        {        //程序进入后台时
            if (!paused)
            {
                Debug.Log("NotificationMgr pause back");
                CleanNotification();
            }
        }

        public void SendNotification(string title, string message, int hour, bool isRepeatDay, long repeatInteralMinute = 1440)
        {
            int year = System.DateTime.Now.Year;
            int month = System.DateTime.Now.Month;
            int day = System.DateTime.Now.Day;
            System.DateTime newDate = new System.DateTime(year, month, day, hour, 0, 0);
            SendNotification(title, message, newDate, isRepeatDay, repeatInteralMinute);
        }

        public void SendRepeatingNotification(TimeSpan delay, TimeSpan timeout, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "", string soundName = null, string channel = "default")
        {
            LocalNotification.Action action2 = new LocalNotification.Action("foreground", "In Foreground", this);
#if UNITY_ANDROID //&& !UNITY_EDITOR
#if UNITY_NOTIFY
            SendNotification(title,message,DateTime.Now.AddSeconds(delay.TotalSeconds),true,timeout,"");
#else
            //LocalNotification.SendRepeatingNotification(2,delay,timeout,title,message,bgColor,sound,vibrate,lights,bigIcon,soundName,channel,action2);
#endif

#elif UNITY_IOS && !UNITY_EDITOR
            var notification = GetIosNotification(delay, title, message, true, timeout.Minutes, soundName);     
            NotificationServices.ScheduleLocalNotification(notification);
#endif

        }

        public void SendNotification(string title, string message, DateTime dateTime, bool isRepeatDay, long repeatMinute = 1440)
        {
            if (dateTime < DateTime.Now)
            {
                dateTime = dateTime.AddDays(1);
            }

            TimeSpan span = dateTime - DateTime.Now;
            if (isRepeatDay)
            {
                SendRepeatingNotification(span, TimeSpan.FromMinutes(repeatMinute), title, message, new Color32(0xff, 0x44, 0x44, 255));
            }
            else
            {
                SendNotification(span, title, message, new Color32(0xff, 0x44, 0x44, 255));
            }
        }



        public void SendNotification(TimeSpan delay, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "", string soundName = null, string channel = "default")
        {
            LocalNotification.Action action2 = new LocalNotification.Action("foreground", "In Foreground", this);
#if UNITY_ANDROID //&& !UNITY_EDITOR
#if UNITY_NOTIFY
            SendNotification(title,message,DateTime.Now.AddSeconds(delay.TotalSeconds),false,delay,"");
#else
            //LocalNotification.SendNotification(1,delay,title,message,bgColor,sound,vibrate,lights,bigIcon,soundName,channel,action2);
#endif
#elif UNITY_IOS && !UNITY_EDITOR
            var notification = GetIosNotification(delay, title, message, false,0, soundName);           
            NotificationServices.ScheduleLocalNotification(notification);
            
#endif
        }

#if UNITY_IOS// || UNITY_EDITOR
        
        public static UnityEngine.iOS.LocalNotification GetIosNotification(TimeSpan delay,string title, string message, bool isrepeat,long timeInternal= 24 * 60, string soundName = null)         
        {
#if UNITY_IOS //|| UNITY_EDITOR
            UnityEngine.iOS.LocalNotification notification = new UnityEngine.iOS.LocalNotification();
            notification.soundName = UnityEngine.iOS.LocalNotification.defaultSoundName;
            notification.alertTitle = title;
            notification.alertBody = message;
            notification.timeZone = "GMT+8";
            notification.applicationIconBadgeNumber = 1;
            notification.fireDate = DateTime.Now + delay;
            notification.alertAction = "OnAction";
            if (isrepeat)
            {
                notification.repeatCalendar = UnityEngine.iOS.CalendarIdentifier.ChineseCalendar;
                notification.repeatInterval = CalculateiOSRepeat(timeInternal);


            }

            return notification;  
#endif
            return null;
        }
        
        public static UnityEngine.iOS.LocalNotification GetIosNotification(DateTime dateTime,string title, string message, bool isrepeat, string soundName = null)         
        {
#if UNITY_IOS
            UnityEngine.iOS.LocalNotification notification = new UnityEngine.iOS.LocalNotification();
            notification.soundName = UnityEngine.iOS.LocalNotification.defaultSoundName;
            notification.alertTitle = title;
            notification.alertBody = message;
            notification.timeZone = "GMT+8";
            notification.applicationIconBadgeNumber = 1;
            notification.fireDate = dateTime;
            notification.alertAction = "OnAction";
            if (isrepeat)
            {
                notification.repeatCalendar = UnityEngine.iOS.CalendarIdentifier.ChineseCalendar;
                notification.repeatInterval = UnityEngine.iOS.CalendarUnit.Weekday;
            }

            return notification;
#endif
            return null;
        }
        
    public static UnityEngine.iOS.CalendarUnit CalculateiOSRepeat(long timeoutMS)
    {
        if (timeoutMS == 0)
            return 0;

        long timeoutMinutes = timeoutMS;
        if (timeoutMinutes == 1)
            return UnityEngine.iOS.CalendarUnit.Minute;
        if (timeoutMinutes == 60)
            return UnityEngine.iOS.CalendarUnit.Hour;
        if (timeoutMinutes == 60 * 24)
            return UnityEngine.iOS.CalendarUnit.Day;
        if (timeoutMinutes >= 60 * 24 * 2 && timeoutMinutes <= 60 * 24 * 5)
            return UnityEngine.iOS.CalendarUnit.Weekday;
        if (timeoutMinutes == 60 * 24 * 7)
            return UnityEngine.iOS.CalendarUnit.Week;
        if (timeoutMinutes >= 60 * 24 * 28 && timeoutMinutes <= 60 * 24 * 31)
            return UnityEngine.iOS.CalendarUnit.Month;
        if (timeoutMinutes >= 60 * 24 * 91 && timeoutMinutes <= 60 * 24 * 92)
            return UnityEngine.iOS.CalendarUnit.Quarter;
        if (timeoutMinutes >= 60 * 24 * 365 && timeoutMinutes <= 60 * 24 * 366)
            return UnityEngine.iOS.CalendarUnit.Year;
        throw new ArgumentException("Unsupported timeout for iOS - must equal a minute, hour, day, 2-5 days (for 'weekday'), week, month, quarter or year but was " + timeoutMS);
    }
#endif  

        public void CleanNotification()
        {
#if UNITY_IOS && !UNITY_EDITOR
            UnityEngine.iOS.LocalNotification l = new UnityEngine.iOS.LocalNotification(); 
            l.applicationIconBadgeNumber = -1;
            NotificationServices.PresentLocalNotificationNow(l);
            NotificationServices.CancelAllLocalNotifications();
            NotificationServices.ClearLocalNotifications();
            NotificationServices.RegisterForNotifications(NotificationType.Badge | NotificationType.Alert | NotificationType.Sound);
#elif UNITY_ANDROID && !UNITY_EDITOR
            //LocalNotification.CancelNotification(1);
            //LocalNotification.CancelNotification(2);
                        
#if UNITY_NOTIFY
            AndroidNotificationCenter.CancelAllNotifications();
#else
            //LocalNotification.ClearNotifications(); 
#endif
#endif
        }

        public void ReopenNotification()
        {
            if (m_DictCachedNotifies.Count > 0)
            {
                CleanNotification();
                foreach (var notify in m_DictCachedNotifies.Values)
                {
                    SendNotification(notify.title, notify.msg, notify.dTime, notify.isRepeat, notify.timespan, notify.icon);
                }
            }
        }

        public void OnAction(string identifier)
        {
            DataAnalysisMgr.S.CustomEvent("Notification_Action");
            Debug.Log("Notification OnAction");
        }


        public void RegisterNoticationChannel()
        {
#if UNITY_ANDROID && !UNITY_EDITOR && UNITY_NOTIFY
            var c = new AndroidNotificationChannel()
            {
                Id = "001_",
                Name = "GP",
                Importance = Importance.High,
                Description = "Generic notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(c);
#endif

#if UNITY_IOS

#endif
        }

        public void SendNotification(string title, string text, DateTime date, bool isrepeat, TimeSpan span, string smallIcon = "")
        {
            var key = string.Concat(title, text);
            if (!m_DictCachedNotifies.ContainsKey(key))
                m_DictCachedNotifies.Add(key, new LocalNotifyHolder(title, text, date, isrepeat, span, smallIcon));

#if UNITY_ANDROID && !UNITY_EDITOR && UNITY_NOTIFY
            var notification = new AndroidNotification();
            notification.Title = title;
            notification.Text = text;
            notification.FireTime = date;// System.DateTime.Now.AddSeconds(5);
            if(isrepeat)
            {
                notification.RepeatInterval = span;
            }
            if(string.IsNullOrEmpty(smallIcon))
            {
                notification.SmallIcon = smallIcon;
            }
            
            AndroidNotificationCenter.SendNotification(notification, "001_");
            
#endif
        }

    }

}

