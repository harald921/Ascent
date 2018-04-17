using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class World
{
	public class CreatureManager
    {
        Dictionary<Guid, Creature> _creatures = new Dictionary<Guid, Creature>();

        public Creature GetCreature(Guid inGuid) =>
            _creatures[inGuid];


        public Creature SpawnCreature(Guid inGuid, Tile inSpawnTile)
        {
            GameObject debugCreatureView = GameObject.CreatePrimitive(PrimitiveType.Cube);

            Creature newCreature = new Creature(inGuid, debugCreatureView);

            _creatures.Add(newCreature.guid, newCreature);

            return newCreature;
        }
    }
}