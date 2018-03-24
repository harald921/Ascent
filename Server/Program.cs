using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

class Program
{
    const string APPLICATION_NAME = "Descent_Server";

    static bool _isRunning = true;
    public static void Quit() => _isRunning = false;


    static void Main()
    {
        while (_isRunning)
        {

        }
    }

}
