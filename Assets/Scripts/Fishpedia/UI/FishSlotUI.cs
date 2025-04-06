using LudumDare57.Fishing;
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
                fishImage.sprite = fish ? fish.Sprite : null;
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
    }
}