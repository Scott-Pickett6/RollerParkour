using Entities;
using UnityEngine;

namespace SpawnSystem
{
    public class PowerUpSpawnStrategy : SpawnStrategy
    {
        Platform nextPlatform;
        public PowerUpSpawnStrategy()
        {
            
        }
        public override SpawnPointData GetSpawnPointData()
        {
            // random power up location
            Transform point = nextPlatform.Points[Random.Range(0, nextPlatform.Points.Length)].transform;
            return new SpawnPointData(
                point.position,
                point.rotation
                );
        }

        // always called before GetSpawnPointData
        public void SetNextPlatform(Platform platform)
        {
            nextPlatform = platform;
        }
    }
}