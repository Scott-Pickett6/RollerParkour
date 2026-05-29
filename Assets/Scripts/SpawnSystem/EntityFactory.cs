using UnityEngine;

namespace SpawnSystem
{
    public class EntityFactory<T> : IEntityFactory<T> where T : Entity
    {
        public T Create(EntityData data, SpawnPointData spawnPointData)
        {
            GameObject instance = Object.Instantiate(data.GetPrefab(), spawnPointData.Position, spawnPointData.Rotation, spawnPointData.Parent);
            return instance.GetComponent<T>();
        }
    }
}