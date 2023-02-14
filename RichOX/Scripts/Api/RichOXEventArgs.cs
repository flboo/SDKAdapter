using System;

namespace ROXBase.Api
{
    public class RichOXEventArgs : EventArgs
    {
        public RichOXEvent RichOXEvent { get; set; }
        public ROXCommonResponse CommonResponse {get; set;}
        public ROXErrorResponse ErrorResponse {get; set;}
    }
}