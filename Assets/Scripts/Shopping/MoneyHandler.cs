using LudumDare57.DataSaving;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare57.Shopping
{
    public class MoneyHandler : MonoBehaviour, IMoneyHolder
    {
        public UnityEvent MoneyChanged => moneyChanged;
        public int Money => money;

        [SerializeField] private UnityEvent moneyChanged;

        private int money;

        private void Start()
        {
            LoadData();
        }

        private void LoadData()
        {
            money = SaveManager.Instance.Money ?? 0;
            moneyChanged.Invoke();
        }

        [ContextMenu(nameof(LineMyPockets))]
        private void LineMyPockets() => AddMoney(int.MaxValue - money);
        
        public void AddMoney(int amount)
        {
            if (amount <= 0) return;

            money += amount;
            SaveManager.Instance.Money = money;

            moneyChanged.Invoke();
        }

        public bool HasMoney(int amount)
        {
            return money >= amount;
        }

        public bool UseMoney(int amount)
        {
            if (amount == 0 || !HasMoney(amount)) return false;

            money -= amount;
            SaveManager.Instance.Money = money;

            moneyChanged.Invoke();

            return true;
        }
    }
}