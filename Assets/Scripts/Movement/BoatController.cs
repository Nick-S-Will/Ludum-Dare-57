using LudumDare57.Resources;
using UnityEngine;

namespace LudumDare57.Movement
{
    [RequireComponent(typeof(GasHandler))]
    [RequireComponent(typeof(Rigidbody))]
    public class BoatController : MonoBehaviour
    {
        public GasHandler GasHandler => gasHandler;
        public Rigidbody Rigidbody => rigidbody;

        [Header("Attributes")]
        [SerializeField][Min(1e-5f)] private float forwardForce = 1000f;
        [SerializeField][Min(1e-5f)] private float turnTorque = 45f, gasUsage = 1f;
        [Header("Checks")]
        [SerializeField][Min(1e-5f)] private float inputDeadZone = .01f;

        private GasHandler gasHandler;
        private new Rigidbody rigidbody;

        private void Awake()
        {
            gasHandler = GetComponent<GasHandler>();
            rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector2 moveInput)
        {
            if (moveInput == Vector2.zero || !gasHandler.UseGas(gasUsage * Time.fixedDeltaTime)) return;

            float forwardInput = moveInput.y > inputDeadZone ? 1f : 0f;
            Vector3 globalForwardForce = forwardInput * forwardForce * transform.forward;
            rigidbody.AddForce(globalForwardForce);

            float turnInput = Mathf.Abs(moveInput.x) > inputDeadZone ? Mathf.Sign(moveInput.x) : 0f;
            Vector3 globalTurnTorque = turnInput * turnTorque * transform.up;
            rigidbody.AddTorque(globalTurnTorque);
        }

        public void Stop()
        {
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.angularVelocity = Vector3.zero;
        }
    }
}