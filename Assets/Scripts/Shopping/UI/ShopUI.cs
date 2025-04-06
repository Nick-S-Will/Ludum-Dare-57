using TMPro;
using UnityEditor;
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

            shop.Opened.AddListener(Show);
            shop.Closed.AddListener(Hide);
            shop.DebtPartlyPaid.AddListener(UpdateDebtText);
            debtTextFormat = debtText.text;
        }

        private void Start()
        {
            UpdateDebtText();
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

        private void UpdateDebtText() => debtText.text = string.Format(debtTextFormat, shop.Debt);
    }
}