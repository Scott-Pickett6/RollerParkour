namespace Spawning
{
    public class EntitySpawner<T> where T : Entity
    {
        IEntityFactory<T> factory;
        ISpawnStrategy strategy;
        EntityData data;
        
        public EntitySpawner(IEntityFactory<T> factory, ISpawnStrategy strategy, EntityData data)
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