using LudumDare57.Extensions;
using LudumDare57.Fishing;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace LudumDare57.Fishpedia.UI
{
    public class FishpediaUI : MonoBehaviour
    {
        [SerializeField] private FishpediaController fishpediaController;
        [SerializeField] private FishSlotUI fishSlotPrefab;
        [SerializeField] private RectTransform fishSlotParent;
        [SerializeField] private FishInfoUI fishInfo;

        private readonly List<FishSlotUI> fishSlotUIs = new();

        private void Awake()
        {
            Assert.IsNotNull(fishpediaController);
            Assert.IsNotNull(fishSlotPrefab);
            Assert.IsNotNull(fishSlotParent);
            Assert.IsNotNull(fishInfo);

            fishpediaController.Opened.AddListener(Show);
            fishpediaController.Closed.AddListener(Hide);
        }

        private void Start()
        {
            CreateSlots();
            Hide();
        }

        [ContextMenu(nameof(Show))]
        public void Show()
        {
            this.ShowGraphics();
            foreach (var slot in fishSlotUIs) slot.UpdateSprite();
            fishInfo.Hide();
        }

        [ContextMenu(nameof(Hide))]
        public void Hide() => this.HideGraphics();

        private void CreateSlots()
        {
            foreach (Fish fish in fishpediaController.ExistingFish)
            {
                FishSlotUI fishSlot = Instantiate(fishSlotPrefab, fishSlotParent);
                fishSlot.Fish = fish;
                fishSlot.Selected.AddListener(ShowInfo);

                fishSlotUIs.Add(fishSlot);
            }
        }

        private void ShowInfo(Fish fish)
        {
            fishInfo.Display(fish);
        }
    }
}