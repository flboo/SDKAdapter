using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qarth
{
    public class LocalNotifyHolder
    {
        public string title;
        public string msg;
        public DateTime dTime;
        public bool isRepeat;
        public TimeSpan timespan;

        public string icon;

        public LocalNotifyHolder(string title, string msg, DateTime dt, bool isRepeat, TimeSpan timeSpan, string icon)
        {
            this.title = title;
            this.msg = msg;
            this.dTime = dt;
            this.isRepeat = isRepeat;
            this.timespan = timeSpan;
            this.icon = icon;
        }
    }
}