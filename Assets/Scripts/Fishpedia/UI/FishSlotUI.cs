using LudumDare57.Fishing;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace LudumDare57.Fishpedia.UI
{
    [RequireComponent(typeof(Image))]
    public class FishSlotUI : MonoBehaviour
    {
        [SerializeField] private Image fishImage;

        private Image image;

        private void Awake()
        {
            Assert.IsNotNull(fishImage);

            image = GetComponent<Image>();
        }

        public void SetFish(Fish fish)
        {
            fishImage.sprite = fish.Sprite;
        }
    }
}