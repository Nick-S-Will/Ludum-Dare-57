using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace LudumDare57.Shopping.UI
{
    public class ShopUI : MonoBehaviour
    {
        private Graphic[] Graphics
        {
            get
            {
                if (!Application.isPlaying || graphics == null || graphics.Length == 0) graphics = GetComponentsInChildren<Graphic>();
                return graphics;
            }
        }

        [SerializeField] private Shop shop;
        [SerializeField] private TMP_Text debtText, gasText, tankUpgradeText, debtPaymentText;

        private Graphic[] graphics;
        private string debtTextFormat, gasTextFormat, tankUpgradeTextFormat, debtPaymentTextFormat;

        private void Awake()
        {
            Assert.IsNotNull(shop);
            Assert.IsNotNull(debtText);
            Assert.IsNotNull(gasText);
            Assert.IsNotNull(tankUpgradeText);
            Assert.IsNotNull(debtPaymentText);

            graphics = null;
            shop.Opened.AddListener(Open);
            shop.Closed.AddListener(Close);
            shop.DebtPartlyPaid.AddListener(UpdateTexts);
            debtTextFormat = debtText.text;
            gasTextFormat = gasText.text;
            tankUpgradeTextFormat = tankUpgradeText.text;
            debtPaymentTextFormat = debtPaymentText.text;
        }

        private void Start()
        {
            UpdateTexts();
        }

        [ContextMenu(nameof(Open))]
        private void Open() => SetVisible(true);

        [ContextMenu(nameof(Close))]
        private void Close() => SetVisible(false);

        private void SetVisible(bool visible)
        {
            if (visible) UpdateTexts();
            foreach (Graphic graphic in Graphics) graphic.enabled = visible;
        }

        private void UpdateTexts()
        {
            debtText.text = string.Format(debtTextFormat, shop.Debt);
            gasText.text = string.Format(gasTextFormat, shop.GasPrice);
            tankUpgradeText.text = string.Format(tankUpgradeTextFormat, shop.TankUpgradePrice);
            debtPaymentText.text = string.Format(debtPaymentTextFormat, shop.DebtPaymentAmount);
        }
    }
}