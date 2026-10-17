using Game;
using UnityEngine;

namespace SpawnSystem
{
    public abstract class SpawnSystemDefinition : ScriptableObject
    {
        public abstract ISpawnSystem CreateSpawnSystem(SpawnContext context);
    }
}