using System;

namespace ROXShare.Api
{
    public class ROXShareEventArgs : EventArgs
    {
        public ROXShareSuccessResponse SuccessResponse {get; set;}
        public ROXShareErrorResponse ErrorResponse {get; set;}
    }
}