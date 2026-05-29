using UnityEngine;

namespace SpawnSystem
{
    [CreateAssetMenu(fileName = "SinglePrefabProvider", menuName = "Entities/Prefab Providers/Single Prefab Provider")]
    public class SinglePrefabProvider : EntityPrefabProvider
    {
        [SerializeField]
        GameObject prefab;
        
        public override GameObject GetPrefab()
        {
            return prefab;
        }
    }
}