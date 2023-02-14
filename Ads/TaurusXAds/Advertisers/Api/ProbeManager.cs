using System;
using Advertisers.Common;
using Advertisers.Platforms;
using TaurusXAdSdk.Api;

namespace Advertisers.Api
{
    public class ProbeManager
    {
        private IProbeManager mClient;
        public ProbeManager() {
            mClient = ClientFactory.ProbeManagerInstance();
        }

        #region api
        public void init() {
            mClient.init();
        } 

        public bool getReportStatus() {
            return mClient.getReportStatus();
        }
        public void setReportStatus(bool status) {
            mClient.setReportStatus(status);
        }

        public void registerTrackListener(TrackListener listener) {
            mClient.registerTrackListener(listener);
        }

        public void unRegisterTrackListener(TrackListener listener) {
            mClient.unRegisterTrackListener(listener);
        }
     
        #endregion
    }

}