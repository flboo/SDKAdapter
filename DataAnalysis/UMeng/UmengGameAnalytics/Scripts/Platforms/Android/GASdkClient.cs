using System;
using UnityEngine;
using System.Collections.Generic;
using GameAnalyticsSdk.Common;
using GameAnalyticsSdk.Api;

namespace GameAnalyticsSdk.Platforms.Android
{
    public class GASdkClient : IGASdkClient
    {
        GASdkClient()
        {
        }

        static GASdkClient sInstance = new GASdkClient();

        public static GASdkClient Instance
        {
            get {
                return sInstance;
            }
        }

        static readonly AndroidJavaObject StatisticsSDK = new AndroidJavaClass("com.coala.statisticscore.StatisticsSDK").CallStatic<AndroidJavaObject>("getInstance");
        static readonly AndroidJavaObject Context = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");

        static readonly AndroidJavaObject StatisticsApplication = new AndroidJavaClass("com.coala.statisticscore.StatisticsApplication");


        public void ApplicaitonInit() {
            AndroidJavaObject mAppContext;
            mAppContext = Context.Call<AndroidJavaObject>("getApplication");
            StatisticsApplication.CallStatic("attachBaseContext", Context);
            StatisticsApplication.CallStatic("onCreate", mAppContext);
        }

        public void Init() {
            StatisticsSDK.Call("init", Context);
            OnResume();
            AddAndroidLifeCycleCallBack();
        }

        void AddAndroidLifeCycleCallBack() {
            GameObject go = new GameObject();
            go.AddComponent<AndroidLifeCycleCallBack>();
            go.name = "UmengManager";
        }

        public void OnPause() {
            StatisticsSDK.Call("onPause");
        }

        public void OnResume() {
            StatisticsSDK.Call("onResume");
        }

        public void SetLogEnabled(bool value) {
        }

        public void ProfileSignIn(string userId, string name) {
            StatisticsSDK.Call("onProfileSignIn", userId, name);
        }
        public void ProfileSignIn(string userId, string name, string provider) {
            StatisticsSDK.Call("onProfileSignIn", userId, name, provider);
        }
        public void ProfileSignOff() {
            StatisticsSDK.Call("onProfileSignOut");
        }

        public void PageBegin(string pageName) {
            StatisticsSDK.Call("onPageStart", pageName);
        }
        public void PageEnd(string pageName) {
            StatisticsSDK.Call("onPageEnd", pageName);
        }

        public void Event(string eventId) {
            StatisticsSDK.Call("onEvent", Context, eventId);
        }
        public void Event(string eventId, string label) {
            StatisticsSDK.Call("onEvent", Context, eventId, label);
        }
        public void Event(string eventId, Dictionary<string, string> attributes) {
            StatisticsSDK.Call("onEvent", Context, eventId, GASdkUtil.ToJavaHashMap(attributes));
        }
        public void EventObject(string eventId, Dictionary<string, object> dict) {
            StatisticsSDK.Call("onEventObject", Context, eventId, GASdkUtil.ToJavaHashMap(dict));
        }

        public void SetFirstLaunchEvent(string[] trackID) {
            StatisticsSDK.Call("setFirstLaunchEvent", Context, GASdkUtil.ToJavaList(trackID));
        }

        public void RegisterPreProperties(JSONObject jsonObject) {
            StatisticsSDK.Call("registerPreProperties", Context, GASdkUtil.AndroidJavaJsonObject(jsonObject));
        }
        public void UnregisterPreProperty(string propertyName) {
            StatisticsSDK.Call("unregisterPreProperty", Context, propertyName);
        }
        public JSONObject GetPreProperties() {
            return GASdkUtil.jsonObjectFromJava(StatisticsSDK.CallStatic<AndroidJavaObject>("getPreProperties", Context));
        }
        public void ClearPreProperties() {
            StatisticsSDK.Call("clearPreProperties", Context);
        }

        public void OnKillProcess() {
            StatisticsSDK.Call("onKillProcess", Context);
        }

        public void SetUserLevel(int level) {
            StatisticsSDK.Call("setPlayerLevel", level);
        }
        public void StartLevel(string level) {
            StatisticsSDK.Call("startLevel", level);
        }
        public void FinishLevel(string level) {
            StatisticsSDK.Call("finishLevel", level);
        }
        public void FailLevel(string level) {
            StatisticsSDK.Call("failLevel", level, "");
        }

        public void Pay(double cash, PaySource source, double coin) {
            StatisticsSDK.Call("pay", cash, coin, (int)source);
        }
        public void Pay(double cash, int source, double coin) {
            StatisticsSDK.Call("pay", cash, coin, source);
        }
        public void Pay(double cash, PaySource source, string item, int amount, double price) {
            StatisticsSDK.Call("pay", cash, item, amount, price, (int)source);
        }
        public void Buy(string item, int amount, double price) {
            StatisticsSDK.Call("buy", item, amount, price);
        }
        public void Use(string item, int amount, double price) {
            StatisticsSDK.Call("use", item, amount, price);
        }

        public void Bonus(double coin, BonusSource source) {
            StatisticsSDK.Call("bonus", coin, (int)source);
        }
        public void Bonus(string item, int amount, double price, BonusSource source) {
            StatisticsSDK.Call("bonus", item, amount, price, (int)source);
        }
    }
}
