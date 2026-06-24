using Entities;
using UnityEngine;

namespace SpawnSystem
{
    public class PlatformSpawnSystem : ISpawnSystem
    {
        EntitySpawner<Platform> entitySpawner;
        EntityFactory<Platform> factory;
        PlatformSpawnStrategy strategy;
        PlatformData data;
        Platform currentPlatform;
        Platform nextPlatform;
        Transform playerTransform;
        SpawnContext spawnContext;
        
        public PlatformSpawnSystem(PlatformData data, EntityFactory<Platform> factory, PlatformSpawnStrategy strategy, SpawnContext spawnContext)
        {
            entitySpawner = new EntitySpawner<Platform>(factory, strategy, data);
            this.factory = factory;
            this.strategy = strategy;
            this.data = data;
            this.spawnContext = spawnContext;
            currentPlatform = spawnContext.InitialPlatform;
            playerTransform = spawnContext.PlayerTransform;
        }
        
        public void Init()
        {
            currentPlatform.PlayerLanded += SpawnPlatformFrom;
        }
        public void Dispose()
        {
            currentPlatform.PlayerLanded -= SpawnPlatformFrom;
        }
        
        void SpawnPlatformFrom(Vector3 previousPosition)
        {
            strategy.SetOrigin(previousPosition);

            nextPlatform = entitySpawner.Spawn();
            nextPlatform.Init(data, playerTransform);
            spawnContext.RaisePlatformSpawned(nextPlatform);

            if (nextPlatform != null)
            {
                nextPlatform.PlayerLanded += SpawnPlatformFrom;
            }
        }
    }
}