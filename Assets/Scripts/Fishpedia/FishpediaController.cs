using LudumDare57.Fishing;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare57.Fishpedia
{
    public class FishpediaController : MonoBehaviour
    {
        public UnityEvent Opened => opened;
        public UnityEvent Closed => closed;
        public Fish[] ExistingFish => existingFish;
        public bool IsOpen => isOpen;

        [Header("Events")]
        [SerializeField] private UnityEvent opened;
        [SerializeField] private UnityEvent closed;

        private Fish[] existingFish;
        private bool isOpen;

        private void Awake()
        {
            FishPool[] fishPools = FindObjectsOfType<FishPool>();
            existingFish = fishPools.SelectMany(fishPool => fishPool.Fishes).Distinct().OrderBy(fish => fish.Price).ToArray();
        }

        public void ToggleOpen()
        {
            if (isOpen) Close();
            else Open();
        }

        public void Open()
        {
            if (isOpen) return;

            isOpen = true;

            opened.Invoke();
        }

        public void Close()
        {
            if (!isOpen) return;

            isOpen = false;

            closed.Invoke();
        }
    }
}