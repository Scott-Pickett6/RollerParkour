using UnityEngine;

namespace SpawnSystem
{
    [CreateAssetMenu(fileName = "PowerUpSpawnLocationData", menuName = "Entities/PowerUpSpawnLocationData")]

    public class PowerUpSpawnLocationData : ScriptableObject
    {
        [SerializeField]
        SpawnPointData[] spawnLocations;
    }
}