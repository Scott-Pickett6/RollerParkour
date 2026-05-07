using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class PrefabSpawner : MonoBehaviour, ISpawner
    {
        [SerializeField]
        private GameObject[] prefabs;

        public GameObject Spawn(Vector3 position, Quaternion rotation)
        {
            int randomIndex = Random.Range(0, prefabs.Length);
            GameObject prefab = prefabs[randomIndex];
            return Instantiate(prefab, position, rotation);
        }
    }
}