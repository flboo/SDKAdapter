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
    public class ROXSectClientManager : AndroidJavaProxy, IROXSectClient
    {
        static ROXSectClientManager sInstance = new ROXSectClientManager();

        public static ROXSectClientManager Instance
        {
            get
            {
                return sInstance;
            }
        }

        private AndroidJavaClass mROXSectClass;


        public ROXSectClientManager() : base(ClassUtils.Object)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(ClassUtils.UnityActivityClassName);
            mROXSectClass = new AndroidJavaClass(ClassUtils.ROXSect);
            // mUnityActivity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
        }

        public void GetSectInfo(ROXInterface<SectInfo> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        AndroidJavaObject reponse = (AndroidJavaObject)args.CommonResponse.GetResponse();
                        SectInfo sectInfo = ROXSectUtils.GenerateSectInfo(reponse);
                        callback.OnSuccess(sectInfo);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
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
                    if (args != null)
                    {
                        int result = (int)args.CommonResponse.GetResponse();
                        callback.OnSuccess(result);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };

            mROXSectClass.CallStatic("getUserSectStatus", androidCallback);
        }

        public void GetApprenticeList(int level, ROXInterface<ApprenticeList> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        AndroidJavaObject reponse = (AndroidJavaObject)args.CommonResponse.GetResponse();
                        ApprenticeList apprenticeList = ROXSectUtils.GenerateApprenticeList(reponse);
                        callback.OnSuccess(apprenticeList);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
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
                    if (args != null)
                    {
                        AndroidJavaObject reponse = (AndroidJavaObject)args.CommonResponse.GetResponse();
                        ApprenticeList apprenticeList = ROXSectUtils.GenerateApprenticeList(reponse);
                        callback.OnSuccess(apprenticeList);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
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
                    if (args != null)
                    {
                        AndroidJavaObject reponse = (AndroidJavaObject)args.CommonResponse.GetResponse();
                        ApprenticeInfo apprentice = ROXSectUtils.GenerateApprenticeInfo(reponse);
                        callback.OnSuccess(apprentice);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
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
                    if (args != null)
                    {
                        AndroidJavaObject reponse = (AndroidJavaObject)args.CommonResponse.GetResponse();
                        Contribution contribution = ROXSectUtils.GenerateContribution(reponse);
                        callback.OnSuccess(contribution);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
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
                    if (args != null)
                    {
                        AndroidJavaObject reponse = (AndroidJavaObject)args.CommonResponse.GetResponse();
                        Contribution contribution = ROXSectUtils.GenerateContribution(reponse);
                        callback.OnSuccess(contribution);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
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
                    if (args != null)
                    {
                        AndroidJavaObject reponse = (AndroidJavaObject)args.CommonResponse.GetResponse();
                        Contribution contribution = ROXSectUtils.GenerateContribution(reponse);
                        callback.OnSuccess(contribution);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXSectClass.CallStatic("getAllContribution", androidCallback);
        }

        public void BindInviter(string inviterUid, ROXInterface<bool> callback)
        {
            AndroidBooleanCallback androidCallback = new AndroidBooleanCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        bool result = (bool)args.CommonResponse.GetResponse();
                        callback.OnSuccess(result);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXSectClass.CallStatic("bindInviter", inviterUid, androidCallback);
        }

        public void GetSettings(ROXInterface<SectSettings> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        AndroidJavaObject response = (AndroidJavaObject)args.CommonResponse.GetResponse();
                        SectSettings sectSettings = ROXSectUtils.GenerateSectSettings(response);
                        callback.OnSuccess(sectSettings);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXSectClass.CallStatic("getSettings", androidCallback);
        }

        public void GetInviteRanking(ROXInterface<List<SectRankingInfo>> callback)
        {
            AndroidCommonCallback androidCallback = new AndroidCommonCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        AndroidJavaObject response = (AndroidJavaObject)args.CommonResponse.GetResponse();
                        List<SectRankingInfo> sectRankingInfoList = ROXSectUtils.GenerateSectRankingInfoList(response);
                        callback.OnSuccess(sectRankingInfoList);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };
            mROXSectClass.CallStatic("getInviteRanking", androidCallback);
        }

        public void GetInviterCounts(long lastTime, int status, ROXInterface<int> callback)
        {
            AndroidIntCallback androidCallback = new AndroidIntCallback();
            androidCallback.OnSuccess += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        int result = (int)args.CommonResponse.GetResponse();
                        callback.OnSuccess(result);
                    }
                }
            };
            androidCallback.OnFailed += (sender, args) =>
            {
                if (callback != null)
                {
                    if (args != null)
                    {
                        callback.OnFailed(args.ErrorResponse.GetCode(), args.ErrorResponse.GetMessage());
                    }
                }
            };

            mROXSectClass.CallStatic("getInviterCounts", lastTime, status, androidCallback);

        }
    }
}