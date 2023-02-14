using RichOX.Common;

namespace RichOX.Platforms.iOS
{
    public class RichOXErrorClient : IRichOXErrorClient
    {
        private readonly int mErrorCode;
        private readonly string mErrorMessage;

        public RichOXErrorClient(int errorCode, string errorMessage)
        {
            mErrorCode = errorCode;
            mErrorMessage = errorMessage;
        }

        #region IRichOXErrorClient

        public int GetCode()
        {
            return mErrorCode;
        }

        public string GetMessage()
        {
            return mErrorMessage;
        }

        #endregion
    }
}
