using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

public class User
{
    public readonly NetConnection connection;

    List<Guid> _creatureIDs = new List<Guid>();


    public void AddCreature(Guid inCreatureGuid) => _creatureIDs.Add(inCreatureGuid);
    public Guid[] GetCreatures() => _creatureIDs.ToArray();


    public User(NetConnection inConnection) =>
        connection = inConnection;
}
