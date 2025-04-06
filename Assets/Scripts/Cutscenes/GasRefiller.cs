using LudumDare57.Shopping;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace LudumDare57.Cutscenes
{
    public class GasRefiller : MonoBehaviour
    {
        public UnityEvent DebtAdded => debtAdded;

        private int GasDebtIncrement => Mathf.FloorToInt(respawnGasPriceMultiplier * shop.GasPrice);

        [SerializeField] private PlayerRespawner playerRespawner;
        [SerializeField] private Shop shop;
        [SerializeField] private TMP_Text gasExhaustedText;
        [Header("Attributes")]
        [SerializeField][Min(1f)] private float respawnGasPriceMultiplier = 5f;
        [SerializeField][Min(1e-5f)] private float textDuration = 2f;
        [Header("Events")]
        [SerializeField] private UnityEvent debtAdded;

        private string gasExhaustedTextFormat;

        private void Awake()
        {
            Assert.IsNotNull(playerRespawner);
            Assert.IsNotNull(shop);

            playerRespawner.PlayerRespawned.AddListener(Refill);
            gasExhaustedTextFormat = gasExhaustedText.text;
        }

        private void Refill()
        {
            playerRespawner.PlayerController.BoatController.GasHandler.Refuel();
            shop.IncreaseDebt(GasDebtIncrement);

            gasExhaustedText.text = string.Format(gasExhaustedTextFormat, GasDebtIncrement);
            gasExhaustedText.enabled = true;
            Invoke(nameof(HideText), textDuration);

            debtAdded.Invoke();
        }

        private void HideText() => gasExhaustedText.enabled = false;
    }
}