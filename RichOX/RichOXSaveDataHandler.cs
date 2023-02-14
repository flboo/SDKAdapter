using System.Collections;
using System.Collections.Generic;
using Qarth;
using UnityEngine;


namespace Qarth
{
	public class RichOXSaveDataHandler : DataClassHandler<RichOXSaveData>
    {
        public static DataDirtyRecorder dataDirtyRecorder = new DataDirtyRecorder();

        public RichOXSaveDataHandler()
        {
            Load();
            EnableAutoSave();
        }
    }
	
}