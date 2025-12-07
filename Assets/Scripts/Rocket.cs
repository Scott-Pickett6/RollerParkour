using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float speed;

    void Start()
    {

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(transform.position.x < -25f || transform.position.x > 25f)
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
