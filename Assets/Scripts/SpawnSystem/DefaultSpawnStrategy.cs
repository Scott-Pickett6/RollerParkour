using UnityEngine;

namespace SpawnSystem
{
    public class DefaultSpawnStrategy : ISpawnStrategy
    {
        readonly Transform spawnPoint;
        
        public DefaultSpawnStrategy(Transform spawnPoint)
        {
            this.spawnPoint = spawnPoint;
        }
        
        public SpawnPointData GetSpawnPointData()
        {
            return new SpawnPointData(spawnPoint.position, spawnPoint.rotation);
        }
    }
}