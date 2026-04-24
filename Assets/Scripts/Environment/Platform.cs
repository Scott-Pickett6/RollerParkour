using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private GameObject player;
    private Spawner spawner;
    private GameObject powerUp;
    private bool hasBeenHit;

    void Start()
    {
        hasBeenHit = false;
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            spawner = player.GetComponent<Spawner>();
        }

        Transform powerUpTransform = transform.Find("Power_Up");
        if (powerUpTransform == null)
        {
            return;
        }

        powerUp = powerUpTransform.gameObject;
        if (Random.value < 0.8f)
        {
            powerUp.SetActive(false);
        }
    }

    
    void Update()
    {
        if (player == null)
        {
            return;
        }

        if(player.transform.position.z - transform.position.z > 7)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") || hasBeenHit)
        {
            return;
        }

        if (GameStateManager.Instance != null && GameStateManager.Instance.CurrentGameState == GameState.GameOver)
        {
            return;
        }

        hasBeenHit = true;
        if (spawner != null)
        {
            spawner.SpawnGround(transform.position);
        }
    }
}
