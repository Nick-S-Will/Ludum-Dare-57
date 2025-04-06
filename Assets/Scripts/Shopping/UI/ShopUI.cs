using LudumDare57.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace LudumDare57.Shopping.UI
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private Shop shop;
        [SerializeField] private TMP_Text debtText, gasText, tankUpgradeText, debtPaymentText;

        private string debtTextFormat, gasTextFormat, tankUpgradeTextFormat, debtPaymentTextFormat;

        private void Awake()
        {
            Assert.IsNotNull(shop);
            Assert.IsNotNull(debtText);
            Assert.IsNotNull(gasText);
            Assert.IsNotNull(tankUpgradeText);
            Assert.IsNotNull(debtPaymentText);

            shop.Opened.AddListener(Show);
            shop.Closed.AddListener(Hide);
            shop.DebtChanged.AddListener(UpdateDebtText);
            debtTextFormat = debtText.text;
            gasTextFormat = gasText.text;
            tankUpgradeTextFormat = tankUpgradeText.text;
            debtPaymentTextFormat = debtPaymentText.text;
        }

        private void Start()
        {
            UpdateTexts();
        }
        
        [ContextMenu(nameof(Show))]
        public void Show() => this.ShowGraphics();

        [ContextMenu(nameof(Hide))]
        public void Hide() => this.HideGraphics();

        private void UpdateTexts()
        {
            UpdateDebtText();
            gasText.text = string.Format(gasTextFormat, shop.GasPrice);
            tankUpgradeText.text = string.Format(tankUpgradeTextFormat, shop.TankUpgradePrice);
            debtPaymentText.text = string.Format(debtPaymentTextFormat, shop.DebtPaymentAmount);
        }

        private void UpdateDebtText() => debtText.text = string.Format(debtTextFormat, shop.Debt);
    }
}