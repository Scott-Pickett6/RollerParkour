using UnityEngine;

namespace SpawnSystem
{
    [CreateAssetMenu(fileName = "PlatformData", menuName = "Entities/PlatformData")]
    public class PlatformData : EntityData
    {
        [SerializeField]
        float zSpacing = 16f;
        [SerializeField]
        float minXOffset = -15f;
        [SerializeField]
        float maxXOffset = 15f;
        [SerializeField]
        float despawnDistance = 15f;
        
        public float ZSpacing => zSpacing;
        public float MinXOffset => minXOffset;
        public float MaxXOffset => maxXOffset;
        public float DespawnDistance => despawnDistance;
    }
}