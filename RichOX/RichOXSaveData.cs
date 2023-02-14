using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Qarth
{
	public class RichOXSaveData : IDataClass
    {
        public string userId;

		public RichOXSaveData()
        {
            SetDirtyRecorder(RichOXSaveDataHandler
                .dataDirtyRecorder);
        }

        public override void InitWithEmptyData()
        {
            userId = "";
            SetDataDirty();
        }

        public override void OnDataLoadFinish()
        {
            
        }

        public void SetUserId(string id)
        {
            userId = id;
            SetDataDirty();
        }
    }
	
}