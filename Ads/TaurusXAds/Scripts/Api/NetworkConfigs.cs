using System.Collections;

namespace TaurusXAdSdk.Api
{
    public class NetworkConfigs
    {
        private ArrayList mConfigList;

        public NetworkConfigs()
        {
            mConfigList = new ArrayList();
        }

        public void AddConfig(NetworkConfig config) {
            if(config != null) {
                mConfigList.Add(config);
            }
        }

        public ArrayList GetConfigList() {
            return mConfigList;
        }
    }
}
