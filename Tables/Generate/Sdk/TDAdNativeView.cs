//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Qarth
{
    public partial class TDAdNativeView
    {


        private string m_Id;
        private string m_LayoutName;
        private string m_WidthType;
        private EFloat m_Width = 0.0f;
        private string m_InteractiveArea;
        private string m_HeightType;
        private EFloat m_Height = 0.0f;

        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();

        /// <summary>
        /// ID
        /// </summary>
        public string id { get { return m_Id; } }

        /// <summary>
        /// 布局文件
        /// </summary>
        public string layoutName { get { return m_LayoutName; } }

        /// <summary>
        /// 宽度类型
        /// </summary>
        public string widthType { get { return m_WidthType; } }

        /// <summary>
        /// 宽度
        /// </summary>
        public float width { get { return m_Width; } }

        /// <summary>
        /// 可点击区
        /// </summary>
        public string interactiveArea { get { return m_InteractiveArea; } }

        /// <summary>
        /// 高度类型
        /// </summary>
        public string heightType { get { return m_HeightType; } }

        /// <summary>
        /// 高度
        /// </summary>
        public float height { get { return m_Height; } }


        public void ReadRow(DataStreamReader dataR, int[] filedIndex)
        {
            //var schemeNames = dataR.GetSchemeName();
            int col = 0;
            while (true)
            {
                col = dataR.MoreFieldOnRow();
                if (col == -1)
                {
                    break;
                }
                switch (filedIndex[col])
                {

                    case 0:
                        m_Id = dataR.ReadString();
                        break;
                    case 1:
                        m_LayoutName = dataR.ReadString();
                        break;
                    case 2:
                        m_WidthType = dataR.ReadString();
                        break;
                    case 3:
                        m_Width = dataR.ReadFloat();
                        break;
                    case 4:
                        m_InteractiveArea = dataR.ReadString();
                        break;
                    case 5:
                        m_HeightType = dataR.ReadString();
                        break;
                    case 6:
                        m_Height = dataR.ReadFloat();
                        break;
                    default:
                        //TableHelper.CacheNewField(dataR, schemeNames[col], m_DataCacheNoGenerate);
                        break;
                }
            }

        }

        public static Dictionary<string, int> GetFieldHeadIndex()
        {
            Dictionary<string, int> ret = new Dictionary<string, int>(5);

            ret.Add("Id", 0);
            ret.Add("LayoutName", 1);
            ret.Add("WidthType", 2);
            ret.Add("Width", 3);
            ret.Add("InteractiveArea", 4);
            ret.Add("HeightType", 5);
            ret.Add("Height", 6);
            return ret;
        }
    }
}//namespace LR