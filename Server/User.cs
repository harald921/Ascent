using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

public class User
{
    public Data data { get; private set; }

    public NetConnection connection { get; private set; }

    List<Guid> _creatureIDs = new List<Guid>();

    public void AddCreature(Guid inCreatureGuid) => _creatureIDs.Add(inCreatureGuid);
    public Guid[] GetCreatures() => _creatureIDs.ToArray();

    public static event Action<User> OnUserLogin;


    public User(Data inData) =>
        data = inData;



    public static User Login(string inUsername, string inPassword, NetConnection inConnection)
    {
        Console.WriteLine(inUsername + " logging in.");
        
        // Check if it eixsts, load and return it
        // DEBUG:
        User newUser = new User(new Data()
        {
            username = inUsername,
            password = inPassword
        });

        newUser.connection = inConnection;

        OnUserLogin?.Invoke(newUser);

        Console.WriteLine("NOT PROPERLY IMPLEMENTED YET!");
        return newUser;
    }

    public static User Register(string inUsername, string inPassword, NetConnection inConnection)
    {
        Console.WriteLine("New User!");
        Console.WriteLine("Username: " + inUsername);
        Console.WriteLine("Password: " + inPassword);

        // Check if it already exists, 
        
        // else...
        User newUser = new User(new Data()
        {
            username = inUsername,
            password = inPassword
        });

        newUser.connection = inConnection;

        OnUserLogin?.Invoke(newUser);

        return newUser;

        // Write to disk
    }


    public struct Data
    {
        public string username;
        public string password;
    }
}
