using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using ROXShare.Api;
using ROXShare.Platforms;

namespace ROXShare.Common
{
    public class DummyROXShareClient :  IROXShare {

        public void Init()
        {

        }
        public void GetInstallParams(ROXShareInterface<Hashtable> callback)
        {

        }

        public void GenShareUrl(string shareUrl, Hashtable urlParams, ROXShareInterface<string> callback)
        {

        }

        public byte[] GetQRCodeBytes(string shareUrl, int width, int height) 
        {
            return null;
        }

        // public Bitmap GetQRCodeBitmap(string shareUrl, int width, int height) 
        // {

        // }

        public void ReportRegister()
        {

        }

        public void ReportEvent(string lable, int value)
        {

        }

        public void ReportOpenShare() 
        {

        }

        public void ReportStartShare()
        {

        }

        public void ReportBindEvent(bool oversea)
        {

        }

        public void ReportBindEvent(bool oversea, Hashtable bindParams)
        {
            
        }

        public void ReportShowShare()
        {

        }

        public void ReportShareSuccess()
        {

        }
    }
    
}