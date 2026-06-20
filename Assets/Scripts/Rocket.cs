using System;
using Game;
using Player;
using PowerUp;
using UnityEngine;

public class Rocket : Entity
{

    [SerializeField]
    float speed;
    [SerializeField]
    GameObject explosionEffectPrefab;
    [SerializeField]
    float destroyDistance;
    
    Rigidbody rb;
    Vector3 startingPos;

    void Start()
    {
        startingPos = transform.position;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        DestroyAllEnemyPowerUpEffect.OnDestroyAllEnemies += Destroy;
    }

    void OnDisable()
    {
        DestroyAllEnemyPowerUpEffect.OnDestroyAllEnemies -= Destroy;
    }

    void Update()
    {
        if (DetectOffMap())
        {
            Destroy(gameObject);
        }
        if (GameStateManager.Instance.CurrentGameState == GameState.GameOver)
        {
            Destroy(gameObject);
        }
    }
    
    void FixedUpdate()
    {
        rb.velocity = -transform.right * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        // better solution would be to handle the shield logic in the player script
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null && player.HasSheild)
        {
            player.HasSheild = false;
        }
        else if(player != null)
        {
            // we don't like this
            GameManager.Instance.GameOver();
        }
        TriggerExplosion();
        Destroy(gameObject);
    }

    void TriggerExplosion()
    {
        if (explosionEffectPrefab != null)
        {
            GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

            Destroy(explosion, 5f);
        }
    }

    bool DetectOffMap()
    {
        if (Mathf.Abs(transform.position.x - startingPos.x) > destroyDistance)
        {
            return true;
        }
        return false;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
