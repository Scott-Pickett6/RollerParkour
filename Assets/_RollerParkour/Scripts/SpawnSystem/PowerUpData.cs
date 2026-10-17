using UnityEngine;

namespace SpawnSystem
{
    [CreateAssetMenu(fileName = "PowerUpData", menuName = "Entities/PowerUpData")]
    public class PowerUpData : EntityData
    {
        [SerializeField]
        float spawnProbability;
        
        public float SpawnProbability => spawnProbability;
    }
}