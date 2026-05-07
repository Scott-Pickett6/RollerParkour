using UnityEngine;

namespace Spawning
{
    public abstract class EntityPrefabProvider : ScriptableObject
    {
        public abstract GameObject GetPrefab();
    }
}
