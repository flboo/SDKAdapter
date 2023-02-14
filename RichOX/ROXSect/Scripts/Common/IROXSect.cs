using System.Collections;
using System.Collections.Generic;
using ROXBase.Api;
using ROXSect.Api;

namespace ROXSect.Common 
{
    public interface IROXSect  
    {
        void GetSectInfo(ROXInterface<SectInfo> calllback);

        void GetUserSectStatus(ROXInterface<int> calllback);

        void GetApprenticeList(int level, ROXInterface<ApprenticeList> callback);

        void GetApprenticeList(int level, int pageSize, int currentPage, ROXInterface<ApprenticeList> callback);

        void GetApprenticeInfo(string apprenticeId, ROXInterface<ApprenticeInfo> callback);

        void GenContribution(int action, ROXInterface<Contribution> callback);

        void GetContribution(string studentUid, ROXInterface<Contribution> callback);

        void getAllContribution(ROXInterface<Contribution> callback);

        void GetInviteAward(int count, ROXInterface<AwardInfo> callback);

        void GetInviteAwardList(ROXInterface<List<InviteAward>> callback);

        void BindInviter(string inviterUid, ROXInterface<bool> callback);

        void GetContributionRecordByDay(int year, int month, ROXInterface<Hashtable> callback);

        void GetRedPacketRecord(ROXInterface<RedPacketRecords> callback);

        void GetRedPacketRecord(int pageSize, int currentPage, ROXInterface<RedPacketRecords> callback);

        void Transform(ROXInterface<TransformInfo> callback);

        void GetSettings(ROXInterface<SectSettings> callback);

    }
}