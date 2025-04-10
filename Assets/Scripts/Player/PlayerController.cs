using LudumDare57.Fishing;
using LudumDare57.Fishpedia;
using LudumDare57.Movement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace LudumDare57.Player
{
    [RequireComponent(typeof(BoatController))]
    [RequireComponent(typeof(FishpediaController))]
    [RequireComponent(typeof(FishingController))]
    public class PlayerController : MonoBehaviour
    {
        public UnityEvent MenuClosed => menuClosed;
        public BoatController BoatController => boatController;
        public FishpediaController FishpediaController => fishpediaController;
        public FishingController FishingController => fishingController;

        [SerializeField] private UnityEvent menuClosed;

        private BoatController boatController;
        private FishpediaController fishpediaController;
        private FishingController fishingController;
        private Vector2 moveInput;

        private void Awake()
        {
            boatController = GetComponent<BoatController>();
            fishpediaController = GetComponent<FishpediaController>();
            fishingController = GetComponent<FishingController>();
        }

        private void FixedUpdate()
        {
            if (fishingController.IsFishing) return;

            boatController.Move(moveInput);
        }

        public void Move(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        public void ToggleFishpedia(InputAction.CallbackContext context)
        {
            if (!context.performed || fishingController.IsFishing) return;

            fishpediaController.ToggleOpen();
        }

        public void Fish(InputAction.CallbackContext context)
        {
            if (!context.performed || fishpediaController.IsOpen) return;

            boatController.Stop();
            fishingController.Fish();
        }

        public void CloseMenu(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            menuClosed.Invoke();
        }
    }
}