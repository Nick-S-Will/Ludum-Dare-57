using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
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

        #region Visibility
        [ContextMenu(nameof(Show))]
        private void Show()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) Undo.RecordObjects(Graphics, $"{nameof(Show)} graphics");
#endif
            SetVisible(true);
        }

        [ContextMenu(nameof(Hide))]
        private void Hide()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) Undo.RecordObjects(Graphics, $"{nameof(Hide)} graphics");
#endif
            SetVisible(false);
        }

        private void SetVisible(bool visible)
        {
            foreach (Graphic graphic in Graphics) graphic.enabled = visible;
        }
        #endregion

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