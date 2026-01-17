using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private GameObject player;
    private Spawner spawner;

    private bool hasBeenHit;

    void Start()
    {
        hasBeenHit = false;
        player = GameObject.FindWithTag("Player");
        spawner = player.GetComponent<Spawner>();
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
        if (collision.gameObject.CompareTag("Player") && !hasBeenHit)
        {
            hasBeenHit = true;
            spawner.SpawnGround(transform.position);
        }
    }
}
