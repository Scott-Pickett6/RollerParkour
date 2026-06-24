using Entities;

namespace SpawnSystem
{
    public class PowerUpSpawnSystem : ISpawnSystem
    {
        EntitySpawner<Platform> entitySpawner;
        SpawnContext spawnContext;
        PowerUpData data;
        EntityFactory<Platform> factory;
        SpawnStrategy strategy;

        public PowerUpSpawnSystem(PowerUpData data, EntityFactory<Platform> factory, SpawnStrategy strategy, SpawnContext spawnContext)
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
            throw new System.NotImplementedException();
        }
    }
}