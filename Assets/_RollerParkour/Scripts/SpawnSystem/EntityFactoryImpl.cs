using Entities;
using UnityEngine;

namespace SpawnSystem
{
    public class EntityFactoryImpl<T> : EntityFactory<T> where T : Entity
    {
        public override T Create(EntityData data, SpawnPointData spawnPointData)
        {
            GameObject prefab = data.GetPrefab();
            GameObject instance = Object.Instantiate(prefab, spawnPointData.Position, spawnPointData.Rotation, spawnPointData.Parent);
            return instance.GetComponent<T>();
        }
    }
}