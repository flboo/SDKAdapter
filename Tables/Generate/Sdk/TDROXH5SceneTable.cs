//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDROXH5SceneTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDROXH5SceneTable.Parse, "ROXH5Scene");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<string, TDROXH5Scene> m_DataCache = new Dictionary<string, TDROXH5Scene>();
        private static List<TDROXH5Scene> m_DataList = new List<TDROXH5Scene >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDROXH5Scene.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDROXH5Scene.GetFieldHeadIndex(), "ROXH5SceneTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDROXH5Scene memberInstance = new TDROXH5Scene();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance);
            }
            Log.i(string.Format("Parse Success TDROXH5Scene"));
        }

        private static void OnAddRow(TDROXH5Scene memberInstance)
        {
            string key = memberInstance.key;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDROXH5SceneTable Id already exists {0}", key));
            }
            else
            {
                m_DataCache.Add(key, memberInstance);
                m_DataList.Add(memberInstance);
            }
        }    
        
        public static void Reload(byte[] fileData)
        {
            Parse(fileData);
        }

        public static int count
        {
            get 
            {
                return m_DataCache.Count;
            }
        }

        public static List<TDROXH5Scene> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDROXH5Scene GetData(string key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDROXH5Scene", key));
                return null;
            }
        }
    }
}//namespace LR