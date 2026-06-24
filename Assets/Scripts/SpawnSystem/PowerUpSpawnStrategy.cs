using Entities;
using UnityEngine;

namespace SpawnSystem
{
    public class PowerUpSpawnStrategy : SpawnStrategy
    {
        SpawnContext spawnContext;
        Platform nextPlatform;
        public PowerUpSpawnStrategy(SpawnContext spawnContext)
        {
            this.spawnContext = spawnContext;
        }
        public override SpawnPointData GetSpawnPointData()
        {
            
        }
    }
}