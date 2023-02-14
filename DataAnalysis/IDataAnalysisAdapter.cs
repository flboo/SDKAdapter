using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace Qarth
{
    public interface IDataAnalysisAdapter : ISDKAdapter
    {
        void OnApplicationQuit();
        void LevelBegin(string levelID);
        void LevelComplate(string levelID);
        void LevelFailed(string levelID, string reason);
        void CustomEvent(string eventID, object label = null);
        void CustomValueEvent(string eventID, float value, string label = null, Dictionary<string, string> dic = null);
        void CustomValueEvent(string eventID, float value, string label = null, Dictionary<string, object> dic = null);
        void CustomEventDuration(string eventID, long duration);
        void CustomEventMapValue(string key, string value);
        void CustomEventMapSend(string eventID);
        void Pay(double cash, double coin);
        void SetUserLevel(int level);
        void CustomEventDic(string eventId, Dictionary<string, string> dic);
        void CustomEventDic(string eventId, Dictionary<string, object> dic);

        void CheckRemoteConfig();

        void AddIgnoreEvent(string adapterClassName, List<string> eventIDs);
        void AddWhiteListEvent(string adapterClassName, List<string> eventIDs);
    }
}
