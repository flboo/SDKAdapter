using System.Collections;
using System.Collections.Generic;
using ROXSect.Common;
using ROXSect.Platforms;
using ROXBase.Api;

namespace ROXSect.Api
{
    public class ROXSectManager
    {
        private static ROXSectManager mInstance;
        private IROXSectClient mROXSect;

        public static ROXSectManager Instance()
        {
            if (mInstance != null)
            {
                return mInstance;
            }
            else
            {
                mInstance = new ROXSectManager();
                return mInstance;
            }
        }

        public ROXSectManager()
        {
            mROXSect = ClientFactory.ROXSectClientInstanceNew();
        }

        /// <summary>
        /// 获取宗门信息
        /// <summary>         
        public void GetSectInfo(ROXInterface<SectInfo> calllback)
        {
            mROXSect.GetSectInfo(calllback);
        }

        /// <summary>
        /// 当前宗门用户状态
        /// 0: 非宗门用户
        /// 1: 宗门未验证
        /// 2: 宗门已验证
        /// <summary> 
        public void GetUserSectStatus(ROXInterface<int> calllback)
        {
            mROXSect.GetUserSectStatus(calllback);
        }

        /// <summary>
        /// 获取某级弟子信息
        /// level: 弟子级数 ，1为掌门弟子
        /// <summary> 
        public void GetApprenticeList(int level, ROXInterface<ApprenticeList> callback)
        {
            mROXSect.GetApprenticeList(level, callback);
        }

        /// <summary>
        /// 分页获取某级弟子信息
        /// level: 弟子级数 ，1为掌门弟子
        /// pageSize: 每页大小，非必传字段，默认 -1
        /// currentPage: 当前页，非必传，默认为 -1
        /// <summary> 
        public void GetApprenticeList(int level, int pageSize, int currentPage, ROXInterface<ApprenticeList> callback)
        {
            mROXSect.GetApprenticeList(level, pageSize, currentPage, callback);
        }

        /// <summary>
        /// 获取某个弟子的详细信息
        /// apprenticeId: 具体弟子id
        /// <summary> 
        public void GetApprenticeInfo(string apprenticeId, ROXInterface<ApprenticeInfo> callback)
        {
            mROXSect.GetApprenticeInfo(apprenticeId, callback);
        }

        /// <summary>
        /// 产生贡献值
        /// action: 行为类型，看视频为 0
        /// <summary> 
        public void GenContribution(int action, ROXInterface<Contribution> callback)
        {
            mROXSect.GenContribution(action, callback);
        }

        /// <summary>
        /// 领取徒弟的贡献值
        /// studentUid: 某个徒弟的id
        /// <summary> 
        public void GetContribution(string studentUid, ROXInterface<Contribution> callback)
        {
            mROXSect.GetContribution(studentUid, callback);
        }

        /// <summary>
        /// 一键领取所有徒弟的贡献值
        /// <summary>
        public void GetAllContribution(ROXInterface<Contribution> callback)
        {
            mROXSect.getAllContribution(callback);
        }

        /// <summary>
        /// 绑定师徒关系
        /// inviterUid: 师傅 ID
        /// <summary>
        public void BindInviter(string inviterUid, ROXInterface<bool> callback)
        {
            mROXSect.BindInviter(inviterUid, callback);
        }

        /// <summary>
        /// 获取宗门设置
        /// <summary>
        public void GetSettings(ROXInterface<SectSettings> callback)
        {
            mROXSect.GetSettings(callback);
        }

        /// <summary>
        /// 宗门邀请排行榜
        /// <summary>
        public void GetInviteRanking(ROXInterface<List<SectRankingInfo>> callback)
        {
            mROXSect.GetInviteRanking(callback);
        }

        /// <summary>
        /// 获取指定用户某个时间段后裂变邀请人数
        /// lastTime : 精确到毫秒, 不填则默认拉取所有人数
        /// status : 1 则只拉取验证弟子数，默认不过滤
        /// <summary>
        public void GetInviterCounts(long lastTime, int status, ROXInterface<int> callback)
        {
            mROXSect.GetInviterCounts(lastTime, status, callback);
        }
        
    }
}