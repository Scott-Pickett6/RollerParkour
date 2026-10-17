using UnityEngine;

namespace SpawnSystem
{
    public abstract class EntityPrefabProvider : ScriptableObject
    {
        public abstract GameObject GetPrefab();
    }
}
