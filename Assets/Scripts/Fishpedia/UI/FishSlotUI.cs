using LudumDare57.Fishing;
using LudumDare57.Fishing.Stats;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LudumDare57.Fishpedia.UI
{
    [RequireComponent(typeof(Image))]
    public class FishSlotUI : MonoBehaviour
    {
        public UnityEvent<Fish> Selected => selected;
        public Fish Fish
        {
            get => fish;
            set
            {
                fish = value;
                UpdateSprite();
            }
        }

        [SerializeField] private Image fishImage;
        [Header("Events")]
        [SerializeField] private UnityEvent<Fish> selected;

        private Image image;
        private Fish fish;

        private void Awake()
        {
            Assert.IsNotNull(fishImage);

            image = GetComponent<Image>();
        }

        public void Select()
        { 
            selected.Invoke(fish);
        }

        public void UpdateSprite()
        {
            fishImage.sprite = fish ? (CatchStatTracker.Instance.HasBeenCaught(fish) ? fish.Sprite : fish.SilhouetteSprite) : null;
        }
    }
}