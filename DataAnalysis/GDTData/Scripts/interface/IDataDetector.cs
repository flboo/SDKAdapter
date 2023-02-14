namespace Tencent.GDT
{
    using System.Collections.Generic;

    internal interface IDataDetector
    {
        void Report(string eventName);

        void Report(string eventName, Dictionary<string, string> param);
    }
}