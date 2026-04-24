using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody rb;
        private float maxSpeed = 8f;
        private float acceleration = 25f;
        private float jumpForce = 3.5f;
        private bool isGrounded;
        private const float groundThreshold = 0.5f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Move(PlayerInputInfo playerInputInfo)
        {
            if (!isGrounded)
                return;

            Vector3 horizontalVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            float horizontalSpeed = horizontalVel.magnitude;

            // accelerate if slower than max speed
            if (horizontalSpeed < maxSpeed)
            {
                rb.AddForce(Vector3.forward * acceleration * playerInputInfo.ForwardInput, ForceMode.Acceleration);
                rb.AddForce(Vector3.right * acceleration * playerInputInfo.SideInput, ForceMode.Acceleration);
            }
            else
            {
                Vector3 limited = horizontalVel.normalized * maxSpeed;
                rb.velocity = new Vector3(limited.x, rb.velocity.y, limited.z);
            }

            if (playerInputInfo.JumpPressed)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                foreach (ContactPoint contact in collision.contacts)
                {
                    if (contact.normal.y > groundThreshold)
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
}