using System;
using System.Collections.Generic;
using GameAnalyticsSdk.Api;

namespace GameAnalyticsSdk.Common
{
    public class DummyGASdkClient : IGASdkClient
    {
        public DummyGASdkClient()
        {
        }

        public void ApplicaitonInit() {
            
        }

        public void Init() {}

        public void OnPause()
        {

        }

        public void OnResume()
        {

        }

        public void SetLogEnabled(bool value)
        {
        }

        public void ProfileSignIn(string userId, string name)
        {
        }
        public void ProfileSignIn(string userId, string name, string provider)
        {
        }
        public void ProfileSignOff()
        {
        }

        public void PageBegin(string pageName)
        {
        }
        public void PageEnd(string pageName)
        {
        }

        public void Event(string eventId)
        {
        }
        public void Event(string eventId, string label)
        {
        }
        public void Event(string eventId, Dictionary<string, string> attributes)
        {
        }
        public void Event(string eventId, Dictionary<string, string> attributes, int value)
        {
        }
        public void EventObject(string eventID, Dictionary<string, object> dict)
        {
        }

        public void SetFirstLaunchEvent(string[] trackID)
        {
        }

        public void RegisterPreProperties(JSONObject jsonObject)
        {
        }
        public void UnregisterPreProperty(string propertyName)
        {
        }

        public JSONObject GetPreProperties()
        {
            return null;
        }
        public void ClearPreProperties()
        {
        }

        public void OnKillProcess()
        {
        }

        public void SetUserLevel(int level)
        {
        }
        public void StartLevel(string level)
        {
        }
        public void FinishLevel(string level)
        {
        }
        public void FailLevel(string level)
        {
        }

        public void Pay(double cash, PaySource source, double coin)
        {
        }
        public void Pay(double cash, int source, double coin)
        {
        }
        public void Pay(double cash, PaySource source, string item, int amount, double price)
        {
        }
        public void Buy(string item, int amount, double price)
        {
        }
        public void Use(string item, int amount, double price)
        {
        }

        public void Bonus(double coin, BonusSource source)
        {
        }
        public void Bonus(string item, int amount, double price, BonusSource source)
        {
        }
    }
}
