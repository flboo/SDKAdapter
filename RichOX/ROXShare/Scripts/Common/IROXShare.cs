using System;
using System.Collections;

using System.Drawing;
using ROXShare.Api;

namespace ROXShare.Common
{
    public interface IROXShare
    {
        void Init();

        void GetInstallParams(ROXShareInterface<Hashtable> callback);

        void GenShareUrl(string shareUrl, Hashtable urlParams, ROXShareInterface<string> callback);

        byte[] GetQRCodeBytes(string shareUrl, int width, int height);

        // Bitmap GetQRCodeBitmap(string shareUrl, int width, int height);

        void ReportRegister();

        void ReportEvent(string lable, int value);

        void ReportOpenShare();

        void ReportStartShare();

        void ReportBindEvent(bool oversea);

        void ReportBindEvent(bool oversea, Hashtable bindParams);

        void ReportShowShare();

        void ReportShareSuccess();
    }
}