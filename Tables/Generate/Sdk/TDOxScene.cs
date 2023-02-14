//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDOxScene
    {
        
       
        private string m_Key;   
        private string m_Name;   
        private string m_OXSceneType;   
        private EInt m_Width = 0;   
        private EInt m_Height = 0;   
        private string m_IDAndroid;   
        private string m_IDIos;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// 场景
        /// </summary>
        public  string  key {get { return m_Key; } }
       
        /// <summary>
        /// 场景
        /// </summary>
        public  string  name {get { return m_Name; } }
       
        /// <summary>
        /// 类型
        /// </summary>
        public  string  oXSceneType {get { return m_OXSceneType; } }
       
        /// <summary>
        /// 宽
        /// </summary>
        public  int  width {get { return m_Width; } }
       
        /// <summary>
        /// 高
        /// </summary>
        public  int  height {get { return m_Height; } }
       
        /// <summary>
        /// UnitIDAndroid
        /// </summary>
        public  string  iDAndroid {get { return m_IDAndroid; } }
       
        /// <summary>
        /// UnitIDIos
        /// </summary>
        public  string  iDIos {get { return m_IDIos; } }
       

        public void ReadRow(DataStreamReader dataR, int[] filedIndex)
        {
          //var schemeNames = dataR.GetSchemeName();
          int col = 0;
          while(true)
          {
            col = dataR.MoreFieldOnRow();
            if (col == -1)
            {
              break;
            }
            switch (filedIndex[col])
            { 
            
                case 0:
                    m_Key = dataR.ReadString();
                    break;
                case 1:
                    m_Name = dataR.ReadString();
                    break;
                case 2:
                    m_OXSceneType = dataR.ReadString();
                    break;
                case 3:
                    m_Width = dataR.ReadInt();
                    break;
                case 4:
                    m_Height = dataR.ReadInt();
                    break;
                case 5:
                    m_IDAndroid = dataR.ReadString();
                    break;
                case 6:
                    m_IDIos = dataR.ReadString();
                    break;
                default:
                    //TableHelper.CacheNewField(dataR, schemeNames[col], m_DataCacheNoGenerate);
                    break;
            }
          }

        }
        
        public static Dictionary<string, int> GetFieldHeadIndex()
        {
          Dictionary<string, int> ret = new Dictionary<string, int>(7);
          
          ret.Add("Key", 0);
          ret.Add("Name", 1);
          ret.Add("OXSceneType", 2);
          ret.Add("Width", 3);
          ret.Add("Height", 4);
          ret.Add("IDAndroid", 5);
          ret.Add("IDIos", 6);
          return ret;
        }
    } 
}//namespace LR