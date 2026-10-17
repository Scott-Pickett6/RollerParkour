using Entities;
using UnityEngine;

namespace SpawnSystem
{
    [CreateAssetMenu(fileName = "RocketSpawnSystemDefinition", menuName = "SpawnSystem/RocketSpawnSystemDefinition")]
    public class RocketSpawnSystemDefinition : SpawnSystemDefinition
    {
        [SerializeField]
        RocketData rocketData;

        public override ISpawnSystem CreateSpawnSystem(SpawnContext context)
        {
            return new RocketSpawnSystem(rocketData, 
                new EntityFactoryImpl<Rocket>(),
                new RocketSpawnStrategy(
                    context.PlayerTransform, 
                    rocketData.HorizontalDistance, 
                    rocketData.MinSpawnHeight, 
                    rocketData.MaxSpawnHeight, 
                    rocketData.MinZOffset,
                    rocketData.MaxZOffset));
        }
    }
}