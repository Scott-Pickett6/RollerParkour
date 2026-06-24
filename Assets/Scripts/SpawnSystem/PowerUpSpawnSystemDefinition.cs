using Entities;
using UnityEngine;

namespace SpawnSystem
{
    [CreateAssetMenu(fileName = "PowerUpSpawnSystemDefinition", menuName = "SpawnSystem/PowerUpSpawnSystemDefinition")]
    public class PowerUpSpawnSystemDefinition : SpawnSystemDefinition
    {
        [SerializeField]
        PowerUpData powerUpData;
        
        public override ISpawnSystem CreateSpawnSystem(SpawnContext context)
        {
            return new PowerUpSpawnSystem(
                powerUpData,
                new EntityFactoryImpl<Platform>(),
                new PowerUpSpawnStrategy(),
                context);
        }
    }
}