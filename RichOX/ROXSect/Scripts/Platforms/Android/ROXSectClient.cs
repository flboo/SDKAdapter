using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

using ROXBase.Api;
using ROXSect.Api;
using ROXSect.Common;
using ROXBase.Platforms.Android;

namespace ROXSect.Platforms.Android
{
    public class ROXSectClient : AndroidJavaProxy, IROXSect
    {
        static ROXSectClient sInstance = new ROXSectClient();

        public static ROXSectClient Instance {
            get {
                return sInstance;
            }
        }

        private AndroidJavaClass mROXSectClass;


        public ROXSectClient() : base (ClassUtils.Object)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(ClassUtils.UnityActivityClassName);
            mROXSectClass = new AndroidJavaClass(ClassUtils.RichOXSect); 
            // mUnityActivity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
        }

        public void GetSectInfo(ROXInterface<SectInfo> callback) 
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidJavaObject reponse = (AndroidJavaObject) args.CommonResponse.GetResponse();
                        SectInfo sectInfo = ROXSectUtils.GenerateSectInfo(reponse);
                        callback.OnSuccess(sectInfo);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };

            mROXSectClass.CallStatic("getSectInfo", androidCallback);
        }

        public void GetUserSectStatus(ROXInterface<int> callback) 
        {
            AndroidIntCallback androidCallback = new AndroidIntCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        int result = (int) args.CommonResponse.GetResponse();
                        callback.OnSuccess(result);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };

            mROXSectClass.CallStatic("getUserSectStatus", androidCallback);
        }

        public  void GetApprenticeList(int level, ROXInterface<ApprenticeList> callback) 
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidJavaObject reponse = (AndroidJavaObject) args.CommonResponse.GetResponse();
                        ApprenticeList apprenticeList = ROXSectUtils.GenerateApprenticeList(reponse);
                        callback.OnSuccess(apprenticeList);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXSectClass.CallStatic("getApprenticeList", level, androidCallback);
        }

        public void GetApprenticeList(int level, int pageSize, int currentPage, ROXInterface<ApprenticeList> callback) 
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidJavaObject reponse = (AndroidJavaObject) args.CommonResponse.GetResponse();
                        ApprenticeList apprenticeList = ROXSectUtils.GenerateApprenticeList(reponse);
                        callback.OnSuccess(apprenticeList);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };

            mROXSectClass.CallStatic("getApprenticeList", level, pageSize, currentPage, androidCallback);
        }

        public void GetApprenticeInfo(string apprenticeId, ROXInterface<ApprenticeInfo> callback) 
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidJavaObject reponse = (AndroidJavaObject) args.CommonResponse.GetResponse();
                        ApprenticeInfo apprentice = ROXSectUtils.GenerateApprenticeInfo(reponse);
                        callback.OnSuccess(apprentice);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXSectClass.CallStatic("getApprenticeInfo", apprenticeId, androidCallback);
        }

        public void GenContribution(int action, ROXInterface<Contribution> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidJavaObject reponse = (AndroidJavaObject) args.CommonResponse.GetResponse();
                        Contribution contribution = ROXSectUtils.GenerateContribution(reponse);
                        callback.OnSuccess(contribution);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXSectClass.CallStatic("genContribution", action, androidCallback);
        }

        public void GetContribution(string studentUid, ROXInterface<Contribution> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidJavaObject reponse = (AndroidJavaObject) args.CommonResponse.GetResponse();
                        Contribution contribution = ROXSectUtils.GenerateContribution(reponse);
                        callback.OnSuccess(contribution);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXSectClass.CallStatic("getContribution", studentUid, androidCallback);
        }

        public void getAllContribution(ROXInterface<Contribution> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidJavaObject reponse = (AndroidJavaObject) args.CommonResponse.GetResponse();
                        Contribution contribution = ROXSectUtils.GenerateContribution(reponse);
                        callback.OnSuccess(contribution);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXSectClass.CallStatic("getAllContribution", androidCallback);
        }

        public void GetInviteAward(int count, ROXInterface<AwardInfo> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidJavaObject reponse = (AndroidJavaObject) args.CommonResponse.GetResponse();
                        AwardInfo awardInfo = ROXSectUtils.GenerateAwardInfo(reponse);
                        callback.OnSuccess(awardInfo);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXSectClass.CallStatic("getInviteAward", count, androidCallback);
        }

        public void GetInviteAwardList(ROXInterface<List<InviteAward>> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidJavaObject reponse = (AndroidJavaObject) args.CommonResponse.GetResponse();
                        List<InviteAward> list = ROXSectUtils.GenerateInviteAwardList(reponse);
                        callback.OnSuccess(list);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXSectClass.CallStatic("getInviteAwardList", androidCallback);
        }

        public void BindInviter(string inviterUid, ROXInterface<bool> callback)
        {
            AndroidBooleanCallback androidCallback = new AndroidBooleanCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        bool result = (bool) args.CommonResponse.GetResponse();
                        callback.OnSuccess(result);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXSectClass.CallStatic("bindInviter", inviterUid, androidCallback);
        }

        public void GetContributionRecordByDay(int year, int month, ROXInterface<Hashtable> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidJavaObject response = (AndroidJavaObject) args.CommonResponse.GetResponse();
                        Hashtable table = ROXSectUtils.GenerateContributionRecordTable(response);
                        callback.OnSuccess(table);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXSectClass.CallStatic("getContributionRecordByDay", year, month, androidCallback);
        }

        public void GetRedPacketRecord(ROXInterface<RedPacketRecords> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidJavaObject response = (AndroidJavaObject) args.CommonResponse.GetResponse();
                        RedPacketRecords redPacketRecords = ROXSectUtils.GenerateRedPacketRecords(response);
                        callback.OnSuccess(redPacketRecords);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXSectClass.CallStatic("getRedPacketRecord", androidCallback);
        }

        public void GetRedPacketRecord(int pageSize, int currentPage, ROXInterface<RedPacketRecords> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidJavaObject response = (AndroidJavaObject) args.CommonResponse.GetResponse();
                        RedPacketRecords redPacketRecords = ROXSectUtils.GenerateRedPacketRecords(response);
                        callback.OnSuccess(redPacketRecords);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXSectClass.CallStatic("getRedPacketRecord", pageSize, currentPage, androidCallback);
        }

        public void Transform(ROXInterface<TransformInfo> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidJavaObject response = (AndroidJavaObject) args.CommonResponse.GetResponse();
                        TransformInfo transformInfo = ROXSectUtils.GenerateTransformInfo(response);
                        callback.OnSuccess(transformInfo);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXSectClass.CallStatic("transform", androidCallback);
        }

        public void GetSettings(ROXInterface<SectSettings> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        AndroidJavaObject response = (AndroidJavaObject) args.CommonResponse.GetResponse();
                        SectSettings sectSettings = ROXSectUtils.GenerateSectSettings(response);
                        callback.OnSuccess(sectSettings);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null) {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXSectClass.CallStatic("getSettings", androidCallback);
        }
    }
}