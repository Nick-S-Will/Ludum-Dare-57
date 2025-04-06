using LudumDare57.Fishing;
using LudumDare57.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LudumDare57.Player
{
    [RequireComponent(typeof(BoatController))]
    [RequireComponent(typeof(FishingController))]
    public class PlayerController : MonoBehaviour
    {
        public BoatController BoatController => boatController;
        public FishingController FishingController => fishingController;

        private BoatController boatController;
        private FishingController fishingController;
        private Vector2 moveInput;

        private void Awake()
        {
            boatController = GetComponent<BoatController>();
            fishingController = GetComponent<FishingController>();
        }

        private void FixedUpdate()
        {
            boatController.Move(moveInput);
        }

        public void Move(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        public void Fish(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            fishingController.Fish();
        }
    }
}