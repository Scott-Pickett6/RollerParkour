using UnityEngine;

namespace Spawning
{
    public struct SpawnPointData
    {
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;
        public readonly Transform Parent;
        
        public SpawnPointData(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Parent = parent;
        }
    }
}