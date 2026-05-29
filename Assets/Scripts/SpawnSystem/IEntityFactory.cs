using UnityEngine;

namespace SpawnSystem
{
    public interface IEntityFactory<T> where T : Entity
    {
        T Create(EntityData data, SpawnPointData spawnPointData);
    }
}