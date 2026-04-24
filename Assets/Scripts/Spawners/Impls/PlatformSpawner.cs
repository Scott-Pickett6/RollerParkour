using UnityEngine;

public class PlatformSpawner : MonoBehaviour, ISpawner
{
    [SerializeField]
    private GameObject[] platformPrefab;

    public void Spawn()
    {
        if (platformPrefab == null || platformPrefab.Length == 0)
        {
            return;
        }

        float randomOffsetX = Random.Range(-15f, 15f);
        Vector3 newPos = transform.position + new Vector3(randomOffsetX, 0f, 16f);

        int randomIndex = Random.Range(0, platformPrefab.Length);
        Instantiate(platformPrefab[randomIndex], newPos, Quaternion.identity);
    }
}
