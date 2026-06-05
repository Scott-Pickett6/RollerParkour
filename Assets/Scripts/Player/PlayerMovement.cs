using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour, IMovement
    {
        [SerializeField]
        float maxSpeed;
        [SerializeField] 
        float accelerationRate;
        [SerializeField]
        float jumpForce;
        
        Rigidbody rb;
        bool isGrounded = true;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 direction)
        {
            if (!isGrounded) return;

            if (direction.sqrMagnitude < 0.01f) return;

            Vector3 currentVelocity = rb.velocity;

            Vector3 targetVelocity = direction.normalized * maxSpeed;

            Vector3 currentXZ = new Vector3(currentVelocity.x, 0f, currentVelocity.z);
            Vector3 targetXZ = new Vector3(targetVelocity.x, 0f, targetVelocity.z);

            Vector3 newXZ = Vector3.MoveTowards(
                currentXZ,
                targetXZ,
                accelerationRate * Time.fixedDeltaTime
            );

            rb.velocity = new Vector3(newXZ.x, currentVelocity.y, newXZ.z);
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

