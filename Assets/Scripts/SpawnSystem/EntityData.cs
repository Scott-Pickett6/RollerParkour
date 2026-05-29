using UnityEngine;

namespace SpawnSystem
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