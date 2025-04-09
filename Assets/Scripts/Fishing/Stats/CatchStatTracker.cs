using LudumDare57.DataSaving;
using LudumDare57.Fishpedia;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace LudumDare57.Fishing.Stats
{
    public class CatchStatTracker : MonoBehaviour
    {
        public static CatchStatTracker Instance { get; private set; }

        public Fish[] ExistingFish => fishpediaController.ExistingFish;

        [SerializeField] private FishingController fishingController;
        [SerializeField] private FishpediaController fishpediaController;

        private readonly Dictionary<Fish, int> fishCatchCounts = new();

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
            LoadData();
        }

        private void LoadData()
        {
            Dictionary<Fish, int> savedFishCatchCounts = SaveManager.Instance.FishCatchCounts;
            foreach (Fish fish in ExistingFish)
            {
                fishCatchCounts[fish] = savedFishCatchCounts != null && savedFishCatchCounts.TryGetValue(fish, out int count) ? count : 0;
            }
            SaveManager.Instance.FishCatchCounts = fishCatchCounts;
        }

        private void AddFish(IList<Fish> caughtFish)
        {
            foreach (Fish fish in caughtFish)
            {
                if (!fishCatchCounts.ContainsKey(fish)) continue;

                fishCatchCounts[fish]++;
                SaveManager.Instance.FishCatchCounts = fishCatchCounts;
            }
        }

        public int GetCatchCount(Fish fish) => fishCatchCounts.TryGetValue(fish, out int count) ? count : 0;

        public bool HasBeenCaught(Fish fish) => GetCatchCount(fish) > 0;
    }
}