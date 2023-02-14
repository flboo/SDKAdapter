using System.Collections;
using System.Collections.Generic;
using ROXBase.Api;
using ROXSect.Api;

namespace ROXSect.Common
{
    public interface IROXSectClient
    {
        void GetSectInfo(ROXInterface<SectInfo> calllback);

        void GetUserSectStatus(ROXInterface<int> calllback);

        void GetApprenticeList(int level, ROXInterface<ApprenticeList> callback);

        void GetApprenticeList(int level, int pageSize, int currentPage, ROXInterface<ApprenticeList> callback);

        void GetApprenticeInfo(string apprenticeId, ROXInterface<ApprenticeInfo> callback);

        void GenContribution(int action, ROXInterface<Contribution> callback);

        void GetContribution(string studentUid, ROXInterface<Contribution> callback);

        void getAllContribution(ROXInterface<Contribution> callback);

        void BindInviter(string inviterUid, ROXInterface<bool> callback);

        void GetSettings(ROXInterface<SectSettings> callback);

        void GetInviteRanking(ROXInterface<List<SectRankingInfo>> callback);

        void GetInviterCounts(long lastTime, int status, ROXInterface<int> callback);
    }
}