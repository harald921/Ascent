using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class User
{
    public Data data { get; private set; } = new Data();


    public List<Guid> _creatureIDs = new List<Guid>();

    public void   AddCreature(Guid inCreatureGuid)     => _creatureIDs.Add(inCreatureGuid);
    public void   RemoveCreature(Guid inCreatureGuid)  => _creatureIDs.Remove(inCreatureGuid);
    public Guid[] GetCreatures()                       => _creatureIDs.ToArray();



    public User(Data inData) =>
        data = inData;


    public static User Load(string inUsername, string inPassword)
    {
        throw new NotImplementedException("Tell Harald he's lazy");

        // Load from disk
        // Check if PW is correct
    }

    public static User Register(string inUsername, string inPassword)
    {
        Console.WriteLine("New User!");
        Console.WriteLine("Username: " + inUsername);
        Console.WriteLine("Password: " + inPassword);

        // Check if it already exists, is so - load and return it
        
        // else...
        return new User(new Data()
        {
            username = inUsername,
            password = inPassword
        });


        // Write to disk
    }


    public struct Data
    {
        public string username;
        public string password;
    }
}
