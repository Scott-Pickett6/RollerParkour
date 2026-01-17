using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject rocketPrefab;

    [SerializeField] 
    private GameObject[] platformPrefab;

    [SerializeField]
    private float rocketSpawnInterval;


    void Start()
    {
        InvokeRepeating("SpawnRocket", 0f, rocketSpawnInterval);
    }

    
    void Update()
    {
        
    }

    private void SpawnRocket()
    {
        bool leftSide = Random.value > 0.5f;

        float lowBias = Random.value * Random.value;
        float spawnHeight = Mathf.Lerp(0.5f, 2.5f, lowBias);

        float zOffset = Random.Range(-1, 5);
        Instantiate(rocketPrefab, transform.position + new Vector3(leftSide ? -16 : 16, spawnHeight, zOffset), Quaternion.Euler(0f, leftSide ? 180f : 0, 0));
    }

    public void SpawnGround(Vector3 oldPos)
    {
        float randomOffsetX = Random.Range(-15f, 15f);
        Vector3 newPos = oldPos + new Vector3(randomOffsetX, 0, 16);

        int randomIndex = Random.Range(0, platformPrefab.Length);
        Instantiate(platformPrefab[randomIndex], newPos, Quaternion.identity);
    }
}
