using System;
using AntiAddictionSdk.Common;
using AntiAddictionSdk.Api;
using System.Runtime.InteropServices;
using AOT;

namespace AntiAddictionSdk.Platforms.iOS
{
    public class AgeTipClient : IAgeTipClient {
        
        private IntPtr AgeTipPtr;
        
        public AgeTipClient(IntPtr ageTipPtr) {
            AgeTipPtr = ageTipPtr;
        }

        public byte[] GetIconByteArray() {
            string bytesString = Externs.AntiAddictionAgeTipGetIconByteArrayString(AgeTipPtr);
            if (bytesString == null)
            {
                return null;
            }
            return System.Convert.FromBase64String(bytesString);
        }
        public string GetText() {
            return Externs.AntiAddictionAgeTipGetText(AgeTipPtr);
        }
    }
}