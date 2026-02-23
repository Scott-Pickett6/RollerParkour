using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float speed;

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

    void Update()
    {
        if (Mathf.Abs(transform.position.x - startingPos.x) > 50f)
        {
            Destroy(gameObject);
        }
        if (GameManager.CurrentGameState == GameState.GameOver)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerExplosion();
            GameManager.Instance.GameOver();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Platform"))
        {
            TriggerExplosion();
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + -transform.right * speed * Time.fixedDeltaTime);
    }

    private void TriggerExplosion()
    {
        if (explosionEffectPrefab != null)
        {
            GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

            Destroy(explosion, 5f);
        }
    }
}
