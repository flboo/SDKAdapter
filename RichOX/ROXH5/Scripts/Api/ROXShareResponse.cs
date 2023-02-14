using System;
using System.Collections;

namespace RichOX.Api
{
    public class ROXShareResponse
    {
        private string m_HostUrl;
        private Hashtable m_ShareParams;
        private string m_Title;
        private string m_Content;
        private byte[] m_BitmapBytes;

        private Object m_CallbackObject;

        public ROXShareResponse(string hostUrl, Hashtable sharetable, string title, string content, byte[] bitmapBytes, Object callbackObject) 
        {
            m_HostUrl = hostUrl;
            m_ShareParams = sharetable;
            m_Title = title;
            m_Content = content;
            m_BitmapBytes = bitmapBytes;
            m_CallbackObject = callbackObject;

        }
        public string GetHostUrl()
        {
            return m_HostUrl;
        }       

        public Hashtable GetShareParams()
        {
            return m_ShareParams;
        }

        public String GetShareTitle()
        {
            return m_Title;
        }

        public String GetShareContent()
        {
            return m_Content;
        }

        public byte[] GetBitmpaBytes()
        {
            return m_BitmapBytes;
        }

        public Object getCallbackObject()
        {
            return m_CallbackObject;
        }

    }
}
