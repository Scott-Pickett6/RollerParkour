using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float speed;

    private Vector3 startingPos;

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
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + -transform.right * speed * Time.fixedDeltaTime);
    }
}
