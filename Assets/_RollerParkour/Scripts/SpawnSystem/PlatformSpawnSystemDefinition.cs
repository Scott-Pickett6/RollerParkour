using Entities;
using UnityEngine;

namespace SpawnSystem
{
    [CreateAssetMenu(fileName = "PlatformSpawnSystemDefinition", menuName = "SpawnSystem/PlatformSpawnSystemDefinition")]
    public class PlatformSpawnSystemDefinition : SpawnSystemDefinition
    {
        [SerializeField]
        PlatformData platformData;
        
        public override ISpawnSystem CreateSpawnSystem(SpawnContext context)
        {
            return new PlatformSpawnSystem(
                platformData, 
                new EntityFactoryImpl<Platform>(),
                new PlatformSpawnStrategy(platformData.MinXOffset, platformData.MaxXOffset, platformData.ZSpacing),
                context);
        }
    }
}