using LudumDare57.Resources;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare57.Shopping
{
    public class Shop : MonoBehaviour
    {
        public int GasPrice => gasPrice;
        public int TankUpgradePrice => tankUpgradePrice;
        public int DebtPaymentAmount => debtPaymentAmount;
        public UnityEvent Opened => opened;
        public UnityEvent Closed => closed;
        public UnityEvent PurchaseFailed => purchaseFailed;
        public UnityEvent GasBought => gasBought;
        public UnityEvent TankUpgraded => tankUpgraded;
        public UnityEvent DebtPartlyPaid => debtPartlyPaid;
        public UnityEvent DebtPaid => debtPaid;
        public IMoneyHolder Shopper => shopper;
        public int Debt => debt;
        public bool IsOpen => shopper != null;

        private IGasHolder GasHolder => IsOpen ? (IGasHolder)shopper : null;

        [Header("Attributes")]
        [SerializeField][Min(1f)] private int startingDebt = 1000;
        [SerializeField][Min(1f)] private int gasPrice = 10, tankUpgradePrice = 20, debtPaymentAmount = 100;
        [Header("Events")]
        [SerializeField] private UnityEvent opened;
        [SerializeField] private UnityEvent closed, purchaseFailed, gasBought, tankUpgraded, debtPartlyPaid, debtPaid;

        private IMoneyHolder shopper;
        private int debt;

        private void Awake()
        {
            debt = startingDebt;
        }

        public void Open<T>(T shopper) where T : IMoneyHolder, IGasHolder
        {
            if (shopper == null || IsOpen) return;

            this.shopper = shopper;

            opened.Invoke();
        }

        public void Close()
        {
            if (!IsOpen) return;

            shopper = null;

            closed.Invoke();
        }

        public void BuyGas()
        {
            if (!IsOpen || GasHolder.Gas == GasHolder.TankSize || !shopper.UseMoney(gasPrice))
            {
                purchaseFailed.Invoke();
                return;
            }

            GasHolder.Refuel();
            gasBought.Invoke();

            return;
        }

        public void BuyTankUpgrade()
        {
            if (!IsOpen || !shopper.HasMoney(tankUpgradePrice) || !GasHolder.IncreaseTankSize())
            {
                purchaseFailed.Invoke();
                return;
            }

            shopper.UseMoney(tankUpgradePrice);
            tankUpgraded.Invoke();
        }

        public void PayDebt()
        {
            if (!IsOpen)
            {
                purchaseFailed.Invoke();
                return;
            }

            int toPay = Mathf.Min(debtPaymentAmount, debt);
            if (toPay == 0 || !shopper.UseMoney(toPay)) return;

            debt -= toPay;
            debtPartlyPaid.Invoke();
            if (debt == 0) debtPaid.Invoke();
        }
    }
}