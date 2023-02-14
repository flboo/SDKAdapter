using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Qarth
{
    public class ShopCheckInfo
    {
        public List<RemoteData> remoteDatas = new List<RemoteData>();

        public class RemoteData
        {
            public string channel;
            public string version;
        }

        public bool IsCheking(string channel,string version)
        {
            RemoteData data= remoteDatas.Find((d)=>
            {
                return d.channel == channel && d.version == version;
            });
            return data != null;
        }
    }

}