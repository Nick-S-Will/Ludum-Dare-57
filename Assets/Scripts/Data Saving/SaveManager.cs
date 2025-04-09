using LudumDare57.Fishing.Stats;
using LudumDare57.Fishing;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

namespace LudumDare57.DataSaving
{
    public class SaveManager : Singleton<SaveManager>
    {
        private const string SaveDirectory = "Saves";
        private const string SaveFile = "Save 1";
        private const string SaveFileType = ".json";

        private static string SaveDirectoryPath => Path.Combine(Application.persistentDataPath, SaveDirectory);
        private static string SaveFilePath => Path.Combine(SaveDirectoryPath, SaveFile + SaveFileType);

        public float? Gas
        {
            get => gasSaved ? saveData.gas : null;
            set
            {
                if (value.HasValue)
                {
                    saveData.gas = value.Value;
                    gasSaved = true;
                }
            }
        }
        public int? TankSegmentCount
        {
            get => tankSegmentCountSaved ? saveData.tankSegmentCount : null;
            set
            {
                if (value.HasValue)
                {
                    saveData.tankSegmentCount = value.Value;
                    tankSegmentCountSaved = true;
                }
            }
        }
        public int? Money
        {
            get => moneySaved ? saveData.money : null;
            set
            {
                if (value.HasValue)
                {
                    saveData.money = value.Value;
                    moneySaved = true;
                }
            }
        }
        public int? Debt
        {
            get => debtSaved ? saveData.debt : null;
            set
            {
                if (value.HasValue)
                {
                    saveData.debt = value.Value;
                    debtSaved = true;
                }
            }
        }
        public Dictionary<Fish, int> FishCatchCounts
        {
            get
            {
                if (saveData.fishCatchCounts == null) return null;

                Dictionary<Fish, int> catchCounts = new();
                foreach (FishCatchCount fishCatchCount in saveData.fishCatchCounts)
                {
                    Fish fish = CatchStatTracker.Instance.ExistingFish.First(fish => fish.name == fishCatchCount.fishName);
                    catchCounts[fish] = fishCatchCount.count;
                }

                return catchCounts;
            }
            set
            {
                saveData.fishCatchCounts = new FishCatchCount[value.Count];
                int index = 0;
                foreach ((Fish fish, int count) in value)
                {
                    saveData.fishCatchCounts[index] = new FishCatchCount { fishName = fish.name, count = count };
                    index++;
                }
            }
        }

        private SaveData saveData;
        private bool gasSaved, tankSegmentCountSaved, moneySaved, debtSaved;

        protected override void Awake()
        {
            base.Awake();

            if (Instance != this) return;

            LoadSaveData();
        }

        private void LoadSaveData()
        {
            string saveDataJson = File.Exists(SaveFilePath) ? File.ReadAllText(SaveFilePath) : null;
            bool wasSaved = saveDataJson != null;
            saveData = wasSaved ? JsonUtility.FromJson<SaveData>(saveDataJson) : new SaveData();
            gasSaved = wasSaved;
            tankSegmentCountSaved = wasSaved;
            moneySaved = wasSaved;
            debtSaved = wasSaved;
        }

        [ContextMenu(nameof(SaveSaveData))]
        public void SaveSaveData()
        {
            _ = Directory.CreateDirectory(SaveDirectoryPath);

            string saveDataJson = JsonUtility.ToJson(saveData);
            File.WriteAllText(SaveFilePath, saveDataJson);
        }

        [ContextMenu(nameof(ClearSave))]
        public void ClearSave()
        {
            if (!File.Exists(SaveFilePath)) return;

            File.Delete(SaveFilePath);
        }
    }
}