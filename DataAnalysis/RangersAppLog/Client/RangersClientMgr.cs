using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qarth
{
    public class RangersClientMgr : TSingleton<RangersClientMgr>
    {
        private IRangersClient m_RangersInstance;
        public IRangersClient GetInstance()
        {
            if (m_RangersInstance == null)
            {
#if UNITY_EDITOR
                m_RangersInstance = new RangersDummyClient();
#elif UNITY_ANDROID
            m_RangersInstance = new RangersAndroidClient();
#else
            m_RangersInstance = new RangersDummyClient();
#endif
            }
            return m_RangersInstance;
        }
    }
}