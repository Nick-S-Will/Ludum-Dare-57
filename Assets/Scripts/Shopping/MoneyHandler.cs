using LudumDare57.Fishing;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare57.Shopping
{
    [RequireComponent(typeof(FishingController))]
    public class MoneyHandler : MonoBehaviour
    {
        public UnityEvent MoneyChanged => moneyChanged;
        public int Money => money;

        [SerializeField] private UnityEvent moneyChanged;

        private FishingController fishingController;
        private int money;

        private void Awake()
        {
            fishingController = GetComponent<FishingController>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent(out Shop _)) return;

            SellFish();
        }

        private void SellFish()
        {
            if (fishingController == null || fishingController.CaughtFish.Count == 0) return;

            foreach (Fish fish in fishingController.CaughtFish) money += fish.Price;
            fishingController.CaughtFish.Clear();

            moneyChanged.Invoke();
        }
    }
}