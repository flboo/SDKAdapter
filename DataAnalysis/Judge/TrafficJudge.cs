using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Proyecto26;
using Qarth;
using TaurusXAdSdk.Api;
using UnityEngine;
using UnityEngine.Networking;

public class TrafficJudge : TMonoSingleton<TrafficJudge>
{
    
    public override void OnSingletonInit()
    {
        Debug.Log("TrafficJudge OnSingletonInit");
    }

    public static readonly string POST_URI = "https://is.snssdk.com/api/ad/union/activate_event";


       
    public void Init()
    {
        

        
        if (PlayerPrefs.GetInt("traffic_judge_key", -1) != -1)
        {
            return;
        }
        
        PlayerPrefs.SetInt("traffic_judge_key", 100);
        if (SDKConfig.S.dataAnalysisConfig.trafficConfig.user_id == 0)

        {
            Debug.Log("PLZ CheckAllConfigRight");
            return;
        }


        
        PostData();
        

    }

    public Boo.Lang.Hash GetPOSTData()
    {
        var config = SDKConfig.S.dataAnalysisConfig.trafficConfig;
        var data_map = new Boo.Lang.Hash();
        Int64 ticks = GetTimeStamp();
        int nonce = RandomHelper.Range(0, 256);

        Int64 appid = 0;
        int os = 0;
        
#if UNITY_IOS
       appid = SDKConfig.S.dataAnalysisConfig.appid_IOS;
       os = 2;
#elif UNITY_ANDROID
        appid = SDKConfig.S.dataAnalysisConfig.trafficConfig.appid_Android;
        os = 1;
#endif
        
        data_map["user_id"] = config.user_id;

        
        StringBuilder sb = new StringBuilder();
        sb.Append(config.secure_key).Append(ticks).Append(nonce);
        
        //string 
        data_map["sign"] = Sha1Sign(sb.ToString(), Encoding.ASCII);
                    

        data_map["nonce"] = nonce;
        data_map["app_id"] = appid;

        var device_map = new Boo.Lang.Hash();

        device_map["os"] = os;

#if UNITY_IOS
        device_map["idfa"] = DeviceInfoHelper.GetIDFA();
#elif UNITY_ANDROID
      
        device_map["imei"] = GetIemi();

        StartCoroutine(IPManager.GetIp(device_map));        
        
        device_map["ua"] = DefineUA.UAANDROID[RandomHelper.Range(0,DefineUA.UAANDROID.Length)];
#endif      
        data_map["device"] = device_map;
        data_map["timestamp"] = ticks;
        data_map["channel"] = GetChannelID();
        

        return data_map;
    }


    private int GetChannelID()
    {

        

#if UNITY_ANDROID
        switch (CustomExtensions.GetSDKChannel())
        {
            case "bytedance" :
                return 1;
            case "kuaishou" :
                return 2;
            case "oppo":
                return 3;
            case "vivo":
                return 4;
            case "ysdk":
                return 5;
            case "xiaomi":
                return 6;
            case "m4399":
                return 7;
            case "taptap":
                return 8;
                
           default:
               return 9;
        }
#endif
    
        return 9;
    }



    public string GetIemi()
    {
          try
          {
              //if (CustomExtensions.IsPermissionGranted(AndroidPermissionDefine.READ_PHONE_STATE))
              {
                  AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                  AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
                  AndroidJavaClass contextClass = new AndroidJavaClass("android.content.Context");
                  string TELEPHONY_SERVICE = contextClass.GetStatic<string>("TELEPHONY_SERVICE");
                  AndroidJavaObject telephonyService = activity.Call<AndroidJavaObject>("getSystemService", TELEPHONY_SERVICE);
                  //AndroidJavaObject TM = new AndroidJavaObject("android.telephony.TelephonyManager");
                  string IMEI = telephonyService.Call<string>("getDeviceId");
                  Debug.Log("Imei1 :" + IMEI);
                  return IMEI;

              }                                                
              
          }
          catch (Exception e)
          {
              
              Debug.Log(e.Message);
              Debug.Log(e.StackTrace);

              return null;
          }

          return null;

    }

    public void PostData()
    {
        
        var rH = new RequestHelper()

        {
            Uri = POST_URI,
            Method = "POST",
            Timeout = 10,
            BodyString = LitJson.JsonMapper.ToJson(GetPOSTData()),
            ContentType = "application/json",

        };
        
        Debug.Log("Post RH BodyString: " + rH.BodyString);
        
        RestClient.Request(rH).Then(response =>  {
            Debug.Log("PostDataCallBack" + response.Text);
        }, reject =>
        {
            Debug.Log(reject.Message);
        }).Catch(e =>
        {
            Debug.Log(e.Message);
        });

    }

    public TrafficJudge()
    {
        
    }

    public static String Sha1Sign(String content,Encoding encode)
    {
        try
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();//创建SHA1对象
            byte[] bytes_in = encode.GetBytes(content);//将待加密字符串转为byte类型
            byte[] bytes_out = sha1.ComputeHash(bytes_in);//Hash运算
            sha1.Dispose();//释放当前实例使用的所有资源
            String result = BitConverter.ToString(bytes_out);//将运算结果转为string类型
            result = result.Replace("-", "").ToUpper().ToLower();//替换并转为大写
            Debug.Log(result);
            return result;
        }catch(Exception ex)
        {
            return ex.Message;
        }
    }
    
    public static Int64 GetTimeStamp()
    {
        string dt = GetNetDateTime();
        TimeSpan ts;
        if(string.IsNullOrEmpty(dt))
        {
            ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);      
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            //(DateTime.Now.ToUniversalTime().Ticks - 
        }else
        {
            DateTime date = Convert.ToDateTime(dt).AddHours(-8);
            ts = date - new DateTime(1970, 1, 1, 0, 0, 0, 0);            
        }        
        return Convert.ToInt64(ts.TotalSeconds) - 3;
    }

    public static string GetNetDateTime()
    {
            WebRequest request = null;
            WebResponse response = null;
            WebHeaderCollection headerCollection = null;
            string datetime = string.Empty;
            try
            {
                request = WebRequest.Create("https://www.baidu.com");
                request.Timeout = 3000;
                request.Credentials = CredentialCache.DefaultCredentials;
                response = (WebResponse)request.GetResponse();
                headerCollection = response.Headers;
                foreach (var h in headerCollection.AllKeys)
                { if (h == "Date") { datetime = headerCollection[h]; } }
                return datetime;
            }
            catch (Exception) { return datetime; }
            finally
            {
                if (request != null)
                { request.Abort(); }
                if (response != null)
                { response.Close(); }
                if (headerCollection != null)
                { headerCollection.Clear(); }
            }
    }      
}


public class IPManager
{
    private static string m_IpUrl = "http://icanhazip.com/";
    public static string GetIP(ADDRESSFAM Addfam)
    {
        //Return null if ADDRESSFAM is Ipv6 but Os does not support it
        if (Addfam == ADDRESSFAM.IPv6 && !Socket.OSSupportsIPv6)
        {
            return null;
        }

        string output = "";

        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
            NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

            if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
#endif 
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    //IPv4
                    if (Addfam == ADDRESSFAM.IPv4)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            if(ip.Address.ToString() != "127.0.0.1")
                            {
                                output = ip.Address.ToString();    
                            }
                                
                            Debug.Log("ip:" + output);
                        }
                    }

                    //IPv6
                    else if (Addfam == ADDRESSFAM.IPv6)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
                        {
                            output = ip.Address.ToString();
                            Debug.Log("ip:" + output);
                        }
                    }
                }
            }
        }
        return output;
    }
    public static IEnumerator GetIp(Boo.Lang.Hash hash)
        {
            UnityWebRequest wr = UnityWebRequest.Get(m_IpUrl);
            yield return wr.SendWebRequest();
            //异常处理
            if (wr.isHttpError || wr.isNetworkError)
             {   
                if (string.IsNullOrEmpty(GetIP(ADDRESSFAM.IPv4)))
                {
                    hash["ip"] = GetIP(ADDRESSFAM.IPv6);
                }
                else
                {
                    hash["ip"] = GetIP(ADDRESSFAM.IPv4);
                }
                Log.e("ipgeterror: " + wr.error);
             }
            else
            {
                hash["ip"] = wr.downloadHandler.text;
                Log.e(wr.downloadHandler.text);
            }
        }
}

public enum ADDRESSFAM
{
    IPv4, IPv6
}

public static class DefineUA
{

    public static string[] UAANDROID = new string[]
    {

        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; MI 5 Build/MXB48T) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.2) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 4.4.4; zh-cn; HM NOTE 1LTE Build/KTU84P) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.4.3) WindVane/8.0.0 720X1280 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; Android 5.1.1; vivo Y51A Build/LMY47V) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/35.0.1916.138 Mobile Safari/537.36 T7/7.4 baiduboxapp/8.3.1 (Baidu; P1 5.1.1)",
        "Mozilla/5.0 (Linux; Android 6.0; HUAWEI MT7-CL00 Build/HuaweiMT7-CL00; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/48.0.2564.116 Mobile Safari/537.36 baidubrowser/7.10.12.0 (Baidu; P1 6.0)",
        "Mozilla/5.0 (Linux; U; Android 5.0.2; zh-cn; ZTE A2015 Build/LRX22G) AppleWebKit/534.30 (KHTML, like Gecko)Version/4.0 MQQBrowser/5.3 Mobile Safari/534.30",
        "Mozilla/5.0 (Linux; Android 6.0; NEM-AL10 Build/HONORNEM-AL10; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/55.0.2883.91 Mobile Safari/537.36 baiduboxapp/6.3.1 (Baidu; P1 6.0)",
        "Mozilla/5.0 (Linux; U; Android 4.3; zh-cn; SM-N9005 Build/JSS15J) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.4.5) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.1; zh-cn; OPPO R9m Build/LMY47I) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; Android 7.0; FRD-AL10 Build/HUAWEIFRD-AL10) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/35.0.1916.138 Mobile Safari/537.36 T7/7.4 baiduboxapp/8.3.1 (Baidu; P1 7.0)",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; SM-A7100 Build/MMB29M) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.1) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.1.1; zh-CN; Coolpad A8-930 Build/LMY47V) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/40.0.2214.89 UCBrowser/11.3.5.908 Mobile Safari/537.36",
        "Mozilla/5.0 (Linux; Android 6.0; PLK-TL01H Build/HONORPLK-TL01H) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/35.0.1916.138 Mobile Safari/537.36 T7/7.4 baiduboxapp/8.3.1 (Baidu; P1 6.0)",
        "Mozilla/5.0 (Linux; U; Android 5.1; zh-cn; MX5 Build/LMY47I) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.2) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0; zh-cn; MP1512 Build/MRA58K) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.2) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0; zh-cn; MI 5 Build/MRA58K) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; MI 5 Build/MXB48T) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.3.2) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; MI 5s Build/MXB48T) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; Android 4.4.4; X9007 Build/KTU84P) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/35.0.1916.138 Mobile Safari/537.36 T7/7.4 light/1.0 baiduboxapp/8.3.1 (Baidu; P1 4.4.4)",
        "Mozilla/5.0 (Linux; U; Android 5.1.1; zh-cn; OPPO R7sm Build/LMY47V) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1800 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0; zh-cn; BLN-AL10 Build/HONORBLN-AL10) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1812 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 4.4.2; zh-cn; vivo X3V Build/KVT49L) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 720X1280 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 4.4.2; zh-cn; 2014501 Build/KOT49H) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 720X1280 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.1.1; zh-CN; vivo X7 Build/LMY47V) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/40.0.2214.89 UCBrowser/11.4.2.936 Mobile Safari/537.36",
        "Mozilla/5.0 (Linux; U; Android 7.0; zh-cn; PRA-AL00 Build/HONORPRA-AL00) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.2) WindVane/8.0.0 1080X1812 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.0.2; zh-cn; MI 2S Build/LRX22G) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/5.11.0) WindVane/8.0.0 720X1280 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; Le X822 Build/FWXCNCT5801507014S) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.3.2) WindVane/8.0.0 1440X2560 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0; zh-cn; VIE-AL10 Build/HUAWEIVIE-AL10) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1800 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.1; zh-cn; 1501_M02 Build/LMY47D) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.1.0) WindVane/8.0.0 720X1280 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.1; zh-cn; OPPO A37m Build/LMY47I) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 720X1280 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; OPPO R9sk Build/MMB29M) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; M836 Build/MMB29M) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.4.2) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 4.4.2; zh-cn; SM-N9008S Build/KOT49H) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.1) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; vivo X9 Build/MMB29M) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; Android 5.0.1; HUAWEI GRA-CL10 Build/HUAWEIGRA-CL10) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/35.0.1916.138 Mobile Safari/537.36 T7/7.4 baiduboxapp/8.2.5 (Baidu; P1 5.0.1)",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-CN; SM-N9200 Build/MMB29K) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/40.0.2214.89 UCBrowser/11.2.8.885 Mobile Safari/537.36",
        "Mozilla/5.0 (Linux; U; Android 4.4.4; zh-cn; vivo X5Max+ Build/KTU84P) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; SM-G9308 Build/MMB29M) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.1.7) WindVane/8.0.0 1440X2560 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0; zh-cn; PLK-AL10 Build/HONORPLK-AL10) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1812 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 7.0; zh-cn; HUAWEI NXT-TL00 Build/HUAWEINXT-TL00) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.2) WindVane/8.0.0 1080X1812 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; vivo Y55A Build/MMB29M) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 720X1280 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.1.1; zh-cn; HUAWEI P7-L00 Build/HuaweiP7-L00) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1776 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0; zh-cn; HUAWEI MT7-CL00 Build/HuaweiMT7-CL00) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.1) WindVane/8.0.0 1080X1812 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.1.1; zh-cn; vivo X6SPlus D Build/LMY47V) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 7.1.1; zh-CN; ONEPLUS A3000 Build/NMF26F) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/40.0.2214.89 UCBrowser/11.4.1.939 Mobile Safari/537.36",
        "Mozilla/5.0 (Linux; U; Android 6.0; zh-cn; HUAWEI NXT-TL00 Build/HUAWEINXT-TL00) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.2.0.15) WindVane/8.0.0 1080X1821 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.1; zh-cn; MX5 Build/LMY47I) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 4.4.4; zh-cn; vivo X5Max L Build/KTU84P) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; Android 5.1.1; PLE-703L Build/HuaweiMediaPad; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/53.0.2785.49 Mobile MQQBrowser/6.2 TBS/043024 Safari/537.36 MicroMessenger/6.5.6.1020 NetType/NON_NETWORK Language/zh_CN",
        "Mozilla/5.0 (Linux; U; Android 6.0; zh-CN; HUAWEI MT7-CL00 Build/HuaweiMT7-CL00) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/40.0.2214.89 UCBrowser/11.4.5.937 Mobile Safari/537.36",
        "Mozilla/5.0 (Linux; U; Android 5.0.2; zh-cn; vivo X5Pro V Build/LRX22G) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.1; zh-cn; m2 note Build/LMY47D) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.2) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 4.4.4; zh-cn; HM NOTE 1LTE Build/KTU84P) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.3.2) WindVane/8.0.0 720X1280 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; SM-C5000 Build/MMB29M) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 4.4.2; zh-cn; H60-L01 Build/HDH60-L01) AppleWebKit/537.36 (KHTML, like Gecko)Version/4.0 Chrome/37.0.0.0 MQQBrowser/6.4 Mobile Safari/537.36",
        "Mozilla/5.0 (Linux; Android 6.0; HUAWEI NXT-AL10 Build/HUAWEINXT-AL10; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/45.0.2454.95 Mobile Safari/537.36 rabbit/1.0 baiduboxapp/7.1 (Baidu; P1 6.0)",
        "Mozilla/5.0 (Linux; U; Android 5.1.1; zh-cn; ATH-AL00 Build/HONORATH-AL00) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.1) WindVane/8.0.0 1080X1776 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0; zh-cn; HUAWEI CRR-UL00 Build/HUAWEICRR-UL00) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.4.5) WindVane/8.0.0 1080X1812 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; SM-G9250 Build/MMB29K) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1440X2560 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 7.0; zh-cn; KNT-UL10 Build/HUAWEIKNT-UL10) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.2) WindVane/8.0.0 1080X1812 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; Redmi Note 3 Build/MMB29M) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/53.0.2785.146 Mobile Safari/537.36 XiaoMi/MiuiBrowser/8.6.5",
        "Mozilla/5.0 (Linux; U; Android 6.0; zh-cn; Letv X500 Build/DBXCNOP5902012161S) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.1; zh-cn; OPPO R9t Build/LMY47I) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.1.7) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; SM-G9350 Build/MMB29M) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.4.3) WindVane/8.0.0 1440X2560 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; Mi Note 2 Build/MXB48T) AppleWebKit/537.36 (KHTML, like Gecko)Version/4.0 Chrome/37.0.0.0 MQQBrowser/7.3 Mobile Safari/537.36",
        "Mozilla/5.0 (Linux; U; Android 4.4.2; zh-cn; H60-L01 Build/HDH60-L01) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1776 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 4.4.2; zh-cn; PE-UL00 Build/HuaweiPE-UL00) AppleWebKit/537.36 (KHTML, like Gecko)Version/4.0 Chrome/37.0.0.0 MQQBrowser/7.1 Mobile Safari/537.36",
        "Mozilla/5.0 (Linux; Android 5.0.2; SM-A5000 Build/LRX22G; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/53.0.2785.49 Mobile MQQBrowser/6.2 TBS/043115 Safari/537.36 MicroMessenger/6.5.4.1000 NetType/WIFI Language/zh_CN",
        "Mozilla/5.0 (Linux; U; Android 4.4.4; zh-cn; vivo X5S L Build/KTU84P) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 720X1280 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; Android 6.0; MI 5 Build/MRA58K; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/53.0.2785.49 Mobile MQQBrowser/6.2 TBS/043115 Safari/537.36 MicroMessenger/6.5.6.1020 NetType/WIFI Language/zh_CN",
        "Mozilla/5.0 (Linux; U; Android 5.0.2; zh-cn; vivo X5M Build/LRX22G) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.1) WindVane/8.0.0 720X1280 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0; zh-cn; KNT-AL10 Build/HUAWEIKNT-AL10) AppleWebKit/537.36 (KHTML, like Gecko)Version/4.0 Chrome/37.0.0.0 MQQBrowser/6.0 Mobile Safari/537.36",
        "Mozilla/5.0 (Linux; Android 4.4.4; vivo X5Max+ Build/KTU84P) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/35.0.1916.138 Mobile Safari/537.36 T7/7.4 baiduboxapp/8.3.1 (Baidu; P1 4.4.4)",
        "Mozilla/5.0 (Linux; Android 7.0; FRD-AL10 Build/HUAWEIFRD-AL10; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/53.0.2785.49 Mobile MQQBrowser/6.2 TBS/043024 Safari/537.36 MicroMessenger/6.5.6.1020 NetType/4G Language/zh_CN",
        "Mozilla/5.0 (Linux; U; Android 5.1.1; zh-cn; E6683 Build/32.0.A.6.209) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1776 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 4.2.2; zh-cn; vivo Y17T Build/JDQ39) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.3.0) WindVane/8.0.0 720X1280 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.0.2; zh-cn; vivo X6A Build/LRX22G) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.2) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.1; zh-cn; GN5001S Build/LMY47D) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 720X1280 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.1.1; zh-cn; OPPO A33 Build/LMY47V) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 540X960 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 7.0; zh-CN; ZUK Z2121 Build/NRD90M) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/40.0.2214.89 UCBrowser/11.4.5.937 Mobile Safari/537.36",
        "Mozilla/5.0 (Linux; U; Android 5.1; zh-cn; vivo X6Plus D Build/LMY47I) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0; zh-cn; FRD-DL00 Build/HUAWEIFRD-DL00) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.2.4) WindVane/8.0.0 1080X1812 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0; zh-cn; MP1602 Build/MRA58K) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 4.4.4; zh-cn; R8107 Build/KTU84P) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; SM-N9100 Build/MMB29M) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1440X2560 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.1.1; zh-cn; MX4 Pro Build/LMY48W) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1536X2560 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; MI 5 Build/MXB48T) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.1.7) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; SM901 Build/MXB48T) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0; zh-cn; ALE-UL00 Build/HuaweiALE-UL00) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 720X1208 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; SM901 Build/MXB48T) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.1) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 4.4.4; zh-cn; SM-E7009 Build/KTU84P) AppleWebKit/537.36 (KHTML, like Gecko)Version/4.0 Chrome/37.0.0.0 MQQBrowser/7.3 Mobile Safari/537.36",
        "Mozilla/5.0 (Linux; Android 5.1; vivo X6D Build/LMY47I; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/53.0.2785.49 Mobile MQQBrowser/6.2 TBS/043124 Safari/537.36 MicroMessenger/6.5.6.1020 NetType/WIFI Language/zh_CN",
        "Mozilla/5.0 (Linux; U; Android 7.0; zh-cn; HUAWEI MLA-UL00 Build/HUAWEIMLA-UL00) AppleWebKit/537.36 (KHTML, like Gecko)Version/4.0 Chrome/37.0.0.0 MQQBrowser/7.3 Mobile Safari/537.36",
        "Mozilla/5.0 (Linux; U; Android 5.1; zh-cn; m1 note Build/LMY47D) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; Android 7.0; HUAWEI NXT-AL10 Build/HUAWEINXT-AL10) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/35.0.1916.138 Mobile Safari/537.36 T7/7.4 baiduboxapp/8.3.1 (Baidu; P1 7.0)",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; OPPO R9s Build/MMB29M) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.4.4) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; SM-G5700 Build/MMB29M) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.2.4) WindVane/8.0.0 720X1280 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 4.4.4; zh-cn; MI 4LTE Build/KTU84P) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.4.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.0.2; zh-cn; SM-A7009 Build/LRX22G) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.2.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 6.0.1; zh-cn; SM-G9200 Build/MMB29K) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1440X2560 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.0.2; zh-cn; MI NOTE Pro Build/LRX22G) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1440X2560 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 5.1; zh-cn; OPPO R9km Build/LMY47I) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.5.3) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; U; Android 7.0; zh-cn; VIE-AL10 Build/HUAWEIVIE-AL10) AppleWebKit/537.36 (KHTML, like Gecko)Version/4.0 Chrome/37.0.0.0 MQQBrowser/7.3 Mobile Safari/537.36",
        "Mozilla/5.0 (Linux; U; Android 5.0; zh-cn; vivo X5Pro D Build/LRX21M) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.4.2) WindVane/8.0.0 1080X1920 GCanvas/1.4.2.21",
        "Mozilla/5.0 (Linux; Android 5.1.1; SCL-AL00 Build/HonorSCL-AL00; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/53.0.2785.49 Mobile MQQBrowser/6.2 TBS/043024 Safari/537.36 MicroMessenger/6.5.6.1020 NetType/WIFI Language/zh_CN",
        "Mozilla/5.0 (Linux; Android 5.1.1; SCL-AL00 Build/HonorSCL-AL00; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/53.0.2785.49 Mobile MQQBrowser/6.2 TBS/043024 Safari/537.36 MicroMessenger/6.5.6.1020 NetType/4G Language/zh_CN",
        "Mozilla/5.0 (Linux; Android 7.0; EVA-TL00 Build/HUAWEIEVA-TL00) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/35.0.1916.138 Mobile Safari/537.36 T7/7.4 baiduboxapp/8.3.1 (Baidu; P1 7.0)",
        "Mozilla/5.0 (Linux; U; Android 5.0.2; zh-cn; PLK-TL00 Build/HONORPLK-TL00) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 UCBrowser/1.0.0.100 U3/0.8.0 Mobile Safari/534.30 AliApp(TB/6.4.2) WindVane/8.0.0 1080X1794 GCanvas/1.4.2.21",
                
        
    };

    public static string[] UAIOS = new string[]
    {
        "Mozilla/5.0 (iPhone; CPU iPhone OS 10_2_1 like Mac OS X) AppleWebKit/602.4.6 (KHTML, like Gecko) Mobile/14D27 baiduboxapp/0_71.0.3.8_enohpi_8022_2421/1.2.01_2C2%258enohPi/1099a/4AB84849827D7C3D4AB2C5F5DFCD9D88C56BF2BE8FCSAMIOIRL/1",
        "Mozilla/5.0 (iPhone; CPU iPhone OS 9_3_5 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Mobile/13G36 baiduboxapp/0_71.0.3.8_enohpi_4331_057/5.3.9_1C2%258enohPi/1099a/51CDB3BE07F988AFAFAC327DC01B206CE8AC20304FCHAQILTTQ/1",
        "Mozilla/5.0 (iPhone; CPU iPhone OS 10_2_1 like Mac OS X) AppleWebKit/602.4.6 (KHTML, like Gecko) Mobile/14D27 baiduboxapp/0_71.0.3.8_enohpi_8022_2421/1.2.01_2C2%258enohPi/1099a/F92A801D91A980E4286A5289FE799868264D751F8OCICLAPCTM/1",

    };
}