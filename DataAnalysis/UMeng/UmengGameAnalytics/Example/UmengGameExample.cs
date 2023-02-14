using UnityEngine;
using GameAnalyticsSdk.Api;
using System.Collections.Generic;

public class UmengGameExample : MonoBehaviour
{
    void Start()
    {
        GASdk.ApplicaitonInit();
        GASdk.Init();
        GASdk.SetLogEnabled(true);
        GASdk.ProfileSignIn("unity_userid", "unity_name");
        GASdk.SetFirstLaunchEvent(new[] { "FirstLaunchEvent1", "FirstLaunchEvent2" });

        var obj2 = new JSONObject();
        obj2["key1"] = 3;
        obj2["key2"] = true;
        obj2["key3"] = 10.0f;
        GASdk.RegisterPreProperties(obj2);

        Debug.Log("gasdk: start");
    }

    void OnGUI()
    {
        int x = 150;
        int y = 50;
        int w = 500;
        int h = 100;
        int d = 150;

        if (GUI.Button(new Rect(x, y, w, h), "Level Test"))
        {
            GASdk.SetUserLevel(11);
            GASdk.StartLevel("unity_start_level");
            GASdk.FinishLevel("unity_finish_level1");
            GASdk.FailLevel("unity_fail_level1");

            Debug.Log("gasdk: level test");
        }
        y += d;
        if (GUI.Button(new Rect(x, y, w, h), "Event Test"))
        {

            GASdk.Event("unity_event_key");
            GASdk.Event("unity_event_key_label", "unity_value_label");

            var dict1 = new Dictionary<string, string>();
            dict1.Add("key_dict", "value_dict");
            GASdk.Event("unity_event_key_dict", dict1);

            var dict = new Dictionary<string, object>();
            dict.Add("key", 1);
            dict.Add("key2",true);
            dict.Add("key3", 2.0);
            GASdk.EventObject("unity_event_key_object", dict);

            Debug.Log("gasdk: event test");
        }
        y += d;
        if (GUI.Button(new Rect(x, y, w, h), "Pay Test"))
        {
            GASdk.Pay(1, PaySource.AppStore, 10);
            GASdk.Pay(2, PaySource.支付宝, 20);
            GASdk.Pay(3, 9, 30);
            GASdk.Pay(4, PaySource.网银, "pay_item", 2, 2);

            Debug.Log("gasdk: pay test");
        }
        y += d;
        if (GUI.Button(new Rect(x, y, w, h), "Buy Test"))
        {
            GASdk.Buy("buy_item", 2, 2);
            GASdk.Buy("buy_item", 1, 1);
            GASdk.Use("buy_item", 1, 1);

            Debug.Log("gasdk: buy test");
        }
        y += d;
        if (GUI.Button(new Rect(x, y, w, h), "Bonus Test"))
        {
            GASdk.Bonus(22, BonusSource.玩家赠送);
            GASdk.Bonus(22, BonusSource.Source3);
            GASdk.Bonus("bouns_item", 2, 5, BonusSource.Source6);

            Debug.Log("gasdk: bonus test");
        }
        y += d;
    }
}
