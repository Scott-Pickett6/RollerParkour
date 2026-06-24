using System;
using System.Collections.Generic;
using Entities;
using Game;
using UnityEngine;

namespace SpawnSystem
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField]
        Transform playerTransform;
        [SerializeField]
        Platform initialPlatform;
        
        [SerializeField]
        SpawnSystemDefinition[] spawnSystemDefinitions;

        List<ISpawnSystem> spawnSystems;
        List<ITickable> tickables;
        
        SpawnContext spawnContext;

        void Awake()
        {
            spawnContext = new SpawnContext
            {
                PlayerTransform = playerTransform,
                InitialPlatform = initialPlatform
            };
            
            spawnSystems = new List<ISpawnSystem>();
            tickables = new List<ITickable>();
            
            foreach(SpawnSystemDefinition spawnSystemDefinition in spawnSystemDefinitions)
            {
                ISpawnSystem system = spawnSystemDefinition.CreateSpawnSystem(spawnContext);
                spawnSystems.Add(system);
                if (system is ITickable tickable)
                {
                    tickables.Add(tickable);
                }
            }
        }
        
        void Start()
        {
            foreach (ISpawnSystem spawnSystem in spawnSystems)
            {
                spawnSystem.Init();
            }
        }

        void OnDestroy()
        {
            foreach (ISpawnSystem spawnSystem in spawnSystems)
            {
                spawnSystem.Dispose();
            }
        }

        void Update()
        {
            foreach (ITickable tickable in tickables)
            {
                tickable.Tick(Time.deltaTime);
            }
        }
    }
}