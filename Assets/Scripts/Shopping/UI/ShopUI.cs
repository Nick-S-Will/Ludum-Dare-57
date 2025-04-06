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
        [SerializeField] private TMP_Text debtText;

        private Graphic[] graphics;
        private string debtTextFormat;

        private void Awake()
        {
            Assert.IsNotNull(shop);
            Assert.IsNotNull(debtText);

            graphics = null;
            shop.Opened.AddListener(Open);
            shop.Closed.AddListener(Close);
            shop.DebtPartlyPaid.AddListener(UpdateDebtText);
            debtTextFormat = debtText.text;
        }

        private void Start()
        {
            UpdateDebtText();
        }

        [ContextMenu(nameof(Open))]
        private void Open() => SetVisible(true);

        [ContextMenu(nameof(Close))]
        private void Close() => SetVisible(false);

        private void SetVisible(bool visible)
        {
            foreach (Graphic graphic in Graphics) graphic.enabled = visible;
        }

        private void UpdateDebtText() => debtText.text = string.Format(debtTextFormat, shop.Debt);
    }
}