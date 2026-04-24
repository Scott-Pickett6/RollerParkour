using Assets.Scripts.Managers;
using UnityEngine;
using System.Collections;

public class RocketSpawner : MonoBehaviour, ISpawner, ITimerBasedSpawner
{
    [SerializeField]
    private GameObject rocketPrefab;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float initialSpawnInterval = 2.0f;

    private float currentSpawnInterval;
    private bool isSpawningRockets = false;
    private Coroutine spawnCoroutine;
    private int maxDistanceTraveled = 0;

    private void OnEnable()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OnGameStarted += StartSpawning;
            GameStateManager.Instance.OnGameOver += StopSpawning;
        }
    }

    private void OnDestroy()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OnGameStarted -= StartSpawning;
            GameStateManager.Instance.OnGameOver -= StopSpawning;
        }
    }

    public void StartSpawning()
    {
        isSpawningRockets = true;
        currentSpawnInterval = initialSpawnInterval;
        spawnCoroutine = StartCoroutine(SpawnRockets());
    }

    public void StopSpawning()
    {
        isSpawningRockets = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    private IEnumerator SpawnRockets()
    {
        while (isSpawningRockets)
        {
            Spawn();
            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }

    public void AdjustSpawnInterval(int distance)
    {
        if (distance > maxDistanceTraveled)
        {
            maxDistanceTraveled = distance;
            if (maxDistanceTraveled % 20 == 0)
            {
                currentSpawnInterval *= 0.9f; // Decrease interval to make spawning faster
            }
        }
    }

    public void Spawn()
    {
        bool leftSide = Random.value > 0.5f;

        float lowBias = Random.value * Random.value;
        float spawnHeight = Mathf.Lerp(0.5f, 2.5f, lowBias);

        float zOffset = Random.Range(-1, 5);
        Instantiate(rocketPrefab, player.transform.position + new Vector3(leftSide ? -20 : 20, spawnHeight, zOffset), Quaternion.Euler(0f, leftSide ? 180f : 0, 0));
    }
}
