using System;
using UnityEngine;

namespace TaurusXAdSdk.Common
{
    public static class Utils
    {
        public static Texture2D GetTexture2DFromByteArray(byte[] img)
        {
            if(img == null) {
                return null;
            }
            
            Texture2D nativeAdTexture = new Texture2D(1, 1);
            if (!nativeAdTexture.LoadImage(img)) {
                throw new InvalidOperationException(@"Could not load custom native template
                        image asset as texture");
            }
            return nativeAdTexture;
        }
    }
}
