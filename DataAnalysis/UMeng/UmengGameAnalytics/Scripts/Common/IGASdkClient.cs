using System.Collections.Generic;
using GameAnalyticsSdk.Api;

namespace GameAnalyticsSdk.Common
{
    public interface IGASdkClient
    {
        void ApplicaitonInit();
        void Init();

        void OnPause();
        void OnResume();

        void SetLogEnabled(bool value);

        void ProfileSignIn(string userId, string name);
        void ProfileSignIn(string userId, string name, string provider);
        void ProfileSignOff();

        void PageBegin(string pageName);
        void PageEnd(string pageName);

        void Event(string eventId);
        void Event(string eventId, string label);
        void Event(string eventId, Dictionary<string, string> attributes);
        void EventObject(string eventID, Dictionary<string, object> dict);

        void SetFirstLaunchEvent(string[] trackID);

        void RegisterPreProperties(JSONObject jsonObject);
        void UnregisterPreProperty(string propertyName);
        JSONObject GetPreProperties();
        void ClearPreProperties();

        void OnKillProcess();

        void SetUserLevel(int level);
        void StartLevel(string level);
        void FinishLevel(string level);
        void FailLevel(string level);

        void Pay(double cash, PaySource source, double coin);
        void Pay(double cash, int source, double coin);
        void Pay(double cash, PaySource source, string item, int amount, double price);
        void Buy(string item, int amount, double price);
        void Use(string item, int amount, double price);

        void Bonus(double coin, BonusSource source);
        void Bonus(string item, int amount, double price, BonusSource source);
    }
}
