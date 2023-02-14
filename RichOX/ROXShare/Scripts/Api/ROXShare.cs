using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Drawing;

using ROXShare.Common;
using ROXShare.Platforms;

namespace ROXShare.Api
{
    public class RichOXShare
    {
        private static RichOXShare mInstance;
        private IROXShare mROXShare;

        public static RichOXShare Instance()
        {
            if (mInstance != null)
            {
                return mInstance;
            }
            else
            {
                mInstance = new RichOXShare();
                return mInstance;
            }
        }


        public RichOXShare()
        {
            mROXShare = ClientFactory.ROXShareClientInstance();
        }

        /// <summary>
        /// 初始化
        /// <summary> 
        public void Init()
        {
            mROXShare.Init();
        }

        /// <summary>
        /// 还原分享参数
        /// <summary> 
        public void GetInstallParams(ROXShareInterface<Hashtable> callback)
        {
            mROXShare.GetInstallParams(callback);
        }

        /// <summary>
        /// 获取无码分享链接
        /// shareUrl : 分享链接
        /// urlParams : 参数信息
        /// <summary> 
        public void GenShareUrl(string shareUrl, Hashtable urlParams, ROXShareInterface<string> callback)
        {
            mROXShare.GenShareUrl(shareUrl, urlParams, callback);
        }

        /// <summary>
        /// 将网址转为二维码图象对应的 byte 数组
        /// shareUrl : 分享链接
        /// width : 二维码宽度, 单位：像素
        /// height : 二维码高度，单位：像素
        /// <summary> 
        public byte[] GetQRCodeBytes(string shareUrl, int width, int height)
        {
            return mROXShare.GetQRCodeBytes(shareUrl, width, height);
        }

        // public Bitmap GetQRCodeBitmap(string shareUrl, int width, int height) 
        // {
        //     return mROXShare.GetQRCodeBitmap(shareUrl, width, height);
        // }

        /// <summary>
        /// 上报注册事件
        /// <summary> 
        [Obsolete("该接口已废弃")]
        public void ReportRegister()
        {
            mROXShare.ReportRegister();
        }

        /// <summary>
        /// 上报用户自定义打点数据
        /// <summary> 
        [Obsolete("该接口已废弃")]
        public void ReportEvent(string lable, int value)
        {
            mROXShare.ReportEvent(lable, value);
        }

        /// <summary>
        /// 上报用户打开分享玩法界面
        /// 务必调用，用来分析转化漏斗
        /// <summary> 
        public void ReportOpenShare()
        {
            mROXShare.ReportOpenShare();
        }

        /// <summary>
        /// 上报用户发起分享
        /// 务必调用，用来分析转化漏斗
        /// <summary> 
        public void ReportStartShare()
        {
            mROXShare.ReportStartShare();
        }

        /// <summary>
        /// 上报师徒关系绑定，仅取到师傅信息时调用
        /// 务必调用，用来分析转化漏斗
        /// oversea 海外版本请设置 oversea 为 true
        /// <summary>  
        public void ReportBindEvent(bool oversea)
        {
            mROXShare.ReportBindEvent(oversea);
        }

        /// <summary>
        /// 上报师徒关系绑定，仅取到师傅信息时调用
        /// 务必调用，用来分析转化漏斗
        /// oversea 海外版本请设置 oversea 为 true
        /// bindParams : 绑定自定义参数
        /// <summary>  
        public void ReportBindEvent(bool oversea, Hashtable bindParams)
        {
            mROXShare.ReportBindEvent(oversea, bindParams);
        }

        /// <summary>
        /// 当分享玩法入口展示给用户时进行上报
        /// 务必调用，用来分析转化漏斗
        /// <summary>  
        public void ReportShowShare()
        {
            mROXShare.ReportShowShare();
        }

        /// <summary>
        /// 当用户发起分享后，分享成功后上报
        /// 务必调用，用来分析转化漏斗
        /// <summary>  
        public void ReportShareSuccess()
        {
            mROXShare.ReportShareSuccess();
        }
    }
}
