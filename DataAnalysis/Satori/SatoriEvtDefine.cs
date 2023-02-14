using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qarth
{
    public class SatoriEvtDefine
    {
        public enum StageUnlockType
        {
            Coins,
            Gem,
            AD,
        }

        public enum CommonResultType
        {
            Fail,
            Success,
        }

        public enum GameResultType
        {
            uncompleted,
            fail,
            success,
        }

        public enum PageClickType
        {
            to_func,
            to_page,
        }


        private static Dictionary<string, int> m_PlayerActionEvtIntervals = new Dictionary<string, int>();
        private static Dictionary<string, int> m_PlayerActionCounts = new Dictionary<string, int>();

        public static int GetActionIntervalFromType(string type)
        {
            if (!m_PlayerActionEvtIntervals.ContainsKey(type))
            {
                Log.e("call SatoriEvt_SetPlayerAction() to set interval first! error type: " + type);
                return 0;
            }
            return m_PlayerActionEvtIntervals[type];
        }

        public static void SetPlayerActionInterval(string type, int interval, bool cleanCount = false)
        {
            if (m_PlayerActionEvtIntervals.ContainsKey(type))
            {
                Log.w("same type has been set already, will update the interval: " + type);
                m_PlayerActionEvtIntervals[type] = interval;

                if (cleanCount)
                    m_PlayerActionCounts[type] = 0;
            }
            else
            {
                m_PlayerActionEvtIntervals.Add(type, interval);
                m_PlayerActionCounts.Add(type, 0);
            }
        }

        public static bool AddPlayerActionCount(string type, int count = 1)
        {
            if (!m_PlayerActionEvtIntervals.ContainsKey(type))
            {
                Log.e("call SatoriEvt_SetPlayerAction() to set interval first! error type: " + type);
                return false;
            }

            m_PlayerActionCounts[type] += count;
            if (m_PlayerActionCounts[type] >= m_PlayerActionEvtIntervals[type])
            {
                m_PlayerActionCounts[type] = 0;
                return true;
            }
            return false;
        }
    }
}