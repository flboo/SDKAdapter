using System;

namespace RichOX.Api
{
    // Event that occurs when an ad fails to load.
    public class FailedToRenderEventArgs : EventArgs
    {
        public RichOXError RichOXError { get; set; }
    }
}
