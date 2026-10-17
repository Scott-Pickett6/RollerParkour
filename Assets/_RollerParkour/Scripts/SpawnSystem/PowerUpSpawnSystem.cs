using Entities;
using UnityEngine;

namespace SpawnSystem
{
    public class PowerUpSpawnSystem : ISpawnSystem
    {
        EntitySpawner<PowerUpPickup> entitySpawner;
        SpawnContext spawnContext;
        PowerUpData data;
        EntityFactory<PowerUpPickup> factory;
        PowerUpSpawnStrategy strategy;

        public PowerUpSpawnSystem(PowerUpData data, EntityFactory<PowerUpPickup> factory, PowerUpSpawnStrategy strategy, SpawnContext spawnContext)
        {
            entitySpawner = new EntitySpawner<PowerUpPickup>(factory, strategy, data);
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