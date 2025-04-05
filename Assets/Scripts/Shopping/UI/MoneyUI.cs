using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace LudumDare57.Shopping.UI
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private MoneyHandler moneyHandler;
        [SerializeField] private TMP_Text moneyText;

        private string textFormat;

        private void Awake()
        {
            Assert.IsNotNull(moneyHandler);

            moneyHandler.MoneyChanged.AddListener(UpdateUI);
            textFormat = moneyText.text;

            UpdateUI();
        }

        private void UpdateUI()
        {
            moneyText.text = string.Format(textFormat, moneyHandler.Money);
        }
    }
}