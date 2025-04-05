using LudumDare57.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LudumDare57.Player
{
    [RequireComponent(typeof(BoatController))]
    public class PlayerController : MonoBehaviour
    {
        private BoatController boatController;
        private Vector2 moveInput;

        private void Awake()
        {
            boatController = GetComponent<BoatController>();
        }

        private void FixedUpdate()
        {
            boatController.Move(moveInput);
        }

        public void Move(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }
    }
}