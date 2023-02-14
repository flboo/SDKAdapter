using System;
using System.Collections.Generic;

namespace RichOX.Common
{
    public interface IRichOXEventClient
    {
        string GetName();

        string GetValue();
        Dictionary<string, string> GetMapValue();
    }
}
