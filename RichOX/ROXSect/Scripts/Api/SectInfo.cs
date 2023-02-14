using System.Collections;
using UnityEngine;

namespace ROXSect.Api 
{
    public class SectInfo 
    {
        /// <summary>
        /// 当前弟子Id
        /// <summary>   
        public ChiefInfo Chief {set; get;}

        /// <summary>
        /// 当前弟子Id
        /// <summary>   
        public Hashtable StudentsMap {set; get;}

        public void ToString() 
        {
            if (Chief != null) {
                Debug.Log("chief info : ");
                Chief.ToString();
            }
            if (StudentsMap != null) 
            {
                foreach(string level in StudentsMap.Keys)
                {
                    Debug.Log("Level: " + level);
                    ApprenticeList list = (ApprenticeList)StudentsMap[level];
                    list.ToString();
                }
            }              
        }
    }
}