using System;
using System.Collections.Generic;

namespace ROXBase.Common
{
    public interface IRichOXEventClient
    {
        string GetName();

        string GetValue();
        Dictionary<string, string> GetMapValue();
    }
}