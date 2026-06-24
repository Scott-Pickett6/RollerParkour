using Entities;
using UnityEngine;

namespace SpawnSystem
{
    public class PowerUpSpawnSystemDefinition : SpawnSystemDefinition
    {
        [SerializeField]
        PowerUpData powerUpData;
        
        public override ISpawnSystem CreateSpawnSystem(SpawnContext context)
        {
            return new PowerUpSpawnSystem(
                powerUpData,
                new EntityFactoryImpl<Platform>(),
                new PowerUpSpawnStrategy(context), // should need the powerUpData, also needs the context to get events
                context);
        }
    }
}