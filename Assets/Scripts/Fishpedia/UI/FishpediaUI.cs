#if UNITY_EDITOR
using LudumDare57.Fishing;
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace LudumDare57.Fishpedia.UI
{
    public class FishpediaUI : MonoBehaviour
    {
        private Graphic[] Graphics
        {
            get
            {
                if (!Application.isPlaying || graphics == null || graphics.Length == 0) graphics = GetComponentsInChildren<Graphic>();
                return graphics;
            }
        }

        [SerializeField] private FishpediaController fishpediaController;
        [SerializeField] private FishSlotUI fishSlotPrefab;
        [SerializeField] private RectTransform fishSlotParent;

        private Graphic[] graphics;

        private void Awake()
        {
            Assert.IsNotNull(fishpediaController);
            Assert.IsNotNull(fishSlotPrefab);
            Assert.IsNotNull(fishSlotParent);

            fishpediaController.Opened.AddListener(Show);
            fishpediaController.Closed.AddListener(Hide);
        }

        private void Start()
        {
            CreateSlots();
            Hide();
        }

        private void CreateSlots()
        {
            foreach (Fish fish in fishpediaController.ExistingFish)
            {
                FishSlotUI fishSlot = Instantiate(fishSlotPrefab, fishSlotParent);
                fishSlot.SetFish(fish);
            }
        }

        #region Visibility
        [ContextMenu(nameof(Show))]
        private void Show()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) Undo.RecordObjects(Graphics, $"{nameof(Show)} graphics");
#endif
            SetVisible(true);
        }

        [ContextMenu(nameof(Hide))]
        private void Hide()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) Undo.RecordObjects(Graphics, $"{nameof(Hide)} graphics");
#endif
            SetVisible(false);
        }

        private void SetVisible(bool visible)
        {
            foreach (Graphic graphic in Graphics) graphic.enabled = visible;
        }
        #endregion
    }
}