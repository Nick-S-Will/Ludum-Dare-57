using LudumDare57.Player;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace LudumDare57.Cutscenes
{
    public class PlayerRespawner : MonoBehaviour
    {
        public PlayerController PlayerController => playerController;
        public UnityEvent PlayerRespawned => playerRespawned;

        [SerializeField] private PlayerController playerController;
        [Header("Events")]
        [SerializeField] private UnityEvent playerRespawned;

        private Vector3 playerStartPosition;
        private Quaternion playerStartRotation;

        private void Awake()
        {
            Assert.IsNotNull(playerController);
        }

        private void Start()
        {
            playerController.BoatController.GasHandler.GasExhausted.AddListener(Respawn);
            playerStartPosition = playerController.BoatController.Rigidbody.position;
            playerStartRotation = playerController.BoatController.Rigidbody.rotation;
        }

        private void Respawn()
        {
            playerController.BoatController.Rigidbody.velocity = Vector3.zero;
            playerController.BoatController.Rigidbody.angularVelocity = Vector3.zero;
            playerController.BoatController.Rigidbody.position = playerStartPosition;
            playerController.BoatController.Rigidbody.rotation = playerStartRotation;

            playerRespawned.Invoke();
        }
    }
}