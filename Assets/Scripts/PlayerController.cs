using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed;
    private float jumpForce;
    private Rigidbody rb;
    private bool isGrounded;
    private float horizontalInput;
    private float verticalInput;
    private float jumpInput;


    void Start()
    {

    }

    private void Awake()
    {
        speed = 15f;
        jumpForce = 3.5f;
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
        horizontalInput = 0;
        verticalInput = 0;
        jumpInput = 0;
    }

    void Update()
    {
       horizontalInput  = Input.GetAxis("Horizontal");
       verticalInput  = Input.GetAxis("Vertical");
       jumpInput  = Input.GetAxis("Jump");
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.forward * speed * verticalInput, ForceMode.Force);
            rb.AddForce(Vector3.right * speed * horizontalInput, ForceMode.Force);
            rb.AddForce(Vector3.up * jumpForce * jumpInput, ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
