using Entities;
using UnityEngine;

namespace SpawnSystem
{
    public abstract class EntityFactory<T> where T : Entity
    {
       public abstract T Create(EntityData data, SpawnPointData spawnPointData);
    }
}