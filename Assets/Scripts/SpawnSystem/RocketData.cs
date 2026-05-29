using UnityEngine;

namespace SpawnSystem
{
    [CreateAssetMenu(fileName = "RocketData", menuName = "Entities/RocketData")]
    public class RocketData : EntityData
    {
        [SerializeField]
        float spawnInterval = 2f;
        [SerializeField]
        float intervalMultiplier = 0.9f;
        [SerializeField]
        int distanceIntervalForDifficultyIncrease = 20;
        [SerializeField]
        float horizontalDistance = 20f;
        [SerializeField]
        float minSpawnHeight = 0.5f;
        [SerializeField]
        float maxSpawnHeight = 2.5f;
        [SerializeField]
        float minZOffset = -1f;
        [SerializeField]
        float maxZOffset = 5f;
        
        public float SpawnInterval => spawnInterval;
        public float IntervalMultiplier => intervalMultiplier;
        public int DistanceIntervalForDifficultyIncrease => distanceIntervalForDifficultyIncrease;
        public float HorizontalDistance => horizontalDistance;
        public float MinSpawnHeight => minSpawnHeight;
        public float MaxSpawnHeight => maxSpawnHeight;
        public float MinZOffset => minZOffset;
        public float MaxZOffset => maxZOffset;
    }
}