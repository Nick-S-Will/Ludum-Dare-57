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

            UpdateMoneyText();
        }

        private void UpdateUI()
        {
            if (changeMoneyRoutine != null) StopCoroutine(changeMoneyRoutine);

            changeMoneyRoutine = StartCoroutine(ChangeMoney(moneyHandler.Money));
        }

        private IEnumerator ChangeMoney(int targetMoney)
        {
            if (displayMoney == targetMoney)
            {
                changeMoneyRoutine = null;
                yield break;
            }

            float changeInterval = changeMoneyTime / Mathf.Abs(targetMoney - displayMoney);
            int changeDirection = (int)Mathf.Sign(targetMoney - displayMoney);
            while (displayMoney != targetMoney)
            {
                displayMoney += changeDirection;
                moneyText.text = string.Format(textFormat, displayMoney);

                yield return new WaitForSeconds(changeInterval);
            }

            changeMoneyRoutine = null;
        }

        private void UpdateMoneyText() => moneyText.text = string.Format(textFormat, displayMoney);
    }
}