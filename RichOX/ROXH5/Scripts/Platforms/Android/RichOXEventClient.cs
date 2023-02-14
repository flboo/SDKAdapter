using System;
using RichOX.Common;
using RichOX.Api;
using UnityEngine;
using System.Collections.Generic;

namespace RichOX.Platforms.Android
{
    public class RichOXEventClient : IRichOXEventClient
    {
        private string mName;
        private string mValue;
        private string mMapValue;

        public RichOXEventClient(string name, string value, string mapValue)
        {
            mName = name;
            mValue = value;
            mMapValue = mapValue;
        }

        public string GetName() {
            return mName;
        }

        public string GetValue() {
            return mValue;
        }

        public Dictionary<string, string> GetMapValue() {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            if (mMapValue.Length > 0)
            {
                JSONObject serverExtrasObject = (JSONObject)JSONNode.Parse(mMapValue);
                foreach (KeyValuePair<string, JSONNode> kv in serverExtrasObject)
                {
                    dictionary.Add(kv.Key, kv.Value);
                }
            }

            return dictionary;
        }
    }
}
