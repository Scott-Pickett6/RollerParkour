using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float maxSpeed;
    private float acceleration;
    private float jumpForce;
    private Rigidbody rb;
    private bool isGrounded;
    private float sideInput;
    private float forwardInput;
    private float jumpInput;


    void Start()
    {

    }

    private void Awake()
    {
        acceleration = 25f;
        maxSpeed = 8f;
        jumpForce = 3.5f;
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
        sideInput = 0;
        forwardInput = 0;
        jumpInput = 0;
    }

    void Update()
    {
       sideInput  = Input.GetAxis("Side");
       forwardInput  = Input.GetAxis("Forward");
       jumpInput  = Input.GetAxis("Jump");
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            Vector3 horizontalVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            float horizontalSpeed = horizontalVel.magnitude;

            // accelerate if slower than max speed
            if (horizontalSpeed < maxSpeed)
            {
                rb.AddForce(Vector3.forward * acceleration * forwardInput, ForceMode.Acceleration);
                rb.AddForce(Vector3.right * acceleration * sideInput, ForceMode.Acceleration);
            }
            else
            {
                Vector3 limited = horizontalVel.normalized * maxSpeed;
                rb.velocity = new Vector3(limited.x, rb.velocity.y, limited.z);
            }

            if(jumpInput > 0)
            {
                rb.AddForce(Vector3.up * jumpForce * jumpInput, ForceMode.Impulse);
                isGrounded = false;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            foreach(ContactPoint contact in collision.contacts)
            {
                if(contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
}
