using System.Collections.Generic;
using Game;
using UnityEngine;

namespace SpawnSystem
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField]
        RocketData rocketData;

        [SerializeField]
        PlatformData platformData;
        
        

        [SerializeField]
        Transform playerTransform;

        EntitySpawner<Rocket> rocketSpawner;
        EntitySpawner<Platform> platformSpawner;

        PlatformSpawnStrategy platformSpawnStrategy;

        float currentRocketSpawnInterval;
        int maxDistanceTraveled;

        void Awake()
        {
            BuildSpawners();
        }

        void Start()
        {
            SubscribeToExistingPlatforms();
            StartRocketSpawning();
            SubscribeToGameEvents();
        }

        void OnDestroy()
        {
            UnsubscribeFromGameEvents();
        }

        void BuildSpawners()
        {
            BuildRocketSpawner();
            BuildPlatformSpawner();
        }

        void BuildRocketSpawner()
        {
            ISpawnStrategy rocketSpawnStrategy = CreateRocketSpawnStrategy();
            IEntityFactory<Rocket> rocketFactory = CreateRocketFactory();

            rocketSpawner = new EntitySpawner<Rocket>(
                rocketFactory,
                rocketSpawnStrategy,
                rocketData);

            currentRocketSpawnInterval = rocketData.SpawnInterval;
        }

        ISpawnStrategy CreateRocketSpawnStrategy()
        {
            return new RocketSpawnStrategy(
                playerTransform,
                rocketData.HorizontalDistance,
                rocketData.MinSpawnHeight,
                rocketData.MaxSpawnHeight,
                rocketData.MinZOffset,
                rocketData.MaxZOffset);
        }

        IEntityFactory<Rocket> CreateRocketFactory()
        {
            return new EntityFactory<Rocket>();
        }

        void BuildPlatformSpawner()
        {
            platformSpawnStrategy = CreatePlatformSpawnStrategy();
            IEntityFactory<Platform> platformFactory = CreatePlatformFactory();

            platformSpawner = new EntitySpawner<Platform>(
                platformFactory,
                platformSpawnStrategy,
                platformData);
        }

        PlatformSpawnStrategy CreatePlatformSpawnStrategy()
        {
            return new PlatformSpawnStrategy(
                platformData.MinXOffset,
                platformData.MaxXOffset,
                platformData.ZSpacing);
        }

        IEntityFactory<Platform> CreatePlatformFactory()
        {
            return new EntityFactory<Platform>();
        }

        void SubscribeToGameEvents()
        {
            if (GameManager.Instance == null)
            {
                return;
            }

            GameManager.Instance.OnDistanceChanged += AdjustRocketSpawnInterval;
            GameManager.Instance.OnGameOver += CancelRocketSpawning;
        }

        void UnsubscribeFromGameEvents()
        {
            if (GameManager.Instance == null)
            {
                return;
            }

            GameManager.Instance.OnDistanceChanged -= AdjustRocketSpawnInterval;
            GameManager.Instance.OnGameOver -= CancelRocketSpawning;
        }

        void SubscribeToExistingPlatforms()
        {
            Platform[] platforms = FindObjectsOfType<Platform>();

            foreach (Platform platform in platforms)
            {
                platform.PlayerLanded += SpawnPlatformFrom;
            }
        }

        void StartRocketSpawning()
        {
            CancelInvoke(nameof(SpawnRocket));
            InvokeRepeating(nameof(SpawnRocket), 0f, currentRocketSpawnInterval);
        }

        void SpawnRocket()
        {
            rocketSpawner.Spawn();
        }

        void AdjustRocketSpawnInterval(int distance)
        {
            if (distance <= maxDistanceTraveled)
            {
                return;
            }

            maxDistanceTraveled = distance;

            if (maxDistanceTraveled % rocketData.DistanceIntervalForDifficultyIncrease != 0)
            {
                return;
            }

            currentRocketSpawnInterval *= rocketData.IntervalMultiplier;

            CancelInvoke(nameof(SpawnRocket));
            InvokeRepeating(nameof(SpawnRocket), 0f, currentRocketSpawnInterval);
        }

        void CancelRocketSpawning(long score)
        {
            CancelInvoke(nameof(SpawnRocket));
        }

        void SpawnPlatformFrom(Vector3 previousPlatformPosition)
        {
            platformSpawnStrategy.SetOrigin(previousPlatformPosition);

            Platform platform = platformSpawner.Spawn();

            if (platform != null)
            {
                platform.PlayerLanded += SpawnPlatformFrom;
            }
        }
    }
}