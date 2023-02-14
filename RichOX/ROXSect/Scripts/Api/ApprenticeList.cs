using System.Collections.Generic;
using UnityEngine;

namespace ROXSect.Api 
{
    public class ApprenticeList 
    {
        /// <summary>
        /// 弟子总数
        /// <summary>   
        public int Total {set; get;}

        /// <summary>
        /// 弟子总数分页显示，对应当前分页大小
        /// <summary>   
        public int PageSize {set; get;}

        /// <summary>
        /// 弟子总数分页显示，对应当前分页索引
        /// <summary>  
        public int PageIndex {set; get;}

        /// <summary>
        /// 当前弟子列表
        /// <summary>  
        public List<ApprenticeInfo> StudentList {set; get;}      

        public void ToString() 
        {
            Debug.Log("Total: " + Total);
            Debug.Log("PageSize: " + PageSize);
            Debug.Log("PageIndex: " + PageIndex);
            Debug.Log("the list of students: ");
            if (StudentList != null) 
            {
                foreach(ApprenticeInfo info in StudentList)
                {
                    info.ToString();
                }
            }
        }  
    }
}