using ROXStrategy.Api;
using ROXBase.Api;
using System;
using System.Collections;
using System.Collections.Generic;


namespace ROXStrategy.Common
{
    public interface IROXNormal
    {
        void Init(int strategyId);
        
        void GetStrategyConfig(ROXInterface<NormalStrategyConfig> callback);

        void DoMission(string taskId, double amount, ROXInterface<NormalMissionResult> callback);

        void DoMission(string taskId, ROXInterface<NormalMissionResult> callback);

        void QueryAssetInfo(ROXInterface<NormalAssetsInfo> callback);

        void ExtremeWithdraw(string taskId , ROXInterface<bool> callback);

        void Withdraw(string taskId, String realName, String cardId, String phoneNumber, ROXInterface<bool> callback);

        void Transform(String exchangeId, double amounts, ROXInterface<NormalTransformResult> callback);

        void Transform(String exchangeId, ROXInterface<NormalTransformResult> callback);

        void QueryProgress(List<string> tasks, ROXInterface<NormalMissionsProgress> callback);

        void QueryAllProgress(ROXInterface<NormalMissionsProgress> callback);

        void DoCustomRulesMission(string taskId, string tid, ROXInterface<NormalMissionResult> callback);

        void DoCustomRulesMission(string taskId, ROXInterface<NormalMissionResult> callback);

        void ExtremeWithdrawNew(string taskId , ROXInterface<List<NormalAssetStock>> callback);

        void WithdrawNew(string taskId, String realName, String cardId, String phoneNumber, ROXInterface<List<NormalAssetStock>> callback);

        void GeneralWithdraw(string taskId, Hashtable withdrawParam, ROXInterface<List<NormalAssetStock>> callback);

        void GlobalWithdraw(string taskId, GlobalWithdrawInfo info, ROXInterface<List<NormalAssetStock>> callback);
    }
}