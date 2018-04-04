using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DebugLogger
{
    public static event Action<string> OnLog;

    public static void Log(string inString) =>
        OnLog?.Invoke(inString);
}
