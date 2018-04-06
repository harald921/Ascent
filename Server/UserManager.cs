using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

public static class UserManager
{
    public static event Action<User> OnUserLogin;


    public static User Login(string inUsername, string inPassword, NetConnection inConnection)
    {
        Console.WriteLine(inUsername + " logging in.");

        User loggedInUser = Register(inUsername, inPassword, inConnection);

        OnUserLogin?.Invoke(loggedInUser);

        Console.WriteLine("NOT PROPERLY IMPLEMENTED YET!");
        return loggedInUser;
    }

    public static User Register(string inUsername, string inPassword, NetConnection inConnection)
    {
        Console.WriteLine("New User!");
        Console.WriteLine("Username: " + inUsername);
        Console.WriteLine("Password: " + inPassword);

        // Check if it already exists, if so - return "already existing error"

        // else...
        User newUser = new User(inConnection);

        OnUserLogin?.Invoke(newUser);

        return newUser;

        // Write to disk
    }
}
