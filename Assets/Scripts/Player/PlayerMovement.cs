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
        [SerializeField] 
        float groundCheckDistance;
        
        Rigidbody rb;
        SphereCollider sc;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            sc = GetComponent<SphereCollider>();
        }

        bool IsGrounded()
        {
            float distance = sc.radius + groundCheckDistance;

            return Physics.Raycast(
                transform.position,
                Vector3.down,
                distance
            );
        }

        public void Move(Vector3 direction)
        {
            if (!IsGrounded()) return;

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
            if (!IsGrounded()) return;

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        public void DisablePhysics()
        {
            rb.isKinematic = true;
        }
    }
}

