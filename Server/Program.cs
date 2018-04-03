using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using Newtonsoft.Json;

class Program
{
    static bool _isRunning = true;
    public static void Quit() => _isRunning = false;

    static void Main()
    {
        var networkManager = new NetworkManager(Constants.APPLICATION_NAME);
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


//  class User
/*  
 *  - Holds list of GUID's representing the characters the user owns
 *  - Password and Username
 *  
 * 
 */
