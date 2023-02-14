using System;
using UnityEngine;
using System.Collections.Generic;
using GameAnalyticsSdk.Common;
using GameAnalyticsSdk.Platforms;

namespace GameAnalyticsSdk.Api
{
    public class GASdk
    {

        static readonly IGASdkClient mClient = GetGASdkClient();

        static IGASdkClient GetGASdkClient() {
            return ClientFactory.GASdkClientInstance();
        }

        public static void ApplicaitonInit() {
            mClient.ApplicaitonInit();
        }

        public static void Init() {
            mClient.Init();
        }

        public static void OnPause() {
            mClient.OnPause();
        }

        public static void OnResume() {
            mClient.OnResume();
        }

        public static void SetLogEnabled(bool value) {
            mClient.SetLogEnabled(value);
        }

        public static void ProfileSignIn(string userId, string name) {
            mClient.ProfileSignIn(userId, name);
        }
        public static void ProfileSignIn(string userId, string name, string provider) {
            mClient.ProfileSignIn(userId, name, provider);
        }
        public static void ProfileSignOff() {
            mClient.ProfileSignOff();
        }

        public static void PageBegin(string pageName) {
            mClient.PageBegin(pageName);
        }
        public static void PageEnd(string pageName) {
            mClient.PageEnd(pageName);
        }

        public static void Event(string eventId) {
            mClient.Event(eventId);
        }
        public static void Event(string eventId, string label) {
            mClient.Event(eventId, label);
        }
        public static void Event(string eventId, Dictionary<string, string> attributes) {
            mClient.Event(eventId, attributes);
        }
        public static void Event(string eventId, Dictionary<string, string> attributes, int value)
        {
            try
            {
                if (attributes == null)
                    attributes = new Dictionary<string, string>();
                if (attributes.ContainsKey("__ct__"))
                {
                    attributes["__ct__"] = value.ToString();
                    Event(eventId, attributes);
                }
                else
                {
                    attributes.Add("__ct__", value.ToString());
                    Event(eventId, attributes);
                    attributes.Remove("__ct__");
                }
            }
            catch (Exception) {
            }
        }
        public static void EventObject(string eventID, Dictionary<string, object> dict) {
            mClient.EventObject(eventID, dict);
        }

        public static void SetFirstLaunchEvent(string[] trackID) {
            mClient.SetFirstLaunchEvent(trackID);
        }

        public static void RegisterPreProperties(JSONObject jsonObject) {
            JSONObject filteredJsonObject = new JSONObject();

            foreach (KeyValuePair<string, JSONNode> kv in jsonObject) {
                if (kv.Value.IsObject || kv.Value.IsArray) {
                    Debug.LogError("不支持加入Object/Array类型,此kv对被丢弃");
                } else if (kv.Value.IsBoolean) {
                    filteredJsonObject.Add(kv.Key, Convert.ToInt32(kv.Value.AsBool));
                } else {
                    filteredJsonObject.Add(kv.Key, kv.Value);
                }
            }
            mClient.RegisterPreProperties(filteredJsonObject);
        }
        public static void UnregisterPreProperty(string propertyName) {
            mClient.UnregisterPreProperty(propertyName);
        }

        public static JSONObject GetPreProperties() {
            return mClient.GetPreProperties();
        }
        public static void ClearPreProperties() {
            mClient.ClearPreProperties();
        }

        public static void OnKillProcess() {
            mClient.OnKillProcess();
        }

        public static void SetUserLevel(int level) {
            mClient.SetUserLevel(level);
        }
        public static void StartLevel(string level) {
            mClient.StartLevel(level);
        }
        public static void FinishLevel(string level) {
            mClient.FinishLevel(level);
        }
        public static void FailLevel(string level) {
            mClient.FailLevel(level);
        }

        public static void Pay(double cash, PaySource source, double coin) {
            mClient.Pay(cash, source, coin);
        }
        public static void Pay(double cash, int source, double coin) {
            mClient.Pay(cash, source, coin);
        }
        public static void Pay(double cash, PaySource source, string item, int amount, double price) {
            mClient.Pay(cash, source, item, amount, price);
        }
        public static void Buy(string item, int amount, double price) {
            mClient.Buy(item, amount, price);
        }
        public static void Use(string item, int amount, double price) {
            mClient.Use(item, amount, price);
        }

        public static void Bonus(double coin, BonusSource source) {
            mClient.Bonus(coin, source);
        }
        public static void Bonus(string item, int amount, double price, BonusSource source) {
            mClient.Bonus(item, amount, price, source);
        }
    }
}