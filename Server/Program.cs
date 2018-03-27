using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using Newtonsoft.Json;

class Program
{
    const string APPLICATION_NAME = "Descent_Server";

    static bool _isRunning = true;
    public static void Quit() => _isRunning = false;

    static void Main()
    {
        var networkManager = new NetworkManager(APPLICATION_NAME);
        var world          = new World();

        while (_isRunning)
        {
            networkManager.Update();
            // playerManager.Update()?
            // creatureManager.Update();
            // world update??
        }
    }
}
