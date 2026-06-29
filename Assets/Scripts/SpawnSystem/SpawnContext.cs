using System;
using Entities;
using UnityEngine;

namespace SpawnSystem
{
    public class SpawnContext
    {
        public Transform PlayerTransform { get; set; }
        public Platform InitialPlatform { get; set; }
        public event Action<Platform> PlatformSpawned;
        
        public void RaisePlatformSpawned(Platform platform)
        {
            PlatformSpawned?.Invoke(platform);
        }
    }
}