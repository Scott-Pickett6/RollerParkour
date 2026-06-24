using Entities;

namespace SpawnSystem
{
    public class EntitySpawner<T> where T : Entity
    {
        EntityFactory<T> factory;
        SpawnStrategy strategy;
        EntityData data;
        
        public EntitySpawner(EntityFactory<T> factory, SpawnStrategy strategy, EntityData data)
        {
            this.factory = factory;
            this.strategy = strategy;
            this.data = data;
        }
        
        public T Spawn()
        {
            SpawnPointData spawnPointData = strategy.GetSpawnPointData();
            return factory.Create(data, spawnPointData);
        }
    }
}