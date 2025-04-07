using LudumDare57.Fishpedia;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace LudumDare57.Fishing.Stats
{
    public class CatchStatTracker : MonoBehaviour
    {
        public static CatchStatTracker Instance { get; private set; }

        [SerializeField] private FishingController fishingController;
        [SerializeField] private FishpediaController fishpediaController;

        private Dictionary<Fish, int> fishCatches = new();

        private void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;

            Assert.IsNotNull(fishingController);
            Assert.IsNotNull(fishpediaController);

            fishingController.FishingEnded.AddListener(AddFish);
        }

        private void Start()
        {
            foreach (Fish fish in fishpediaController.ExistingFish) fishCatches[fish] = 0;
        }

        private void AddFish(IList<Fish> caughtFish)
        {
            foreach (Fish fish in caughtFish)
            {
                if (!fishCatches.ContainsKey(fish)) continue;

                fishCatches[fish]++;
            }
        }

        public bool HasBeenCaught(Fish fish) => fishCatches.TryGetValue(fish, out int count) && count > 0;
    }
}