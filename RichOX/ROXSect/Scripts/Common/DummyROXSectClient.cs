using System;
using System.Collections;
using System.Collections.Generic;

using ROXBase.Api;
using ROXSect.Api;

namespace ROXSect.Common
{
    public class DummyROXSectClient : IROXSectClient
    {
        #region IROXSectClient

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

        public void BindInviter(string inviterUid, ROXInterface<bool> callback)
        {

        }


        public void GetSettings(ROXInterface<SectSettings> callback)
        {

        }

        public void GetInviteRanking(ROXInterface<List<SectRankingInfo>> callback)
        {

        }

        public void GetInviterCounts(long lastTime, int status, ROXInterface<int> callback){

        }

        #endregion
    }
}