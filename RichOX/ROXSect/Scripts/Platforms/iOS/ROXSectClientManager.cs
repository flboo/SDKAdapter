using System;
using ROXBase.Api;
using ROXSect.Api;
using ROXSect.Common;
using AOT;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace ROXSect.Platforms.iOS
{
    public class ROXSectClientManager : IROXSectClient
    {
        static ROXSectClientManager sInstance = new ROXSectClientManager();

        public static ROXSectClientManager Instance
        {
            get
            {
                return sInstance;
            }
        }

        #region callback types

        internal delegate void RichOXSectGetSectInfoCallback(IntPtr sectInfoPtr);
        internal delegate void RichOXSectGetApprenticeListCallback(IntPtr apprenticeList);
        internal delegate void RichOXSectGetApprenticeInfoCallback(IntPtr apprenticeInfo);
        internal delegate void RichOXSectGenContributionCallback(int contribution);
        internal delegate void RichOXSectGetContributionCallback(int contribution, int deltaContribution);
        internal delegate void RichOXSectGetInviteCountCallback(int count);
        internal delegate void RichOXSectGetRankingListCallback(IntPtr arrayPtr);
        internal delegate void RichOXSectGetSettingCallback(IntPtr settingPtr);

        internal delegate void RichOXSectGetSectStatusCallback(int status);

        internal delegate void RichOXFailureCallback(int code, string message);

        #endregion

        private ROXInterface<SectInfo> OnGetSectInfoCallback;
        private ROXInterface<ApprenticeList> OnGetApprenticeListCallback;
        private ROXInterface<ApprenticeInfo> OnGetApprenticeInfoCallback;
        private ROXInterface<Contribution> OnGenContributionCallback;
        private ROXInterface<Contribution> OnGetContributionCallback;
        private ROXInterface<SectSettings> OnGetSettingCallback;

        private ROXInterface<List<SectRankingInfo>> OnGetInviteRankingCallback;
        private ROXInterface<int> OnGetInviterCountsCallback;

        private ROXInterface<int> OnGetStatusCallback;

        public void GetSectInfo(ROXInterface<SectInfo> callback)
        {
            OnGetSectInfoCallback = callback;
            Externs.RichOXSectGetSectInfo(getSectInfoSuccessCallback, getSectInfoFailedCallback);
        }

        public void GetUserSectStatus(ROXInterface<int> callback)
        {
            OnGetStatusCallback = callback;
            Externs.RichOXSectGetSectStatus(getSectStatusSuccessCallback, getSectStatusFailedCallback);
        }

        public void GetApprenticeList(int level, ROXInterface<ApprenticeList> callback)
        {
            OnGetApprenticeListCallback = callback;
            Externs.RichOXSectGetApprenticeList(level, 0, 0, getApprenticeListSuccessCallback, getApprenticeListFailedCallback);
        }

        public void GetApprenticeList(int level, int pageSize, int currentPage, ROXInterface<ApprenticeList> callback)
        {
            OnGetApprenticeListCallback = callback;
            Externs.RichOXSectGetApprenticeList(level, pageSize, currentPage, getApprenticeListSuccessCallback, getApprenticeListFailedCallback);
        }

        public void GetApprenticeInfo(string apprenticeId, ROXInterface<ApprenticeInfo> callback)
        {
            OnGetApprenticeInfoCallback = callback;
            Externs.RichOXSectGetApprenticeInfo(apprenticeId, getApprenticeInfoSuccessCallback, getApprenticeInfoFailedCallback);
        }

        public void GenContribution(int action, ROXInterface<Contribution> callback)
        {
            OnGenContributionCallback = callback;
            Externs.RichOXSectGenContribution(action, genContributionSuccessCallback, genContributionFailedCallback);
        }

        public void GetContribution(string studentUid, ROXInterface<Contribution> callback)
        {
            OnGetContributionCallback = callback;
            Externs.RichOXSectGetContribution(studentUid, getContributionSuccessCallback, getContributionFailedCallback);
        }

        public void getAllContribution(ROXInterface<Contribution> callback)
        {
            OnGetContributionCallback = callback;
            Externs.RichOXSectGetContribution(null, getContributionSuccessCallback, getContributionFailedCallback);
        }

        public void BindInviter(string inviterUid, ROXInterface<bool> callback)
        {

        }

        public void GetSettings(ROXInterface<SectSettings> callback)
        {
            OnGetSettingCallback = callback;
            Externs.RichOXSectGetSetting(getSettingSuccessCallback, getSettingFailedCallback);
        }

        public void GetInviteRanking(ROXInterface<List<SectRankingInfo>> callback)
        {
            OnGetInviteRankingCallback = callback;
            Externs.RichOXSectGetRankingList(getRankListSuccessCallback, getRankListFailedCallback);
        }

        public void GetInviterCounts(long lastTime, int status, ROXInterface<int> callback)
        {
            OnGetInviterCountsCallback = callback;
            Externs.RichOXSectGetInviteCount(lastTime, status==1, getInviteCountSuccessCallback, getInviteCountFailedCallback);
        }

        private static ApprenticeList transApprenticeListFromPtr(IntPtr apprenticeListPtr)
        {
            ApprenticeList apprenticeList = new ApprenticeList();
            apprenticeList.Total = Externs.RichOXSectApprenticeListTypeGetTotal(apprenticeListPtr);
            apprenticeList.PageSize = Externs.RichOXSectApprenticeListTypeGetPageSize(apprenticeListPtr);
            apprenticeList.PageIndex = Externs.RichOXSectApprenticeListTypeGetCurrentPage(apprenticeListPtr);

            IntPtr listObject = Externs.RichOXSectApprenticeListTypeGetApprenticeDataArray(apprenticeListPtr);
            if (listObject != null)
            {
                List<ApprenticeInfo> apprenticeInfoList = new List<ApprenticeInfo>();
                int size = Externs.RichOXSectApprenticeDataTypeArrayGetCount(listObject);
                for (int i = 0; i < size; i++)
                {
                    IntPtr apprenticeInfoObject = Externs.RichOXSectApprenticeDataTypeArrayGetItem(listObject, i);
                    ApprenticeInfo apprenticeInfo = new ApprenticeInfo();
                    apprenticeInfo.StudentUid = Externs.RichOXSectApprenticeDataGetApprenticeUId(apprenticeInfoObject);
                    apprenticeInfo.HasVerified = Externs.RichOXSectApprenticeDataGetVerified(apprenticeInfoObject);
                    apprenticeInfo.UnclaimedReward = Externs.RichOXSectApprenticeDataGetContribution(apprenticeInfoObject);
                    apprenticeInfo.TotalReward = Externs.RichOXSectApprenticeDataGetTotalContribution(apprenticeInfoObject);
                    apprenticeInfo.NickName = Externs.RichOXSectApprenticeDataGetNickName(apprenticeInfoObject);
                    apprenticeInfo.Avatar = Externs.RichOXSectApprenticeDataGetAvatar(apprenticeInfoObject);

                    Hashtable dailyReward = new Hashtable();
                    IntPtr dailyRewardObject = Externs.RichOXSectApprenticeDataGetDailyContribution(apprenticeInfoObject);
                    if (dailyRewardObject != null)
                    {
                        IntPtr keysPtr = Externs.RichOXSectDictionaryTypeGetAllKeys(dailyRewardObject);
                        if (keysPtr != null)
                        {
                            int count = Externs.RichOXSectArrayTypeGetCount(keysPtr);
                            for (int j = 0; j < count; j++)
                            {
                                string key = Externs.RichOXSectArrayTypeGetKeyItem(keysPtr, j);
                                int value = Externs.RichOXSectDictionaryTypeGetIntValue(dailyRewardObject, key);

                                dailyReward.Add(key, value);
                            }
                        }
                    }
                    apprenticeInfoList.Add(apprenticeInfo);
                }
                apprenticeList.StudentList = apprenticeInfoList;
            }
            return apprenticeList;
        }


        [MonoPInvokeCallback(typeof(RichOXSectGetSectInfoCallback))]
        private static void getSectInfoSuccessCallback(IntPtr sectData)
        {
            if (Instance.OnGetSectInfoCallback != null)
            {
                SectInfo sectInfo = new SectInfo();
                ChiefInfo chiefInfo = new ChiefInfo();
                chiefInfo.MasterUid = Externs.RichOXSectDataTypeGetMasterUId(sectData);
                chiefInfo.TeacherUid = Externs.RichOXSectDataTypeGetTeacherUId(sectData);
                chiefInfo.HasVerified = Externs.RichOXSectDataTypeGetVerified(sectData);
                chiefInfo.CurrentReward = Externs.RichOXSectDataTypeGetContribution(sectData);
                chiefInfo.TongLevel = Externs.RichOXSectDataTypeGetLevel(sectData);
                chiefInfo.TotalStudents = Externs.RichOXSectDataTypeGetInviteApprenticeCount(sectData);
                chiefInfo.TotalVerifiedStudents = Externs.RichOXSectDataTypeGetVerifiedApprenticeCount(sectData);
                chiefInfo.TransformCounts = Externs.RichOXSectDataTypeGetTransformCount(sectData);
                chiefInfo.TransformPacketCounts = Externs.RichOXSectDataTypeGetTimesPacketCount(sectData);

                IntPtr inviteAwardPtr = Externs.RichOXSectDataTypeGetInviteAwardInfo(sectData);
                if (inviteAwardPtr != null)
                {
                    Hashtable table = new Hashtable();

                    IntPtr keysPtr = Externs.RichOXSectDictionaryTypeGetAllKeys(inviteAwardPtr);
                    if (keysPtr != null)
                    {
                        int count = Externs.RichOXSectArrayTypeGetCount(keysPtr);
                        for (int i = 0; i < count; i++)
                        {
                            string key = Externs.RichOXSectArrayTypeGetKeyItem(keysPtr, i);
                            string value = Externs.RichOXSectDictionaryTypeGetStringValue(inviteAwardPtr, key);
                            table.Add(key, value);
                        }
                    }
                    chiefInfo.InviteAwardMap = table;
                }
                sectInfo.Chief = chiefInfo;

                IntPtr studentMapObject = Externs.RichOXSectDataTypeGetApprenticeList(sectData);
                if (studentMapObject != null)
                {
                    Hashtable studengTable = new Hashtable();
                    IntPtr keysPtr = Externs.RichOXSectDictionaryTypeGetAllKeys(studentMapObject);
                    if (keysPtr != null)
                    {
                        int count = Externs.RichOXSectArrayTypeGetCount(keysPtr);
                        for (int i = 0; i < count; i++)
                        {
                            string key = Externs.RichOXSectArrayTypeGetKeyItem(keysPtr, i);
                            IntPtr apprenticeListPtr = Externs.RichOXSectDictionaryTypeGetApprenticeListValue(studentMapObject, key);
                            ApprenticeList apprenticeList = transApprenticeListFromPtr(apprenticeListPtr);
                            studengTable.Add(key, apprenticeList);
                        }
                    }
                    sectInfo.StudentsMap = studengTable;
                }


                Instance.OnGetSectInfoCallback.OnSuccess(sectInfo);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXFailureCallback))]
        public static void getSectInfoFailedCallback(int code, string message)
        {
            if (Instance.OnGetSectInfoCallback != null)
            {
                Instance.OnGetSectInfoCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXSectGetApprenticeListCallback))]
        private static void getApprenticeListSuccessCallback(IntPtr apprenticeListPtr)
        {
            if (Instance.OnGetApprenticeListCallback != null)
            {
                ApprenticeList apprenticeList = transApprenticeListFromPtr(apprenticeListPtr);

                Instance.OnGetApprenticeListCallback.OnSuccess(apprenticeList);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXFailureCallback))]
        public static void getApprenticeListFailedCallback(int code, string message)
        {
            if (Instance.OnGetApprenticeListCallback != null)
            {
                Instance.OnGetApprenticeListCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXSectGetApprenticeInfoCallback))]
        private static void getApprenticeInfoSuccessCallback(IntPtr apprenticeInfoPtr)
        {
            if (Instance.OnGetApprenticeInfoCallback != null)
            {
                ApprenticeInfo apprenticeInfo = new ApprenticeInfo();
                apprenticeInfo.StudentUid = Externs.RichOXSectApprenticeDataGetApprenticeUId(apprenticeInfoPtr);
                apprenticeInfo.TeacherUid = Externs.RichOXSectApprenticeInfoTypeGetTeacherUId(apprenticeInfoPtr);
                apprenticeInfo.HasVerified = Externs.RichOXSectApprenticeDataGetVerified(apprenticeInfoPtr);
                apprenticeInfo.UnclaimedReward = Externs.RichOXSectApprenticeDataGetContribution(apprenticeInfoPtr);
                apprenticeInfo.TotalReward = Externs.RichOXSectApprenticeDataGetTotalContribution(apprenticeInfoPtr);
                apprenticeInfo.CurrentLevel = Externs.RichOXSectApprenticeInfoTypeGetLevel(apprenticeInfoPtr);
                apprenticeInfo.TotalStudents = Externs.RichOXSectApprenticeInfoTypeGetInvitedApprenticeCount(apprenticeInfoPtr);
                apprenticeInfo.NickName = Externs.RichOXSectApprenticeDataGetNickName(apprenticeInfoPtr);
                apprenticeInfo.Avatar = Externs.RichOXSectApprenticeDataGetAvatar(apprenticeInfoPtr);

                Hashtable dailyReward = new Hashtable();
                IntPtr dailyRewardObject = Externs.RichOXSectApprenticeDataGetDailyContribution(apprenticeInfoPtr);
                if (dailyRewardObject != null)
                {
                    IntPtr keysPtr = Externs.RichOXSectDictionaryTypeGetAllKeys(dailyRewardObject);
                    if (keysPtr != null)
                    {
                        int count = Externs.RichOXSectArrayTypeGetCount(keysPtr);
                        for (int i = 0; i < count; i++)
                        {
                            string key = Externs.RichOXSectArrayTypeGetKeyItem(keysPtr, i);
                            int value = Externs.RichOXSectDictionaryTypeGetIntValue(dailyRewardObject, key);

                            dailyReward.Add(key, value);
                        }
                    }
                }
                apprenticeInfo.DailyRewardMap = dailyReward;

                Instance.OnGetApprenticeInfoCallback.OnSuccess(apprenticeInfo);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXFailureCallback))]
        public static void getApprenticeInfoFailedCallback(int code, string message)
        {
            if (Instance.OnGetApprenticeInfoCallback != null)
            {
                Instance.OnGetApprenticeInfoCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXSectGenContributionCallback))]
        private static void genContributionSuccessCallback(int contribution)
        {
            if (Instance.OnGenContributionCallback != null)
            {
                Contribution contributionObj = new Contribution();
                contributionObj.TotalContribution = contribution;

                Instance.OnGenContributionCallback.OnSuccess(contributionObj);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXFailureCallback))]
        public static void genContributionFailedCallback(int code, string message)
        {
            if (Instance.OnGenContributionCallback != null)
            {
                Instance.OnGenContributionCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXSectGetContributionCallback))]
        private static void getContributionSuccessCallback(int contribution, int deltaContribution)
        {
            if (Instance.OnGetContributionCallback != null)
            {
                Contribution contributionObj = new Contribution();
                contributionObj.TotalContribution = contribution;
                contributionObj.ReceivedContribution = deltaContribution;

                Instance.OnGetContributionCallback.OnSuccess(contributionObj);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXFailureCallback))]
        public static void getContributionFailedCallback(int code, string message)
        {
            if (Instance.OnGetContributionCallback != null)
            {
                Instance.OnGetContributionCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXSectGetSettingCallback))]
        private static void getSettingSuccessCallback(IntPtr settingPtr)
        {
            if (Instance.OnGetSettingCallback != null)
            {
                SectSettings sectSettings = new SectSettings();
                sectSettings.Hierarchy = Externs.RichOXSectSettingDataTypeGetHierarchy(settingPtr);
                sectSettings.TransformContribution = Externs.RichOXSectSettingDataTypeGetTransformContribution(settingPtr);
                sectSettings.MaxContributionLeft = Externs.RichOXSectSettingDataTypeGetMaxPoolContribution(settingPtr);

                IntPtr transformStepObject = Externs.RichOXSectSettingDataTypeGetTransfromSteps(settingPtr);
                if (transformStepObject != null)
                {
                    Hashtable stepTables = new Hashtable();
                    int count = Externs.RichOXSectTransformStepArrayTypeGetCount(transformStepObject);
                    for (int i = 0; i < count; i++)
                    {
                        IntPtr step = Externs.RichOXSectTransformStepArrayTypeGetItem(transformStepObject, i);
                        int stepKey = Externs.RichOXSectTransformStepTypeGetStep(step);
                        int stepStar = Externs.RichOXSectTransformStepTypeGetTransformContribution(step);
                        stepTables.Add(stepKey, stepStar);
                    }
                    sectSettings.HashSteps = stepTables;
                }

                // IntPtr transformTimeObject = Externs.
                // if (transformTimeObject != null) 
                // {
                //     Hashtable timeTables = new Hashtable();
                //     AndroidJavaObject timeKeySetObject = transformTimeObject.Call<AndroidJavaObject>("keySet");
                //     AndroidJavaObject timeIteratorObject = timeKeySetObject.Call<AndroidJavaObject>("iterator");
                //     while(timeIteratorObject.Call<bool>("hasNext"))
                //     {
                //         AndroidJavaObject transformkeyObject = timeIteratorObject.Call<AndroidJavaObject>("next");
                //         int timeKey = transformkeyObject.Call<int>("intValue");
                //         // AndroidJavaObject vauleObject = transformStepObject.Call<AndroidJavaObject>("get", transformkeyObject);
                //         // stepTables.Add(stepKey, vauleObject.Call<int>("intValue"));
                //         double[] numberList = transformTimeObject.Call<double []>("get", transformkeyObject);
                //         timeTables.Add(timeKey, numberList);
                //     } 

                //     // HashSet<int> timeKeys = transformTimeObject.Call<HashSet<int>>("getTransformTimesMap");

                //     // foreach(int timeKey in timeKeys)
                //     // {
                //     //     double[] numberList = transformTimeObject.Call<double []>("get", timeKey);
                //     //     timeTables.Add(timeKey, numberList);
                //     // }
                //     sectSettings.TransformTimesMap = timeTables;
                // }
                IntPtr GradesObject = Externs.RichOXSectSettingDataTypeGetGrades(settingPtr);
                if (GradesObject != null)
                {

                    int gradesCount = Externs.RichOXSectArrayTypeGetCount(GradesObject);
                    int[] grades = new int[gradesCount];
                    for (int i = 0; i < gradesCount; i++)
                    {
                        grades[i] = Externs.RichOXSectArrayTypeGetIntItem(GradesObject, i);
                    }
                    sectSettings.Grades = grades;
                }

                IntPtr awardListObject = Externs.RichOXSectSettingDataTypeGetInviteAwardsSetting(settingPtr);
                if (awardListObject != null)
                {
                    int size = Externs.RichOXSectInviteAwardsSettingDataArrayTypeGetCount(awardListObject);
                    List<InviteAward> list = new List<InviteAward>();
                    for (int i = 0; i < size; i++)
                    {
                        IntPtr awardObject = Externs.RichOXSectInviteAwardsSettingDataArrayTypeGetItem(awardListObject, i);
                        InviteAward inviteAward = new InviteAward();
                        inviteAward.Level = Externs.RichOXSectInviteAwardsSettingDataTypeGetCount(awardObject).ToString();
                        inviteAward.AwardType = Externs.RichOXSectInviteAwardsSettingDataTypeGetAwardType(awardObject);
                        inviteAward.AwardAmount = Externs.RichOXSectInviteAwardsSettingDataTypeGetAwardValue(awardObject);
                        Debug.Log("inviteAward.AwardAmount: " + inviteAward.AwardAmount);
                        list.Add(inviteAward);
                    }
                    sectSettings.AwardSettingsList = list;
                }

                Instance.OnGetSettingCallback.OnSuccess(sectSettings);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXFailureCallback))]
        public static void getSettingFailedCallback(int code, string message)
        {
            if (Instance.OnGetSettingCallback != null)
            {
                Instance.OnGetSettingCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXSectGetRankingListCallback))]
        private static void getRankListSuccessCallback(IntPtr arrayPtr)
        {
            if (Instance.OnGetInviteRankingCallback != null)
            {
                if (arrayPtr != null)
                {
                    int size = Externs.RichOXSectRankingObjectTypeArrayGetCount(arrayPtr);
                    List<SectRankingInfo> list = new List<SectRankingInfo>();
                    for (int i = 0; i < size; i++)
                    {
                        IntPtr rankPtr = Externs.RichOXSectRankingObjectTypeArrayGetItem(arrayPtr, i);
                        SectRankingInfo rank = new SectRankingInfo();
                        rank.Index = Externs.RichOXSectRankingObjectTypeGetIndex(rankPtr);
                        rank.MasterId = Externs.RichOXSectRankingObjectTypeGetMasterUId(rankPtr);
                        rank.Counts = Externs.RichOXSectRankingObjectTypeGetInviteCount(rankPtr);
                        list.Add(rank);
                    }

                    Instance.OnGetInviteRankingCallback.OnSuccess(list);
                }
            }
        }

        [MonoPInvokeCallback(typeof(RichOXFailureCallback))]
        public static void getRankListFailedCallback(int code, string message)
        {
            if (Instance.OnGetInviteRankingCallback != null)
            {
                Instance.OnGetInviteRankingCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXSectGetInviteCountCallback))]
        private static void getInviteCountSuccessCallback(int inviteCount)
        {
            if (Instance.OnGetInviterCountsCallback != null)
            {
                Instance.OnGetInviterCountsCallback.OnSuccess(inviteCount);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXFailureCallback))]
        public static void getInviteCountFailedCallback(int code, string message)
        {
            if (Instance.OnGetInviterCountsCallback != null)
            {
                Instance.OnGetInviterCountsCallback.OnFailed(code, message);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXSectGetSectStatusCallback))]
        private static void getSectStatusSuccessCallback(int status)
        {
            if (Instance.OnGetStatusCallback != null)
            {
                Instance.OnGetStatusCallback.OnSuccess(status);
            }
        }

        [MonoPInvokeCallback(typeof(RichOXFailureCallback))]
        public static void getSectStatusFailedCallback(int code, string message)
        {
            if (Instance.OnGetStatusCallback != null)
            {
                Instance.OnGetStatusCallback.OnFailed(code, message);
            }
        }
    }
}