using RichOX.Common;
using RichOX.Platforms;
using System;
using System.Collections;

namespace RichOX.Api
{
    public interface H5ShareCallback
    {
        /// <summary>
        /// 返回给 APP 生成分享链接，开发者无需处理
        /// host ： 落地页地址
        /// paramsMap ： 分享参数，分享链接中传递的参数，开发者可在其中添加自定义参数
        /// <summary> 
        void GenShareUrl(string host, Hashtable paramsMap);

        /// <summary>
        /// 分享信息，APP 通过 H5 返回的分享信息，调用第三方的分享 SDK 将分享内容分享出去
        /// title ： 分享 title
        /// content ： 分享内容
        /// bitmap : 分享图片对应的 byte 数组
        /// <summary> 
        void ShareContent(String title, String content, byte[] bitmap);
    }
}