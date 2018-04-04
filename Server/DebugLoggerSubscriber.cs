using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DebugLoggerSubscriber
{
    public static void Initialize()
    {
        DebugLogger.OnLog += Console.WriteLine;
    }
}
