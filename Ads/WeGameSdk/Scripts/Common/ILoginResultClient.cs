using System.Collections.Generic;
using WeGameSdk.Api;

namespace WeGameSdk.Common
{
    public interface ILoginResultClient
    {
        string GetUserId();
        string GetUserName();
        string GetToken();
        string GetExtension();
        string GetDescription();
    }
}
