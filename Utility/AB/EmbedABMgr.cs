using System.Collections;
using System.Collections.Generic;
using Qarth;
using QuickEngine.Extensions;
using UnityEngine;


namespace GameWish.Game
{

    public class EmbedABMgr : TSingleton<EmbedABMgr>
    {
        private const string AB_PREKEY = "embed_prekey_";

        private Dictionary<string, string> m_Headers = new Dictionary<string, string>();
        public bool IsPullEnd = false;
        public class ABParams
        {
            public string abdes;
            public List<ABRatio> variant; //示例  abkey1 , 30 | abkey2, 30 | abkey3, 40;

            public string GetBelongVariant()
            {
                if (!string.IsNullOrEmpty(PlayerPrefs.GetString(AB_PREKEY + abdes, "")))
                {
                    return PlayerPrefs.GetString(AB_PREKEY + abdes, "");
                }


                var listint = new List<int>();
                variant.ForEach(v => { listint.Add(v.ratio); });

                var idx = GetRandomElementBWeight(listint);
                PlayerPrefs.SetString(AB_PREKEY + abdes, variant[idx].variant_name);
                return variant[idx].variant_name;
            }

            private int GetRandomElementBWeight(List<int> weight)
            {
                var list = new List<int>();
                {
                    for (int i = 0; i < weight.Count; i++)
                    {
                        for (int j = 0; j < weight[i]; j++)
                        {
                            list.Add(i);
                        }
                    }
                }
                list.Shuffle();


                return list.GetRandomElement();
            }

        }

        public struct ABRatio
        {
            public string variant_name { get; set; }
            public int ratio { get; set; }
        }

        private List<ABParams> AbParamses;
        private Dictionary<string, ABParams> ABParamsDic = new Dictionary<string, ABParams>();


        public void Init(string url, string appName, string channel = "all")
        {
            if (!m_Headers.ContainsKey("Content-Encoding"))
                m_Headers.Add("Content-Encoding", "gzip");
            else
                m_Headers["Content-Encoding"] = "gzip";

            if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(appName))
            {
                CustomExtensions.FetchRemoteConfParams(
                    appName,
                    "embed_ab_params",
                    OnRemoteValueFetched,
                    null,
                    channel,
                    url,
                    m_Headers);
            }
        }

        void OnRemoteValueFetched(string value)
        {
            AbParamses = LitJson.JsonMapper.ToObject<List<ABParams>>(value);

            if (AbParamses != null)
            {
                foreach (var t in AbParamses)
                {
                    ABParamsDic.Add(t.abdes, t);
                    //DataAnalysisMgr.S.EmbedAbTestLog(t.abdes,t.GetBelongVariant());
                    //PlayerPrefs.SetString(AB_PREKEY+ t.abdes, t.);
                }
            }
            IsPullEnd = true;
            EventSystem.S.Send(SDKEventID.OnEmbedAbPullEnd);
        }

        public string GetVariantByDesc(string desc)
        {
            if (ABParamsDic.ContainsKey(desc))
            {
                return ABParamsDic[desc].GetBelongVariant();
            }

            return "default";
        }
    }


}