using UnityEngine;

namespace Spawning
{
    public interface IEntityFactory<T> where T : Entity
    {
        T Create(EntityData data, SpawnPointData spawnPointData);
    }
}