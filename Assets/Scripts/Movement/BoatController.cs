using UnityEngine;

namespace LudumDare57.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class BoatController : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField][Min(1e-5f)] private float forwardForce = 1000f;
        [SerializeField][Min(1e-5f)] private float turnTorque = 45f;
        [Header("Checks")]
        [SerializeField][Min(1e-5f)] private float inputDeadZone = .01f;

        private new Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector2 moveInput)
        {
            float forwardInput = moveInput.y > inputDeadZone ? 1f : 0f;
            Vector3 globalForwardForce = forwardInput * forwardForce * transform.forward;
            rigidbody.AddForce(globalForwardForce);

            float turnInput = Mathf.Abs(moveInput.x) > inputDeadZone ? Mathf.Sign(moveInput.x) : 0f;
            Vector3 globalTurnTorque = turnInput * turnTorque * transform.up;
            rigidbody.AddTorque(globalTurnTorque);
        }
    }
}