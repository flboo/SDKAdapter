using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using ROXStrategy.Common;
using ROXStrategy.Platforms;
using ROXBase.Api;

namespace ROXStrategy.Api
{
    public class ROXNormalStrategy
    {
        private static ROXNormalStrategy mInstance;
        private static IROXNormal mROXNormal;

        /// 用户真实姓名
        public static string USER_REAL_NAME = "real_name";
        /// 用户身份证号码
        public static string USER_ID_CARD = "id_card";
        /// 用户手机号
        public static string USER_PHONE_NUMBER = "phone";
        /// 支付方式
        public static string PAY_METHOD_ID = "withdraw_way";
        /// 自定义提现金额
        public static string WITHDRAW_AMOUNT = "withdraw_amount";

        public static ROXNormalStrategy Instance(int strategyId)
        {
            if (mInstance != null)
            {
                return mInstance;
            }
            else
            {
                mInstance = new ROXNormalStrategy(strategyId);
                return mInstance;
            }
        }

        /// <summary>
        /// 获取实例
        /// strategyId: 策略Id
        /// <summary>  
        public ROXNormalStrategy(int strategyId)
        {
            mROXNormal = ClientFactory.RichOXClientInstance();
            mROXNormal.Init(strategyId);
        }

        /// <summary>
        /// 获取通用策略配置信息       
        /// <summary>  
        public void GetStrategyConfig(ROXInterface<NormalStrategyConfig> callback)
        {
            if (mROXNormal != null)
            {
                mROXNormal.GetStrategyConfig(callback);
            }

        }

        /// <summary>
        /// 按最大值方式发放奖励，后台任务配置为最大值，请用该接口
        /// taskId: 任务ID
        /// amount: 奖励数量，该值不能超过后台配置的最大奖励数值
        /// <summary>          
        public void DoMission(string taskId, double amount, ROXInterface<NormalMissionResult> callback)
        {
            if (mROXNormal != null)
            {
                mROXNormal.DoMission(taskId, amount, callback);
            }
        }

        /// <summary>
        /// 按固定值方式发放奖励，后台任务配置为固定值，请用该接口
        /// taskId: 任务ID       
        /// <summary> 
        public void DoMission(string taskId, ROXInterface<NormalMissionResult> callback)
        {
            if (mROXNormal != null)
            {
                mROXNormal.DoMission(taskId, callback);
            }

        }

        /// <summary>
        /// 查询当前资产信息       
        /// <summary> 
        public void QueryAssetInfo(ROXInterface<NormalAssetsInfo> callback)
        {
            if (mROXNormal != null)
            {
                mROXNormal.QueryAssetInfo(callback);
            }
        }

        /// <summary>
        /// 极速提现
        /// taskId: 提现任务Id     
        /// <summary> 
        public void ExtremeWithdraw(string taskId, ROXInterface<bool> callback)
        {
            if (mROXNormal != null)
            {
                mROXNormal.ExtremeWithdraw(taskId, callback);
            }
        }

        /// <summary>
        /// 普通提现
        /// realName: 用户姓名
        /// cardId: 用户身份证   
        /// phoneNumber: 用户手机号 
        /// <summary> 
        public void Withdraw(string taskId, String realName, String cardId, String phoneNumber, ROXInterface<bool> callback)
        {
            if (mROXNormal != null)
            {
                mROXNormal.Withdraw(taskId, realName, cardId, phoneNumber, callback);
            }
        }

        /// <summary>
        /// 指定数量兑换
        /// exchangeId: 兑换任务Id
        /// amounts: 兑换数量，兑换数量不得小于兑换任务中设置的数量   
        /// <summary> 
        public void Transform(string exchangeId, double amounts, ROXInterface<NormalTransformResult> callback)
        {
            if (mROXNormal != null)
            {
                mROXNormal.Transform(exchangeId, amounts, callback);
            }
        }

        /// <summary>
        /// 当前兑换任务中对应的资产全部兑换，多余的零头不替换
        /// exchangeId: 兑换任务Id
        /// <summary> 
        public void Transform(string exchangeId, ROXInterface<NormalTransformResult> callback)
        {
            if (mROXNormal != null)
            {
                mROXNormal.Transform(exchangeId, callback);
            }
        }

        /// <summary>
        /// 查询指定任务的进度
        /// taskList: 指定任务列表
        /// <summary> 
        public void QueryProgress(List<string> taskList, ROXInterface<NormalMissionsProgress> callback)
        {
            if (mROXNormal != null)
            {
                mROXNormal.QueryProgress(taskList, callback);
            }
        }

        /// <summary>
        /// 查询所有任务进度
        /// <summary> 
        public void QueryAllProgress(ROXInterface<NormalMissionsProgress> callback)
        {
            if (mROXNormal != null)
            {
                mROXNormal.QueryAllProgress(callback);
            }
        }


        /// <summary>
        /// 提交任务
        /// 服务端定义奖励数量发放逻辑，需根据客户端传递规则进行发放
        /// taskId  任务Id
        /// tid     校验Id
        /// <summary> 
        public void DoCustomRulesMission(string taskId, string tid, ROXInterface<NormalMissionResult> callback)
        {
            if (mROXNormal != null)
            {
                mROXNormal.DoCustomRulesMission(taskId, tid, callback);
            }
        }

        /// <summary>
        /// 提交任务
        /// 服务端定义奖励数量发放逻辑，无需客户端传递参数
        /// taskId  任务Id
        /// <summary> 
        public void DoCustomRulesMission(string taskId, ROXInterface<NormalMissionResult> callback)
        {
            if (mROXNormal != null)
            {
                mROXNormal.DoCustomRulesMission(taskId, callback);
            }
        }

        /// <summary>
        /// 极速提现
        /// taskId: 提现任务Id     
        /// <summary> 
        public void ExtremeWithdrawNew(string taskId, ROXInterface<List<NormalAssetStock>> callback)
        {
            if (mROXNormal != null)
            {
                mROXNormal.ExtremeWithdrawNew(taskId, callback);
            }
        }

        /// <summary>
        /// 普通提现
        /// realName: 用户姓名
        /// cardId: 用户身份证   
        /// phoneNumber: 用户手机号 
        /// <summary> 
        public void WithdrawNew(string taskId, String realName, String cardId, String phoneNumber, ROXInterface<List<NormalAssetStock>> callback)
        {
            if (mROXNormal != null)
            {
                mROXNormal.WithdrawNew(taskId, realName, cardId, phoneNumber, callback);
            }
        }

        /// <summary>
        /// 通用提现
        /// taskId: 提现任务Id  
        /// withdrawParam : 提现参数，根据不同的场景配置相应参数，部分键值定义如下：
        /// ROXNormalStrategy.PAY_METHOD_ID（必传字段） ： "11101"- 内部微信兑付， "11102” - 内部支付宝兑付，"12101" - QTT小额微信兑付，"12102" - QTT小额支付宝兑付
        /// ROXNormalStrategy.USER_REAL_NAME : 用户真实姓名，仅内部大额支付使用
        /// ROXNormalStrategy.USER_ID_CARD : 用户身份证号码，仅内部大额支付使用
        /// ROXNormalStrategy.USER_PHONE_NUMBER : 用户手机号，仅内部大额支付使用
        /// ROXNormalStrategy.WITHDRAW_AMOUNT : 自定义提现金额，仅提现任务类型为 “range" 具体类型参考 NormalStrategyWithdrawTask 类
        /// 其他键值信息根据兑付方式，传入指定信息，比如 QTT 兑付需要传入 "tk" 和 "tuid"，具体获取方式请参考对应文档。
        /// <summary> 
        public void GeneralWithdraw(string taskId, Hashtable withdrawParam, ROXInterface<List<NormalAssetStock>> callback)
        {
            if (mROXNormal != null)
            {
                mROXNormal.GeneralWithdraw(taskId, withdrawParam, callback);
            }
        }

        /// <summary>
        /// 海外现金提现
        /// taskId: 提现任务Id  
        /// info : 提现信息
        /// <summary> 
        public void GlobalWithdraw(string taskId, GlobalWithdrawInfo info, ROXInterface<List<NormalAssetStock>> callback)
        {
            if (mROXNormal != null)
            {
                mROXNormal.GlobalWithdraw(taskId, info, callback);
            }
        }
    }
}
