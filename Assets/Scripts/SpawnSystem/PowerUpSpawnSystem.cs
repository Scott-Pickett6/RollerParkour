using Entities;
using UnityEngine;

namespace SpawnSystem
{
    public class PowerUpSpawnSystem : ISpawnSystem
    {
        EntitySpawner<Platform> entitySpawner;
        SpawnContext spawnContext;
        PowerUpData data;
        EntityFactory<Platform> factory;
        PowerUpSpawnStrategy strategy;

        public PowerUpSpawnSystem(PowerUpData data, EntityFactory<Platform> factory, PowerUpSpawnStrategy strategy, SpawnContext spawnContext)
        {
            entitySpawner = new EntitySpawner<Platform>(factory, strategy, data);
            this.data = data;
            this.factory = factory;
            this.strategy = strategy;
            this.spawnContext = spawnContext;
        }
        public void Init()
        {
            spawnContext.PlatformSpawned += SpawnPowerUp;
        }

        public void Dispose()
        {
            spawnContext.PlatformSpawned -= SpawnPowerUp;
        }

        void SpawnPowerUp(Platform platform)
        {
            strategy.SetNextPlatform(platform);
            entitySpawner.Spawn();
        }
    }
}