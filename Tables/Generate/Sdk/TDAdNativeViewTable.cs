//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Qarth
{
    public static partial class TDAdNativeViewTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDAdNativeViewTable.Parse, "ad_native_view");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }

        private static Dictionary<string, TDAdNativeView> m_DataCache = new Dictionary<string, TDAdNativeView>();
        private static List<TDAdNativeView> m_DataList = new List<TDAdNativeView>();

        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDAdNativeView.GetFieldHeadIndex());
#if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDAdNativeView.GetFieldHeadIndex(), "AdNativeViewTable");
#endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDAdNativeView memberInstance = new TDAdNativeView();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance);
            }
            Log.i(string.Format("Parse Success TDAdNativeView"));
        }

        private static void OnAddRow(TDAdNativeView memberInstance)
        {
            string key = memberInstance.id;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDAdNativeViewTable Id already exists {0}", key));
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

        public static List<TDAdNativeView> dataList
        {
            get
            {
                return m_DataList;
            }
        }

        public static TDAdNativeView GetData(string key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDAdNativeView", key));
                return null;
            }
        }
    }
}//namespace LR