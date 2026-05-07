using UnityEngine;

namespace Spawning
{
    public abstract class EntityData : ScriptableObject
    {
        [SerializeField]
        EntityPrefabProvider prefabProvider;

        public GameObject GetPrefab()
        {
            return prefabProvider.GetPrefab();
        }
    }
}