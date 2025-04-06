using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace LudumDare57.Shopping.UI
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private MoneyHandler moneyHandler;
        [SerializeField] private TMP_Text moneyText;
        [Header("Attributes")]
        [SerializeField][Min(0f)] private float changeMoneyTime = 1f;

        private Coroutine changeMoneyRoutine;
        private string textFormat;
        private int displayMoney;

        private void Awake()
        {
            Assert.IsNotNull(moneyHandler);

            moneyHandler.MoneyChanged.AddListener(UpdateUI);
            textFormat = moneyText.text;
        }

        private void Start()
        {
            UpdateText();
        }

        private void UpdateUI()
        {
            if (changeMoneyRoutine != null) StopCoroutine(changeMoneyRoutine);

            changeMoneyRoutine = StartCoroutine(ChangeMoneyRoutine(moneyHandler.Money));
        }

        private IEnumerator ChangeMoneyRoutine(int targetMoney)
        {
            if (displayMoney == targetMoney)
            {
                changeMoneyRoutine = null;
                yield break;
            }

            float changeInterval = Mathf.Max(changeMoneyTime / Mathf.Abs(targetMoney - displayMoney), 1e-5f);
            int changeDirection = (int)Mathf.Sign(targetMoney - displayMoney);
            while (displayMoney != targetMoney)
            {
                displayMoney += changeDirection;
                UpdateText();

                yield return new WaitForSeconds(changeInterval);
            }

            changeMoneyRoutine = null;
        }

        private void UpdateText() => moneyText.text = string.Format(textFormat, displayMoney);
    }
}