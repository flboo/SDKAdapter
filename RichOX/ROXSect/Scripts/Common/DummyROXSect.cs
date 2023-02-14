using System;
using System.Collections;
using System.Collections.Generic;

using ROXBase.Api;
using ROXSect.Api;

namespace ROXSect.Common
{
    public class DummyROXSect : IROXSect
    {
        #region IROXSect

        public void GetSectInfo(ROXInterface<SectInfo> calllback) 
        {

        }

        public void GetUserSectStatus(ROXInterface<int> calllback) 
        {

        }

        public  void GetApprenticeList(int level, ROXInterface<ApprenticeList> callback) 
        {

        }

        public void GetApprenticeList(int level, int pageSize, int currentPage, ROXInterface<ApprenticeList> callback) 
        {

        }

        public void GetApprenticeInfo(string apprenticeId, ROXInterface<ApprenticeInfo> callback) 
        {

        }

        public void GenContribution(int action, ROXInterface<Contribution> callback)
        {

        }

        public void GetContribution(string studentUid, ROXInterface<Contribution> callback)
        {

        }

        public void getAllContribution(ROXInterface<Contribution> callback)
        {

        }

        public void GetInviteAward(int count, ROXInterface<AwardInfo> callback)
        {

        }

        public void GetInviteAwardList(ROXInterface<List<InviteAward>> callback)
        {

        }

        public void BindInviter(string inviterUid, ROXInterface<bool> callback)
        {

        }

        public void GetContributionRecordByDay(int year, int month, ROXInterface<Hashtable> callback)
        {

        }

        public void GetRedPacketRecord(ROXInterface<RedPacketRecords> callback)
        {

        }

        public void GetRedPacketRecord(int pageSize, int currentPage, ROXInterface<RedPacketRecords> callback)
        {

        }

        public void Transform(ROXInterface<TransformInfo> callback)
        {

        }

        public void GetSettings(ROXInterface<SectSettings> callback)
        {

        }

        #endregion
    }
}