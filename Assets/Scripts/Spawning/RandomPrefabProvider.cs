using UnityEngine;

namespace Spawning
{
    [CreateAssetMenu(fileName = "RandomPrefabProvider", menuName = "Entities/Prefab Providers/Random Prefab Provider")]
    public class RandomPrefabProvider : EntityPrefabProvider
    {
        [SerializeField]
        GameObject[] prefabs;
        
        public override GameObject GetPrefab()
        {
            return prefabs[Random.Range(0, prefabs.Length)];
        }
    }
}