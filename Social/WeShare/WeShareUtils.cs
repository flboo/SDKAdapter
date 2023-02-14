using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace Qarth
{
    public static class WeShareUtils
    {
        public static void SharePicByWXSession(Texture2D pic, bool needCachePic, Action<int> shareCallback = null)
        {
            var path = Application.persistentDataPath + "/share_img.png";
            if (!File.Exists(path) || needCachePic)
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                byte[] dataBytes = pic.EncodeToPNG();
                fs.Write(dataBytes, 0, dataBytes.Length);
                fs.Flush();
                fs.Dispose();
            }

            WeShareMgr.S.ShareImageByPath(ShareType.WeChat, SharePlace.Session, path, shareCallback);
        }

        public static void SharePicBytesByWXSession(byte[] bytes, Action<int> shareCallback = null)
        {
            WeShareMgr.S.ShareImageBytes(ShareType.WeChat, SharePlace.Session, bytes, shareCallback);
        }

        public static void SharePicBytesByWXSessionNotUnity(byte[] bytes, Action<int> shareCallback = null)
        {
            WeShareMgr.S.ShareImageBytesNotUnity(ShareType.WeChat, SharePlace.Session, bytes, shareCallback);
        }
    }
}