using LudumDare57.Fishing;
using LudumDare57.Resources;
using UnityEngine;
using UnityEngine.Assertions;

namespace LudumDare57.Shopping
{
    [RequireComponent(typeof(FishingController))]
    public class Shopper : MonoBehaviour, IGasHolder, IMoneyHolder
    {
        public float MaxTankSize => gasHandler.MaxTankSize;
        public float TankSize => gasHandler.TankSize;
        public float Gas => gasHandler.Gas;

        [SerializeField] private GasHandler gasHandler;
        [SerializeField] private MoneyHandler moneyHandler;

        private FishingController fishingController;

        private void Awake()
        {
            Assert.IsNotNull(gasHandler);
            Assert.IsNotNull(moneyHandler);

            fishingController = GetComponent<FishingController>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent(out Shop shop)) return;

            shop.Open(this);
            SellFish();
        }

        private void SellFish()
        {
            foreach (Fish fish in fishingController.CaughtFish) AddMoney(fish.Price);
            fishingController.CaughtFish.Clear();
        }

        public bool IncreaseTankSize() => gasHandler.IncreaseTankSize();
        public void Refuel() => gasHandler.Refuel();
        public bool HasGas(float amount) => gasHandler.HasGas(amount);
        public bool UseGas(float amount) => gasHandler.UseGas(amount);

        public void AddMoney(int amount) => moneyHandler.AddMoney(amount);
        public bool HasMoney(int amount) => moneyHandler.HasMoney(amount);
        public bool UseMoney(int amount) => moneyHandler.UseMoney(amount);
    }
}