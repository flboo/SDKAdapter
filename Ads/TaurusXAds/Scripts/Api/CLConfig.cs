namespace TaurusXAdSdk.Api
{
    public class CLConfig
    {
        private int mMinErrorTime = 10;
        private int mMinFreezeTime = 30;
        private float mDelayFactor = 1.5f;
        private int mMaxErrorTime = 120;
        private int mMaxFreezeTime = 240;

        private int mCacheCount = 1;

        public CLConfig()
        {
        }

        public void SetMinErrorTime(int time)
        {
            mMinErrorTime = time;
        }

        public int GetMinErrorTime()
        {
            return mMinErrorTime;
        }

        public void SetMinFreezeTime(int time)
        {
            mMinFreezeTime = time;
        }

        public int GetMinFreezeTime()
        {
            return mMinFreezeTime;
        }

        public void SetDelayFactor(float factor)
        {
            mDelayFactor = factor;
        }

        public float GetDelayFactor()
        {
            return mDelayFactor;
        }

        public void SetMaxErrorTime(int time)
        {
            mMaxErrorTime = time;
        }

        public int GetMaxErrorTime()
        {
            return mMaxErrorTime;
        }

        public void SetMaxFreezeTime(int time)
        {
            mMaxFreezeTime = time;
        }

        public int GetMaxFreezeTime()
        {
            return mMaxFreezeTime;
        }

        public void SetCacheCount(int cacheCount)
        {
            mCacheCount = cacheCount;
        }

        public int GetCacheCount()
        {
            return mCacheCount;
        }
    }
}
