using Game;
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
        rb.MovePosition(transform.position + -transform.right * (speed * Time.fixedDeltaTime));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerExplosion();
            GameManager.Instance.RocketHitPlayer();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Platform"))
        {
            TriggerExplosion();
            Destroy(gameObject);
        }
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
    
}
