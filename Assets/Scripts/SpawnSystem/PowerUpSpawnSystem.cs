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
            spawnContext.PlatformSpawned += OnPlatformSpawned;
        }

        public void Dispose()
        {
            spawnContext.PlatformSpawned -= OnPlatformSpawned;
        }

        void OnPlatformSpawned(Platform platform)
        {
            if (Random.value < data.SpawnProbability)
            {
                SpawnPowerUp(platform);
            }
        }

        void SpawnPowerUp(Platform platform)
        {
            strategy.SetNextPlatform(platform);
            entitySpawner.Spawn();
        }
    }
}