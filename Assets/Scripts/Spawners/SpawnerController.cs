using Game;
using UnityEngine;

namespace Spawners
{
    public class SpawnerController : MonoBehaviour
    { 
        [Header("Prefab Spawners")]
        [SerializeField]
        PrefabSpawner rocketSpawner;

        [SerializeField]
        PrefabSpawner platformSpawner;

        [SerializeField]
        Transform playerTransform;

        [SerializeField]
        float rocketSpawnInterval = 2f;

        [SerializeField]
        float rocketIntervalMultiplier = 0.9f;

        [SerializeField]
        int distanceIntervalForDifficultyIncrease = 20;

        [SerializeField]
        float rocketHorizontalDistance = 20f;

        [SerializeField]
        float minRocketSpawnHeight = 0.5f;

        [SerializeField]
        float maxRocketSpawnHeight = 2.5f;

        [SerializeField]
        float minRocketZOffset = -1f;

        [SerializeField]
        float maxRocketZOffset = 5f;

        [SerializeField]
        float minPlatformXOffset = -15f;

        [SerializeField]
        float maxPlatformXOffset = 15f;

        [SerializeField]
        float platformZSpacing = 16f;

        int maxDistanceTraveled;
        
        void Start()
        {
            SubscribeToExistingPlatforms();
            StartRocketSpawning();

            if (GameManager.Instance == null)
            {
                return;
            }

            GameManager.Instance.OnDistanceChanged += AdjustSpawnInterval;
            GameManager.Instance.OnGameOver += CancelRocketSpawning;
        }

        void SubscribeToExistingPlatforms()
        {
            Platform[] platforms = FindObjectsOfType<Platform>();

            foreach (Platform platform in platforms)
            {
                platform.PlayerLanded += SpawnGround;
            }
        }

        void OnDestroy()
        {
            if (GameManager.Instance == null)
            {
                return;
            }

            GameManager.Instance.OnDistanceChanged -= AdjustSpawnInterval;
            GameManager.Instance.OnGameOver -= CancelRocketSpawning;
        }

        void StartRocketSpawning()
        {
            CancelInvoke(nameof(SpawnRocket));
            InvokeRepeating(nameof(SpawnRocket), 0f, rocketSpawnInterval);
        }

        void SpawnRocket()
        {
            bool spawnOnLeftSide = Random.value > 0.5f;

            float lowBias = Random.value * Random.value;
            float spawnHeight = Mathf.Lerp(minRocketSpawnHeight, maxRocketSpawnHeight, lowBias);

            float xOffset = spawnOnLeftSide ? -rocketHorizontalDistance : rocketHorizontalDistance;
            float zOffset = Random.Range(minRocketZOffset, maxRocketZOffset);

            Vector3 spawnPosition = playerTransform.position + new Vector3(xOffset, spawnHeight, zOffset);
            Quaternion spawnRotation = Quaternion.Euler(0f, spawnOnLeftSide ? 180f : 0f, 0f);

            rocketSpawner.Spawn(spawnPosition, spawnRotation);
        }

        void AdjustSpawnInterval(int distance)
        {
            if (distance <= maxDistanceTraveled)
            {
                return;
            }

            maxDistanceTraveled = distance;

            if (maxDistanceTraveled % distanceIntervalForDifficultyIncrease != 0)
            {
                return;
            }

            rocketSpawnInterval *= rocketIntervalMultiplier;

            CancelInvoke(nameof(SpawnRocket));
            InvokeRepeating(nameof(SpawnRocket), 0f, rocketSpawnInterval);
        }

        void CancelRocketSpawning(long score)
        {
            CancelInvoke(nameof(SpawnRocket));
        }

        public void SpawnGround(Vector3 oldPosition)
        {
            float randomOffsetX = Random.Range(minPlatformXOffset, maxPlatformXOffset);
            Vector3 spawnPosition = oldPosition + new Vector3(randomOffsetX, 0f, platformZSpacing);

            GameObject platformObject = platformSpawner.Spawn(spawnPosition, Quaternion.identity);

            if (platformObject != null && platformObject.TryGetComponent(out Platform platform))
            {
                platform.PlayerLanded += SpawnGround;
            }
        }
    }
}
