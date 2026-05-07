using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour, IMovement
    {
        [SerializeField]
        float maxSpeed = 8f;
        [SerializeField]
        float acceleration = 25f;
        [SerializeField]
        float jumpForce = 8f;
        
        Rigidbody rb;
        bool isGrounded = true;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 direction)
        {
            if (!isGrounded) return;

            Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            float horizontalSpeed = horizontalVelocity.magnitude;

            if (horizontalSpeed < maxSpeed)
            {
                Vector3 movementDirection = new Vector3(direction.x, 0f, direction.z);
                Vector3 accelerationForce = movementDirection * acceleration;

                rb.AddForce(accelerationForce, ForceMode.Force);
            }
            else
            {
                Vector3 limitedVelocity = horizontalVelocity.normalized * maxSpeed;
                rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
            }
        }

        public void TryJump()
        {
            if (!isGrounded) return;

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        public void DisablePhysics()
        {
            rb.isKinematic = true;
        }

        void OnCollisionStay(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Platform")) return;

            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                    return;
                }
            }
        }

        void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                isGrounded = false;
            }
        }
    }
}

