using UnityEngine;

namespace SpawnSystem
{
    public class DefaultSpawnStrategy : SpawnStrategy
    {
        readonly Transform spawnPoint;
        
        public DefaultSpawnStrategy(Transform spawnPoint)
        {
            this.spawnPoint = spawnPoint;
        }
        
        public override SpawnPointData GetSpawnPointData()
        {
            return new SpawnPointData(spawnPoint.position, spawnPoint.rotation);
        }
    }
}