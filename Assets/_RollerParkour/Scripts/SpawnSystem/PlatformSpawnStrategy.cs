using UnityEngine;

namespace SpawnSystem
{
    
    public class PlatformSpawnStrategy : SpawnStrategy
    {
        readonly float minXOffset;
        readonly float maxXOffset;
        readonly float zSpacing;
        
        Vector3 originPosition;

        public PlatformSpawnStrategy(float minXOffset, float maxXOffset, float zSpacing)
        {
            this.minXOffset = minXOffset;
            this.maxXOffset = maxXOffset;
            this.zSpacing = zSpacing;
        }

        public void SetOrigin(Vector3 originPosition)
        {
            this.originPosition = originPosition;
        }
        
        public override SpawnPointData GetSpawnPointData()
        {
            float randomXOffset = Random.Range(minXOffset, maxXOffset);
            Vector3 position = originPosition + new Vector3(randomXOffset, 0f, zSpacing);
            return new SpawnPointData(position, Quaternion.identity);
        }
    }
}