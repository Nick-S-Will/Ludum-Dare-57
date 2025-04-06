using LudumDare57.Player.Transitions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace LudumDare57.Shopping
{
    public class DebtAdder : MonoBehaviour
    {
        public UnityEvent DebtAdded => debtAdded;
        public int GasDebtIncrement => Mathf.FloorToInt(respawnGasPriceMultiplier * shop.GasPrice);

        [SerializeField] private PlayerRespawner playerRespawner;
        [SerializeField] private Shop shop;
        [Header("Attributes")]
        [SerializeField] private float respawnGasPriceMultiplier = 5f;
        [Header("Events")] 
        [SerializeField] private UnityEvent debtAdded;

        private void Awake()
        {
            Assert.IsNotNull(playerRespawner);
            Assert.IsNotNull(shop);

            playerRespawner.PlayerRespawned.AddListener(AddDebt);
        }

        private void AddDebt()
        {
            shop.IncreaseDebt(GasDebtIncrement);

            debtAdded.Invoke();
        }
    }
}