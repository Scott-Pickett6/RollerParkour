using UnityEngine;

namespace SpawnSystem
{
    public class RocketSpawnStrategy : SpawnStrategy
    {
        readonly Transform originTransform;
        readonly float horizontalDistance;
        readonly float minHeight;
        readonly float maxHeight;
        readonly float minZOffset;
        readonly float maxZOffset;

        public RocketSpawnStrategy(
            Transform originPosition,
            float horizontalDistance,
            float minHeight,
            float maxHeight,
            float minZOffset,
            float maxZOffset)
        {
            this.originTransform = originPosition;
            this.horizontalDistance = horizontalDistance;
            this.minHeight = minHeight;
            this.maxHeight = maxHeight;
            this.minZOffset = minZOffset;
            this.maxZOffset = maxZOffset;
        }

        public override SpawnPointData GetSpawnPointData()
        {
            bool spawnOnLeftSide = Random.value > 0.5f;

            float lowBias = Random.value * Random.value;
            float spawnHeight = Mathf.Lerp(minHeight, maxHeight, lowBias);
            
            float xOffset = spawnOnLeftSide ? -horizontalDistance : horizontalDistance;
            float zOffset = Mathf.Lerp(
                minZOffset,
                maxZOffset,
                1f - (Random.value * Random.value * Random.value)
            );

            Vector3 position = originTransform.position + new Vector3(xOffset, spawnHeight, zOffset);
            Quaternion rotation = Quaternion.Euler(0f, spawnOnLeftSide ? 180f : 0f, 0f);

            return new SpawnPointData(position, rotation);
        }
    }
}