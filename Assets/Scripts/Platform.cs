using System.Collections;
using System.Collections.Generic;
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
        spawner = player.GetComponent<Spawner>();
        powerUp = gameObject.transform.Find("Power_Up").gameObject;
        float powerUpChance = Random.value;
        if (powerUpChance < 0.8f)
        {
            powerUp.SetActive(false);
        }
    }

    
    void Update()
    {
        if(player.transform.position.z - transform.position.z > 7)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasBeenHit && GameManager.CurrentGameState != GameState.GameOver)
        {
            hasBeenHit = true;
            spawner.SpawnGround(transform.position);
        }
    }
}
