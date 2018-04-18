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


        public Creature SpawnCreature(Guid inGuid, Vector2DInt inSpawnWorldPosition)
        {
            GameObject debugCreatureView = GameObject.CreatePrimitive(PrimitiveType.Cube);

            Creature newCreature = new Creature(inGuid, debugCreatureView);

            _creatures.Add(newCreature.guid, newCreature);

            newCreature.viewGO.transform.position = new Vector3(inSpawnWorldPosition.x, 0, inSpawnWorldPosition.y) + Vector3.one * 0.5f;

            return newCreature;
        }
    }
}