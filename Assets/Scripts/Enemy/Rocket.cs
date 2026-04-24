using Assets.Scripts.Interfaces;
using Assets.Scripts.Managers;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float speed = 6;

    private Vector3 startingPos;

    [SerializeField]
    private GameObject explosionEffectPrefab;

    void Start()
    {
        startingPos = transform.position;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OnGameOver += HandleGameOver;
        }
    }
    private void OnDisable()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OnGameOver -= HandleGameOver;
        }
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x - startingPos.x) > 50f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IRocketHittable>(out IRocketHittable hittable))
        {
            hittable.OnRocketHit();
        }
        Explode();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + -transform.right * speed * Time.fixedDeltaTime);
    }

    private void HandleGameOver()
    {
        Destroy(gameObject);
    }

    private void Explode()
    {
        if (explosionEffectPrefab != null)
        {
            GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

            Destroy(explosion, 5f);
            Destroy(gameObject);
        }
    }
}
