using UnityEngine;
namespace AntiAddictionSdk.Common
{
    public static class Utils {
        public static Texture2D GetTexture2DFromByteArray(byte[] img)
        {
            // Create a texture. Texture size does not matter, since
            // LoadImage will replace with with incoming image size.
            Texture2D texture = new Texture2D(1, 1);
            if (!texture.LoadImage(img))
            {
                Debug.Log(@"Could not load image as texture");
            }
            return texture;
        }
    }
}