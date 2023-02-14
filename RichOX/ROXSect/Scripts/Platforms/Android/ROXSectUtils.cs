using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROXSect.Api;
using System;
using System.Runtime.InteropServices;

namespace ROXSect.Platforms.Android
{
    public static class ROXSectUtils
    {
        public static SectInfo GenerateSectInfo(AndroidJavaObject androidObject) 
        {
            SectInfo sectInfo = new SectInfo();
            AndroidJavaObject chiefInfoObject = androidObject.Call<AndroidJavaObject>("getChiefInfo");
            ChiefInfo chiefInfo = new ChiefInfo();
            chiefInfo.MasterUid = chiefInfoObject.Call<string>("getSectUid");
            chiefInfo.TeacherUid = chiefInfoObject.Call<string>("getMasterUid");
            chiefInfo.HasVerified = chiefInfoObject.Call<bool>("isHasVerified");
            chiefInfo.CurrentReward = chiefInfoObject.Call<int>("getCurrentContribution");
            chiefInfo.TongLevel = chiefInfoObject.Call<int>("getSectLevel");
            chiefInfo.TotalStudents = chiefInfoObject.Call<int>("getTotalApprentices");
            chiefInfo.TotalVerifiedStudents = chiefInfoObject.Call<int>("getTotalVerifiedApprentices");
            chiefInfo.TransformCounts = chiefInfoObject.Call<int>("getTransformCounts");
            chiefInfo.TransformPacketCounts = chiefInfoObject.Call<int>("getTransformPacketCounts");

            AndroidJavaObject levelRewardMap = chiefInfoObject.Call<AndroidJavaObject>("getInviteAwardMap");
            if (levelRewardMap != null) {
                Hashtable table = new Hashtable();
                AndroidJavaObject keysObject = levelRewardMap.Call<AndroidJavaObject>("keySet");
                AndroidJavaObject iteratorObject = keysObject.Call<AndroidJavaObject>("iterator");
                while(iteratorObject.Call<bool>("hasNext"))
                {
                    string keyInvite = iteratorObject.Call<string>("next");
                    table.Add(keyInvite, levelRewardMap.Call<bool>("get", keyInvite));
                }               
                chiefInfo.InviteAwardMap = table;               
            }
            sectInfo.Chief = chiefInfo;

            AndroidJavaObject studentMapObject = androidObject.Call<AndroidJavaObject>("getApprenticeMap");
            if (studentMapObject != null) {                
                Hashtable studengTable = new Hashtable();
                AndroidJavaObject studentKeysObject = studentMapObject.Call<AndroidJavaObject>("keySet");
                AndroidJavaObject iteratorObject = studentKeysObject.Call<AndroidJavaObject>("iterator");
                while(iteratorObject.Call<bool>("hasNext"))
                {
                    string keyLevel = iteratorObject.Call<string>("next");                    
                    AndroidJavaObject ApprenticeListObject = studentMapObject.Call<AndroidJavaObject>("get", keyLevel);
                    ApprenticeList apprenticeList = GenerateApprenticeList(ApprenticeListObject);
                    studengTable.Add(keyLevel, apprenticeList);
                } 
                sectInfo.StudentsMap = studengTable;               
            } 
            return sectInfo;        
        }

        public static ApprenticeList GenerateApprenticeList(AndroidJavaObject androidObject)
        {
            ApprenticeList apprenticeList = new ApprenticeList();
            int total = androidObject.Call<int>("getTotal");
            int pageSize = androidObject.Call<int>("getPageSize");
            int pageIndex = androidObject.Call<int>("getPageIndex");
            apprenticeList.Total = total;
            apprenticeList.PageSize = pageSize;
            apprenticeList.PageIndex = pageIndex;

            AndroidJavaObject listObject = androidObject.Call<AndroidJavaObject>("getApprenticeList");
            if (listObject != null) 
            {
                List<ApprenticeInfo> apprenticeInfoList = new List<ApprenticeInfo>();
                int size = listObject.Call<int>("size");
                for (int i=0; i<size; i++) 
                {
                    AndroidJavaObject apprenticeInfoObject = listObject.Call<AndroidJavaObject>("get", i);

                    ApprenticeInfo apprenticeInfo = GenerateApprenticeInfo(apprenticeInfoObject);
                    apprenticeInfoList.Add(apprenticeInfo);
                }
                apprenticeList.StudentList = apprenticeInfoList;
            }
            return apprenticeList;
        }

        public static ApprenticeInfo GenerateApprenticeInfo(AndroidJavaObject androidObject) 
        {
            ApprenticeInfo apprenticeInfo = new ApprenticeInfo();
            apprenticeInfo.StudentUid =  androidObject.Call<string>("getApprenticeUid");
            apprenticeInfo.TeacherUid =  androidObject.Call<string>("getMasterUid");
            apprenticeInfo.HasVerified =  androidObject.Call<bool>("isHasVerified");
            apprenticeInfo.UnclaimedReward =  androidObject.Call<int>("getUnclaimedContribution");
            apprenticeInfo.TotalReward =  androidObject.Call<int>("getTotalContribution");
            apprenticeInfo.CurrentLevel =  androidObject.Call<int>("getCurrentLevel");
            apprenticeInfo.NickName = androidObject.Call<string>("getName");
            apprenticeInfo.Avatar = androidObject.Call<string>("getAvatar");
            apprenticeInfo.TotalStudents =  androidObject.Call<int>("getTotalStudents");

            Hashtable dailyReward = new Hashtable();
            AndroidJavaObject dailyRewardObject  =  androidObject.Call<AndroidJavaObject>("getDailyRewardMap");
            if (dailyRewardObject != null) 
            {
                AndroidJavaObject keySetObject = dailyRewardObject.Call<AndroidJavaObject>("keySet");
                AndroidJavaObject iteratorObject = keySetObject.Call<AndroidJavaObject>("iterator");
                while(iteratorObject.Call<bool>("hasNext"))
                {
                    string dailyKey = iteratorObject.Call<string>("next");
                    AndroidJavaObject amountObject = dailyRewardObject.Call<AndroidJavaObject>("get", dailyKey); 
                    dailyReward.Add(dailyKey, amountObject.Call<int>("intValue"));                            
                }                
                apprenticeInfo.DailyRewardMap = dailyReward;
            }
            return apprenticeInfo;
        }

        public static Contribution GenerateContribution(AndroidJavaObject androidObject) 
        {
            if (androidObject != null) 
            {   
                Contribution contribution = new Contribution();            
                contribution.TotalContribution = androidObject.Call<int>("getTotalContribution");
                contribution.ReceivedContribution = androidObject.Call<int>("getReceivedContribution");
                return contribution;
            }
            return null;
        }

        public static AwardInfo GenerateAwardInfo(AndroidJavaObject androidObject) 
        {
            if (androidObject != null) 
            {
                AwardInfo awardInfo = new AwardInfo();
                awardInfo.AwardType = androidObject.Call<int>("getAwardType");
                awardInfo.TotalContribution = androidObject.Call<int>("getTotalContribution");
                awardInfo.ReceivedAccount = androidObject.Call<int>("getReceivedAccount");
                awardInfo.TotalCash = androidObject.Call<double>("getTotalCash");
                return awardInfo;
            }
            return null;
        }

        public static List<InviteAward> GenerateInviteAwardList(AndroidJavaObject androidObject)
        {
            if (androidObject != null) 
            {                
                int size = androidObject.Call<int>("size");
                List<InviteAward> list = new List<InviteAward>();
                for (int i=0; i<size; i++) 
                {
                    AndroidJavaObject awardObject = androidObject.Call<AndroidJavaObject>("get", i);
                    InviteAward inviteAward = new InviteAward();
                    inviteAward.Level = awardObject.Call<string>("getInvitedLevel");
                    inviteAward.AwardType = awardObject.Call<int>("getAwardType");
                    inviteAward.AwardAmount = awardObject.Call<double>("getAwardAmount");
                    list.Add(inviteAward);
                }
                return list;
            }
            return null;
        } 

        public static Hashtable GenerateContributionRecordTable(AndroidJavaObject androidObject) 
        {
            if (androidObject != null) 
            {
                Hashtable resultTable = new Hashtable();
                AndroidJavaObject keySetObject = androidObject.Call<AndroidJavaObject>("keySet");
                AndroidJavaObject iteratorObject = keySetObject.Call<AndroidJavaObject>("iterator");
                while(iteratorObject.Call<bool>("hasNext"))
                {
                    string key = iteratorObject.Call<string>("next");
                    AndroidJavaObject recordObject = androidObject.Call<AndroidJavaObject>("get", key);
                    ContributionRecord contributionRecord = new ContributionRecord();
                    contributionRecord.Total = recordObject.Call<int>("getTotal");
                    AndroidJavaObject table = recordObject.Call<AndroidJavaObject>("getRecordMap");
                    if (table != null) 
                    {  
                        Hashtable rewardMap = new Hashtable();
                        AndroidJavaObject rewardKeySetObject = table.Call<AndroidJavaObject>("keySet");
                        AndroidJavaObject rewardIteratorObject = rewardKeySetObject.Call<AndroidJavaObject>("iterator");
                        while(rewardIteratorObject.Call<bool>("hasNext"))
                        {
                            string rewardKey = rewardIteratorObject.Call<string>("next");
                            AndroidJavaObject amountObject = table.Call<AndroidJavaObject>("get", rewardKey); 
                            rewardMap.Add(rewardKey, amountObject.Call<int>("intValue"));  
                        }                        
                        contributionRecord.RecordMap = rewardMap;
                    }
                    resultTable.Add(key, contributionRecord);
                }               
                return resultTable;
            }
            return null;
        }

        public static RedPacketRecords GenerateRedPacketRecords(AndroidJavaObject androidObject) 
        {
            if (androidObject != null) 
            {
                RedPacketRecords redPacketRecords = new RedPacketRecords();
                redPacketRecords.Total = androidObject.Call<int>("getTotal");
                redPacketRecords.PageSize = androidObject.Call<int>("getPageSize");
                redPacketRecords.CurrentPage = androidObject.Call<int>("getCurrentPage");

                AndroidJavaObject listObject = androidObject.Call<AndroidJavaObject>("getRecordList");
                if (listObject != null) 
                {
                    List<Item> list = new List<Item>();
                    int size = listObject.Call<int>("size");
                    for (int index = 0; index < size; index++)
                    {
                        AndroidJavaObject itemObject = listObject.Call<AndroidJavaObject>("get", index);
                        Item item = new Item();
                        item.MasterId = itemObject.Call<string>("getMasterId");
                        item.Type = itemObject.Call<int>("getType");
                        item.TongLevel = itemObject.Call<int>("getSectLevel");
                        item.Amount = itemObject.Call<double>("getAmount");
                        item.TimePacketCount = itemObject.Call<int>("getTimePacketCount");
                        item.TransformCount = itemObject.Call<int>("getTransformCount");
                        item.PacketGetTime = itemObject.Call<long>("getPacketGetTime");
                        list.Add(item);
                    }
                    redPacketRecords.RecordList = list;
                }
                return redPacketRecords;
            } 
            return null;
        }

        public static TransformInfo GenerateTransformInfo(AndroidJavaObject androidObject) 
        {
            if (androidObject != null) 
            {
                TransformInfo transformInfo = new TransformInfo();
                transformInfo.CurrentContribution = androidObject.Call<int>("getCurrentContribution");
                transformInfo.TransformTimes = androidObject.Call<int>("getTransformTimes");
                transformInfo.CashDelta = androidObject.Call<double>("getCashDelta");
                transformInfo.CurrentCash = androidObject.Call<double>("getCurrentCash");

                AndroidJavaObject list = androidObject.Call<AndroidJavaObject>("getPacketList"); 
                if (list != null) 
                {
                    List<double>  packageList = new List<double>();
                    int size = list.Call<int>("size");
                    for (int index=0 ; index<size; index++) {
                        packageList.Add(list.Call<double>("get", index));
                    }
                    transformInfo.PacketList = packageList;
                }
                return transformInfo;
            }
            return null;
        }

        public static SectSettings GenerateSectSettings(AndroidJavaObject androidObject) 
        {
            if (androidObject != null) 
            {
                SectSettings sectSettings = new SectSettings();
                sectSettings.Hierarchy = androidObject.Call<int>("getHierarchy");
                sectSettings.TransformContribution = androidObject.Call<int>("getConsumeContribution");
                sectSettings.MaxContributionLeft = androidObject.Call<int>("getMaxContributionLeft");

                AndroidJavaObject transformStepObject = androidObject.Call<AndroidJavaObject>("getTransformStep");
                if (transformStepObject != null) 
                {
                    Hashtable stepTables = new Hashtable();
                    AndroidJavaObject transformKeySetObject = transformStepObject.Call<AndroidJavaObject>("keySet");
                    AndroidJavaObject transformIteratorObject = transformKeySetObject.Call<AndroidJavaObject>("iterator");
                    while(transformIteratorObject.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject transformkeyObject = transformIteratorObject.Call<AndroidJavaObject>("next");
                        int stepKey = transformkeyObject.Call<int>("intValue");
                        AndroidJavaObject vauleObject = transformStepObject.Call<AndroidJavaObject>("get", transformkeyObject);
                        stepTables.Add(stepKey, vauleObject.Call<int>("intValue"));
                    }                  
                    sectSettings.HashSteps = stepTables;
                }

                AndroidJavaObject transformTimeObject = androidObject.Call<AndroidJavaObject>("getTransformTimesMap");
                if (transformTimeObject != null) 
                {
                    Hashtable timeTables = new Hashtable();
                    AndroidJavaObject timeKeySetObject = transformTimeObject.Call<AndroidJavaObject>("keySet");
                    AndroidJavaObject timeIteratorObject = timeKeySetObject.Call<AndroidJavaObject>("iterator");
                    while(timeIteratorObject.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject transformkeyObject = timeIteratorObject.Call<AndroidJavaObject>("next");
                        int timeKey = transformkeyObject.Call<int>("intValue");
                        // AndroidJavaObject vauleObject = transformStepObject.Call<AndroidJavaObject>("get", transformkeyObject);
                        // stepTables.Add(stepKey, vauleObject.Call<int>("intValue"));
                        double[] numberList = transformTimeObject.Call<double []>("get", transformkeyObject);
                        timeTables.Add(timeKey, numberList);
                    } 

                    // HashSet<int> timeKeys = transformTimeObject.Call<HashSet<int>>("getTransformTimesMap");
                    
                    // foreach(int timeKey in timeKeys)
                    // {
                    //     double[] numberList = transformTimeObject.Call<double []>("get", timeKey);
                    //     timeTables.Add(timeKey, numberList);
                    // }
                    sectSettings.TransformTimesMap = timeTables;
                }

                sectSettings.Grades = androidObject.Call<int[]>("getGrades");

                AndroidJavaObject awardListObject = androidObject.Call<AndroidJavaObject>("getAwardSettingsList");
                if (awardListObject != null) 
                {
                    int size = awardListObject.Call<int>("size");
                    List<InviteAward> list = new List<InviteAward>();
                    for (int i=0; i<size; i++) 
                    {
                        AndroidJavaObject awardObject = awardListObject.Call<AndroidJavaObject>("get", i);
                        InviteAward inviteAward = new InviteAward();
                        inviteAward.Level = awardObject.Call<string>("getInvitedLevel");
                        inviteAward.AwardType = awardObject.Call<int>("getAwardType");
                        inviteAward.AwardAmount = awardObject.Call<double>("getAwardAmount");
                        list.Add(inviteAward);
                    }
                    sectSettings.AwardSettingsList = list;
                }
                return sectSettings;
            }
            return null;
        }

        public static List<SectRankingInfo> GenerateSectRankingInfoList(AndroidJavaObject androidObject)
        {
            if (androidObject != null) 
            {                
                int size = androidObject.Call<int>("size");
                List<SectRankingInfo> list = new List<SectRankingInfo>();
                for (int i=0; i<size; i++) 
                {
                    AndroidJavaObject awardObject = androidObject.Call<AndroidJavaObject>("get", i);
                    SectRankingInfo sectRankingInfo = new SectRankingInfo();
                    sectRankingInfo.Index = awardObject.Call<int>("getIndex");
                    sectRankingInfo.MasterId = awardObject.Call<string>("getMasterId");
                    sectRankingInfo.Counts = awardObject.Call<int>("getCounts");
                    list.Add(sectRankingInfo);
                }
                return list;
            }
            return null;
        } 
    }    
}